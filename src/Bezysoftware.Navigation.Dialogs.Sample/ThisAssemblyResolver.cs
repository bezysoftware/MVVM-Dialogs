namespace Bezysoftware.Navigation.Dialogs.Sample
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Bezysoftware.Navigation.Lookup;
    using Bezysoftware.Navigation.Dialogs.ViewModel;

    public class ThisAssemblyResolver : IAssemblyResolver
    {
        public async Task<IEnumerable<Assembly>> GetAssembliesAsync()
        {
            return new[] { typeof(ThisAssemblyResolver).GetTypeInfo().Assembly, typeof(SystemDialogViewModel).GetTypeInfo().Assembly };
        }
    }
}
