import { BaseCommand } from './base-command.js';
import { PnPConnection } from '../auth/connection.js';
import { ConnectionStore } from '../auth/connection-store.js';

/**
 * Base class for all commands that require an active connection.
 * Equivalent to PnPConnectedCmdlet in the C# codebase.
 */
export abstract class ConnectedCommand extends BaseCommand {
  protected connection!: PnPConnection;

  protected override async beforeExecute(options: Record<string, unknown>): Promise<void> {
    await super.beforeExecute(options);

    const connection = await ConnectionStore.getCurrent();
    if (!connection) {
      throw new Error('No active connection. Run "pnp connect" first.');
    }
    this.connection = connection;
  }
}
