namespace Bezysoftware.Navigation.Dialogs.View
{
    using System.Collections.Generic;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;

    public static class VisualTreeExtensions
    {
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject parent) where T : class
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                    var dataContext = ((child as FrameworkElement)?.DataContext as T);
                    if (dataContext != null)
                    {
                        yield return dataContext;
                    }

                    if (child != null && child is T)
                    {
                        yield return child as T;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
