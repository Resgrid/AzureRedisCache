using System;
using System.Threading;
using Resgrid.AzureRedisCache.Properties;
using StackExchange.Redis;

namespace Resgrid.AzureRedisCache
{
	public class AzureRedisCacheProvider : IAzureCacheProvider
	{
		#if (RELEASE)
			private const bool EnableCache = true;
#else
		private const bool EnableCache = false;
#endif

		private static ConnectionMultiplexer _connection = null;
		public AzureRedisCacheProvider()
		{
			if (EnableCache)
				EstablishRedisConnection();
		}

		public T Retrieve<T>(string cacheKey, Func<T> fallbackFunction, TimeSpan expiration)
			where T : class
		{
			T data = null;
			IDatabase cache = null;

			try
			{
				if (EnableCache && _connection != null)
				{
					cache = _connection.GetDatabase();

					var cacheValue = cache.StringGet(cacheKey);

					try
					{
						if (cacheValue.HasValue)
							data = ObjectSerialization.Deserialize<T>(cacheValue);
					}
					catch
					{
						// Oh snap the object couldn't be deserilized, so invalidate and just fallback
						Invalidate(cacheKey);
						throw;
					}

					if (data != null)
						return data;
				}
			}
			catch (TimeoutException)
			{
				// I wouldn't recommend logging timeouts, they happen and are just a fact of life
			}
			catch (Exception ex)
			{
				// I would log these :-)
			}

			data = fallbackFunction();

			if (EnableCache && _connection != null)
			{
				if (data != null && cache != null)
				{
					try
					{
						cache.StringSet(cacheKey, ObjectSerialization.Serialize(data), expiration);
					}
					catch (TimeoutException)
					{
						// I wouldn't recommend logging timeouts, they happen and are just a fact of life
					}
					catch (Exception ex)
					{
						// I would log these :-)
					}
				}
			}

			return data;
		}

		public void Invalidate(string cacheKey)
		{
			try
			{
				if (EnableCache && _connection != null)
				{
					IDatabase cache = _connection.GetDatabase();
					cache.KeyDelete(cacheKey);
				}
			}
			catch (Exception ex)
			{
				// Hook in your logger here
			}
		}

		private void EstablishRedisConnection()
		{
			int retry = 0;

			try
			{
				if (_connection != null && _connection.IsConnected == false)
				{
					_connection.Close();
					_connection = null;
				}

			}
			catch { }

			while (_connection == null && retry <= Settings.Default.RedisConnectionRetryCount)
			{
				retry++;

				try
				{
					_connection = ConnectionMultiplexer.Connect(Settings.Default.AzureRedisConnectionString);
				}
				catch (Exception ex)
				{
					_connection = null;
					// Hook in your logger here

					Thread.Sleep(150);
				}
			}
		}
	}
}