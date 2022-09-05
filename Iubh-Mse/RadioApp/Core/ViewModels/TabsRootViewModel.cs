using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class TabsRootViewModel :BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public TabsRootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        
    }
}
