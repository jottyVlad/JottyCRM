using System.Collections.Generic;
using JottyCRM.Models.Lead;
using JottyCRM.Services;

namespace JottyCRM.Commands
{
    public class DeleteLeadCommand : BaseCommand
    {
        private readonly ILeadService _leadService;
        private Lead _lead;

        public DeleteLeadCommand(ILeadService leadService,
            Lead lead)
        {
            _leadService = leadService;
            _lead = lead;
        }

        public override void Execute(object parameter)
        {
            _leadService.DeleteLead(_lead);
        }
    }
}