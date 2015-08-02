namespace Bezysoftware.Navigation.Dialogs.Interception
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;

    using Bezysoftware.Navigation.Platform;
    using Bezysoftware.Navigation.Dialogs.View;    

    /// <summary>
    /// Interceptor which can display dialogs.
    /// </summary>
    public class DialogInterceptor : INavigationInterceptor
    {
        private readonly IDialogContainerFactory dialogActivator;
        private readonly Dictionary<Type, IDialogContainer> dialogs;
        private readonly IEnumerable<IDialogContainer> dialogContainers;

        public DialogInterceptor(IEnumerable<IDialogContainer> dialogContainers,  IDialogContainerFactory dialogActivator)
        {
            this.dialogActivator = dialogActivator;
            this.dialogContainers = dialogContainers;
            this.dialogs = new Dictionary<Type, IDialogContainer>();
        }

        /// <summary>
        /// Never raised.
        /// </summary>
        public event EventHandler<TypeEventArgs> ConditionChanged;

        /// <summary>
        /// Intercepts navigation in case the targetViewType has <see cref="DialogContainerAttribute"/>.
        /// </summary>
        /// <param name="targetViewType">Type of target view that navigation is going for. </param>
        /// <returns> True if navigation should be prevented, otherwise false. </returns>
        public bool InterceptNavigation(Type targetViewType)
        {
            var attr = targetViewType.GetTypeInfo().GetCustomAttribute<DialogContainerAttribute>();
            if (attr != null)
            {
                var container = this.dialogContainers.FirstOrDefault(c => c.GetType() == attr.ContainerType);
                if (container != null)
                {
                    this.dialogs[targetViewType] = container;
                    container.Show(targetViewType);
                    
                    return true;
                }
            }

            return false;
        }

        public void HookType(Type viewType)
        {
        }

        /// <summary>
        /// Hides the dialog.
        /// </summary>
        /// <param name="viewType"> Type of view currently active. </param>
        public void UnhookType(Type viewType)
        {
            // this mehod is called when navigating back, so hide the view
            if (this.dialogs.ContainsKey(viewType))
            {
                this.dialogs[viewType].Hide(viewType);
                this.dialogs.Remove(viewType);
            }
        }
    }
}
