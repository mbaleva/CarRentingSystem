using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace CarRentingSystem.Common.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class CacheActionAttribute : Attribute, IActionFilter
{
    private readonly IMemoryCache _cache;

    public CacheActionAttribute(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (_cache.TryGetValue(context.ActionDescriptor.DisplayName, out IActionResult cachedResult))
        {
            context.Result = cachedResult;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.Canceled && context.Result is IActionResult result)
        {
            _cache.Set(context.ActionDescriptor.DisplayName, result, TimeSpan.FromMinutes(10));
        }
    }
}
