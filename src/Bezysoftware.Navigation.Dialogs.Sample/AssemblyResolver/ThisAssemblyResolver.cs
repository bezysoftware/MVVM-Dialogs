namespace Bezysoftware.Navigation.Dialogs.Sample
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Bezysoftware.Navigation.Lookup;
    using Bezysoftware.Navigation.Dialogs.ViewModel;
    using Settings.ViewModel;
    using About.ViewModel;

    public class ThisAssemblyResolver : IAssemblyResolver
    {
        public async Task<IEnumerable<Assembly>> GetAssembliesAsync()
        {
            // The home assembly of SystemDialogViewModel also needs to be added to this list
            return new[]
            {
                typeof(SystemDialogViewModel).GetTypeInfo().Assembly,
                typeof(ThisAssemblyResolver).GetTypeInfo().Assembly, // this assembly
                //typeof(SettingsViewModel<>).GetTypeInfo().Assembly,  // settings
                //typeof(AboutViewModel).GetTypeInfo().Assembly        // about
            };
        }
    }
}
