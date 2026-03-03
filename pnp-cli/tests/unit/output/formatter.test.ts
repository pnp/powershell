import { OutputFormatter } from '../../../src/output/formatter';

describe('OutputFormatter', () => {
  const testData = [
    { id: 1, name: 'Alice', email: 'alice@example.com' },
    { id: 2, name: 'Bob', email: 'bob@example.com' },
  ];

  describe('JSON format', () => {
    it('should format data as pretty JSON', () => {
      const output = OutputFormatter.format(testData, 'json');
      const parsed = JSON.parse(output);
      expect(parsed).toEqual(testData);
    });

    it('should handle null', () => {
      const output = OutputFormatter.format(null, 'json');
      expect(output).toBe('null');
    });
  });

  describe('CSV format', () => {
    it('should format data as CSV', () => {
      const output = OutputFormatter.format(testData, 'csv');
      const lines = output.split('\n');
      expect(lines[0]).toBe('id,name,email');
      expect(lines[1]).toBe('1,Alice,alice@example.com');
      expect(lines[2]).toBe('2,Bob,bob@example.com');
    });

    it('should escape fields with commas', () => {
      const data = [{ name: 'Alice, Bob', value: 'test' }];
      const output = OutputFormatter.format(data, 'csv');
      expect(output).toContain('"Alice, Bob"');
    });
  });

  describe('text format', () => {
    it('should format data as key-value pairs', () => {
      const output = OutputFormatter.format({ id: 1, name: 'Alice' }, 'text');
      expect(output).toContain('id');
      expect(output).toContain('Alice');
    });
  });

  describe('table format', () => {
    it('should format data as a table', () => {
      const output = OutputFormatter.format(testData, 'table');
      expect(output).toContain('id');
      expect(output).toContain('name');
      expect(output).toContain('Alice');
      expect(output).toContain('Bob');
    });
  });
});
