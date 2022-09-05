using Android.Content;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Iubh.RadioApp.Droid.Adapters.ViewHolders;

namespace Iubh.RadioApp.Droid.Adapters
{
    public class OnboardingAdapter : BasePagerAdapter
    {
        public OnboardingAdapter(Context context, IMvxAndroidBindingContext bindingContext) : base(context,
           bindingContext)
        {
        }

        protected override BaseViewPagerViewHolder CreateBindableViewHolder(int templateId)
        {
            return new OnboardingCellViewHolder(this.Context, this.BindingContext.LayoutInflaterHolder, templateId);
        }
    }
}