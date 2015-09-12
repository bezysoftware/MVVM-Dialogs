namespace Bezysoftware.Navigation.Dialogs.Lookup
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Strategy which can decide which view should be used for given ViewModel.
    /// </summary>
    public interface IViewLookupStrategy
    {
        /// <summary>
        /// Returns whether this strategy is able to pick a view from the list, under current conditions.
        /// </summary>
        bool CanSelectView(Type viewModelType, IEnumerable<Type> viewTypes);

        /// <summary>
        /// Select a view which should be used at this time for the given ViewModel.
        /// </summary>
        Type SelectView(Type viewModelType, IEnumerable<Type> viewTypes);
    }
}
