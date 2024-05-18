namespace JottyCRM.ViewModel
{
    public class WithNavbarViewModel : BaseViewModel
    {
        public NavbarViewModel NavbarViewModel_ { get; }
        public BaseViewModel CurrentViewModel { get; }

        public WithNavbarViewModel(NavbarViewModel navbarViewModel, BaseViewModel currentViewModel)
        {
            NavbarViewModel_ = navbarViewModel;
            CurrentViewModel = currentViewModel;
        }

        public override void Dispose()
        {
            NavbarViewModel_.Dispose();
            CurrentViewModel.Dispose();
            
            base.Dispose();
        }
    }
}