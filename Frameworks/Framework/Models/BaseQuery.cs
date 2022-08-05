namespace Framework.Models
{
    public class BaseQuery
    {
        public int PageSize { get; set; } = 10; // default 10 items per pages
        public int PageIndex { get; set; } = 1; // start from 1
        public string? OrderBys { get; set; }
        public PaginationOptions PaginationOption { get; set; } = PaginationOptions.Paged;


        public bool AdvancedQuery { get; set; } = false;
        public Framework.Models.PagedViewOptions PagedViewOption { get; set; } = Framework.Models.PagedViewOptions.List;
        public Framework.Models.ViewItemTemplateNames Template { get; set; } = Framework.Models.ViewItemTemplateNames.Details;
    }
}

