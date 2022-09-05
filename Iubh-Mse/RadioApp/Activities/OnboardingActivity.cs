
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using Iubh.RadioApp.Droid.Adapters;
using Iubh.RadioApp.Droid.Views;

namespace Iubh.RadioApp.Droid.Activities
{
    [Activity(Name = "Iubh.RadioApp.Droid.activities.onboardingactivity", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class OnboardingActivity: MvxActivity<OnboardingViewModel>
    {
        private IuViewPager viewPager;
        private IuPageControl pageControl;
        private Button close;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.activity_onboarding);

            this.viewPager = this.FindViewById<IuViewPager>(Resource.Id.Onboarding_viewpager);
            this.pageControl = this.FindViewById<IuPageControl>(Resource.Id.Onboarding_pagecontrol);
            this.close = this.FindViewById<Button>(Resource.Id.Onboarding_close);

            this.viewPager.Adapter = new OnboardingAdapter(this, (IMvxAndroidBindingContext)this.BindingContext);

            var set = this.CreateBindingSet<OnboardingActivity, OnboardingViewModel>();
            set.Bind(this.viewPager).For(x => x.ItemsSource).To(x => x.Pages);
            set.Bind(this.viewPager).To(x => x.SelectedPage);
            set.Bind(this.close).To(x => x.TurnOverCommand);
            set.Apply();

            this.pageControl.ViewPager = this.viewPager;
        }
    }
}