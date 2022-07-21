﻿using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Interfaces
{
    public interface IReadRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
