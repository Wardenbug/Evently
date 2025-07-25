﻿using Microsoft.Extensions.Caching.Distributed;

namespace Evently.Common.Infrastructure.Caching;
public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
    };


    public static DistributedCacheEntryOptions Create(TimeSpan? expxiration) =>
        expxiration is not null ?
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expxiration } :
            DefaultExpiration;
}
