using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Provider.Controllers;
using Provider.Data;

namespace Provider.Tests.Shared
{
    public class TestDvdRepository: IDvdRepository
    {
        private readonly Dictionary<string, DvdResponse> _dvdsByName = new();
        
        private readonly Dictionary<Guid, DvdResponse> _dvdsById = new();

        public Task Create(DvdResponse dvdResponse)
        {
            if (_dvdsByName.ContainsKey(dvdResponse.Name)) throw new ConflictException("Conflict");
            if (_dvdsById.ContainsKey(dvdResponse.Id)) throw new ConflictException("Conflict");
            
            _dvdsByName.Add(dvdResponse.Name, dvdResponse);
            _dvdsById.Add(dvdResponse.Id, dvdResponse);
            
            return Task.CompletedTask;
        }

        public Task<DvdResponse> GetById(Guid id)
        {
            if (!_dvdsById.ContainsKey(id)) throw new NotFoundException("Not Found");
            return Task.FromResult(_dvdsById[id]);
        }

        public IReadOnlyCollection<DvdResponse> GetCreatedDvds() => _dvdsById.Values;
    }
}