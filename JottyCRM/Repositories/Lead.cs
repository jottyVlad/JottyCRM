using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JottyCRM.DatabaseContext.ContractorContext;
using JottyCRM.DatabaseContext.LeadContext;
using JottyCRM.Models.Lead;
using Mehdime.Entity;

namespace JottyCRM.repositories
{
    public interface ILeadRepository
    {
        Lead CreateLead(Lead lead);
        List<Lead> GetLeadsByName(string leadName, int userId);
        List<Lead> GetAll(int userId);
        int GetCountOfLeadsOnDate(DateTime dateTime, int userId);
    }
    
    public class LeadRepository : ILeadRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private LeadManagementDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<LeadManagementDbContext>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type ContractorManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return dbContext;
            }
        }

        public LeadRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if(ambientDbContextLocator == null) 
                throw new ArgumentNullException(nameof(ambientDbContextLocator));
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public List<Lead> GetAll(int userId)
        {
            return DbContext.Leads.Where(s => s.UserId == userId).ToList();
        }

        public int GetCountOfLeadsOnDate(DateTime dateTime, int userId)
        {
            int count = DbContext.Leads.Count(s => DbFunctions.TruncateTime(s.CreatedAt) == dateTime.Date && s.UserId == userId);
            return count;
        }

        public List<Lead> GetLeadsByName(string leadName, int userId)
        {
            var leads = DbContext.Leads.Where(s => s.Name.Contains(leadName) && s.UserId == userId).ToList();
            return leads;
        }

        public Lead CreateLead(Lead lead)
        {
            try
            {
                return DbContext.Leads.Add(lead);
            }
            catch
            {
                return null;
            }
        }
    }
}