using VectorSearch.Domain.Enums;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Tools
{
    public static class ExtensionMethods
    {
        public static int GetPageNumber(this VectorSearchViewModel viewModel, PaginationType paginationType)
        {
            int pageNumber;
            switch (paginationType)
            {
                case PaginationType.CurrentPage:
                    pageNumber = viewModel.CurrentPage;
                    break;
                case PaginationType.NextPage:
                    pageNumber = viewModel.NextPage;
                    break;
                case PaginationType.PreviousPage:
                    pageNumber = viewModel.PreviousPage;
                    break;
                default:
                    throw new ArgumentException("PaginationType not assigned");
            }
            return pageNumber;
        }
    }
}
