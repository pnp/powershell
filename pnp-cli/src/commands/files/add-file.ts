import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import * as fs from 'fs';
import * as path from 'path';

export class AddFileCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('add-file')
      .description('Uploads a file to a SharePoint folder')
      .requiredOption('--folder <folder>', 'Site relative URL of the folder to upload to')
      .option('--path <path>', 'Local file path to upload')
      .option('--filename <filename>', 'Target filename in SharePoint (required when using --content)')
      .option('--new-filename <newFilename>', 'Rename the file on upload')
      .option('--content <content>', 'Text content to upload as a file')
      .option('--overwrite', 'Overwrite the file if it already exists', true)
      .option('--checkout', 'Check out the file before uploading')
      .option('--checkin-comment <checkinComment>', 'Checkin comment')
      .option('--checkin-type <checkinType>', 'Checkin type: MinorCheckIn, MajorCheckIn, or OverwriteCheckIn', 'MinorCheckIn')
      .option('--publish', 'Publish the file after upload')
      .option('--publish-comment <publishComment>', 'Publish comment')
      .option('--approve', 'Approve the file after upload')
      .option('--approve-comment <approveComment>', 'Approve comment')
      .option('--content-type <contentType>', 'Content type name or ID to set on the uploaded file')
      .option('--values <values>', 'JSON string of field values to set on the uploaded file')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    const webPath = new URL(webUrl).pathname.replace(/\/$/, '');
    const folderRelativeUrl = options.folder as string;

    // Build server-relative folder URL
    const folderServerRelativeUrl = folderRelativeUrl.startsWith('/')
      ? folderRelativeUrl
      : `${webPath}/${folderRelativeUrl}`;

    let fileContent: Buffer;
    let targetFilename: string;

    if (this.parameterSpecified(options, 'path')) {
      // Upload from local file
      const localPath = options.path as string;
      const resolvedPath = path.isAbsolute(localPath)
        ? localPath
        : path.resolve(process.cwd(), localPath);

      if (!fs.existsSync(resolvedPath)) {
        throw new Error(`File not found: ${resolvedPath}`);
      }

      fileContent = fs.readFileSync(resolvedPath);
      targetFilename = (options.newFilename as string) || (options.filename as string) || path.basename(resolvedPath);
    } else if (this.parameterSpecified(options, 'content')) {
      // Upload from text content
      if (!options.filename) {
        throw new Error('--filename is required when using --content');
      }
      fileContent = Buffer.from(options.content as string, 'utf-8');
      targetFilename = options.filename as string;
    } else {
      throw new Error('Either --path or --content must be specified');
    }

    const overwrite = options.overwrite !== false;
    const encodedFolder = encodeURIComponent(folderServerRelativeUrl);
    const encodedFilename = encodeURIComponent(targetFilename);

    // Check out the file if it already exists and --checkout is specified
    if (options.checkout) {
      try {
        const fileServerRelativeUrl = `${folderServerRelativeUrl}/${targetFilename}`;
        await this.sharePointRequestHelper.post(
          `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileServerRelativeUrl)}')/checkout()`,
        );
        this.writeVerbose(`File checked out: ${fileServerRelativeUrl}`);
      } catch {
        // File may not exist yet, ignore checkout error
        this.writeVerbose('File does not exist yet, skipping checkout');
      }
    }

    // Upload the file using the REST API
    const uploadUrl = `${webUrl}/_api/web/GetFolderByServerRelativeUrl('${encodedFolder}')/Files/add(overwrite=${overwrite},url='${encodedFilename}')`;

    const response = await this.sharePointRequestHelper.postRaw(
      uploadUrl,
      fileContent.toString('binary'),
      {
        'Content-Type': 'application/octet-stream',
        'Content-Length': fileContent.length.toString(),
      },
    );

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Failed to upload file: HTTP ${response.status} - ${errorText}`);
    }

    const result = await response.json();

    // Set field values if provided
    if (this.parameterSpecified(options, 'values') || this.parameterSpecified(options, 'contentType')) {
      const fileServerRelativeUrl = `${folderServerRelativeUrl}/${targetFilename}`;
      const listItemUrl = `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileServerRelativeUrl)}')/ListItemAllFields`;

      const fieldValues: Record<string, unknown> = {};

      if (options.values) {
        try {
          const parsed = JSON.parse(options.values as string);
          Object.assign(fieldValues, parsed);
        } catch {
          throw new Error('--values must be a valid JSON string');
        }
      }

      if (options.contentType) {
        fieldValues['ContentTypeId'] = options.contentType;
      }

      if (Object.keys(fieldValues).length > 0) {
        await this.sharePointRequestHelper.patch(
          listItemUrl,
          fieldValues,
          {
            'IF-MATCH': '*',
            'X-HTTP-Method': 'MERGE',
          },
        );
      }
    }

    // Check in the file if checkout was requested
    if (options.checkout) {
      const fileServerRelativeUrl = `${folderServerRelativeUrl}/${targetFilename}`;
      const checkinComment = (options.checkinComment as string) || '';
      const checkinType = this.getCheckinTypeValue(options.checkinType as string);
      await this.sharePointRequestHelper.post(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileServerRelativeUrl)}')/checkin(comment='${encodeURIComponent(checkinComment)}',checkintype=${checkinType})`,
      );
    }

    // Publish if requested
    if (options.publish) {
      const fileServerRelativeUrl = `${folderServerRelativeUrl}/${targetFilename}`;
      const publishComment = (options.publishComment as string) || '';
      await this.sharePointRequestHelper.post(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileServerRelativeUrl)}')/publish(comment='${encodeURIComponent(publishComment)}')`,
      );
    }

    // Approve if requested
    if (options.approve) {
      const fileServerRelativeUrl = `${folderServerRelativeUrl}/${targetFilename}`;
      const approveComment = (options.approveComment as string) || '';
      await this.sharePointRequestHelper.post(
        `${webUrl}/_api/web/GetFileByServerRelativeUrl('${encodeURIComponent(fileServerRelativeUrl)}')/approve(comment='${encodeURIComponent(approveComment)}')`,
      );
    }

    this.writeOutput(result);
  }

  private getCheckinTypeValue(checkinType: string): number {
    switch (checkinType?.toLowerCase()) {
      case 'minorcheckin':
        return 0;
      case 'majorcheckin':
        return 1;
      case 'overwritecheckin':
        return 2;
      default:
        return 0;
    }
  }
}
