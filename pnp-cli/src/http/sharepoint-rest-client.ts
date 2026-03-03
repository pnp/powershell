import { PnPConnection } from '../auth/connection.js';
import { withThrottleRetry } from './throttle-handler.js';
import { logger } from '../utils/logger.js';
import { trimTrailingSlash } from '../utils/url-utilities.js';

/**
 * HTTP client for SharePoint REST API calls.
 * Handles request digest for POST/PATCH/DELETE operations.
 */
export class SharePointRestClient {
  private siteUrl: string;
  private requestDigest?: string;
  private requestDigestExpiry?: Date;

  constructor(
    private connection: PnPConnection,
    siteUrl?: string,
  ) {
    this.siteUrl = trimTrailingSlash(siteUrl || connection.url || '');
  }

  async get<T>(endpoint: string): Promise<T> {
    const url = this.resolveEndpoint(endpoint);
    const response = await this.sendRequest(url, 'GET');
    const text = await response.text();
    return JSON.parse(text) as T;
  }

  async getRaw(endpoint: string): Promise<string> {
    const url = this.resolveEndpoint(endpoint);
    const response = await this.sendRequest(url, 'GET');
    return response.text();
  }

  async post<T>(endpoint: string, body?: unknown): Promise<T> {
    const url = this.resolveEndpoint(endpoint);
    const digest = await this.getRequestDigest();
    const response = await this.sendRequest(url, 'POST', body ? JSON.stringify(body) : undefined, {
      'X-RequestDigest': digest,
      'Content-Type': 'application/json;odata=verbose',
    });
    const text = await response.text();
    if (!text) return undefined as unknown as T;
    return JSON.parse(text) as T;
  }

  async patch<T>(endpoint: string, body?: unknown): Promise<T> {
    const url = this.resolveEndpoint(endpoint);
    const digest = await this.getRequestDigest();
    const response = await this.sendRequest(url, 'POST', body ? JSON.stringify(body) : undefined, {
      'X-RequestDigest': digest,
      'Content-Type': 'application/json;odata=verbose',
      'X-HTTP-Method': 'MERGE',
      'IF-MATCH': '*',
    });
    const text = await response.text();
    if (!text) return undefined as unknown as T;
    return JSON.parse(text) as T;
  }

  async delete(endpoint: string): Promise<void> {
    const url = this.resolveEndpoint(endpoint);
    const digest = await this.getRequestDigest();
    await this.sendRequest(url, 'POST', undefined, {
      'X-RequestDigest': digest,
      'X-HTTP-Method': 'DELETE',
      'IF-MATCH': '*',
    });
  }

  async getResponse(endpoint: string): Promise<Response> {
    const url = this.resolveEndpoint(endpoint);
    return this.sendRequest(url, 'GET');
  }

  private resolveEndpoint(endpoint: string): string {
    if (endpoint.startsWith('https://')) return endpoint;
    if (endpoint.startsWith('/')) return `${this.siteUrl}${endpoint}`;
    return `${this.siteUrl}/${endpoint}`;
  }

  private async getRequestDigest(): Promise<string> {
    // Return cached digest if still valid
    if (this.requestDigest && this.requestDigestExpiry && this.requestDigestExpiry > new Date()) {
      return this.requestDigest;
    }

    const accessToken = await this.getAccessToken();
    const url = `${this.siteUrl}/_api/contextinfo`;

    logger.debug('SharePointRestClient', `Fetching request digest from ${url}`);

    const response = await fetch(url, {
      method: 'POST',
      headers: {
        Accept: 'application/json;odata=verbose',
        Authorization: `Bearer ${accessToken}`,
        'Content-Length': '0',
      },
    });

    if (!response.ok) {
      throw new Error(`Failed to get request digest: HTTP ${response.status}`);
    }

    const data = await response.json() as { d: { GetContextWebInformation: { FormDigestValue: string; FormDigestTimeoutSeconds: number } } };
    this.requestDigest = data.d.GetContextWebInformation.FormDigestValue;
    this.requestDigestExpiry = new Date(
      Date.now() + data.d.GetContextWebInformation.FormDigestTimeoutSeconds * 1000 - 60000,
    );

    return this.requestDigest;
  }

  private async getAccessToken(): Promise<string> {
    const resourceUri = new URL(this.siteUrl);
    return this.connection.getAccessToken(`${resourceUri.protocol}//${resourceUri.host}/.default`);
  }

  private async sendRequest(
    url: string,
    method: string,
    body?: string,
    additionalHeaders?: Record<string, string>,
  ): Promise<Response> {
    const accessToken = await this.getAccessToken();

    logger.debug('SharePointRestClient', `Making ${method} call to ${url}`);

    const headers: Record<string, string> = {
      Accept: 'application/json;odata=verbose',
      Authorization: `Bearer ${accessToken}`,
      ...additionalHeaders,
    };

    const response = await withThrottleRetry(() =>
      fetch(url, {
        method,
        headers,
        body: body || undefined,
      }),
    );

    if (!response.ok) {
      const errorText = await response.text();
      logger.error('SharePointRestClient', `Request failed: HTTP ${response.status} - ${errorText.substring(0, 500)}`);
      throw new Error(`SharePoint REST API call to ${url} failed with status ${response.status}: ${errorText}`);
    }

    return response;
  }
}
