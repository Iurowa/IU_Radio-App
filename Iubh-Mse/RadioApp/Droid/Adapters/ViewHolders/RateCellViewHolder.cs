using Android.Util;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Views;

namespace Iubh.RadioApp.Droid.Adapters.ViewHolders
{
    public class RateCellViewHolder : BaseViewHolder
    {
        private TextView rating;
        private TextView text;

        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
        }

        public RateCellViewHolder(View itemView, IMvxAndroidBindingContext context)
            : base(itemView, context)
        {            
            this.rating = itemView.FindViewById<TextView>(Resource.Id.Cell_Rate_Rating);
            this.text = itemView.FindViewById<TextView>(Resource.Id.Cell_Rate_Text);

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<RateCellViewHolder,RateTeaserViewModel>();
                set.Bind(this.rating).To(x => x.Rate);
                set.Bind(this.text).To(x => x.Text);
                set.Apply();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.rating.Dispose();
            this.rating = null;

            this.text.Dispose();
            this.text = null;
        }
    }
}