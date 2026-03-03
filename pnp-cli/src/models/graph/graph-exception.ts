export interface GraphError {
  code?: string;
  message?: string;
  innerError?: {
    'request-id'?: string;
    date?: string;
    code?: string;
  };
}

export class GraphException extends Error {
  error?: GraphError;
  accessToken?: string;
  httpStatusCode?: number;
  httpResponse?: Response;

  constructor(error?: GraphError, statusCode?: number) {
    super(error?.message || 'Microsoft Graph API error');
    this.name = 'GraphException';
    this.error = error;
    this.httpStatusCode = statusCode;
  }

  static fromResponse(body: string, statusCode: number, response?: Response): GraphException {
    try {
      const parsed = JSON.parse(body);
      const error = parsed.error as GraphError | undefined;
      const exception = new GraphException(error, statusCode);
      exception.httpResponse = response;
      return exception;
    } catch {
      const exception = new GraphException({ message: body }, statusCode);
      exception.httpResponse = response;
      return exception;
    }
  }
}
