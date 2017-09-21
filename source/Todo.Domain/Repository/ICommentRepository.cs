using Todo.Entities;

namespace Todo.Domain.Repository
{
    public interface ICommentRepository
    {
        TodoComment Create(TodoComment domainModel);
        void Save();
    }
}