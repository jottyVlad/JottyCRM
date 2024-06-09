using System.Collections.Generic;
using JottyCRM.Models.Contractor;
using JottyCRM.Services;

namespace JottyCRM.Commands
{
    public class DeleteContractorCommand : BaseCommand
    {
        private readonly IContractorService _contractorService;
        private Contractor _contractor;

        public DeleteContractorCommand(IContractorService contractorService,
            Contractor contractor)
        {
            _contractorService = contractorService;
            _contractor = contractor;
        }

        public override void Execute(object parameter)
        {
            _contractorService.DeleteContractor(_contractor);
        }
    }
}