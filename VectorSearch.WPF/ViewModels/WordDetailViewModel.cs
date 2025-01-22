namespace VectorSearch.WPF.ViewModels
{
    public class WordDetailViewModel : ViewModelBase
    {
        private string _word;
        public string Word
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
                OnPropertyChanged(nameof(Word));    
            }
        }

        private double _similarity;
        public double Similarity
        {
            get
            {
                return _similarity;
            }
            set
            {
                _similarity = value;
                OnPropertyChanged(nameof(Similarity));
            }
        }

        private string _vector;
        public string Vector
        {
            get
            {
                return _vector;
            }
            set
            {
                _vector = value;
                OnPropertyChanged(nameof(Vector));
            }
        }
    }
}
