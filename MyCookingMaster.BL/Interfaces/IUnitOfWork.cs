using MyCookingMaster.BL.Models.Common;
using System;

namespace MyCookingMaster.BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        int Complete();
    }
}
