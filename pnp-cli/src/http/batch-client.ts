import { PnPConnection } from '../auth/connection.js';
import { logger } from '../utils/logger.js';

interface BatchRequest {
  id: string;
  method: string;
  url: string;
  body?: unknown;
  headers?: Record<string, string>;
}

interface BatchResponse {
  id: string;
  status: number;
  headers?: Record<string, string>;
  body?: unknown;
}

/**
 * Client for Microsoft Graph $batch API.
 * Allows batching up to 20 requests into a single HTTP call.
 */
export class BatchClient {
  private requests: BatchRequest[] = [];
  private requestCounter = 0;

  constructor(private connection: PnPConnection) {}

  addRequest(method: string, url: string, body?: unknown, headers?: Record<string, string>): string {
    const id = String(++this.requestCounter);
    this.requests.push({ id, method, url, body, headers });
    return id;
  }

  get count(): number {
    return this.requests.length;
  }

  async execute(): Promise<BatchResponse[]> {
    if (this.requests.length === 0) {
      return [];
    }

    const allResponses: BatchResponse[] = [];

    // Graph $batch supports max 20 requests per batch
    const chunks = [];
    for (let i = 0; i < this.requests.length; i += 20) {
      chunks.push(this.requests.slice(i, i + 20));
    }

    const accessToken = await this.connection.getAccessToken(`https://${this.connection.graphEndPoint}/.default`);

    for (const chunk of chunks) {
      const batchBody = {
        requests: chunk.map((r) => ({
          id: r.id,
          method: r.method,
          url: r.url,
          body: r.body,
          headers: r.headers,
        })),
      };

      logger.debug('BatchClient', `Executing batch with ${chunk.length} requests.`);

      const response = await fetch(`https://${this.connection.graphEndPoint}/v1.0/$batch`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${accessToken}`,
        },
        body: JSON.stringify(batchBody),
      });

      if (!response.ok) {
        throw new Error(`Batch request failed with status ${response.status}`);
      }

      const result = (await response.json()) as { responses: BatchResponse[] };
      allResponses.push(...result.responses);
    }

    // Clear the request queue
    this.requests = [];
    this.requestCounter = 0;

    return allResponses;
  }
}
