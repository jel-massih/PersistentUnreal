using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PersistentUnreal.ViewModels
{
    public class BaseApiResponse
    {
        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public BaseApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public BaseApiResponse(HttpStatusCode statusCode, string message = null) : this((int)statusCode, message) { }
    }

    public class ApiOkResponse : BaseApiResponse
    {
        public object Result { get; }

        public ApiOkResponse() : this (null) {}

        public ApiOkResponse(object result)
            :base (HttpStatusCode.OK)
        {
            Result = result;
        }
    }

    public class ApiBadRequestResponse : BaseApiResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public ApiBadRequestResponse(ModelStateDictionary modelState)
            : base(HttpStatusCode.BadRequest)
        {
            if(modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            ErrorMessages = modelState.SelectMany(r => r.Value.Errors).Select(r => r.ErrorMessage);
        }
    }
}
