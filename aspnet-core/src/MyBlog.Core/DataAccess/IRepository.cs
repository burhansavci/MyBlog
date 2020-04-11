using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyBlog.Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
        List<TEntity> GetAllIncludingList(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors);
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAllList();
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity GetIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
