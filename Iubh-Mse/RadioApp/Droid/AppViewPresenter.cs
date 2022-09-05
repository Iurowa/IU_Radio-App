using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvvmCross.Droid.Support.V7.AppCompat;
using Fragment = Android.Support.V4.App.Fragment;
using System.Threading.Tasks;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Presenters.Attributes;

namespace Iubh.RadioApp.Droid
{
    public class AppViewPresenter : MvxAppCompatViewPresenter
    {
        public AppViewPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        protected override IMvxFragmentView CreateFragment(MvxBasePresentationAttribute attribute, string fragmentName)
        {
            return base.CreateFragment(attribute, fragmentName);
        }

        protected override Task<bool> ShowFragment(Type view, MvxFragmentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            return base.ShowFragment(view, attribute, request);
        }

        protected override Task<bool> ShowViewPagerFragment(Type view, MvxViewPagerFragmentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            return base.ShowViewPagerFragment(view, attribute, request);
        }

        protected override void ShowNestedFragment(Type view, MvxFragmentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            base.ShowNestedFragment(view, attribute, request);
        }
        protected override Fragment GetFragmentByViewType(Type type)
        {
            var fragment = base.GetFragmentByViewType(type);
            if (fragment == null)
            {
                fragment = this.CurrentFragmentManager.Fragments.SingleOrDefault(x => x.GetType() == type);
            }

            return fragment;
        }
    }
}