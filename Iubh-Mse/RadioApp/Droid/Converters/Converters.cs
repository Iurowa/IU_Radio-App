using System;
namespace Iubh.RadioApp.Droid.Converters
{
    public static class Converters
    {
        public static InvertedVisibilityConverter InvertedVisibilityConverter = new InvertedVisibilityConverter();
        public static VisibilityConverter VisibilityConverter = new VisibilityConverter();
        public static DrawableConverter DrawableConverter = new DrawableConverter();
        public static InvertedBoolConverter InvertedBoolConverter = new InvertedBoolConverter();
    }
}
