using Microsoft.AspNetCore.WebUtilities;
using PWAY_ASPNetCore_WebAPI.Filter;

namespace PWAY_ASPNetCore_WebAPI.Services
{
    public class UriService : IUriService // to get GetPageUri function
    {
        private readonly string _baseUri; // get base URL via Dependency Injection in Program.cs
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route) 
        {
            var _endpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_endpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
