import { Command } from 'commander';
import { ConnectedCommand } from '../../core/connected-command.js';

/**
 * Get information about the current site context.
 * Equivalent to Get-PnPContext in the C# codebase.
 */
export class GetContextCommand extends ConnectedCommand {
  register(program: Command): void {
    program
      .command('get-context')
      .description('Returns the current context')
      .action(this.createAction());
  }

  async execute(_options: Record<string, unknown>): Promise<void> {
    const context = {
      url: this.connection.url,
      connectionMethod: this.connection.connectionMethod,
      connectionType: this.connection.connectionType,
      clientId: this.connection.clientId,
      azureEnvironment: this.connection.azureEnvironment,
      graphEndPoint: this.connection.graphEndPoint,
      connectedAt: this.connection.connectedAt,
    };
    this.writeOutput(context);
  }
}
