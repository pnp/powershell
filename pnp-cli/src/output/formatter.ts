import { JsonFormatter } from './json-formatter.js';
import { TableFormatter } from './table-formatter.js';
import { CsvFormatter } from './csv-formatter.js';
import { TextFormatter } from './text-formatter.js';

export type OutputFormat = 'json' | 'table' | 'csv' | 'text';

export class OutputFormatter {
  static format(data: unknown, format: OutputFormat = 'json'): string {
    switch (format) {
      case 'json':
        return JsonFormatter.format(data);
      case 'table':
        return TableFormatter.format(data);
      case 'csv':
        return CsvFormatter.format(data);
      case 'text':
        return TextFormatter.format(data);
      default:
        return JsonFormatter.format(data);
    }
  }
}
