using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EcommerceProject.Core.Models;
using EcommerceProject.Core.Repositories;

namespace EcommerceProject.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }

        ICategoryRepository Categories { get; }

        IRepository<CurrencyUnit> Currencies{ get;}

        Task CommitAsync();

        void Commit();
    }
}