using PWAY_ASPNetCore_WebAPI.Filter;
using PWAY_ASPNetCore_WebAPI.Services;
using PWAY_ASPNetCore_WebAPI.Wrappers;

namespace PWAY_ASPNetCore_WebAPI.Helpers
{
    public class PaginationHelper //implements UriService (creation of Uri) + instead of all in BookController
    {
        public static PagedRespond<List<T>> CreatePagedRespond<T>(
            List<T> pagedData, PaginationFilter validFilter, 
            int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedRespond<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage = //generate uri of next page
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage = // GET Uri of previous page
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            // GET Uri of first and last page
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
