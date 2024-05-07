namespace TemplateBaseMicroservice.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<long> Insert(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(long id);
        Task<bool> Delete(string id);
    }
}

