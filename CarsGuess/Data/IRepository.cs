namespace CarsGuess.Data
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);


        //Add other CRUD operations here
    }
}
