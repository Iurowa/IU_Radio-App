using Android.Graphics.Drawables;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;

namespace Iubh.RadioApp.Droid.Bindings
{
    public class ImageViewImageTargetBinding : MvxConvertingTargetBinding<ImageView, Drawable>
    {
        public ImageViewImageTargetBinding(ImageView target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValueImpl(ImageView target, Drawable value)
        {
            target.SetImageDrawable(value);
        }
    }
}