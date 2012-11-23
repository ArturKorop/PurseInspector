using System.Linq;
using Domain.Purse;
using Domain.Repository;

namespace Domain.Abstract
{
    /// <summary>
    /// Interface for connection to operation database
    /// </summary>
    public interface IOperationRepository
    {
        /// <summary>
        /// Class that we give to our View
        /// </summary>
        /// <param name="userID">Unique identifier of User</param>
        /// <Returns></Returns>
        PreviewModel Model(int userID);
        /// <summary>
        /// Add a new finance operation to Database
        /// </summary>
        /// <param name="repositoryOperation">Object that provides a description of the new operation</param>
        /// <Returns>ID of new operation</Returns>
        int AddOperation(RepositoryOperation repositoryOperation);
        /// <summary>
        /// Change an existing operation
        /// </summary>
        /// <param name="operation">Object that provides a description of the new parameters of changed operation</param>
        /// <param name="userID">Unique identifier of User</param>
        void ChangeOperation(SingleOperation operation, int userID);
        /// <summary>
        /// Delete an existing operation
        /// </summary>
        /// <param name="id">ID of operation, thatwe delete</param>
        /// <param name="userID">Unique identifier of User</param>
        void RemoveOperation(int id, int userID);
    }
}