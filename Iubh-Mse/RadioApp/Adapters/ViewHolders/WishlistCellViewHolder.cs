using Android.Util;
using Android.Views;
using Android.Widget;
using Iubh.RadioApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Views;

namespace Iubh.RadioApp.Droid.Adapters.ViewHolders
{
    public class WishlistCellViewHolder : BaseViewHolder
    {
        private TextView name;
        private TextView text;

        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
        }

        public WishlistCellViewHolder(View itemView, IMvxAndroidBindingContext context)
            : base(itemView, context)
        {            
            this.name = itemView.FindViewById<TextView>(Resource.Id.Cell_Wishlist_Name);
            this.text = itemView.FindViewById<TextView>(Resource.Id.Cell_Wishlist_Text);

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<WishlistCellViewHolder, WishTeaserViewModel>();
                set.Bind(this.name).To(x => x.Name);
                set.Bind(this.text).To(x => x.Text);
                set.Apply();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.name.Dispose();
            this.name = null;

            this.text.Dispose();
            this.text = null;
        }
    }
}