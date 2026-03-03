export class CsvFormatter {
  static format(data: unknown): string {
    if (data === null || data === undefined) {
      return '';
    }

    const items = Array.isArray(data) ? data : [data];
    if (items.length === 0) {
      return '';
    }

    // Collect all keys
    const allKeys = new Set<string>();
    for (const item of items) {
      if (typeof item === 'object' && item !== null) {
        Object.keys(item).forEach((key) => allKeys.add(key));
      }
    }

    const headers = Array.from(allKeys);
    const rows: string[] = [headers.map(CsvFormatter.escapeCsvField).join(',')];

    for (const item of items) {
      const row = headers.map((key) => {
        const val = (item as Record<string, unknown>)[key];
        if (val === null || val === undefined) return '';
        if (typeof val === 'object') return CsvFormatter.escapeCsvField(JSON.stringify(val));
        return CsvFormatter.escapeCsvField(String(val));
      });
      rows.push(row.join(','));
    }

    return rows.join('\n');
  }

  private static escapeCsvField(field: string): string {
    if (field.includes(',') || field.includes('"') || field.includes('\n')) {
      return `"${field.replace(/"/g, '""')}"`;
    }
    return field;
  }
}
