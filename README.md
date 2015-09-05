Resgrid Azure Redis Cache with Protobuf
===========================

This is the code that Resgrid uses to interface with Azure's Redis Cache (but I'll work with any Redis Cache Server) utilizing Protobuf. Unlike a number of simple samples out there for interacting with Azure's Redis cache this code also has support for fallback function making it painless to call the cache and manage adding new objects.

The caching pattern and code is used by Resgrid (http://resgrid.com) running on Microsoft's Azure and runs tens of tousands of cache requests a day.

*********

About Resgrid
-------------
Resgrid is a software as a service (SaaS) logistics, management and communications platform for first responders, volunteer fire departments, career fire, EMS, public safety, HAZMAT, CERT, disaster response, etc.  

*********

Solution
--------
 - Resgrid.AzureRedisCache
   - Logic for interacting with Azure's Redis Cache
   - Protobuf Serialization Logic
 - Resgrid.CacheExample
   - Example of how to interact with the cache

*******

Notes
-------------
In the AzureRedisCacheProvider class we have a preprocessor directive to turn Redis Caching OFF for non-release builds. This allows for devs (in Debug builds) to test locally without hitting the Redis server while Release builds can connect as normal.

Shawn Jackson (Twitter: @DesignLimbo Blog: http:\\designlimbo.com)
Jason Jarrett (Twitter: @staxmanade Blog: http:\\staxmanade.com)

*******

License
-------
Apache 2.0