using Android.OS;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using Iubh.RadioApp.Droid.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Iubh.RadioApp.Droid.Fragments
{
    [MvxFragmentPresentation(activityHostViewModelType: typeof(TabBarViewModel), fragmentContentId: Resource.Id.fragment_container, AddToBackStack = true)]

    public class LoginFragment : BaseFormFragment<LoginViewModel>
    {
        private HeaderView header;
        private EditText passwort;
        private EditText mail;
        private Button login;
        private ScrollView scrollView;
        private RelativeLayout loading;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_login, container, false);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Login_Header);
            this.passwort = view.FindViewById<EditText>(Resource.Id.Login_Passwort);
            this.mail = view.FindViewById<EditText>(Resource.Id.Login_Name);
            this.login = view.FindViewById<Button>(Resource.Id.Login_LoginButton);
            this.scrollView = view.FindViewById<ScrollView>(Resource.Id.Login_ScrollView);
            this.loading = view.FindViewById<RelativeLayout>(Resource.Id.Login_Loading);

            var set = this.CreateBindingSet<LoginFragment, LoginViewModel>();
            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header.BackBtn).To(x => x.NavigateBackCommand);

            set.Bind(this.login).To(x => x.LoginCommand);
            set.Bind(this.mail).For(x => x.Text).To(x => x.Mail);
            set.Bind(this.passwort).For(x => x.Text).To(x => x.Password);
            set.Bind(this.loading).For(x => x.Visibility).To(x => x.IsLoading).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.login).For(x => x.Enabled).To(x => x.IsLoading).WithConversion(Converters.Converters.InvertedBoolConverter);

            set.Apply();

            this.RegisterForHideKeyboard(this.scrollView);
            this.ViewModel.AlertStyleId = Resource.Style.IuAlertDialog;
            return view;
        }
    }
}