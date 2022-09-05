using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class BaseViewModel: MvxViewModel
    {
        public readonly IMvxNavigationService NavigationService;

        public bool ShowHeaderImage { get; set; }

        public string HeaderTitle { get; set; }

        public string HeaderActionTitle { get; set; }

        public string HeaderActionImage { get; set; }

        public bool ShowHeaderButton { get; set; }

        public bool ShowBack { get; set; }


        public string ProfileImage { get; set; }

        public BaseViewModel(string headerTitle, bool showHeaderButton, bool showBack, string headerActionImage = null, string headerActionTitle = null, string profileImage = null, bool showHeaderImage = true)
        {
            this.NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();

            this.ShowHeaderImage = showHeaderImage;
            this.HeaderTitle = headerTitle;
            this.HeaderActionTitle = headerActionTitle;
            this.HeaderActionImage = headerActionImage;
            this.ShowHeaderButton = showHeaderButton;
            this.ShowBack = showBack;
            this.ProfileImage = profileImage;
        }

        public BaseViewModel()
        {

        }

    }
}
