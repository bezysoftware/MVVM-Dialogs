namespace Bezysoftware.Navigation.Dialogs.View
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The dialog control base.
    /// </summary>
    public class VisualDialogContainer : UserControl, IDialogContainer
    {
        private Grid parentGrid;

        /// <summary>
        /// Closes the window
        /// </summary>
        public virtual void Hide(Type viewType)
        {
            if (this.parentGrid.Children.Contains(this))
            {
                this.parentGrid.Children.Remove(this);
            }
        }

        /// <summary>
        /// Shows the dialog window
        /// </summary>
        public virtual void Show(Type viewType)
        {
            this.parentGrid = (this.GetFrame()?.Content as Page)?.Content as Grid;

            if (parentGrid != null)
            {
                this.parentGrid.Children.Add(this);
            }
        }

        private Frame GetFrame()
        {
            return (Window.Current.Content as Frame) ?? (Window.Current.Content as SplitView)?.Content as Frame;
        }
    }
}
