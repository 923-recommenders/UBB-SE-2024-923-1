namespace UBB_SE_2024_923_1.Repositories
{
    /// <summary>
    /// Defines a generic repository interface for CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<T> GetByTwoIdentifiers(int id1, int id2);
        Task<T> GetByThreeIdentifiers(int id1, int id2, int id3);
        Task<T> GetByThreeIdentifiers(int id1, int id2, DateTime id3);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
