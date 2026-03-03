import { ApiRequestHelper } from './api-request-helper.js';
import { PnPConnection } from '../auth/connection.js';

/**
 * Convenience wrapper for Microsoft Graph API calls.
 * Uses ApiRequestHelper under the hood.
 */
export class GraphClient {
  private requestHelper: ApiRequestHelper;

  constructor(connection: PnPConnection) {
    this.requestHelper = new ApiRequestHelper(connection, `https://${connection.graphEndPoint}/.default`);
  }

  async get<T>(url: string, headers?: Record<string, string>): Promise<T> {
    return this.requestHelper.getTyped<T>(url, headers);
  }

  async getAll<T>(url: string, headers?: Record<string, string>): Promise<T[]> {
    return this.requestHelper.getResultCollection<T>(url, headers);
  }

  async post<T>(url: string, content?: unknown, headers?: Record<string, string>): Promise<T> {
    return this.requestHelper.post<T>(url, content, headers);
  }

  async patch<T>(url: string, content?: unknown, headers?: Record<string, string>): Promise<T> {
    return this.requestHelper.patch<T>(url, content, headers);
  }

  async put<T>(url: string, content?: unknown, headers?: Record<string, string>): Promise<T> {
    return this.requestHelper.put<T>(url, content, headers);
  }

  async delete(url: string, headers?: Record<string, string>): Promise<Response> {
    return this.requestHelper.delete(url, headers);
  }
}
