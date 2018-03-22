using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wonders.Api.Models;

namespace Wonders.Api.Data
{
    public class WondersRepository : IWondersRepository
    {
        private IDb _wondersDb;
        public WondersRepository(IDb db)
        {
            _wondersDb = db;
        }

        public async Task<IEnumerable<Wonder>> All()
        {
            return await Task.Run(() => _wondersDb.Wonders.AsEnumerable());
        }

        public async Task New(Wonder wonder)
        {
            var existingWonder = await One(wonder.Title);
            if (existingWonder != null)
            {
                throw new ArgumentException("Already exists");
            }
            existingWonder = await One(wonder.Id);
            if (existingWonder != null)
            {
                throw new ArgumentException("Id already used");
            }
           _wondersDb.Wonders.Add(wonder);
        }

        public async Task<Wonder> One(string title)
        {
            var wonders = await All();
            return wonders.FirstOrDefault(w => string.Compare(w.Title, title, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public async Task<Wonder> One(int id)
        {
            var wonders = await All();
            return wonders.FirstOrDefault(w => w.Id == id);
        }
    }
}
