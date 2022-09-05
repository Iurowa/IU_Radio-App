using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Adapters;
using Iubh.RadioApp.Droid.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Iubh.RadioApp.Droid.Fragments
{
    [MvxFragmentPresentation(activityHostViewModelType: typeof(TabBarModeratorViewModel), fragmentContentId: Resource.Id.moderator_fragment_container, AddToBackStack = true)]
    public class WishDetailFragment : MvxFragment<WishDetailViewModel>
    {
        private HeaderView header;
        private TextView wish;
        private TextView name;
        private Button save;
        private ScrollView scrollView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_wish_detail, container, false);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Wish_Detail_Header);
            this.wish = view.FindViewById<TextView>(Resource.Id.Wish_Detail_MusicWish);
            this.name = view.FindViewById<TextView>(Resource.Id.Wish_Detail_Name);
            this.save = view.FindViewById<Button>(Resource.Id.Wish_Detail_SendButton);
            this.scrollView = view.FindViewById<ScrollView>(Resource.Id.Wish_Detail_ScrollView);

            var set = this.CreateBindingSet<WishDetailFragment, WishDetailViewModel>();
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header.BackBtn).To(x => x.NavigateBackCommand);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);

            set.Bind(this.save).To(x => x.SaveCommand);
            set.Bind(this.name).For(x => x.Text).To(x => x.Name);
            set.Bind(this.wish).For(x => x.Text).To(x => x.MusicWish);

            set.Apply();

            this.ViewModel.AlertStyleId = Resource.Style.IuAlertDialog;
            return view;
        }
    }
}