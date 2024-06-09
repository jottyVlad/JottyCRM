namespace JottyCRM.Commands
{
    public class LogoutCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}