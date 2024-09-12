namespace TemplateBaseMicroservice.Repository
{
    public interface IDeleteLongRepository
    {
        Task<bool> Delete(long id);
    }
    public interface IDeleteStringRepository
    {
        Task<bool> Delete(string id);
    }
    public interface IInsertRepository<T> where T : class
    {
        Task<long> Insert(T item);
    }
    public interface IUpdateRepository<T> where T : class
    {
        Task<bool> Update(T item);
    }
}
