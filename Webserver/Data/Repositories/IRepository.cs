﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Webserver.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        void Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        void Delete(T entity);

        // Marks a collection of entities specified by predicate to be removed
        void Delete(Expression<Func<T, bool>> where);

        // Get an entity by int id
        T GetById(int id);

        // Get an entity using delegate
        T Get(Expression<Func<T, bool>> where);

        // Gets all entities of type T
        IEnumerable<T> GetAll();

        // Gets entities using delegate
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        // Gets entity entry for modifying state
        DbEntityEntry GetEntry(T entity);

        // Saves added entities to database
        Task Commit();
    }
}