Resgrid Azure Redis Cache Interface with Protobuf
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
When we were looking around for some good example of wrapping Entity Framework inside a repository pattern there were tons of one off examples or examples buried in extremely large projects. Our goal here to show a clean and simple of a generic entity framework repository. Inside the Resgrid.Repository project you can open up the RepositoryBase file to see the meat and potatoes of the generic repo.

Shawn Jackson (@DesignLimbo\designlimbo.com)
Jason Jarrett (@staxmanade\staxmanade.com)

*******

License
-------
Apache 2.0