namespace JottyCRM.ViewModel
{
    public class WithNavbarViewModel : BaseViewModel
    {
        public NavbarViewModel NavbarViewModel { get; }
        public BaseViewModel CurrentViewModel { get; }

        public WithNavbarViewModel(NavbarViewModel navbarViewModel, BaseViewModel currentViewModel)
        {
            NavbarViewModel = navbarViewModel;
            CurrentViewModel = currentViewModel;
        }

        public override void Dispose()
        {
            NavbarViewModel.Dispose();
            CurrentViewModel.Dispose();
        }
    }
}