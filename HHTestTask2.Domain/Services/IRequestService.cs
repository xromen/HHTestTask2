using HHTestTask2.Domain.Common;
using HHTestTask2.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Services
{
    public interface IRequestService
    {
        Task<RequestDto> AddRequest(HttpContext httpContext);
    }
}
