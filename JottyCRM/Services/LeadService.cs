using System;
using System.Collections.Generic;
using System.Data.Entity;
using JottyCRM.Models;
using JottyCRM.Models.Lead;
using JottyCRM.repositories;
using Mehdime.Entity;

namespace JottyCRM.Services
{
    public interface ILeadService
    {
        Lead Create(string name, string status, string description, DateTime createdAt, User user);
        List<Lead> GetAll(int userId);
    }
    
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _repository;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        
        public LeadService(IDbContextScopeFactory dbContextScopeFactory, ILeadRepository repository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException(nameof(dbContextScopeFactory));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Lead Create(string name, string status, string description, DateTime createdAt, User user)
        {
            if (name == null)
                return null;

            var lead = new Lead()
            {
                Name = name,
                Status = status,
                Description = description,
                CreatedAt = createdAt,
                UserId = user.Id
            };

            using (var ctx = _dbContextScopeFactory.Create())
            {
                _repository.CreateLead(lead);
                ctx.SaveChanges();
            }

            return lead;
        }

        public List<Lead> GetAll(int userId)
        {
            using (var _ = _dbContextScopeFactory.Create())
            {
                return _repository.GetAll(userId);
            }
        }
    }
}