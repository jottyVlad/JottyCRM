using System;
using System.Collections.Generic;
using System.Linq;
using JottyCRM.DatabaseContext.ContractorContext;
using JottyCRM.DatabaseContext.UserContext;
using JottyCRM.Models;
using JottyCRM.Models.Contractor;
using Mehdime.Entity;

namespace JottyCRM.repositories
{
    public interface IContractorRepository
    {
        Contractor CreateContractor(Contractor contractor);
        List<Contractor> GetContractorsByName(string name, int userId);
        List<Contractor> GetAll(int userId);
        List<UserPropertyContractor> GetUserProperties(int userId);
    }
    
    public class ContractorRepository : IContractorRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private ContractorManagementDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<ContractorManagementDbContext>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type ContractorManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return dbContext;
            }
        }

        public ContractorRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if(ambientDbContextLocator == null) 
                throw new ArgumentNullException(nameof(ambientDbContextLocator));
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public List<Contractor> GetAll(int userId)
        {
            return DbContext.Contractors.Where(s => s.UserId == userId).ToList();
        }

        public List<UserPropertyContractor> GetUserProperties(int userId)
        {
            return DbContext.UserProperties.Where(s => s.UserTableId == userId).ToList();
        }

        public Contractor CreateContractor(Contractor contractor)
        {
            try
            {
                contractor = DbContext.Contractors.Add(contractor);
                return contractor;
            }
            catch
            {
                return null;
            }
        }

        public List<Contractor> GetContractorsByName(string name, int userId)
        {
            var contractors = DbContext.Contractors.Where(s => s.FullName.Contains(name) && s.UserId == userId).ToList();
            return contractors;
        }
    }
}