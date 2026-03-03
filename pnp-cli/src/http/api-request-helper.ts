import { PnPConnection } from '../auth/connection.js';
import { GraphException } from '../models/graph/graph-exception.js';
import { RestResultCollection } from './rest-result-collection.js';
import { withThrottleRetry } from './throttle-handler.js';
import { logger } from '../utils/logger.js';

/**
 * Central HTTP helper for making API calls to Microsoft Graph and SharePoint REST APIs.
 * Equivalent to ApiRequestHelper in the C# codebase.
 */
export class ApiRequestHelper {
  constructor(
    private connection: PnPConnection,
    private audience: string,
  ) {}

  get graphEndPoint(): string {
    return this.connection.graphEndPoint;
  }

  // --- GET ---

  async get(url: string, additionalHeaders?: Record<string, string>): Promise<string> {
    const response = await this.sendRequest(url, 'GET', undefined, additionalHeaders);
    return response;
  }

  async getTyped<T>(
    url: string,
    additionalHeaders?: Record<string, string>,
  ): Promise<T> {
    const content = await this.get(url, additionalHeaders);
    if (!content) return undefined as unknown as T;

    try {
      return JSON.parse(content) as T;
    } catch (e) {
      logger.error('ApiRequestHelper', `Failed to parse response: ${(e as Error).message}`);
      return undefined as unknown as T;
    }
  }

  /**
   * Fetches all pages of a paged result collection.
   * Equivalent to GetResultCollection<T> in the C# codebase.
   */
  async getResultCollection<T>(
    url: string,
    additionalHeaders?: Record<string, string>,
  ): Promise<T[]> {
    const results: T[] = [];
    let request = await this.getTyped<RestResultCollection<T>>(url, additionalHeaders);

    if (request?.value?.length) {
      results.push(...request.value);

      while (request?.['@odata.nextLink']) {
        logger.debug('ApiRequestHelper', `Paged request. ${results.length} items retrieved so far.`);
        request = await this.getTyped<RestResultCollection<T>>(request['@odata.nextLink'], additionalHeaders);
        if (request?.value?.length) {
          results.push(...request.value);
        }
      }
    }

    logger.debug('ApiRequestHelper', `Returning ${results.length} items.`);
    return results;
  }

  async getResponse(url: string, additionalHeaders?: Record<string, string>): Promise<Response> {
    return this.sendRawRequest(url, 'GET', undefined, additionalHeaders);
  }

  // --- POST ---

  async post<T>(
    url: string,
    content?: unknown,
    additionalHeaders?: Record<string, string>,
  ): Promise<T> {
    const body = content ? JSON.stringify(content) : undefined;
    const responseContent = await this.sendRequest(url, 'POST', body, {
      'Content-Type': 'application/json',
      ...additionalHeaders,
    });

    if (!responseContent) return undefined as unknown as T;

    try {
      return JSON.parse(responseContent) as T;
    } catch {
      return undefined as unknown as T;
    }
  }

  async postRaw(
    url: string,
    content?: string,
    additionalHeaders?: Record<string, string>,
  ): Promise<Response> {
    return this.sendRawRequest(url, 'POST', content, additionalHeaders);
  }

  // --- PATCH ---

  async patch<T>(
    url: string,
    content?: unknown,
    additionalHeaders?: Record<string, string>,
  ): Promise<T> {
    const body = content ? JSON.stringify(content) : undefined;
    const responseContent = await this.sendRequest(url, 'PATCH', body, {
      'Content-Type': 'application/json',
      ...additionalHeaders,
    });

    if (!responseContent) return undefined as unknown as T;

    try {
      return JSON.parse(responseContent) as T;
    } catch {
      return undefined as unknown as T;
    }
  }

  async patchRaw(
    url: string,
    content?: string,
    additionalHeaders?: Record<string, string>,
  ): Promise<Response> {
    return this.sendRawRequest(url, 'PATCH', content, additionalHeaders);
  }

  // --- PUT ---

  async put<T>(
    url: string,
    content?: unknown,
    additionalHeaders?: Record<string, string>,
  ): Promise<T> {
    const body = content ? JSON.stringify(content) : undefined;
    const responseContent = await this.sendRequest(url, 'PUT', body, {
      'Content-Type': 'application/json',
      ...additionalHeaders,
    });

    if (!responseContent) return undefined as unknown as T;

    try {
      return JSON.parse(responseContent) as T;
    } catch {
      return undefined as unknown as T;
    }
  }

  // --- DELETE ---

  async delete(url: string, additionalHeaders?: Record<string, string>): Promise<Response> {
    return this.sendRawRequest(url, 'DELETE', undefined, additionalHeaders);
  }

  async deleteTyped<T>(
    url: string,
    additionalHeaders?: Record<string, string>,
  ): Promise<T> {
    const response = await this.delete(url, additionalHeaders);
    if (response.ok) {
      const text = await response.text();
      if (text) {
        try {
          return JSON.parse(text) as T;
        } catch {
          return undefined as unknown as T;
        }
      }
    }
    return undefined as unknown as T;
  }

  // --- Internal ---

  /**
   * Normalizes the URL, auto-prepending the Graph endpoint for relative URLs.
   * Mirrors the logic in GetMessage() in the C# ApiRequestHelper.
   */
  private normalizeUrl(url: string): string {
    if (url.startsWith('/')) {
      url = url.substring(1);
    }

    if (!url.startsWith('https://')) {
      if (!url.startsWith('v1.0/') && !url.startsWith('beta/')) {
        url = `v1.0/${url}`;
      }
      url = `https://${this.connection.graphEndPoint}/${url}`;
    }

    return url;
  }

  private async sendRequest(
    url: string,
    method: string,
    body?: string,
    additionalHeaders?: Record<string, string>,
  ): Promise<string> {
    const response = await this.sendRawRequest(url, method, body, additionalHeaders);

    if (response.ok) {
      const responseBody = await response.text();
      logger.debug(
        'ApiRequestHelper',
        `Response successful with HTTP ${response.status} containing ${responseBody.length} characters.`,
      );
      return responseBody;
    }

    const errorContent = await response.text();
    logger.error(
      'ApiRequestHelper',
      `Response failed with HTTP ${response.status}: ${errorContent.substring(0, 500)}`,
    );

    throw GraphException.fromResponse(errorContent, response.status, response);
  }

  private async sendRawRequest(
    url: string,
    method: string,
    body?: string,
    additionalHeaders?: Record<string, string>,
  ): Promise<Response> {
    const normalizedUrl = this.normalizeUrl(url);
    const accessToken = await this.connection.getAccessToken(this.audience);

    logger.debug('ApiRequestHelper', `Making ${method} call to ${normalizedUrl}`);

    const headers: Record<string, string> = {
      Accept: 'application/json',
      Authorization: `Bearer ${accessToken}`,
      ...additionalHeaders,
    };

    const requestInit: RequestInit = {
      method,
      headers,
    };

    if (body && (method === 'POST' || method === 'PUT' || method === 'PATCH')) {
      requestInit.body = body;
    }

    return withThrottleRetry(() => fetch(normalizedUrl, requestInit));
  }
}
