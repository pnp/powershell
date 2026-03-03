import { Command } from 'commander';
import { ConnectedCommand } from '../../core/connected-command.js';

/**
 * Get an access token for the current connection.
 * Equivalent to Get-PnPAccessToken in the C# codebase.
 */
export class GetAccessTokenCommand extends ConnectedCommand {
  register(program: Command): void {
    program
      .command('get-access-token')
      .description('Returns the current OAuth 2.0 Access Token')
      .option('--resource-url <url>', 'Resource URL to get token for')
      .option('--decoded', 'Decode and display the token claims')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const resourceUrl = (options.resourceUrl as string) ||
      (this.connection.url
        ? `${new URL(this.connection.url).origin}/.default`
        : `https://${this.connection.graphEndPoint}/.default`);

    const token = await this.connection.getAccessToken(resourceUrl);

    if (options.decoded) {
      const parts = token.split('.');
      if (parts.length === 3) {
        const payload = JSON.parse(Buffer.from(parts[1], 'base64').toString('utf-8'));
        this.writeOutput(payload);
      } else {
        this.writeOutput(token);
      }
    } else {
      console.log(token);
    }
  }
}
