using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace WebAPIExample.Models
{

    public interface IResponse
    {
        string Message { get; set; }

        bool DidError { get; set; }

        string ErrorMessage { get; set; }
    }

    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }

    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }

    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }

        double PageCount { get; }
    }

    public class Response : IResponse
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public TModel Model { get; set; }
    }

    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }
    }

    public class PagedResponse<TModel> : IPagedResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int ItemsCount { get; set; }

        public double PageCount
            => ItemsCount < PageSize ? 1 : (int)(((double)ItemsCount / PageSize) + 1);
    }

    public static class ResponseExtensions
    {
        public static IActionResult toHttpResponse(this IResponse response)
        {
            var status= response.DidError ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;
            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult toHttpResponse<TModel>(this ISingleResponse<TModel> singleResponse)
        {
            var status = HttpStatusCode.OK;
            if (singleResponse.DidError) status = HttpStatusCode.InternalServerError;
            else if (singleResponse.Model == null) status = HttpStatusCode.NotFound;
            return new ObjectResult(singleResponse)
            {
                StatusCode = (int)status
            };
        }




        public static IActionResult toHttpResponse<TModel>(this IListResponse<TModel> listResponse)
        {
            var status = HttpStatusCode.OK;
            if (listResponse.DidError) status = HttpStatusCode.InternalServerError;
            else if (listResponse.Model == null) status = HttpStatusCode.NotFound;
            return new ObjectResult(listResponse)
            {
                StatusCode = (int)status
            };
        }




    }



}
