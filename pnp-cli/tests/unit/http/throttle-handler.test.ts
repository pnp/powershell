import { withThrottleRetry } from '../../../src/http/throttle-handler';

describe('withThrottleRetry', () => {
  it('should return immediately on success', async () => {
    const mockResponse = new Response('ok', { status: 200 });
    const sendRequest = jest.fn().mockResolvedValue(mockResponse);

    const result = await withThrottleRetry(sendRequest);
    expect(result.status).toBe(200);
    expect(sendRequest).toHaveBeenCalledTimes(1);
  });

  it('should retry on 429 and succeed', async () => {
    const throttledResponse = new Response('throttled', {
      status: 429,
      headers: { 'Retry-After': '0' },
    });
    const successResponse = new Response('ok', { status: 200 });

    const sendRequest = jest.fn()
      .mockResolvedValueOnce(throttledResponse)
      .mockResolvedValueOnce(successResponse);

    const result = await withThrottleRetry(sendRequest);
    expect(result.status).toBe(200);
    expect(sendRequest).toHaveBeenCalledTimes(2);
  });

  it('should stop retrying after max retries', async () => {
    const throttledResponse = new Response('throttled', {
      status: 429,
      headers: { 'Retry-After': '0' },
    });

    const sendRequest = jest.fn().mockResolvedValue(throttledResponse);

    const result = await withThrottleRetry(sendRequest, 3);
    expect(result.status).toBe(429);
    expect(sendRequest).toHaveBeenCalledTimes(4); // initial + 3 retries
  });
});
