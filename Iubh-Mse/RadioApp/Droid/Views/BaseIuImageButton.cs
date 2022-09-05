using System;
using System.Collections.Generic;
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
    public abstract class BaseIuImageButton : ImageButton
    {
        protected BaseIuImageButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        protected BaseIuImageButton(Context context) : this(context, null) { }
        protected BaseIuImageButton(Context context, IAttributeSet attrs) : base(context, attrs) { }
    }
}