using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TransactionsExample.Domain.Interfaces;

public interface IGenericRepository<EntityType>
    where EntityType : class, IEntity
{
    void Delete(EntityType entityToDelete);
    bool Exists(Guid id);
    EntityType GetById(Guid id);
    EntityType Insert(EntityType entity);
    void InsertMany(List<EntityType> entities);
    IQueryable<EntityType> Query();
    DatabaseFacade Transaction();
    void Update(EntityType entityToUpdate);
    void UpdateMany(List<EntityType> entityToUpdates);
}