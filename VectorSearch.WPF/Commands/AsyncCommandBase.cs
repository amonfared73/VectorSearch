namespace VectorSearch.WPF.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                OnCanExecutedChanged();
            }
        }
        public override async void Execute(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception)
            {

            }
            finally
            {
                IsExecuting = false;
            }
        }
        public abstract Task ExecuteAsync(object? parameter);
    }
}
