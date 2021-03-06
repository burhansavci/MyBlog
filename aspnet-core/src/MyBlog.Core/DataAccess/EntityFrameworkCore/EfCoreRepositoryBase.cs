﻿using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyBlog.Core.DataAccess.EntityFrameworkCore
{
    public abstract class EfCoreRepositoryBase<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContext : DbContext, new()
    {
        protected readonly DbContext _dbContext;
        protected virtual DbSet<TEntity> Table => _dbContext.Set<TEntity>();
        public EfCoreRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual TEntity Insert(TEntity entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public virtual void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity GetIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAllIncluding(propertySelectors).FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetAllIncluding();
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();
            if (propertySelectors.Length != 0)
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return query;
        }

        public IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAllIncluding(propertySelectors).Where(predicate);
        }

        public List<TEntity> GetAllIncludingList(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAllIncluding(propertySelectors).ToList();
        }

        public List<TEntity> GetAllIncludingList(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAllIncluding(propertySelectors).Where(predicate).ToList();
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual TEntity Update(TEntity entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

    }
}
