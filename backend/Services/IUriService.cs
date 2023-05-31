using PWAY_ASPNetCore_WebAPI.Filter;

namespace PWAY_ASPNetCore_WebAPI.Services
{
    public interface IUriService //interface for GetPageUrifunction
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
