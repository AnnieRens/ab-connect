using System.Collections.Generic;

namespace DatabaseAccess
{
    public class MessagesRepository : IRepository<Message>
    {
        private readonly DbLogContext _dbLogContext;

        public MessagesRepository()
        {
            _dbLogContext = new DbLogContext(); // todo: use DI
        }

        public void Add(Message entity)
        {
            _dbLogContext.Add(entity);
        }

        public void Save()
        {
            _dbLogContext.SaveChanges();
        }

        public IEnumerable<Message> GetAll()
        {
            return _dbLogContext.Messages;
        }
    }
}