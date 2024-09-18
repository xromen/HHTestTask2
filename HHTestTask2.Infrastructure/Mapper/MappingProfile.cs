using AutoMapper;
using HHTestTask2.Domain.DTOs;
using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JournalDto, Journal>().ReverseMap();
            CreateMap<RequestDto, Request>().ReverseMap();
            CreateMap<Tree, TreeDto>();
            CreateMap<Node, NodeDto>();

            CreateMap<JournalDto, MJournalInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(journal => journal.Id))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(journal => journal.Request.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(journal => journal.Request.CreatedAt));

            CreateMap<JournalDto, MJournal>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(journal => GetJournalText(journal)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(journal => journal.Id))
                .ForMember(dest => dest.Eventid, opt => opt.MapFrom(journal => journal.Request.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(journal => journal.Request.CreatedAt));
        }

        private string GetJournalText(JournalDto journal)
        {
            string text = "";
            text += $"Request ID = {journal.Request.Id}\r\n";
            text += $"Path = {journal.Request.Path}\r\n";
            text += journal.Request.QueryParameters?.Substring(1).Replace("&", "\r\n") + "\r\n";
            text += journal.Request.BodyParameters + "\r\n";
            text += journal.StackTrace + "\r\n";
            text += journal.ExceptionData;

            return text;
        }
    }
}
