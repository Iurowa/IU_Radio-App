using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace Iubh.RadioApp.Droid.Views
{
    [Register("Iubh.RadioApp.Droid.views.BounceScrollView")]
    public class BounceScrollView : ScrollView
    {
        private int overscrollDistance = 20;
        private Context mContext;
        private int mMaxYOverscrollDistance;


        public BounceScrollView(Context context): base(context)
        {
            mContext = context;
            initBounceScrollView();
        }

        public BounceScrollView(Context context, IAttributeSet attrs): base(context, attrs)
        {
            mContext = context;
            initBounceScrollView();
        }

        public BounceScrollView(Context context, IAttributeSet attrs, int defStyle):base(context, attrs, defStyle)
        {
            mContext = context;
            initBounceScrollView();
        }

        private void initBounceScrollView()
        {
            //get the density of the screen and do some maths with it on the max overscroll distance
            //variable so that you get similar behaviors no matter what the screen size

            var metrics = mContext.Resources.DisplayMetrics; 
            var  density = metrics.Density;

            this.mMaxYOverscrollDistance = (int)(density * this.overscrollDistance);
        }

        protected override bool OverScrollBy(int deltaX, int deltaY, int scrollX, int scrollY, int scrollRangeX, int scrollRangeY, int maxOverScrollX, int maxOverScrollY, bool isTouchEvent)
        {
            return base.OverScrollBy(deltaX, deltaY, scrollX, scrollY, scrollRangeX, scrollRangeY, maxOverScrollX, this.mMaxYOverscrollDistance, isTouchEvent);
        }
    }
}