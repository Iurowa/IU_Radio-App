using System.Collections.ObjectModel;
using MvvmCross.Commands;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class ServiceViewModel : BaseViewModel
    {
        public ObservableCollection<IServiceTableViewModel> Items { get; private set; }
        public MvxCommand<IServiceTableViewModel> NavigateDetailCommand { get; private set; }

        public ServiceViewModel() : base("Service", false, false, null, null, null, false)
        {
            var ratingUrl = "market://details?id=Iubh.RadioApp.Droid";
            this.NavigateDetailCommand = new MvxCommand<IServiceTableViewModel>(this.NavigateToDetail);

            this.Items = new ObservableCollection<IServiceTableViewModel> {
                new TableHeadViewModel { Title = "SERVICES"},
                new ServiceTableViewModel { Type = Options.ServiceOption.View, Title = "Moderator-Login", Model = typeof(LoginViewModel) },
           
                new TableHeadViewModel { Title = "APP"},
                new ServiceTableViewModel { Type = Options.ServiceOption.Share, Title = "App teilen", Message = "Ich benutze die Radio-App, schaue sie dir mal an."},
                new ServiceTableViewModel { Type = Options.ServiceOption.External, Title = "App bewerten", Url = ratingUrl},
                new ServiceTableViewModel { Type = Options.ServiceOption.Mail, Title = "Support kontaktieren", MailAddress = "roman.wackernagel@iubh-fernstudium.de"},
                new ServiceTableViewModel { Type = Options.ServiceOption.Onboarding, Title = "Onboarding" },

                new TableHeadViewModel { Title = "RECHTLICHES"},
                new ServiceTableViewModel { Type = Options.ServiceOption.View, Title = "Impressum", Model = typeof(ImprintViewModel) },
            };
        }

        private void NavigateToDetail(IServiceTableViewModel item)
        {
            if (item is ServiceTableViewModel)
            {
                (item as ServiceTableViewModel).NavigateToDetailCommand.Execute();
            }
        }
    }
}
