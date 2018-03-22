using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wonders.Api.Models;

namespace Wonders.Api.Data
{
    public interface IWondersRepository
    {
        Task<IEnumerable<Wonder>> All();
        Task<Wonder> One(string title);
        Task<Wonder> One(int id);
        Task New(Wonder wonder);
    }
}
