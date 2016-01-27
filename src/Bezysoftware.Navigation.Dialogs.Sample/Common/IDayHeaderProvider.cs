    namespace Svatky.Common
{
    using System.Threading.Tasks;

    public interface IDayHeaderProvider
    {
        Task<string> GetTodayHeaderAsync();
    }
}
