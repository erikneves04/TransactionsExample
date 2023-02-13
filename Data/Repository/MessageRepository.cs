using TransactionsExample.Domain.Entities;
using TransactionsExample.Domain.Interfaces;

namespace TransactionsExample.Data.Repository;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository()
        : base(new())
    {
    }
}