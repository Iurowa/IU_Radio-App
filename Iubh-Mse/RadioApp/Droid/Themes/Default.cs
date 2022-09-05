using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

namespace Iubh.RadioApp.Droid.Themes
{
    public static class Default
    {
        public static int PageControlItemSize => (int)Android.App.Application.Context.Resources.GetDimension(Resource.Dimension.PageControlItemSize);

        private static readonly Lazy<int> iuWhiteColor = new Lazy<int>(() => ContextCompat.GetColor(Android.App.Application.Context, Resource.Color.IuWhiteColor));
        public static int IuWhiteColor => iuWhiteColor.Value;

        private static readonly Lazy<int> daHighlightColor = new Lazy<int>(() => ContextCompat.GetColor(Android.App.Application.Context, Resource.Color.IuBlueColor));
        public static int IuBlueColor => daHighlightColor.Value;

        private static readonly Lazy<int> iuFontColor = new Lazy<int>(() => ContextCompat.GetColor(Android.App.Application.Context, Resource.Color.IuFontColor));
        public static int IuFontColor => iuFontColor.Value;

        private static readonly Lazy<int> iuGreyColor = new Lazy<int>(() => ContextCompat.GetColor(Android.App.Application.Context, Resource.Color.IuGreyColor));
        public static int IuGreyColor => iuGreyColor.Value;

       
    }
}