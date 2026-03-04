import { Command } from 'commander';
import { BaseCommand } from './core/base-command.js';
import { setLogLevel, LogLevel } from './utils/logger.js';

// Import all commands explicitly (auto-discovery would need build step)
import { ConnectCommand } from './commands/base/connect.js';
import { DisconnectCommand } from './commands/base/disconnect.js';
import { GetConnectionCommand } from './commands/base/get-connection.js';
import { GetAccessTokenCommand } from './commands/base/get-access-token.js';
import { GetContextCommand } from './commands/base/get-context.js';
import { InvokeSPRestMethodCommand } from './commands/base/invoke-sp-rest-method.js';
import { InvokeGraphRequestCommand } from './commands/base/invoke-graph-request.js';

// List commands
import {
  GetListCommand,
  GetListItemCommand,
  AddListItemCommand,
  SetListItemCommand,
  RemoveListCommand,
  RemoveListItemCommand,
  NewListCommand,
  SetListCommand,
  GetViewCommand,
} from './commands/lists/index.js';

// Teams commands
import {
  GetTeamsTeamCommand,
  NewTeamsTeamCommand,
  RemoveTeamsTeamCommand,
  GetTeamsChannelCommand,
  NewTeamsChannelCommand,
  GetTeamsUserCommand,
} from './commands/teams/index.js';

// Microsoft 365 Groups commands
import {
  GetM365GroupCommand,
  NewM365GroupCommand,
  RemoveM365GroupCommand,
  GetM365GroupMemberCommand,
} from './commands/groups/index.js';

// Entra ID commands
import {
  GetEntraUserCommand,
  GetEntraGroupCommand,
  GetEntraAppCommand,
} from './commands/entra/index.js';

// File commands
import {
  GetFileCommand,
  AddFileCommand,
  RemoveFileCommand,
  CopyFileCommand,
  MoveFileCommand,
  GetFolderCommand,
  AddFolderCommand,
  RemoveFolderCommand,
  GetFolderItemCommand,
  FindFileCommand,
} from './commands/files/index.js';

// Site commands
import {
  GetSiteCommand,
  SetSiteCommand,
  RemoveSiteCommand,
  GetSiteDesignCommand,
} from './commands/sites/index.js';

// Web commands
import {
  GetWebCommand,
  SetWebCommand,
  NewWebCommand,
  RemoveWebCommand,
  GetSubwebsCommand,
  GetPropertyBagCommand,
} from './commands/webs/index.js';

// Field commands
import {
  GetFieldCommand,
  AddFieldCommand,
  RemoveFieldCommand,
  SetFieldCommand,
} from './commands/fields/index.js';

// Content type commands
import {
  GetContentTypeCommand,
  AddContentTypeCommand,
  RemoveContentTypeCommand,
  AddContentTypeToListCommand,
} from './commands/content-types/index.js';

// Principals commands
import {
  GetSPUserCommand,
  GetSPGroupCommand,
  GetGroupMemberCommand,
  NewSPGroupCommand,
  AddGroupMemberCommand,
  RemoveGroupMemberCommand,
} from './commands/principals/index.js';

const pkg = { version: '1.0.0', name: '@pnp/cli' };

/**
 * All registered command classes. New commands should be added here.
 */
const COMMAND_CLASSES: Array<new () => BaseCommand> = [
  // Base commands
  ConnectCommand,
  DisconnectCommand,
  GetConnectionCommand,
  GetAccessTokenCommand,
  GetContextCommand,
  InvokeSPRestMethodCommand,
  InvokeGraphRequestCommand,
  // List commands
  GetListCommand,
  GetListItemCommand,
  AddListItemCommand,
  SetListItemCommand,
  RemoveListCommand,
  RemoveListItemCommand,
  NewListCommand,
  SetListCommand,
  GetViewCommand,
  // Teams commands
  GetTeamsTeamCommand,
  NewTeamsTeamCommand,
  RemoveTeamsTeamCommand,
  GetTeamsChannelCommand,
  NewTeamsChannelCommand,
  GetTeamsUserCommand,
  // Microsoft 365 Groups commands
  GetM365GroupCommand,
  NewM365GroupCommand,
  RemoveM365GroupCommand,
  GetM365GroupMemberCommand,
  // Entra ID commands
  GetEntraUserCommand,
  GetEntraGroupCommand,
  GetEntraAppCommand,
  // File commands
  GetFileCommand,
  AddFileCommand,
  RemoveFileCommand,
  CopyFileCommand,
  MoveFileCommand,
  GetFolderCommand,
  AddFolderCommand,
  RemoveFolderCommand,
  GetFolderItemCommand,
  FindFileCommand,
  // Site commands
  GetSiteCommand,
  SetSiteCommand,
  RemoveSiteCommand,
  GetSiteDesignCommand,
  // Web commands
  GetWebCommand,
  SetWebCommand,
  NewWebCommand,
  RemoveWebCommand,
  GetSubwebsCommand,
  GetPropertyBagCommand,
  // Field commands
  GetFieldCommand,
  AddFieldCommand,
  RemoveFieldCommand,
  SetFieldCommand,
  // Content type commands
  GetContentTypeCommand,
  AddContentTypeCommand,
  RemoveContentTypeCommand,
  AddContentTypeToListCommand,
  // Principals commands
  GetSPUserCommand,
  GetSPGroupCommand,
  GetGroupMemberCommand,
  NewSPGroupCommand,
  AddGroupMemberCommand,
  RemoveGroupMemberCommand,
];

export function buildCli(): Command {
  const program = new Command();

  program
    .name('pnp')
    .version(pkg.version)
    .description('PnP CLI - Microsoft 365 administration and automation')
    .option('--format <format>', 'Output format: json, table, csv, text', 'json')
    .option('--verbose', 'Enable verbose output')
    .option('--debug', 'Enable debug output')
    .option('--output <file>', 'Write output to file');

  // Set up logging based on global options
  program.hook('preAction', (_thisCommand, actionCommand) => {
    const opts = actionCommand.optsWithGlobals();
    if (opts.debug) {
      setLogLevel(LogLevel.Debug);
    } else if (opts.verbose) {
      setLogLevel(LogLevel.Info);
    }
  });

  // Register all commands
  for (const CommandClass of COMMAND_CLASSES) {
    const command = new CommandClass();
    command.register(program);
  }

  return program;
}
