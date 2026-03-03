import { Command } from 'commander';
import { v4 as uuidv4 } from 'uuid';
import { OutputFormat, OutputFormatter } from '../output/formatter.js';
import { logger } from '../utils/logger.js';

export interface GlobalOptions {
  format?: OutputFormat;
  verbose?: boolean;
  debug?: boolean;
  output?: string;
}

/**
 * Abstract base class for all PnP CLI commands.
 * Equivalent to BasePSCmdlet in the C# codebase.
 */
export abstract class BaseCommand {
  protected correlationId: string = uuidv4();
  protected globalOptions: GlobalOptions = {};

  /**
   * Register this command with the commander program.
   * Each subclass defines its own command name, options, and action.
   */
  abstract register(program: Command): void;

  /**
   * Execute the command logic.
   * Equivalent to ExecuteCmdlet() in the C# codebase.
   */
  abstract execute(options: Record<string, unknown>): Promise<void>;

  /**
   * Called before execute() to set up the command context.
   * Subclasses override this to add connection validation, etc.
   */
  protected async beforeExecute(options: Record<string, unknown>): Promise<void> {
    this.globalOptions = {
      format: (options.format as OutputFormat) || 'json',
      verbose: options.verbose as boolean,
      debug: options.debug as boolean,
      output: options.output as string,
    };
  }

  /**
   * Creates the action handler that wires beforeExecute -> execute with error handling.
   */
  protected createAction(): (options: Record<string, unknown>) => Promise<void> {
    return async (options: Record<string, unknown>) => {
      try {
        await this.beforeExecute(options);
        await this.execute(options);
      } catch (error) {
        this.writeError(error instanceof Error ? error.message : String(error), error instanceof Error ? error : undefined);
        process.exitCode = 1;
      }
    };
  }

  /**
   * Write output data to stdout in the configured format.
   * Equivalent to WriteObject() in the C# codebase.
   */
  protected writeOutput(data: unknown, enumerateCollection?: boolean): void {
    if (data === null || data === undefined) return;

    const format = this.globalOptions.format || 'json';
    const output = OutputFormatter.format(data, format);

    if (this.globalOptions.output) {
      const fs = require('fs');
      fs.writeFileSync(this.globalOptions.output, output, 'utf-8');
      this.writeVerbose(`Output written to ${this.globalOptions.output}`);
    } else {
      console.log(output);
    }
  }

  protected writeWarning(message: string): void {
    logger.warning(this.constructor.name, message, this.correlationId);
  }

  protected writeError(message: string, error?: Error): void {
    logger.error(this.constructor.name, message, this.correlationId);
    if (error && this.globalOptions.debug) {
      console.error(error.stack);
    }
  }

  protected writeVerbose(message: string): void {
    if (this.globalOptions.verbose || this.globalOptions.debug) {
      logger.info(this.constructor.name, message, this.correlationId);
    }
  }

  protected writeDebug(message: string): void {
    if (this.globalOptions.debug) {
      logger.debug(this.constructor.name, message, this.correlationId);
    }
  }

  /**
   * Check if a parameter was explicitly provided.
   * Equivalent to ParameterSpecified() in the C# codebase.
   */
  protected parameterSpecified(options: Record<string, unknown>, name: string): boolean {
    return options[name] !== undefined && options[name] !== null;
  }
}
