﻿using Common.Helpers;
using System.Text.Json;

namespace UltimateCheatsheetApp.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader<T>(this HttpResponse httpResponse, PagedList<T> data)
        {
            var paginationHeader = new PaginationHeader(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            httpResponse.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));

            //Marks what headers need to be available for CORS
            httpResponse.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
