import Table from 'cli-table3';

export class TableFormatter {
  static format(data: unknown): string {
    if (data === null || data === undefined) {
      return '';
    }

    const items = Array.isArray(data) ? data : [data];
    if (items.length === 0) {
      return 'No results found.';
    }

    // Collect all keys from all items
    const allKeys = new Set<string>();
    for (const item of items) {
      if (typeof item === 'object' && item !== null) {
        Object.keys(item).forEach((key) => allKeys.add(key));
      }
    }

    if (allKeys.size === 0) {
      return String(data);
    }

    const headers = Array.from(allKeys);
    const table = new Table({
      head: headers,
      wordWrap: true,
    });

    for (const item of items) {
      const row: string[] = headers.map((key) => {
        const val = (item as Record<string, unknown>)[key];
        if (val === null || val === undefined) return '';
        if (typeof val === 'object') return JSON.stringify(val);
        return String(val);
      });
      table.push(row);
    }

    return table.toString();
  }
}
