export enum LogLevel {
  Debug = 0,
  Info = 1,
  Warning = 2,
  Error = 3,
  Silent = 4,
}

let currentLevel: LogLevel = LogLevel.Warning;

export function setLogLevel(level: LogLevel): void {
  currentLevel = level;
}

export function getLogLevel(): LogLevel {
  return currentLevel;
}

function timestamp(): string {
  return new Date().toISOString();
}

export const logger = {
  debug(source: string, message: string, correlationId?: string): void {
    if (currentLevel <= LogLevel.Debug) {
      const prefix = correlationId ? `[${correlationId}] ` : '';
      console.debug(`${timestamp()} DEBUG ${prefix}[${source}] ${message}`);
    }
  },

  info(source: string, message: string, correlationId?: string): void {
    if (currentLevel <= LogLevel.Info) {
      const prefix = correlationId ? `[${correlationId}] ` : '';
      console.info(`${timestamp()} INFO  ${prefix}[${source}] ${message}`);
    }
  },

  warning(source: string, message: string, correlationId?: string): void {
    if (currentLevel <= LogLevel.Warning) {
      const prefix = correlationId ? `[${correlationId}] ` : '';
      console.warn(`${timestamp()} WARN  ${prefix}[${source}] ${message}`);
    }
  },

  error(source: string, message: string, correlationId?: string): void {
    if (currentLevel <= LogLevel.Error) {
      const prefix = correlationId ? `[${correlationId}] ` : '';
      console.error(`${timestamp()} ERROR ${prefix}[${source}] ${message}`);
    }
  },
};
