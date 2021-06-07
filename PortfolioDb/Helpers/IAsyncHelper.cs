using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Helpers
{
    public interface IAsyncHelper<T>
    {
        Task<T> CreateAsync(T objectToCreate);
    }
}
