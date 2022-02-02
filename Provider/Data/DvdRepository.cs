using System;
using System.Threading.Tasks;
using Provider.Controllers;

namespace Provider.Data
{
    public class DvdRepository : IDvdRepository
    {
        public Task Create(DvdResponse dvdResponse) => throw new NotImplementedException();

        public Task<DvdResponse> GetById(Guid id) => throw new NotImplementedException();
    }
}