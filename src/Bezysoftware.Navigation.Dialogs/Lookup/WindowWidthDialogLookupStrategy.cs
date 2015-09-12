namespace Bezysoftware.Navigation.Dialogs.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Windows.UI.Xaml;
    using Bezysoftware.Navigation.Dialogs.Interception;

    /// <summary>
    /// Strategy for selecting target View for given ViewModel based on the width of current Window.
    /// </summary>
    public class WindowWidthDialogLookupStrategy : IViewLookupStrategy
    {
        public WindowWidthDialogLookupStrategy()
        {
            this.MinWidthForDialog = 400;
        }

        public bool CanSelectView(Type viewModelType, IEnumerable<Type> viewTypes)
        {
            var dialogs = viewTypes.Where(v => v.GetTypeInfo().GetCustomAttribute<DialogContainerAttribute>() != null).ToList();
            var others = viewTypes.Where(v => v.GetTypeInfo().GetCustomAttribute<DialogContainerAttribute>() == null).ToList();
            var shouldBeDialog = this.ShouldBeDialog();

            // if should be dialog, there must be exactly one view with DialogContainer attribute. 
            // if should NOT be dialog, same thing for others.
            return shouldBeDialog ? dialogs.Count == 1 : others.Count == 1;
        }

        public Type SelectView(Type viewModelType, IEnumerable<Type> viewTypes)
        {
            if (this.ShouldBeDialog())
            {
                return viewTypes.FirstOrDefault(v => v.GetTypeInfo().GetCustomAttribute<DialogContainerAttribute>() != null);
            }
            else
            {
                return viewTypes.FirstOrDefault(v => v.GetTypeInfo().GetCustomAttribute<DialogContainerAttribute>() == null);
            }
        }

        /// <summary>
        /// Minimum Window width to choose dialog over page. Default value is 400.
        /// </summary>
        public double MinWidthForDialog
        {
            get;
            set;
        }

        private bool ShouldBeDialog()
        {
            return Window.Current.Bounds.Width >= this.MinWidthForDialog;
        }
    }
}
