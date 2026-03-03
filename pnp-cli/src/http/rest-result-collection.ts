/**
 * Represents a paged collection result from a REST API.
 * Equivalent to RestResultCollection<T> in the C# codebase.
 */
export interface RestResultCollection<T> {
  value: T[];
  '@odata.nextLink'?: string;
  '@odata.count'?: number;
}
