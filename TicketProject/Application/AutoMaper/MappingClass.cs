using Application.DTO;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMaper
{
    public class MappingClass : Profile
    {
        public MappingClass()
        {
            CreateMap<TicketDto, TicketModel>().ReverseMap();

            CreateMap<MultiAttachmentDto, MultiAttachment>().ReverseMap();
        }
    }
}