using System;
using System.Globalization;

using Android.App;
using Android.Graphics.Drawables;
using Android.Support.V4.Content.Res;
using MvvmCross.Converters;

namespace Iubh.RadioApp.Droid.Converters
{
    public class DrawableConverter : MvxValueConverter<string, Drawable>
    {
        protected override Drawable Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var resId = Application.Context.Resources.GetIdentifier(value.ToLower(), "drawable", Application.Context.PackageName);
            return ResourcesCompat.GetDrawable(Application.Context.Resources, resId, null);
        }
    }
}