using HHTestTask2.Domain;
using HHTestTask2.Domain.DTOs;
using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using HHTestTask2.Domain.Services;
using System.Collections;

namespace HHTestTask2.API.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private RequestDto RequestDto;

        private IRequestService _requestService;
        private IJournalService _journalService;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IRequestService requestService, IJournalService journalService)
        {
            _requestService = requestService;
            _journalService = journalService;

            RequestDto = await requestService.AddRequest(httpContext);

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            ExceptionResponseDto response = new ExceptionResponseDto();
            response.Id = RequestDto.Id.ToString();

            var internalException = exception as SecureException;

            if (internalException != null)
            {
                response.Type = "Secure";
                response.Data = new ExceptionResponseDto.DataDto() { Message = internalException.Message };
            }
            else
            {
                response.Type = "Exception";
                response.Data = new ExceptionResponseDto.DataDto() { Message = "Internal server error ID = " + RequestDto.Id };
            }

            var dictData = exception.Data.Cast<DictionaryEntry>();

            JournalDto journal = new JournalDto()
            {
                ExceptionMessage = response.Data.Message,
                ExceptionType = response.Type,
                ExceptionData = dictData == null ? null : string.Join("\r\n", dictData.Select(c => $"   {c.Key}: {c.Value}")),
                Request = RequestDto,
                StackTrace = exception.ToString()
            };

            await _journalService.AddJournal(journal);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            await context.Response.WriteAsync(json);
        }
        
    }
}
