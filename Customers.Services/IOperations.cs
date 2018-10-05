using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customers.Services
{
    /// <summary>
    /// Contains Common Operations upon a Table of the Relational Model
    /// </summary>
    /// <param name="entity">type of Dto Model</param>
    public interface IOperations<TDto>
    {
        /// <summary>
        ///  Retrieve all Items of the Tale of type T
        /// </summary>
        /// <typeparam name="T">type of Dto Model</typeparam>
        /// <returns>Ienumerable items of T</returns>
        IEnumerable<TDto> GetAll();

        /// <summary>
        /// Retrieve Record according to Key Value
        /// </summary>
        /// <param name="id">key value</param>
        /// <returns>First or Default Value</returns>
        Task<TDto> GetByIdAsync(int id);

        /// <summary>
        ///  Update the Enity
        /// </summary>
        /// <returns>number of modified rows</returns>
        Task<int> UpdateAsync(TDto dto);

        /// <summary>
        ///  Insert a new Enity
        /// </summary>
        /// <param name="entity"type of Dto Model</param>
        /// <returns>number of created rows</returns>
        Task<int> InsertAsync(TDto dto);

        /// <summary>
        ///  Delete Enity
        /// </summary>
        /// <param name="id">key value</param>
        /// <returns>number of deleted rows</returns>
        Task<int> DeleteAsync(int id);
    }
}
