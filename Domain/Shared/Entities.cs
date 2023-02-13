namespace TransactionsExample.Domain.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
}

public class Entity : IEntity
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
}