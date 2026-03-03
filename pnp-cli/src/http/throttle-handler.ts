import { logger } from '../utils/logger.js';

const DEFAULT_RETRY_AFTER_MS = 5000;
const MAX_RETRIES = 10;

/**
 * Handles HTTP 429 (Too Many Requests) responses with exponential backoff.
 * Equivalent to the throttle handling in ApiRequestHelper.SendMessage() in the C# codebase.
 */
export async function withThrottleRetry(
  sendRequest: () => Promise<Response>,
  maxRetries: number = MAX_RETRIES,
): Promise<Response> {
  let response = await sendRequest();
  let retryCount = 0;

  while (response.status === 429 && retryCount < maxRetries) {
    retryCount++;

    // Parse Retry-After header
    const retryAfterHeader = response.headers.get('Retry-After');
    let retryAfterMs: number;

    if (retryAfterHeader) {
      const seconds = parseInt(retryAfterHeader, 10);
      if (!isNaN(seconds)) {
        retryAfterMs = seconds * 1000;
      } else {
        // Could be an HTTP-date
        const date = new Date(retryAfterHeader);
        retryAfterMs = Math.max(0, date.getTime() - Date.now());
      }
    } else {
      retryAfterMs = DEFAULT_RETRY_AFTER_MS * retryCount;
    }

    logger.debug(
      'ThrottleHandler',
      `Request throttled (429). Retry ${retryCount}/${maxRetries} after ${retryAfterMs}ms.`,
    );

    await new Promise((resolve) => setTimeout(resolve, retryAfterMs));
    response = await sendRequest();
  }

  return response;
}
