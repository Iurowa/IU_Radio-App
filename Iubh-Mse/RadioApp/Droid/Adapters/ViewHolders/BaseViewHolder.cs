using Android.Graphics;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Themes;

namespace Iubh.RadioApp.Droid.Adapters.ViewHolders
{
    public abstract class BaseViewHolder : MvxRecyclerViewHolder, IItemTouchHelperViewHolder
    {
        protected BaseViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {
        }

        public virtual void OnItemSelected()
        {
            this.ItemView.Alpha = 0.8f;
            this.ItemView.SetBackgroundColor(new Color(Default.IuWhiteColor));
        }

        public virtual void OnItemClear()
        {
            this.ItemView.Alpha = 1.0f;
            this.ItemView.SetBackgroundColor(new Color(Default.IuWhiteColor));
        }
    }
}