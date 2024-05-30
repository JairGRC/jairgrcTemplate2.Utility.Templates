namespace TemplateBaseMicroservice.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<long> Insert(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(long id);
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

    public interface IDeleteRepository<T> where T : class
    {
        Task<bool> Delete(long id);
        Task<bool> Delete(string id);
    }

}

