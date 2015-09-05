using System;

namespace Resgrid.AzureRedisCache
{
	public interface IAzureCacheProvider
	{
		/// <summary>
		/// Retrieves an object from Azure Redis cache. If the object isn't in cache the Fallback function is called 
		/// and the item will be added to the cache.
		/// </summary>
		/// <typeparam name="T">Type of object your retrieving</typeparam>
		/// <param name="cacheKey">String based cache key</param>
		/// <param name="fallbackFunction">Fallback function to be evaluated if the item isn't in cache</param>
		/// <param name="expiration">When the cache item will expire</param>
		/// <returns>Object from the cache</returns>
		T Retrieve<T>(string cacheKey, Func<T> fallbackFunction, TimeSpan expiration) where T : class;

		/// <summary>
		/// Invalidates (removes) an object in the cache
		/// </summary>
		/// <param name="cacheKey">Cache key to invalidate</param>
		void Invalidate(string cacheKey);
	}
}
