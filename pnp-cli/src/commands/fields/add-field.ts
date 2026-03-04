import { Command } from 'commander';
import { SharePointWebCommand } from '../../core/sharepoint-web-command.js';
import { ListResolver } from '../../resolvers/list-resolver.js';

export class AddFieldCommand extends SharePointWebCommand {
  register(program: Command): void {
    program
      .command('add-field')
      .description('Add a field to a web or list')
      .option('--list <nameOrId>', 'List title, URL, or GUID')
      .option('--display-name <name>', 'Display name of the field')
      .option('--internal-name <name>', 'Internal name of the field')
      .option('--type <type>', 'Field type (Text, Note, Number, DateTime, Boolean, Choice, Lookup, User, URL, etc.)')
      .option('--group <group>', 'Field group name')
      .option('--required', 'Make the field required')
      .option('--add-to-default-view', 'Add the field to the default view')
      .option('--xml <xml>', 'Raw CAML XML for field definition')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    const webUrl = this.getWebUrl();
    let baseEndpoint: string;

    if (this.parameterSpecified(options, 'list')) {
      const listResolver = new ListResolver(options.list as string);
      const list = await listResolver.resolve(this.sharePointRequestHelper as any, webUrl);
      const listId = (list as any).Id;
      baseEndpoint = `${webUrl}/_api/web/lists(guid'${listId}')/fields`;
    } else {
      baseEndpoint = `${webUrl}/_api/web/fields`;
    }

    if (this.parameterSpecified(options, 'xml')) {
      const url = `${baseEndpoint}/createfieldasxml`;
      const body = {
        parameters: {
          __metadata: { type: 'SP.XmlSchemaFieldCreationInformation' },
          SchemaXml: options.xml as string,
          Options: options.addToDefaultView ? 8 : 0, // AddFieldToDefaultView = 8
        },
      };
      const result = await this.sharePointRequestHelper.post<any>(url, body);
      this.writeOutput(result);
    } else {
      const fieldTypeMap: Record<string, number> = {
        text: 2, note: 3, number: 9, datetime: 4, boolean: 8,
        choice: 6, multichoice: 15, lookup: 7, user: 20,
        url: 11, currency: 10, calculated: 17, integer: 1,
      };
      const typeName = (options.type as string || 'text').toLowerCase();
      const fieldTypeKind = fieldTypeMap[typeName] ?? 2;

      const body: any = {
        __metadata: { type: 'SP.Field' },
        Title: options.displayName || options.internalName,
        FieldTypeKind: fieldTypeKind,
        Required: !!options.required,
        InternalName: options.internalName || options.displayName,
      };

      if (options.group) {
        body.Group = options.group;
      }

      const result = await this.sharePointRequestHelper.post<any>(baseEndpoint, body);

      if (options.addToDefaultView && result?.InternalName) {
        const listEndpoint = baseEndpoint.replace('/fields', '');
        const viewUrl = `${listEndpoint}/defaultview/viewfields/addviewfield('${result.InternalName}')`;
        await this.sharePointRequestHelper.post<void>(viewUrl, {});
      }

      this.writeOutput(result);
    }
  }
}
