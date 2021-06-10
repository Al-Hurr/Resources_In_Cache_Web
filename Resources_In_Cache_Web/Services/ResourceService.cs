using Microsoft.Extensions.Caching.Memory;
using Resources_In_Cache_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources_In_Cache_Web.Services
{
    public class ResourceService
    {
        private readonly IMemoryCache _cache;
        private const string _resourcesKey = "resourcesKey";

        public ResourceService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Create(string name)
        {
            var resource = new Resource
            {
                Id = Guid.NewGuid(),
                Title = name
            };
            Add(resource);
        }

        public void Update(Guid id, string title)
        {
            var resource = new Resource
            {
                Id = id,
                Title = title
            };
            Add(resource, true);
        }

        public Resource Get(Guid id)
        {
            Resource resource = null;
            _cache.TryGetValue(id, out resource);
            return resource;
        }

        public void Remove(Guid id)
        {
            _cache.Remove(id);

            List<Guid> keys;
            _cache.TryGetValue(_resourcesKey, out keys);
            if(keys.Count == 1)
            {
                _cache.Remove(_resourcesKey);
            }
            else
            {
                keys.Remove(id);
                _cache.Set(_resourcesKey, keys, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
                });
            }
        }

        private void Add(Resource resource, bool isUpdate = false)
        {
            _cache.Set(resource.Id, resource, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });

            if (!isUpdate)
            {
                List<Guid> keys;
                _cache.TryGetValue(_resourcesKey, out keys);

                if (keys == null)
                {
                    keys = new List<Guid>() { resource.Id };
                }
                else
                {
                    keys.Add(resource.Id);
                }
                _cache.Set(_resourcesKey, keys, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
                });
            }
        }

        public IEnumerable<Resource> GetAll()
        {
            Resource resource;
            var resourcesList = new List<Resource>();
            List<Guid> keys = new List<Guid>();
            _cache.TryGetValue(_resourcesKey, out keys);
            if (keys != null)
            {
                foreach (var key in keys)
                {
                    _cache.TryGetValue(key, out resource);
                    resourcesList.Add(resource);
                }
            }
            return resourcesList;
        }
    }
}
