using System;
using System.Threading.Tasks;
using Provider.Controllers;

namespace Provider.Data
{
    public interface IDvdRepository
    {
        Task Create(DvdResponse dvdResponse);

        Task<DvdResponse> GetById(Guid id);
    }
}