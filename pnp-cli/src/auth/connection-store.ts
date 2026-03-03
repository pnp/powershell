import * as fs from 'fs';
import * as path from 'path';
import * as os from 'os';
import { PnPConnection, PnPConnectionData } from './connection.js';

/**
 * Persists connection state to disk between CLI invocations.
 *
 * Since Node CLI is process-per-invocation (unlike PowerShell's persistent session),
 * the connection + MSAL token cache must be serialized to ~/.pnp-cli/.
 */
export class ConnectionStore {
  static readonly CONFIG_DIR = path.join(os.homedir(), '.pnp-cli');
  static readonly CONNECTION_FILE = path.join(ConnectionStore.CONFIG_DIR, 'connection.json');
  static readonly TOKEN_CACHE_FILE = path.join(ConnectionStore.CONFIG_DIR, 'msal-cache.json');

  private static ensureConfigDir(): void {
    if (!fs.existsSync(ConnectionStore.CONFIG_DIR)) {
      fs.mkdirSync(ConnectionStore.CONFIG_DIR, { recursive: true, mode: 0o700 });
    }
  }

  /**
   * Save a connection to disk.
   */
  static async save(connection: PnPConnection): Promise<void> {
    ConnectionStore.ensureConfigDir();
    const data = JSON.stringify(connection.toJSON(), null, 2);
    fs.writeFileSync(ConnectionStore.CONNECTION_FILE, data, { encoding: 'utf-8', mode: 0o600 });
  }

  /**
   * Load the current connection from disk.
   * Returns null if no connection file exists.
   */
  static async getCurrent(): Promise<PnPConnection | null> {
    if (!fs.existsSync(ConnectionStore.CONNECTION_FILE)) {
      return null;
    }

    try {
      const raw = fs.readFileSync(ConnectionStore.CONNECTION_FILE, 'utf-8');
      const data: PnPConnectionData = JSON.parse(raw);
      return PnPConnection.fromJSON(data);
    } catch {
      return null;
    }
  }

  /**
   * Clear the connection file (disconnect).
   */
  static async clear(): Promise<void> {
    if (fs.existsSync(ConnectionStore.CONNECTION_FILE)) {
      fs.unlinkSync(ConnectionStore.CONNECTION_FILE);
    }
  }

  /**
   * Read the MSAL token cache from disk.
   */
  static readTokenCache(): string | undefined {
    if (!fs.existsSync(ConnectionStore.TOKEN_CACHE_FILE)) {
      return undefined;
    }
    return fs.readFileSync(ConnectionStore.TOKEN_CACHE_FILE, 'utf-8');
  }

  /**
   * Write the MSAL token cache to disk.
   */
  static writeTokenCache(cache: string): void {
    ConnectionStore.ensureConfigDir();
    fs.writeFileSync(ConnectionStore.TOKEN_CACHE_FILE, cache, { encoding: 'utf-8', mode: 0o600 });
  }
}
