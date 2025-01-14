using VectorSearch.Domain.Configurations;
using VectorSearch.WPF.Components;

namespace VectorSearch.WPF.Services
{
    public class DialougeService : IDialougeService
    {
        public void ShowDialouge(Action<DialougeBoxOptions> optionsDelegate = null)
        {
            var options = new DialougeBoxOptions();
            if (optionsDelegate != null)
                optionsDelegate?.Invoke(options);
            var dialouge = new DialougeBox(options);
            dialouge.ShowDialog();
        }
    }
}
