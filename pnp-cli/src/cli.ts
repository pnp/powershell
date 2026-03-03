import { Command } from 'commander';
import { BaseCommand } from './core/base-command.js';
import { setLogLevel, LogLevel } from './utils/logger.js';

// Import all commands explicitly (auto-discovery would need build step)
import { ConnectCommand } from './commands/base/connect.js';
import { DisconnectCommand } from './commands/base/disconnect.js';
import { GetConnectionCommand } from './commands/base/get-connection.js';
import { GetAccessTokenCommand } from './commands/base/get-access-token.js';
import { GetContextCommand } from './commands/base/get-context.js';
import { InvokeSPRestMethodCommand } from './commands/base/invoke-sp-rest-method.js';
import { InvokeGraphRequestCommand } from './commands/base/invoke-graph-request.js';

const pkg = { version: '1.0.0', name: '@pnp/cli' };

/**
 * All registered command classes. New commands should be added here.
 */
const COMMAND_CLASSES: Array<new () => BaseCommand> = [
  ConnectCommand,
  DisconnectCommand,
  GetConnectionCommand,
  GetAccessTokenCommand,
  GetContextCommand,
  InvokeSPRestMethodCommand,
  InvokeGraphRequestCommand,
];

export function buildCli(): Command {
  const program = new Command();

  program
    .name('pnp')
    .version(pkg.version)
    .description('PnP CLI - Microsoft 365 administration and automation')
    .option('--format <format>', 'Output format: json, table, csv, text', 'json')
    .option('--verbose', 'Enable verbose output')
    .option('--debug', 'Enable debug output')
    .option('--output <file>', 'Write output to file');

  // Set up logging based on global options
  program.hook('preAction', (_thisCommand, actionCommand) => {
    const opts = actionCommand.optsWithGlobals();
    if (opts.debug) {
      setLogLevel(LogLevel.Debug);
    } else if (opts.verbose) {
      setLogLevel(LogLevel.Info);
    }
  });

  // Register all commands
  for (const CommandClass of COMMAND_CLASSES) {
    const command = new CommandClass();
    command.register(program);
  }

  return program;
}
