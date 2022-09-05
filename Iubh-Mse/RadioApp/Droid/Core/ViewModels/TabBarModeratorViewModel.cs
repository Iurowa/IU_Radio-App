using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class TabBarModeratorViewModel: MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        
        public TabBarModeratorViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
