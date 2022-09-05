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
using MvvmCross.Platforms.Android.Views.Fragments;
using Iubh.RadioApp.Droid.Adapters;
using Iubh.RadioApp.Droid.Views;

namespace Iubh.RadioApp.Droid.Fragments
{
    public class WishFragment : BaseFormFragment<WishViewModel>
    {
        private HeaderView header;
        private EditText wish;
        private EditText name;
        private Button save;
        private ScrollView scrollView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_wish, container, false);
            this.header = view.FindViewById<HeaderView>(Resource.Id.Wish_Header);
            this.wish = view.FindViewById<EditText>(Resource.Id.Wish_MusicWish);
            this.name = view.FindViewById<EditText>(Resource.Id.Wish_Name);
            this.save = view.FindViewById<Button>(Resource.Id.Wish_SendButton);
            this.scrollView = view.FindViewById<ScrollView>(Resource.Id.Wish_ScrollView);

            var set = this.CreateBindingSet<WishFragment, WishViewModel>();
            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.save).To(x => x.SaveCommand);
            set.Bind(this.name).For(x => x.Text).To(x => x.Name);
            set.Bind(this.wish).For(x => x.Text).To(x => x.MusicWish);

            set.Apply();

            this.RegisterForHideKeyboard(this.scrollView);
            this.ViewModel.AlertStyleId = Resource.Style.IuAlertDialog;

            this.save.Click += Save_Click;
            return view;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            this.CloseKeboard(this.View);
        }
    }
}