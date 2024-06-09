using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        void DeleteLead(Lead lead);
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
                    throw new InvalidOperationException("Не найден DbContext");

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

        public void DeleteLead(Lead lead)
        {
            DbContext.Entry(lead).State = EntityState.Deleted;
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
                lead = DbContext.Leads.Add(lead);
                return lead;
            }
            catch
            {
                return null;
            }
        }
    }
}