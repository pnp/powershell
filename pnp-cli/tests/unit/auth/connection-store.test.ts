import * as fs from 'fs';
import * as path from 'path';
import * as os from 'os';
import { ConnectionStore } from '../../../src/auth/connection-store';
import { PnPConnection } from '../../../src/auth/connection';
import { AzureEnvironment } from '../../../src/enums/azure-environment';

describe('ConnectionStore', () => {
  const testDir = path.join(os.tmpdir(), '.pnp-cli-test-' + Date.now());
  const originalConfigDir = ConnectionStore.CONFIG_DIR;
  const originalConnectionFile = ConnectionStore.CONNECTION_FILE;

  beforeAll(() => {
    // Override paths for testing
    (ConnectionStore as any).CONFIG_DIR = testDir;
    (ConnectionStore as any).CONNECTION_FILE = path.join(testDir, 'connection.json');
  });

  afterEach(() => {
    if (fs.existsSync(testDir)) {
      fs.rmSync(testDir, { recursive: true, force: true });
    }
  });

  afterAll(() => {
    (ConnectionStore as any).CONFIG_DIR = originalConfigDir;
    (ConnectionStore as any).CONNECTION_FILE = originalConnectionFile;
  });

  it('should return null when no connection exists', async () => {
    const connection = await ConnectionStore.getCurrent();
    expect(connection).toBeNull();
  });

  it('should save and load a connection', async () => {
    const connection = PnPConnection.createWithAccessToken(
      'https://contoso.sharepoint.com',
      'fake-token',
      undefined,
      AzureEnvironment.Production,
    );

    await ConnectionStore.save(connection);
    const loaded = await ConnectionStore.getCurrent();

    expect(loaded).not.toBeNull();
    expect(loaded!.url).toBe('https://contoso.sharepoint.com');
    expect(loaded!.connectionMethod).toBe('AccessToken');
  });

  it('should clear the connection', async () => {
    const connection = PnPConnection.createWithAccessToken(
      'https://contoso.sharepoint.com',
      'fake-token',
    );

    await ConnectionStore.save(connection);
    expect(await ConnectionStore.getCurrent()).not.toBeNull();

    await ConnectionStore.clear();
    expect(await ConnectionStore.getCurrent()).toBeNull();
  });
});
