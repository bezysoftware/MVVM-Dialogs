namespace Bezysoftware.Navigation.Dialogs.Sample.ViewModels
{
    using Bezysoftware.Navigation.Dialogs.Interception;
    using Bezysoftware.Navigation.Dialogs.View;
    using Bezysoftware.Navigation.Dialogs.ViewModel;
    using Bezysoftware.Navigation.Lookup;
    using Bezysoftware.Navigation.Platform;
    using Bezysoftware.Navigation.StatePersistence;
    using Bezysoftware.Navigation.Dialogs.Sample.Views;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using System.Collections.Generic;
    using Bezysoftware.Navigation.Dialogs.Lookup;
    using Repository.Settings;
    using Bezysoftware.Platform.Communication.Chat;
    using Repository.About.Apps;
    using Bezysoftware.Platform.Communication.Calls;
    using Repository.About;
    using System;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            var unity = new UnityContainer();

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unity));


            unity
                .RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager())
                .RegisterType<IViewLocator, ViewLocator>(new ContainerControlledLifetimeManager())
                .RegisterType<IViewModelLocator, ViewModelServiceLocator>(new ContainerControlledLifetimeManager())
                .RegisterType<IPlatformNavigator, PlatformNavigator>(new ContainerControlledLifetimeManager()/*, new InjectionProperty("InterceptBackNavigation", false)*/) // TODO
                .RegisterType<IStatePersistor, StatePersistor>(new ContainerControlledLifetimeManager())
                .RegisterType<IAssemblyResolver, ThisAssemblyResolver>(new ContainerControlledLifetimeManager())
                .RegisterType<IApplicationFrameProvider, CurrentWindowFrameProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<INavigationInterceptor, DialogInterceptor>("DialogInterceptor", new ContainerControlledLifetimeManager(), new InjectionProperty(nameof(DialogInterceptor.InnerNavigationInterceptor), new ResolvedParameter(typeof(INavigationInterceptor), "AdaptiveInterceptor")))
                .RegisterType<INavigationInterceptor, AdaptiveNavigationInterceptor>("AdaptiveInterceptor", new ContainerControlledLifetimeManager())
                .RegisterType<IEnumerable<INavigationInterceptor>, INavigationInterceptor[]>(new ContainerControlledLifetimeManager())
                .RegisterType<ISmsSender, SmsSender>(new ContainerControlledLifetimeManager())
                .RegisterType<ICallManager, CallManager>(new ContainerControlledLifetimeManager())
                .RegisterType<ISettingsRepository<Settings>, SettingsRepository<Settings>>(new ContainerControlledLifetimeManager())
                .RegisterType<IEnumerable<IDialogContainer>, IDialogContainer[]>(new ContainerControlledLifetimeManager())
                .RegisterType<IDialogContainer, PopupDialogContainer>("PopupContainer", new ContainerControlledLifetimeManager())
                .RegisterType<IDialogContainer, SystemDialogContainer>("SystemContainer", new ContainerControlledLifetimeManager())
                .RegisterType<IDialogContainer, SlidingDialogContainer>("SlidingContainer", new ContainerControlledLifetimeManager())
                .RegisterType<IOfflineAppRepository, OfflineAppRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IOnlineAppRepository, OnlineAppRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IAppDetailsProvider, AppDetailsProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<MainViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<SystemDialogViewModel>(new ContainerControlledLifetimeManager());

            unity.Resolve<MainViewModel>();

            //unity
            //    .RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager())
            //    .RegisterType<IViewLocator, ViewLocator>(new ContainerControlledLifetimeManager())
            //    .RegisterType<IViewModelLocator, ViewModelServiceLocator>(new ContainerControlledLifetimeManager())
            //    .RegisterType<IPlatformNavigator, PlatformNavigator>(new ContainerControlledLifetimeManager())
            //    .RegisterType<IApplicationFrameProvider, CurrentWindowFrameProvider>(new ContainerControlledLifetimeManager())
            //    .RegisterType<IStatePersistor, StatePersistor>(new ContainerControlledLifetimeManager())
            //    .RegisterType<IAssemblyResolver, ThisAssemblyResolver>(new ContainerControlledLifetimeManager())
            //    .RegisterType<INavigationInterceptor, DialogInterceptor>("Dialog", new ContainerControlledLifetimeManager(), new InjectionProperty("InnerNavigationInterceptor", new ResolvedParameter(typeof(INavigationInterceptor), "Adaptive")))
            //    .RegisterType<INavigationInterceptor, AdaptiveNavigationInterceptor>("Adaptive", new ContainerControlledLifetimeManager())
            //    .RegisterType<IEnumerable<INavigationInterceptor>, INavigationInterceptor[]>(new ContainerControlledLifetimeManager())
            //    .RegisterType<IEnumerable<IDialogContainer>, IDialogContainer[]>(new ContainerControlledLifetimeManager())
            //    .RegisterType<IDialogContainer, SystemDialogContainer>("SystemContainer", new ContainerControlledLifetimeManager())
            //    .RegisterType<IDialogContainer, PopupDialogContainer>("ContentContainer", new ContainerControlledLifetimeManager())
            //    .RegisterType<IDialogContainer, SlidingDialogContainer>("SlidingContainer", new ContainerControlledLifetimeManager())
            //    .RegisterType<MainViewModel>(new ContainerControlledLifetimeManager())
            //    .RegisterType<SlidingDialogViewModel>(new ContainerControlledLifetimeManager())
            //    .RegisterType<DialogViewModel>(new ContainerControlledLifetimeManager())
            //    .RegisterType<DialogWithoutPageViewModel>(new ContainerControlledLifetimeManager())
            //    .RegisterType<SystemDialogViewModel>(new ContainerControlledLifetimeManager());

            // manually register association for the main page to speed up application startup
            unity.Resolve<IViewLocator>().RegisterAssociation<MainViewModel, MainPage>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Init()
        {
        }
    }

    public class Settings : ISettings
    {
        public string Id
        {
            get;
            set;
        }

        public DateTime SomeDate
        { get; set; }
    }
}