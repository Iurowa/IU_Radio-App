using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Views.Accessibility;
using Android.Views.Animations;
using Android.Views.Autofill;
using Android.Views.InputMethods;
using Android.Views.TextClassifiers;
using Android.Widget;
using Java.Interop;
using Java.Lang;
using Java.Util;

namespace Iubh.RadioApp.Droid.Views
{
    [Register("Iubh.RadioApp.Droid.views.iubutton")]
    public class IuButton : Button
    {
        protected IuButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        protected IuButton(Context context) : this(context, null) { }
        protected IuButton(Context context, IAttributeSet attrs) : base(context, attrs) { }
    }
}