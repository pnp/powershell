import { Command } from 'commander';
import { BaseCommand } from '../../../src/core/base-command';

class TestCommand extends BaseCommand {
  public lastOutput: unknown;
  public executed = false;

  register(program: Command): void {
    program
      .command('test-cmd')
      .description('Test command')
      .option('--name <name>', 'A name')
      .action(this.createAction());
  }

  async execute(options: Record<string, unknown>): Promise<void> {
    this.executed = true;
    this.lastOutput = { name: options.name || 'default' };
    this.writeOutput(this.lastOutput);
  }
}

describe('BaseCommand', () => {
  it('should register a command with commander', () => {
    const program = new Command();
    const cmd = new TestCommand();
    cmd.register(program);

    const registered = program.commands.find((c) => c.name() === 'test-cmd');
    expect(registered).toBeDefined();
    expect(registered?.description()).toBe('Test command');
  });

  it('should detect specified parameters', () => {
    const cmd = new TestCommand();
    expect(cmd['parameterSpecified']({ name: 'hello' }, 'name')).toBe(true);
    expect(cmd['parameterSpecified']({ name: undefined }, 'name')).toBe(false);
    expect(cmd['parameterSpecified']({}, 'name')).toBe(false);
    expect(cmd['parameterSpecified']({ name: null }, 'name')).toBe(false);
  });

  it('should have a correlation ID', () => {
    const cmd = new TestCommand();
    expect(cmd['correlationId']).toBeDefined();
    expect(cmd['correlationId'].length).toBeGreaterThan(0);
  });
});
