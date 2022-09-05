using System;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class ImprintViewModel : MvxViewModel 
    {
        public bool ShowHeaderImage => false;
        public MvxCommand NavigateBackCommand { get; private set; }

        private readonly IMvxNavigationService navigationService;

        public string HeaderTitle { get; set; }

        public string HeaderActionTitle { get; set; }

        public string HeaderActionImage { get; set; }

        public bool ShowHeaderButton { get; set; }

        public bool ShowBack { get; set; }

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

        public ImprintViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.NavigateBackCommand = new MvxCommand(this.NavigateBack);

            this.HeaderTitle = "Impressum";
            this.ShowHeaderButton = false;
            this.ShowBack = true;

            this.Text = 
                "Diese App wurde im Rahmen des Bachelor-Studiums an der IU innerhalb des Moduls Mobile Software Engineering entwickelt. Die App wurde umgesetzt von:" + Environment.NewLine +
                Environment.NewLine +
                "Daniel Biermann &" + Environment.NewLine +
                "Roman Wackernagel" + Environment.NewLine;
        }

        protected void NavigateBack()
        {
            this.navigationService.Close(this, System.Threading.CancellationToken.None);
        }
    }
}
 