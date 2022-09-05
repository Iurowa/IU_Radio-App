using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Iubh.RadioApp.Droid.Views;

namespace Iubh.RadioApp.Droid.Fragments
{
    [MvxFragmentPresentation(activityHostViewModelType: typeof(TabBarViewModel), fragmentContentId: Resource.Id.fragment_container, AddToBackStack = true)]
    public class ImprintFragment : MvxFragment<ImprintViewModel>
    {
        private HeaderView header;
        private TextView text;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_imprint, container, false);

            this.header = view.FindViewById<HeaderView>(Resource.Id.Imprint_Header);
            this.text = view.FindViewById<TextView>(Resource.Id.Imprint_text);

            var set = this.CreateBindingSet<ImprintFragment, ImprintViewModel> ();

            set.Bind(this.header).For(x => x.ActionButtonImage).To(x => x.HeaderActionImage).WithConversion(Converters.Converters.DrawableConverter);
            set.Bind(this.header).For(x => x.HeaderTitle).To(x => x.HeaderTitle);
            set.Bind(this.header.BackBtn).To(x => x.NavigateBackCommand);
            set.Bind(this.header).For(x => x.ShowHeaderButton).To(x => x.ShowHeaderButton).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.header).For(x => x.ShowBack).To(x => x.ShowBack).WithConversion(Converters.Converters.VisibilityConverter);
            set.Bind(this.text).To(x => x.Text);

            set.Apply();

            return view;
        }
    }
}