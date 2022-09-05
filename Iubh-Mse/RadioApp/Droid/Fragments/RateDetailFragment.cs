using Android.OS;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using Iubh.RadioApp.Droid.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Iubh.RadioApp.Droid.Fragments
{
    [MvxFragmentPresentation(activityHostViewModelType: typeof(TabBarModeratorViewModel), fragmentContentId: Resource.Id.moderator_fragment_container, AddToBackStack = true)]
    public class RateDetailFragment : MvxFragment<RateDetailViewModel>
    {
        private HeaderView header;
        private TextView text;
        private ScrollView scrollView;
        private ImageView rateOne;
        private ImageView rateTwo;
        private ImageView rateThree;
        private ImageView rateFour;
        private ImageView rateFive;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_rate_detail, container, false);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Rate_Detail_Header);
            this.text = view.FindViewById<TextView>(Resource.Id.Rate_Detail_Text);
            this.rateOne = view.FindViewById<ImageView>(Resource.Id.Rate_Detail_One);
            this.rateTwo = view.FindViewById<ImageView>(Resource.Id.Rate_Detail_Two);
            this.rateThree = view.FindViewById<ImageView>(Resource.Id.Rate_Detail_Three);
            this.rateFour = view.FindViewById<ImageView>(Resource.Id.Rate_Detail_Four);
            this.rateFive = view.FindViewById<ImageView>(Resource.Id.Rate_Detail_Five);

            this.scrollView = view.FindViewById<ScrollView>(Resource.Id.Rate_Detail_ScrollView);

            var set = this.CreateBindingSet<RateDetailFragment, RateDetailViewModel>();
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header.BackBtn).To(x => x.NavigateBackCommand);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.text).For(x => x.Text).To(x => x.Text);

            set.Apply();

            switch (this.ViewModel.Rate)
            {
                case 1:
                    {
                        this.rateOne.Background = this.Context.GetDrawable(Resource.Drawable.smiley_1_selected);
                        break;
                    }
                case 2:
                    {
                        this.rateTwo.Background = this.Context.GetDrawable(Resource.Drawable.smiley_2_selected);
                        break;
                    }
                case 3:
                    {
                        this.rateThree.Background = this.Context.GetDrawable(Resource.Drawable.smiley_3_selected);
                        break;
                    }
                case 4:
                    {
                        this.rateFour.Background = this.Context.GetDrawable(Resource.Drawable.smiley_4_selected);
                        break;
                    }
                case 5:
                    {
                        this.rateFive.Background = this.Context.GetDrawable(Resource.Drawable.smiley_5_selected);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
           
            this.ViewModel.AlertStyleId = Resource.Style.IuAlertDialog;
            return view;
        }

    }
}