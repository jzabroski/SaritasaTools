﻿// Copyright (c) 2015-2017, Saritasa. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Saritasa.Tools.Domain;

namespace Saritasa.Tools.EF
{
    /// <summary>
    /// Entity Framework repository implementation.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TContext">Database context type.</typeparam>
    public class EFRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class where TContext : DbContext
    {
        /// <summary>
        /// Database context.
        /// </summary>
        protected TContext Context { get; }

        /// <summary>
        /// Entity set.
        /// </summary>
        public DbSet<TEntity> Set { get; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="context">Database context.</param>
        public EFRepository(TContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Context = context;
            Set = Context.Set<TEntity>();
        }

        /// <inheritdoc />
        public virtual void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        /// <inheritdoc />
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Set.AddRange(entities);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Find<TProperty>(Expression<Func<TEntity, bool>> predicate,
            IEnumerable<Expression<Func<TEntity, TProperty>>> includes)
        {
            return Set.Where(predicate).Include(includes);
        }

        /// <inheritdoc />
        public virtual TEntity Get(params object[] keyValues)
        {
            return Set.Find(keyValues);
        }

        /// <inheritdoc />
        public virtual TEntity Get<TProperty>(IEnumerable<Expression<Func<TEntity, TProperty>>> includes, params object[] keyValues)
        {
            return Set.Find(keyValues);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Set.ToList();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> GetAll<TProperty>(IEnumerable<Expression<Func<TEntity, TProperty>>> includes)
        {
            return Set.Include(includes).ToList();
        }

        /// <inheritdoc />
        public virtual void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }

        /// <inheritdoc />
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Set.RemoveRange(entities);
        }
    }
}
