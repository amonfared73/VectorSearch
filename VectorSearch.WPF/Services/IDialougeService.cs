using VectorSearch.Domain.Configurations;

namespace VectorSearch.WPF.Services
{
    public interface IDialougeService
    {
        void ShowDialouge(Action<DialougeBoxOptions> options = null);
    }
}
