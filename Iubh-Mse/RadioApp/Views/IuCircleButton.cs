using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Iubh.RadioApp.Droid.Views
{
    [Register("Iubh.RadioApp.Droid.views.iucirclebutton")]
    public class IuCircleButton : BaseIuImageButton
    {
        public int Radius => this.Height / 2;

        public IuCircleButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public IuCircleButton(Context context) : this(context, null) { }
        public IuCircleButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            base.OnTouchEvent(e);

            var point = new PointF(e.GetX(), e.GetY());
            if (point.X < 0 || point.Y < 0)
            {
                return false;
            }

            var center = new Point(this.Radius, this.Radius);

            float dx = center.X - point.X;
            float dy = center.Y - point.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            return distance <= this.Radius;
        }
    }
}