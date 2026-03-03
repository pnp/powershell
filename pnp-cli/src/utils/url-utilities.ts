/**
 * Checks if a string is a valid GUID/UUID
 */
export function isGuid(value: string): boolean {
  return /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(value);
}

/**
 * Ensures a URL ends without a trailing slash
 */
export function trimTrailingSlash(url: string): string {
  return url.replace(/\/+$/, '');
}

/**
 * Ensures a URL starts with https://
 */
export function ensureHttps(url: string): string {
  if (url.startsWith('http://')) {
    return 'https://' + url.substring(7);
  }
  if (!url.startsWith('https://')) {
    return 'https://' + url;
  }
  return url;
}

/**
 * Extracts the root site URL from a SharePoint URL
 * e.g. https://contoso.sharepoint.com/sites/team -> https://contoso.sharepoint.com
 */
export function getRootSiteUrl(siteUrl: string): string {
  const url = new URL(ensureHttps(siteUrl));
  return `${url.protocol}//${url.host}`;
}

/**
 * Derives the tenant admin URL from a SharePoint site URL
 * e.g. https://contoso.sharepoint.com/sites/team -> https://contoso-admin.sharepoint.com
 */
export function getTenantAdminUrl(siteUrl: string): string {
  const url = new URL(ensureHttps(siteUrl));
  const hostname = url.hostname;

  // Already an admin URL
  if (hostname.includes('-admin.sharepoint.')) {
    return `${url.protocol}//${hostname}`;
  }

  // Replace .sharepoint.com with -admin.sharepoint.com
  const adminHostname = hostname.replace('.sharepoint.', '-admin.sharepoint.');
  return `${url.protocol}//${adminHostname}`;
}

/**
 * URL-encodes a string
 */
export function urlEncode(value: string): string {
  return encodeURIComponent(value);
}
