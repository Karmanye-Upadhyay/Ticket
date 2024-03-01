using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AllQuery.TicketCQRS.Commend
{
    public class UpdateCommentProperty : IRequest<List<TicketModel>>
    {
        public int Id { get; set; }
        public TicketModel TicketModels { get; set; }
    }

    public class UpdateCommentPropertyHandler : IRequestHandler<UpdateCommentProperty, List<TicketModel>>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        private readonly IMapper _mapper;

        public UpdateCommentPropertyHandler(IApplicationDBContext applicationDBContext, IMapper mapper)
        {
            _applicationDBContext = applicationDBContext;
            _mapper = mapper;
        }



        public async Task<List<TicketModel>> Handle(UpdateCommentProperty request, CancellationToken cancellationToken)
        {

            var existingRecord = _applicationDBContext.TicketTable.FirstOrDefault(r => r.Ticket_Id == request.Id);
            existingRecord.Comment = request.TicketModels.Comment;

            existingRecord.Ticket_Status = request.TicketModels.Ticket_Status;

            await _applicationDBContext.SaveChangesAsync();
            return await _applicationDBContext.TicketTable.ToListAsync();


        }
    }
}
