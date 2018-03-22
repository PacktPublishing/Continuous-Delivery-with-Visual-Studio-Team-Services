using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wonders.Api.Models;

namespace Wonders.Api.Data
{
    public interface IDb
    {
        List<Wonder> Wonders { get; set; }
    }
}
