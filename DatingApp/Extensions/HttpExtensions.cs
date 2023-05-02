using DatingApp.Helper;
using System.Text.Json;

namespace DatingApp.Extensions
{
    //adding extension method allows add a pgination header to our http reponse 
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response,int currentPage,int itemsPerPage,
            int totalItems,int totalPages) //this is gonna receive our query where we work out the pagination info,int totalPages)
        {
            //create pagination header

            var PaginationHeader =new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var Options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            // add our pagination to our response header
            response.Headers.Add("Pagination",JsonSerializer.Serialize(PaginationHeader, Options));//need to serialize this because our response header takes a key
            //because we are adding a custom header "pagination" we need to add a core header to make this header available 
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }


    }
}
