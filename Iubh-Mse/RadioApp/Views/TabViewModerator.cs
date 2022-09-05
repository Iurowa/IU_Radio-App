using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace Iubh.RadioApp.Droid.Views
{
    [Register("Iubh.RadioApp.Droid.views.tabviewmoderator")]
    public class TabViewModerator : RelativeLayout
    {
        public TabViewModerator(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            this.Initialize(context, attrs);
        }

        private void Initialize(Context context, IAttributeSet attrs)
        {
            TabView.Inflate(context, Resource.Menu.tabbarmoderator, this);
        }
    }
}