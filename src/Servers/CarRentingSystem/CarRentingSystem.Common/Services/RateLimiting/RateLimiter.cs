using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CarRentingSystem.Common.Services.RateLimiting;

public class RateLimiter : IRateLimiter
{
    private readonly int _maxRequests;
    private readonly TimeSpan _perTimeInterval;

    private readonly ConcurrentQueue<DateTime> _requestQueue = new ConcurrentQueue<DateTime>();
    private static readonly object _lockObject = new object();

    public RateLimiter(int maxRequests, TimeSpan perTimeInterval)
    {
        _maxRequests = maxRequests;
        _perTimeInterval = perTimeInterval;

        Task.Run(() => CleanupExpiredRequests());
    }

    public bool AllowRequest()
    {
        lock (_lockObject)
        {
            if (_requestQueue.Count < _maxRequests)
            {
                _requestQueue.Enqueue(DateTime.Now);
                return true;
            }

            return false;
        }
    }

    private void CleanupExpiredRequests()
    {
        while (true)
        {
            lock (_lockObject)
            {
                while (_requestQueue.Count > 0 && _requestQueue.TryPeek(out var earliestRequest) && DateTime.Now - earliestRequest > _perTimeInterval)
                {
                    _requestQueue.TryDequeue(out _);
                }
            }

            Thread.Sleep(1000);
        }
    }
}