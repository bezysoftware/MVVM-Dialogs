namespace Bezysoftware.Navigation.Dialogs.Lookup
{
    using System;
    using Bezysoftware.Navigation.Lookup;
    using Bezysoftware.Navigation.Platform;

    /// <summary>
    /// Use this attribute on a page when you want to have and adaptive Dialog, which depending on <see cref="AdaptiveNavigationAttribute"/> 
    /// can either navigate to that page or show a dialog on the current page.
    /// </summary>
    public class AssociatedDialogViewModel : AssociatedViewModelAttribute
    {
        public readonly Type DialogType;

        public AssociatedDialogViewModel(Type viewModel, Type dialogType) : base(viewModel)
        {
            this.DialogType = dialogType;
        }
    }
}
