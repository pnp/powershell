import { Command } from 'commander';
import { BaseCommand } from '../../core/base-command.js';
import { ConnectionStore } from '../../auth/connection-store.js';

/**
 * Disconnect from the current Microsoft 365 environment.
 * Equivalent to Disconnect-PnPOnline in the C# codebase.
 */
export class DisconnectCommand extends BaseCommand {
  register(program: Command): void {
    program
      .command('disconnect')
      .description('Disconnect from the current Microsoft 365 environment')
      .action(this.createAction());
  }

  async execute(_options: Record<string, unknown>): Promise<void> {
    await ConnectionStore.clear();
    console.log('Disconnected');
  }
}
