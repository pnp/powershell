import { Command } from 'commander';
import { GraphCommand } from '../../core/graph-command.js';

/**
 * Invoke a raw Microsoft Graph API request.
 * Equivalent to Invoke-PnPGraphMethod in the C# codebase.
 */
export class InvokeGraphRequestCommand extends GraphCommand {
  register(program: Command): void {
    program
      .command('invoke-graph-request')
      .description('Invokes a REST request towards the Microsoft Graph API')
      .requiredOption('--url <url>', 'The URL of the Graph endpoint (relative or absolute)')
      .option('--method <method>', 'HTTP method to use (GET, POST, PUT, PATCH, DELETE)', 'GET')
      .option('--content <content>', 'Body content for POST/PUT/PATCH as JSON string')
      .option('--content-type <contentType>', 'Content-Type header', 'application/json')
      .option('--all', 'Automatically follow @odata.nextLink to retrieve all pages')
      .option('--consistency-level <level>', 'ConsistencyLevel header value')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const url = options.url as string;
    const method = ((options.method as string) || 'GET').toUpperCase();
    const content = options.content as string | undefined;

    const additionalHeaders: Record<string, string> = {};
    if (content && ['POST', 'PUT', 'PATCH'].includes(method)) {
      additionalHeaders['Content-Type'] = (options.contentType as string) || 'application/json';
    }
    if (options.consistencyLevel) {
      additionalHeaders['ConsistencyLevel'] = options.consistencyLevel as string;
    }

    if (method === 'GET' && options.all) {
      const results = await this.graphRequestHelper.getResultCollection(url, additionalHeaders);
      this.writeOutput(results);
    } else if (method === 'GET') {
      const result = await this.graphRequestHelper.getTyped(url, additionalHeaders);
      this.writeOutput(result);
    } else if (method === 'POST') {
      const body = content ? JSON.parse(content) : undefined;
      const result = await this.graphRequestHelper.post(url, body, additionalHeaders);
      this.writeOutput(result);
    } else if (method === 'PATCH') {
      const body = content ? JSON.parse(content) : undefined;
      const result = await this.graphRequestHelper.patch(url, body, additionalHeaders);
      this.writeOutput(result);
    } else if (method === 'PUT') {
      const body = content ? JSON.parse(content) : undefined;
      const result = await this.graphRequestHelper.put(url, body, additionalHeaders);
      this.writeOutput(result);
    } else if (method === 'DELETE') {
      const response = await this.graphRequestHelper.delete(url, additionalHeaders);
      if (response.status === 204) {
        console.log('Successfully deleted');
      } else {
        const text = await response.text();
        if (text) {
          this.writeOutput(JSON.parse(text));
        }
      }
    }
  }
}
