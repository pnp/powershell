export class TextFormatter {
  static format(data: unknown): string {
    if (data === null || data === undefined) {
      return '';
    }

    if (typeof data === 'string') {
      return data;
    }

    const items = Array.isArray(data) ? data : [data];
    const lines: string[] = [];

    for (const item of items) {
      if (typeof item === 'object' && item !== null) {
        const entries = Object.entries(item as Record<string, unknown>);
        const maxKeyLen = Math.max(...entries.map(([k]) => k.length));
        for (const [key, value] of entries) {
          const displayValue = value === null || value === undefined ? '' : typeof value === 'object' ? JSON.stringify(value) : String(value);
          lines.push(`${key.padEnd(maxKeyLen)} : ${displayValue}`);
        }
        lines.push('');
      } else {
        lines.push(String(item));
      }
    }

    return lines.join('\n').trimEnd();
  }
}
