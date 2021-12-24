using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUow
    {
        IGenericRepository GetRepository();
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
