import { Command } from 'commander';
import { BaseCommand } from '../../core/base-command.js';
import { ConnectionStore } from '../../auth/connection-store.js';

/**
 * Get information about the current connection.
 * Equivalent to Get-PnPConnection in the C# codebase.
 */
export class GetConnectionCommand extends BaseCommand {
  register(program: Command): void {
    program
      .command('get-connection')
      .description('Returns the current connection information')
      .action(this.createAction());
  }

  async execute(_options: Record<string, unknown>): Promise<void> {
    const connection = await ConnectionStore.getCurrent();
    if (!connection) {
      console.log('No active connection. Run "pnp connect" first.');
      return;
    }
    this.writeOutput(connection.toJSON());
  }
}
