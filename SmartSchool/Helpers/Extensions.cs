using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.Helpers
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response,
        int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
           var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
           var camelCaseFormat = new JsonSerializerSettings();
           camelCaseFormat.ContractResolver = new CamelCasePropertyNamesContractResolver();

           response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormat));
           response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }
    }
}