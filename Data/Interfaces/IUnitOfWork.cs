namespace bookstore.Data
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}