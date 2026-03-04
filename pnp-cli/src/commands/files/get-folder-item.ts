import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';

export class GetFolderItemCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('get-folder-item')
      .description('Returns items (files and/or folders) in a folder')
      .option('--folder-url <folderUrl>', 'Site relative URL of the folder')
      .option('--list <list>', 'Title, URL, or GUID of the list to retrieve items from')
      .option('--item-type <itemType>', 'Filter by type: File, Folder, or All', 'All')
      .option('--item-name <itemName>', 'Filter by item name')
      .option('--recursive', 'Recursively retrieve items from sub-folders')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const itemType = (options.itemType as string) || 'All';
    const itemName = options.itemName as string;

    let results: unknown[];

    if (this.parameterSpecified(options, 'list')) {
      // Get items from a document library using CAML query approach
      results = await this.getItemsFromList(webUrl, options.list as string, itemType);
    } else {
      // Get items from a folder URL
      const folderUrl = options.folderUrl as string;
      results = await this.getItemsByUrl(webUrl, folderUrl, itemType, options.recursive === true);
    }

    // Filter by name if specified
    if (itemName) {
      results = results.filter((item: any) => {
        const name = item.Name || item.FileLeafRef || '';
        return name.toLowerCase() === itemName.toLowerCase();
      });
    }

    this.writeOutput(results);
  }

  private async getItemsFromList(
    webUrl: string,
    listIdentity: string,
    itemType: string,
  ): Promise<unknown[]> {
    // Determine list URL
    const guidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
    let listBaseUrl: string;

    if (guidRegex.test(listIdentity)) {
      listBaseUrl = `${webUrl}/_api/web/lists(guid'${listIdentity}')`;
    } else {
      listBaseUrl = `${webUrl}/_api/web/lists/GetByTitle('${encodeURIComponent(listIdentity)}')`;
    }

    // Build CAML query based on item type filter
    let camlWhere = '';
    if (itemType === 'File') {
      camlWhere = '<Where><Eq><FieldRef Name="FSObjType" /><Value Type="Integer">0</Value></Eq></Where>';
    } else if (itemType === 'Folder') {
      camlWhere = '<Where><Eq><FieldRef Name="FSObjType" /><Value Type="Integer">1</Value></Eq></Where>';
    }

    const camlQuery = {
      query: {
        __metadata: { type: 'SP.CamlQuery' },
        ViewXml: `<View Scope="RecursiveAll"><Query>${camlWhere}</Query><RowLimit Paged="TRUE">1000</RowLimit></View>`,
      },
    };

    const result = await this.sharePointRequestHelper.post<{ value: unknown[] }>(
      `${listBaseUrl}/GetItems`,
      camlQuery,
    );

    return result?.value || [];
  }

  private async getItemsByUrl(
    webUrl: string,
    folderSiteRelativeUrl: string | undefined,
    itemType: string,
    recursive: boolean,
  ): Promise<unknown[]> {
    const webPath = new URL(webUrl).pathname.replace(/\/$/, '');
    let folderServerRelativeUrl: string;

    if (folderSiteRelativeUrl) {
      folderServerRelativeUrl = folderSiteRelativeUrl.startsWith('/')
        ? folderSiteRelativeUrl
        : `${webPath}/${folderSiteRelativeUrl}`;
    } else {
      // Use root folder of the web
      const rootFolder = await this.sharePointRequestHelper.getTyped<{ ServerRelativeUrl: string }>(
        `${webUrl}/_api/web/RootFolder?$select=ServerRelativeUrl`,
      );
      folderServerRelativeUrl = rootFolder?.ServerRelativeUrl || webPath;
    }

    const encodedUrl = encodeURIComponent(folderServerRelativeUrl);
    const results: unknown[] = [];

    // Fetch files
    if (itemType === 'File' || itemType === 'All') {
      const files = await this.sharePointRequestHelper.getTyped<{ value: unknown[] }>(
        `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodedUrl}')/Files`,
      );
      if (files?.value) {
        results.push(...files.value);
      }
    }

    // Fetch folders
    let folders: any[] = [];
    if (itemType === 'Folder' || itemType === 'All' || recursive) {
      const foldersResult = await this.sharePointRequestHelper.getTyped<{ value: any[] }>(
        `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodedUrl}')/Folders`,
      );
      if (foldersResult?.value) {
        folders = foldersResult.value;
        if (itemType === 'Folder' || itemType === 'All') {
          results.push(...folders);
        }
      }
    }

    // Recurse into sub-folders
    if (recursive && folders.length > 0) {
      for (const folder of folders) {
        const subFolderUrl = folder.ServerRelativeUrl as string;
        // Convert back to site-relative for recursive call
        const siteRelativeUrl = subFolderUrl.startsWith(webPath)
          ? subFolderUrl.substring(webPath.length)
          : subFolderUrl;
        const subResults = await this.getItemsByUrl(webUrl, siteRelativeUrl, itemType, true);
        results.push(...subResults);
      }
    }

    return results;
  }
}
