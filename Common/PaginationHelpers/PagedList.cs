namespace Common.PaginationHelpers
{
    public class PagedList<T>: List<T>
    {
        public PagedList(IEnumerable<T> items, int pageSize, int currentPage, int totalCount)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            AddRange(items);
        }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

    }
}
