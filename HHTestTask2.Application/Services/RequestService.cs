using AutoMapper;
using HHTestTask2.Domain;
using HHTestTask2.Domain.DTOs;
using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Services;
using HHTestTask2.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RequestDto> AddRequest(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            StreamReader reader = new StreamReader(httpContext.Request.Body);
            string text = await reader.ReadToEndAsync();

            httpContext.Request.Body.Position = 0;

            Request request = new Request()
            {
                Path = httpContext.Request.Path,
                QueryParameters = httpContext.Request.QueryString.Value,
                BodyParameters = text,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.RequestRepository.AddAsync(request);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RequestDto>(request);
        }
    }

}
