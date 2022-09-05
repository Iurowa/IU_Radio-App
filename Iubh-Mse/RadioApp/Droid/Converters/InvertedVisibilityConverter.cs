using System;
using System.Globalization;
using Android.Views;
using MvvmCross.Converters;

namespace Iubh.RadioApp.Droid.Converters
{
    public class InvertedVisibilityConverter : MvxValueConverter<bool, ViewStates>
    {
        protected override ViewStates Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == true)
            {
                return ViewStates.Gone;
            }
            return ViewStates.Visible;

        }
    }
}
