using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Acr.UserDialogs;
using Iubh.RadioApp.Data;
using MvvmCross.Commands;

namespace Iubh.RadioApp.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string mail;
        public string Mail
        {
            get { return this.mail; }
            set
            {
                this.mail = value;
                this.RaisePropertyChanged(() => this.Mail);
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                this.RaisePropertyChanged(() => this.Password);
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
            }
        }

        public MvxCommand NavigateBackCommand { get; private set; }

        public int AlertStyleId { get; set; }

        public MvxCommand LoginCommand { get; private set; }

        public LoginViewModel() : base("Moderatoren-Login", false, true, null, null, null, false)
        {
            this.LoginCommand = new MvxCommand(this.Login);
            this.NavigateBackCommand = new MvxCommand(this.NavigateBack);
        }

        protected void Login()
        {
            this.IsLoading = true;
            if (string.IsNullOrEmpty(this.Mail) == true || string.IsNullOrEmpty(this.Password) == true)
            {
                UserDialogs.Instance.Alert(new AlertConfig { Message = "Bitte geben Sie die E-Mail-Adresse und das Passwort ein.", Title = "Fehler", OkText = "Ok", AndroidStyleId = this.AlertStyleId });
                this.IsLoading = false;
                return;
            }

            var thread = new Thread(() =>
            {
                if (App.Db.IsModeratorLoginSuccessfully(this.Mail, this.Password) == true)
                {
                    App.DbLocal.AddConfigValue(Config.Static.IsLoginId, "true");
                    this.NavigationService.Navigate<TabBarModeratorViewModel>();
                }
                else
                {
                    UserDialogs.Instance.Alert(new AlertConfig { Message = "E-Mail-Adresse oder Passwort fehlerhaft. Bitte überprüfen Sie Ihre Eingaben.", Title = "Fehler", OkText = "Ok", AndroidStyleId = this.AlertStyleId });
                }
                this.IsLoading = false;
            });
            thread.Start();
        }

        protected void NavigateBack()
        {
            this.NavigationService.Close(this, System.Threading.CancellationToken.None);
        }
    }
}
