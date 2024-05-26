using System;
using JottyCRM.Models;
using JottyCRM.Models.Contractor;

namespace JottyCRM.Stores
{
    public class LeadsStore
    {
        public event Action<string, string, string, DateTime> LeadCreated;

        public void CreateLead(string name, string status, string description, DateTime createdAt)
        {
            LeadCreated?.Invoke(name, status, description, createdAt);
        }
    }
}