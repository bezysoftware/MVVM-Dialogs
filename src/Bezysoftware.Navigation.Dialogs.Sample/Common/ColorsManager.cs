namespace Svatky.Common
{
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;
    using Svatky.Views.Extensions;

    /// <summary>
    /// Helper class for getting some common colors and brushes.
    /// </summary>
    public class ColorsManager
    {
        private static readonly LinearGradientBrush LinearBrush;

        static ColorsManager()
        {
            LinearBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(2, 2),
                MappingMode = BrushMappingMode.Absolute,
                SpreadMethod = GradientSpreadMethod.Repeat
            };

            LinearBrush.GradientStops.Add(new GradientStop {Offset = 0});
            LinearBrush.GradientStops.Add(new GradientStop {Offset = 0.5});
            LinearBrush.GradientStops.Add(new GradientStop {Offset = 0.5, Color = Colors.Transparent});
            LinearBrush.GradientStops.Add(new GradientStop {Offset = 1, Color = Colors.Transparent});
        }

        public static Color GetSystemColor()
        {
            return (Color) Application.Current.Resources["SystemListAccentLowColor"];
        }


        public static Brush GetSystemBrush()
        {
            return new SolidColorBrush(GetSystemColor());
        }

        public static Brush GetSystemInverseBrush()
        {
            return new SolidColorBrush(GetSystemColor().GetContrast(true));
        }

        public static Brush GetSystemInverseLinearBrush()
        {
            var color = GetSystemColor().GetContrast(true);
            LinearBrush.GradientStops[0].Color = color;
            LinearBrush.GradientStops[1].Color = color;
            return LinearBrush;
        }
    }
}
