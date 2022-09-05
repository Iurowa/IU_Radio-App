using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class TabBarViewModel: MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        
        public TabBarViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;

        }
    }
}
