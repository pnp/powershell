import { Command } from 'commander';
import { SharePointCommand } from '../../core/sharepoint-command.js';

/**
 * Invoke a raw SharePoint REST API method.
 * Equivalent to Invoke-PnPSPRestMethod in the C# codebase.
 */
export class InvokeSPRestMethodCommand extends SharePointCommand {
  register(program: Command): void {
    program
      .command('invoke-sp-rest-method')
      .description('Invokes a REST request towards a SharePoint site')
      .requiredOption('--url <url>', 'The URL of the REST endpoint (relative or absolute)')
      .option('--method <method>', 'HTTP method to use (GET, POST, PUT, PATCH, DELETE)', 'GET')
      .option('--content <content>', 'Body content for POST/PUT/PATCH as JSON string')
      .option('--content-type <contentType>', 'Content-Type header', 'application/json')
      .option('--accept <accept>', 'Accept header', 'application/json;odata=verbose')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    let url = options.url as string;
    const method = ((options.method as string) || 'GET').toUpperCase();
    const content = options.content as string | undefined;

    // Resolve relative URLs against the site URL
    if (!url.startsWith('https://')) {
      if (url.startsWith('/')) {
        url = `${this.connection.url}${url}`;
      } else {
        url = `${this.connection.url}/${url}`;
      }
    }

    const accessToken = await this.accessToken;
    const headers: Record<string, string> = {
      Accept: options.accept as string || 'application/json;odata=verbose',
      Authorization: `Bearer ${accessToken}`,
    };

    const requestInit: RequestInit = {
      method,
      headers,
    };

    if (content && ['POST', 'PUT', 'PATCH'].includes(method)) {
      requestInit.body = content;
      headers['Content-Type'] = options.contentType as string || 'application/json';
    }

    const response = await fetch(url, requestInit);

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Request failed with status ${response.status}: ${errorText}`);
    }

    const responseText = await response.text();
    if (responseText) {
      try {
        const json = JSON.parse(responseText);
        this.writeOutput(json);
      } catch {
        console.log(responseText);
      }
    }
  }
}
