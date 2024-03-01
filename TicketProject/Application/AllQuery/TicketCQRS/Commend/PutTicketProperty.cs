using Application.AllQuery.TicketCQRS.Querry;
using Application.DTO;
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
    public class PutTicketProperty : IRequest<List<TicketModel>>
    {
        public int Id { get; set; }
        public TicketModel TicketModels { get; set; }
    }

    public class PutTicketPropertyHandler : IRequestHandler<PutTicketProperty, List<TicketModel>>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        private readonly IMapper _mapper;

        public PutTicketPropertyHandler(IApplicationDBContext applicationDBContext, IMapper mapper)
        {
            _applicationDBContext = applicationDBContext;
            _mapper = mapper;            
        }

       

        public async Task<List<TicketModel>> Handle(PutTicketProperty request, CancellationToken cancellationToken)
        {

           
           var karma= _applicationDBContext.Entry(request.TicketModels);
            
           request.TicketModels.Ticket_Status = "Pending";
            await _applicationDBContext.SaveChangesAsync();
            return await _applicationDBContext.TicketTable.ToListAsync();


        }
    }
}
