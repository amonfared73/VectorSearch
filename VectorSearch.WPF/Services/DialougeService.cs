using VectorSearch.WPF.Components;

namespace VectorSearch.WPF.Services
{
    public class DialougeService : IDialougeService
    {
        public void ShowDialouge()
        {
            var dialouge = new DialougeBox();
            dialouge.ShowDialog();
        }
    }
}
