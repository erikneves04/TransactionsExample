using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TransactionsExample.Data.ContextDb;
using TransactionsExample.Domain.Interfaces;

namespace TransactionsExample.Data.Repository;

public class GenericRepository<EntityType> : IGenericRepository<EntityType>
    where EntityType : class, IEntity
{
    private Context _context;
    private DbSet<EntityType> _dbSet;
    
    public GenericRepository(Context context)
    {
        _context = context;
        _dbSet = _context.Set<EntityType>();
    }
    
    public virtual EntityType GetById(Guid id)
    {
        return _dbSet.Find(id);
    }
    
    public bool Exists(Guid id)
    {
        return Query().Any(e => e.Id == id);
    }
    
    public EntityType Insert(EntityType entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public void InsertMany(List<EntityType> entities)
    {
        if (!entities.Any())
            return;

        _dbSet.AddRange(entities);
        _context.SaveChanges();
    }

    public void Update(EntityType entityToUpdate)
    {
        _context.Entry(entityToUpdate).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void UpdateMany(List<EntityType> entityToUpdates)
    {
        if (!entityToUpdates.Any())
            return;

        _context.UpdateRange(entityToUpdates);
        _context.SaveChanges();
    }

    public void Delete(EntityType entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
            _dbSet.Attach(entityToDelete);
        
        _dbSet.Remove(entityToDelete);
        _context.SaveChanges();
    }
    
    public IQueryable<EntityType> Query()
    {
        return _dbSet.AsNoTracking().AsQueryable();
    }
    
    public DatabaseFacade Transaction()
    {
        return _context.Database;
    }
}