﻿using System.Linq;
using Domain.Purse;
using Domain.Repository;

namespace Domain.Abstract
{
    public interface IOperationRepository
    {
        IQueryable<RepositoryOperation> Repository(int userID);
        int AddOperation(RepositoryOperation repositoryOperation);
        void ChangeOperation(SingleOperation operation, int userID);
        void RemoveOperation(int id, int userID);

    }
}