using System.Collections.Generic;
using JottyCRM.Models.Lead;
using JottyCRM.Models.Sell;
using JottyCRM.Services;

namespace JottyCRM.Commands
{
    public class DeleteSellCommand : BaseCommand
    {
        private readonly ISellService _sellService;
        private Sell _sell;

        public DeleteSellCommand(ISellService sellService,
            Sell sell)
        {
            _sellService = sellService;
            _sell = sell;
        }

        public override void Execute(object parameter)
        {
            _sellService.DeleteSell(_sell);
        }
    }
}