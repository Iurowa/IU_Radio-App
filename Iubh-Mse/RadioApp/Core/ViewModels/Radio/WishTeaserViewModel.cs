using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class WishTeaserViewModel : MvxViewModel
    {
        public MvxCommand NavigateToDetailCommand { get; private set; }

        private readonly IMvxNavigationService navigationService;

        private string key;
        public string Key
        {
            get { return this.key; }
            set
            {
                this.key = value;
                this.RaisePropertyChanged(() => this.Key);
            }
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.RaisePropertyChanged(() => this.Name);
            }
        }

        private string text;
        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                this.RaisePropertyChanged(() => this.Text);
            }
        }

        public WishTeaserViewModel()
        {
            this.navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            this.NavigateToDetailCommand = new MvxCommand(this.NavigateToDetail);
        }

        protected void NavigateToDetail()
        {
            this.navigationService.Navigate(typeof(WishDetailViewModel), this.Key);
        }
    }
}
