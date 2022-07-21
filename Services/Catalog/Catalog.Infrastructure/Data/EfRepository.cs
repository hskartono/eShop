using Ardalis.Specification.EntityFrameworkCore;
using Catalog.Core.Interfaces;
using Catalog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
    {
        public EfRepository(CatalogContext context):base(context)
        {

        }
    }
}
