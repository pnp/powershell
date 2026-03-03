export class JsonFormatter {
  static format(data: unknown): string {
    return JSON.stringify(data, null, 2);
  }
}
