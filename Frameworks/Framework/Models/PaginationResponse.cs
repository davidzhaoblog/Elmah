namespace Framework.Models
{
    public class PaginationResponse
    {
        public int PageSize { get; set; } = 10; // default 10 items per pages
        public int PageIndex { get; set; } = 1; // start from 1

        public int TotalCount { get; set; } // all records in database
        public int Count { get; set; } // all records in page
        public int StartIndex => (PageSize - 1) * PageIndex + 1; // start item index of current page
        public int EndIndex => StartIndex + Count;
    }
}

