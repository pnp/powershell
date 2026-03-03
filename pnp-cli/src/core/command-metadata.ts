/**
 * Metadata storage for command decorators.
 * We use a simple Map instead of reflect-metadata to avoid an extra dependency.
 */
const metadataStore = new Map<Function, Map<string, unknown>>();

function getOrCreateMetadata(target: Function): Map<string, unknown> {
  if (!metadataStore.has(target)) {
    metadataStore.set(target, new Map());
  }
  return metadataStore.get(target)!;
}

export interface PermissionsConfig {
  delegated?: string[];
  application?: string[];
  delegatedOrApplication?: string[];
}

/**
 * Decorator to declare required API permissions for a command.
 * Equivalent to [RequiredApiDelegatedPermissions] and [RequiredApiApplicationPermissions] attributes.
 */
export function RequiredPermissions(permissions: PermissionsConfig) {
  return function (target: Function) {
    const metadata = getOrCreateMetadata(target);
    metadata.set('requiredPermissions', permissions);
  };
}

/**
 * Decorator to mark a command as deprecated.
 * Equivalent to [WriteAliasWarning] attribute.
 */
export function Deprecated(message: string) {
  return function (target: Function) {
    const metadata = getOrCreateMetadata(target);
    metadata.set('deprecated', message);
  };
}

/**
 * Retrieve permissions metadata for a command class.
 */
export function getRequiredPermissions(target: Function): PermissionsConfig | undefined {
  return metadataStore.get(target)?.get('requiredPermissions') as PermissionsConfig | undefined;
}

/**
 * Retrieve deprecation message for a command class.
 */
export function getDeprecationMessage(target: Function): string | undefined {
  return metadataStore.get(target)?.get('deprecated') as string | undefined;
}
