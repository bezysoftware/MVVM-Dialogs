namespace Bezysoftware.Navigation.Dialogs.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bezysoftware.Navigation.Lookup;

    public class AdaptiveViewLocator : IViewLocator
    {
        private readonly Dictionary<Type, List<Type>> lookupCache;
        private readonly IAssemblyResolver assemblyResolver;
        private readonly IEnumerable<IViewLookupStrategy> viewLookupStrategies;

        public AdaptiveViewLocator(IAssemblyResolver assemblyResolver, IEnumerable<IViewLookupStrategy> viewLookupStrategies)
        {
            this.assemblyResolver = assemblyResolver;
            this.viewLookupStrategies = viewLookupStrategies;

            this.lookupCache = new Dictionary<Type, List<Type>>();
        }

        public async Task<Type> GetViewTypeAsync(Type viewModelType)
        {
            if (!this.lookupCache.ContainsKey(viewModelType))
            {
                var assemblies = await this.assemblyResolver.GetAssembliesAsync();
                var pairs = ReflectionUtils.GetTypesWithAttribute<AssociatedViewModelAttribute>(assemblies);
                var ts = pairs.Where(p => p.Value.ViewModel == viewModelType);

                ts.ToList().ForEach(t => RegisterAssociation(viewModelType, t.Key));
            }

            // determine which should be used
            var views = this.lookupCache[viewModelType];

            if (views.Count == 1)
            {
                return views.First();
            }

            return this.viewLookupStrategies
                .FirstOrDefault(s => s.CanSelectView(viewModelType, views))?
                .SelectView(viewModelType, views) ?? views.FirstOrDefault();
        }

        public IViewLocator RegisterAssociation(Type viewModelType, Type viewType)
        {
            if (!this.lookupCache.ContainsKey(viewModelType))
            {
                this.lookupCache.Add(viewModelType, new List<Type>());
            }

            this.lookupCache[viewModelType].Add(viewType);
            return this;
        }
    }
}
