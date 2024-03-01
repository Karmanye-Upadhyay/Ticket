using Application.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AllQuery.TicketCQRS.Querry
{
    public class GetTicketDetail : IRequest<List<TicketViewDto>>
    { }

    public class GetTicketDetailHandler : IRequestHandler<GetTicketDetail, List<TicketViewDto>>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        private readonly IMapper _mapper;

        public GetTicketDetailHandler(IApplicationDBContext applicationDBContext, IMapper mapper)
        {
            _applicationDBContext = applicationDBContext;
            _mapper = mapper;
        }



        public async Task<List<TicketViewDto>> Handle(GetTicketDetail request, CancellationToken cancellationToken)
        {
            var tickets = await _applicationDBContext.TicketTable
                .Include(t => t.MultiAttachments)
                .ToListAsync();

            var ticketViewDtos = tickets.Select(ticket => new TicketViewDto
            {
                Ticket_Id = ticket.Ticket_Id,
                Ticket_Name = ticket.Ticket_Name,
                Category = ticket.Category,
                Priority_Level = ticket.Priority_Level,
                Issues = ticket.Issues,
                Ticket_Status = ticket.Ticket_Status,
                Comment = ticket.Comment,
                MultiAttachmentDtos = ticket.MultiAttachments.Select(attachment => new MultiAttachmentDto
                {
                    // Map properties from attachment entity to MultiAttachmentDto properties
                    AttachmentId = attachment.AttachmentId,
                    Attachment_Name = attachment.Attachment_Name,
                    OrignalName = attachment.OrignalName,
                    Extension = attachment.Extension,
                    NewName = attachment.NewName,
                    TickId = attachment.TickId,
                    // Map other properties as needed
                }).ToList()
            }).ToList();
            return ticketViewDtos;

        }
    }
}