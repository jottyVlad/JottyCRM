using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        void DeleteContractor(Contractor contractor);
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
                    throw new InvalidOperationException("Не найден DbContext");

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

        public void DeleteContractor(Contractor contractor)
        {
            DbContext.Entry(contractor).State = EntityState.Deleted;
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