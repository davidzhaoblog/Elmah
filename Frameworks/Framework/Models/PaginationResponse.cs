namespace Framework.Models
{
    public class PaginationResponse
    {
        public int PageSize { get; set; } = 10; // default 10 items per pages
        public int PageIndex { get; set; } = 1; // start from 1
        public int LastPageIndex => (int)Math.Ceiling((double)TotalCount / (double)PageSize);

        public int TotalCount { get; set; }
        public int Count { get; set; } // all records in page
        public int StartIndex => (PageSize - 1) * PageIndex + 1; // start item index of current page
        public int EndIndex => StartIndex + Count;

        public bool EnableFirstAndPrevious => TotalCount > PageSize && PageIndex != 1;
        public bool EnableLastAndNext => TotalCount > PageSize && PageIndex != LastPageIndex;

        public int[] PreCurrent => PageIndex > 1 ? Enumerable.Range(PageIndex - (PageIndex > 2 ? 2 : 1), PageIndex - 1).ToArray() : Enumerable.Empty<int>().ToArray();
        public int[] PostCurrent => LastPageIndex - PageIndex > 1 ? Enumerable.Range(PageIndex + 1, PageIndex + (LastPageIndex - PageIndex > 2 ? 2 : 1)).ToArray() : Enumerable.Empty<int>().ToArray();
        public bool ThreeDotPreCurrent => PageIndex > 3;
        public bool ThreeDotPostCurrent => LastPageIndex - PageIndex > 3;
    }
}

