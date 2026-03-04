import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class FindFileCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('find-file')
      .description('Finds files in the current web matching a given pattern')
      .requiredOption('--match <match>', 'Filename pattern to search for (supports * and ? wildcards)')
      .option('--list <list>', 'Title, URL, or GUID of the list to search in')
      .option('--folder <folder>', 'Site relative URL of the folder to search in')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const matchPattern = options.match as string;

    if (this.parameterSpecified(options, 'list')) {
      // Search within a specific list
      const listIdentity = options.list as string;
      const results = await this.findInList(webUrl, listIdentity, matchPattern);
      this.writeOutput(results);
    } else if (this.parameterSpecified(options, 'folder')) {
      // Search within a specific folder
      const folderUrl = options.folder as string;
      const results = await this.findInFolder(webUrl, folderUrl, matchPattern);
      this.writeOutput(results);
    } else {
      // Search across the entire web using recursive folder traversal from root
      const results = await this.findInWeb(webUrl, matchPattern);
      this.writeOutput(results);
    }
  }

  private async findInList(
    webUrl: string,
    listIdentity: string,
    matchPattern: string,
  ): Promise<unknown[]> {
    const guidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
    let listBaseUrl: string;

    if (guidRegex.test(listIdentity)) {
      listBaseUrl = `${webUrl}/_api/web/lists(guid'${listIdentity}')`;
    } else {
      listBaseUrl = `${webUrl}/_api/web/lists/GetByTitle('${encodeURIComponent(listIdentity)}')`;
    }

    // Use CAML query to get all files, then filter client-side by match pattern
    const camlQuery = {
      query: {
        __metadata: { type: 'SP.CamlQuery' },
        ViewXml: `<View Scope="RecursiveAll"><Query><Where><Eq><FieldRef Name="FSObjType" /><Value Type="Integer">0</Value></Eq></Where></Query><RowLimit Paged="TRUE">5000</RowLimit></View>`,
      },
    };

    const result = await this.sharePointRequestHelper.post<{ value: any[] }>(
      `${listBaseUrl}/GetItems`,
      camlQuery,
    );

    const items = result?.value || [];
    return items.filter((item: any) => {
      const fileName = item.FileLeafRef || item.File?.Name || '';
      return this.matchesPattern(fileName, matchPattern);
    });
  }

  private async findInFolder(
    webUrl: string,
    folderUrl: string,
    matchPattern: string,
  ): Promise<unknown[]> {
    const webPath = new URL(webUrl).pathname.replace(/\/$/, '');
    const serverRelativeUrl = folderUrl.startsWith('/')
      ? folderUrl
      : `${webPath}/${folderUrl}`;

    return this.findInFolderRecursive(webUrl, serverRelativeUrl, matchPattern);
  }

  private async findInWeb(webUrl: string, matchPattern: string): Promise<unknown[]> {
    const rootFolder = await this.sharePointRequestHelper.getTyped<{ ServerRelativeUrl: string }>(
      `${webUrl}/_api/web/RootFolder?$select=ServerRelativeUrl`,
    );

    if (!rootFolder?.ServerRelativeUrl) {
      throw new Error('Could not determine the root folder of the web');
    }

    return this.findInFolderRecursive(webUrl, rootFolder.ServerRelativeUrl, matchPattern);
  }

  private async findInFolderRecursive(
    webUrl: string,
    folderServerRelativeUrl: string,
    matchPattern: string,
  ): Promise<unknown[]> {
    const encodedUrl = encodeURIComponent(folderServerRelativeUrl);
    const results: unknown[] = [];

    // Get files in the current folder
    const files = await this.sharePointRequestHelper.getTyped<{ value: any[] }>(
      `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodedUrl}')/Files`,
    );

    if (files?.value) {
      for (const file of files.value) {
        if (this.matchesPattern(file.Name, matchPattern)) {
          results.push(file);
        }
      }
    }

    // Get sub-folders and recurse
    const folders = await this.sharePointRequestHelper.getTyped<{ value: any[] }>(
      `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodedUrl}')/Folders`,
    );

    if (folders?.value) {
      for (const folder of folders.value) {
        // Skip system folders
        if (folder.Name === 'Forms' || folder.Name === '_catalogs') continue;

        const subResults = await this.findInFolderRecursive(
          webUrl,
          folder.ServerRelativeUrl,
          matchPattern,
        );
        results.push(...subResults);
      }
    }

    return results;
  }

  /**
   * Matches a filename against a simple wildcard pattern (supports * and ?).
   */
  private matchesPattern(fileName: string, pattern: string): boolean {
    // Convert the simple wildcard pattern to a regex
    const escapedPattern = pattern
      .replace(/[.+^${}()|[\]\\]/g, '\\$&')
      .replace(/\*/g, '.*')
      .replace(/\?/g, '.');

    const regex = new RegExp(`^${escapedPattern}$`, 'i');
    return regex.test(fileName);
  }
}
