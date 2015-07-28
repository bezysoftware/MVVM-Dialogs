namespace Bezysoftware.Navigation.Dialogs.Interception
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Bezysoftware.Navigation.Platform;
    using Bezysoftware.Navigation.Dialogs.View;
    using System.Collections.Generic;

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

        public event EventHandler<TypeEventArgs> ConditionChanged;

        public bool InterceptNavigation(Type targetViewType)
        {
            var attr = targetViewType.GetTypeInfo().GetCustomAttribute<DialogAttribute>();
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
