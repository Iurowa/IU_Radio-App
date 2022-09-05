using Android.Util;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Views;

namespace Iubh.RadioApp.Droid.Adapters.ViewHolders
{
    public class HeadCellViewHolder : BaseViewHolder
    {
        private TextView title;

        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
        }

        public HeadCellViewHolder(View itemView, IMvxAndroidBindingContext context)
            : base(itemView, context)
        {

            this.title = itemView.FindViewById<TextView>(Resource.Id.Cell_Head_title);

               
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<HeadCellViewHolder, TableHeadViewModel>();
                set.Bind(this.title).To(x => x.Title);
                set.Apply();

            });
        }

        public override void OnViewRecycled()
        {
            base.OnViewRecycled();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.title.Dispose();
            this.title = null;
        }
    }
}