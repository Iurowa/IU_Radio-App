using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.Views;

namespace Iubh.RadioApp.Droid.Adapters.ViewHolders
{
    public abstract class BaseViewPagerViewHolder : Java.Lang.Object, IMvxBindingContextOwner
    {
        private readonly IMvxAndroidBindingContext bindingContext;

        protected BaseViewPagerViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,
            transfer)
        {
        }

        protected BaseViewPagerViewHolder(Context context, IMvxLayoutInflaterHolder layoutInflaterHolder, int templateId)
        {
            this.bindingContext = new MvxAndroidBindingContext(context, layoutInflaterHolder);
            this.View = this.bindingContext.BindingInflate(templateId, null);
        }

        public IMvxBindingContext BindingContext
        {
            get => this.bindingContext;
            set => throw new NotImplementedException("BindingContext is readonly in the list item");
        }

        public object DataContext
        {
            get => this.bindingContext.DataContext;
            set => this.bindingContext.DataContext = value;
        }

        public View View { get; }
    }
}