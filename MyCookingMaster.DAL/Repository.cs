using Microsoft.EntityFrameworkCore;
using MyCookingMaster.BL.Interfaces;
using MyCookingMaster.BL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyCookingMaster.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public bool Contains(ISpecification<TEntity> specification = null)
        {
            return Count(specification) > 0 ? true : false;
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate) > 0 ? true : false;
        }

        public int Count(ISpecification<TEntity> specification = null)
        {
            return ApplySpecification(specification).Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).Count();
        }

        public IEnumerable<TEntity> Find(ISpecification<TEntity> specification = null)
        {
            return ApplySpecification(specification);
        }

        public TEntity FindById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        }
    }
}
