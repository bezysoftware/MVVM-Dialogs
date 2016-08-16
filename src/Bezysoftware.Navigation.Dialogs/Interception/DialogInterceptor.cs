namespace Bezysoftware.Navigation.Dialogs.Interception
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using Windows.UI.Xaml.Controls;
    using Bezysoftware.Navigation.Dialogs.Lookup;
    using Bezysoftware.Navigation.Platform;
    using Bezysoftware.Navigation.Dialogs.View;    

    /// <summary>
    /// Interceptor which can display dialogs.
    /// </summary>
    public class DialogInterceptor : INavigationInterceptor
    {
        private readonly Dictionary<Type, IDialogContainer> dialogs;
        private readonly IEnumerable<IDialogContainer> dialogContainers;
        private INavigationInterceptor innerNavigationInterceptor;

        public DialogInterceptor(IEnumerable<IDialogContainer> dialogContainers)
        {
            this.dialogContainers = dialogContainers;
            this.dialogs = new Dictionary<Type, IDialogContainer>();
        }

        /// <summary>
        /// Never raised.
        /// </summary>
        public event EventHandler<TypeEventArgs> ConditionChanged;

        /// <summary>
        /// Inner interceptor, optional
        /// </summary>
        public INavigationInterceptor InnerNavigationInterceptor
        {
            get
            {
                return this.innerNavigationInterceptor;
            }
            set
            {
                this.innerNavigationInterceptor = value;
                if (this.innerNavigationInterceptor != null)
                {
                    this.innerNavigationInterceptor.ConditionChanged += this.ConditionChanged;
                }
            }
        }

        /// <summary>
        /// Intercepts navigation in case the targetViewType has <see cref="DialogContainerAttribute"/>.
        /// </summary>
        /// <param name="targetViewType">Type of target view that navigation is going for. </param>
        /// <returns> True if navigation should be prevented, otherwise false. </returns>
        public bool InterceptNavigation(Type targetViewType) 
        {
            var intercepted = this.InnerNavigationInterceptor?.InterceptNavigation(targetViewType) ?? false;
            var page = this.IsPageType(targetViewType);
            var viewTypeOverride = this.GetViewTypeOverride(targetViewType);

            if (page && !intercepted)
            {
                // page, no interception, just navigate to that page. Also if for some reason a dialog is already displayed, hide it.
                this.HideDialog(targetViewType);
                this.HideDialog(viewTypeOverride);
                return false;
            }

            if (page && viewTypeOverride == null)
            {
                // page, no associated dialog (via AssociatedDialogViewModel), navigate to that page. 
                return false;
            }

            targetViewType = viewTypeOverride ?? targetViewType;

            var attr = targetViewType.GetTypeInfo().GetCustomAttribute<DialogContainerAttribute>();
            if (attr == null)
            {
                // no dialog container found
                return false;
            }

            var container = this.dialogContainers.FirstOrDefault(c => c.GetType() == attr.ContainerType);
            if (container == null)
            {
                // requested dialog container was not passed to this interceptor
                return false;
            }

            if (!this.dialogs.ContainsKey(targetViewType))
            {
                this.dialogs[targetViewType] = container;
                container.Show(targetViewType);
            }

            return true;
        }

        public void HookType(Type viewType)
        {
            this.InnerNavigationInterceptor?.HookType(viewType);
        }

        /// <summary>
        /// Hides the dialog.
        /// </summary>
        /// <param name="viewType"> Type of view currently active. </param>
        public void UnhookType(Type viewType)
        {
            this.InnerNavigationInterceptor?.UnhookType(viewType);
            viewType = this.GetViewTypeOverride(viewType) ?? viewType;

            this.HideDialog(viewType);
        }

        private void HideDialog(Type viewType)
        {
            if (viewType != null && this.dialogs.ContainsKey(viewType))
            {
                this.dialogs[viewType].Hide(viewType);
                this.dialogs.Remove(viewType);
            }
        }

        private bool IsPageType(Type viewType)
        {
            return typeof(Page).IsAssignableFrom(viewType);
        }

        private Type GetViewTypeOverride(Type viewType)
        {
            return viewType.GetTypeInfo().GetCustomAttribute<AssociatedDialogViewModelAttribute>()?.DialogType;
        }
    }
}
