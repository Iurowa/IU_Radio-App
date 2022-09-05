using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.Views;

namespace Iubh.RadioApp.Droid.Adapters.ViewHolders
{
    public class OnboardingCellViewHolder : BaseViewPagerViewHolder
    {
        public OnboardingCellViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,
            transfer)
        {
        }

        public OnboardingCellViewHolder(Context context, IMvxLayoutInflaterHolder layoutInflaterHolder, int templateId)
            : base(context, layoutInflaterHolder, templateId)
        {
            var image = this.View.FindViewById<ImageView>(Resource.Id.Cell_Onboarding_image);
            var headline = this.View.FindViewById<TextView>(Resource.Id.Cell_Onboarding_headline);
            var text = this.View.FindViewById<TextView>(Resource.Id.Cell_Onboarding_text);
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<OnboardingCellViewHolder, PageViewModel>();

                set.Bind(headline).To(x => x.Headline);
                set.Bind(text).To(x => x.Text);
                set.Bind(image).For(x => x.Drawable).To(x => x.Image).WithConversion(Converters.Converters.DrawableConverter);
                set.Apply();
            });
        }
    }
}