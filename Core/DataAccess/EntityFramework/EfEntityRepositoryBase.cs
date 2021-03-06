﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity: class, IEntity, new()
        where TContext: DbContext, new()
    {
        public void Add(TEntity entity)
        {   //IDispossable pattern implemantation of C#
            using (TContext context = new TContext()) // bu yapı blok bitince oluşturulan objeyi hafızadan siler
            {
                var addedEntity = context.Entry(entity); //referansı yakalar
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext()) // bu yapı blok bitince oluşturulan objeyi hafızadan siler
            {
                var deletedEntity = context.Entry(entity); //referansı yakalar
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }

        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList(); //select * from products : filtrelenmiş select
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext()) // bu yapı blok bitince oluşturulan objeyi hafızadan siler
            {
                var updatedEntity = context.Entry(entity); //referansı yakalar
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }

        }
    }
}
