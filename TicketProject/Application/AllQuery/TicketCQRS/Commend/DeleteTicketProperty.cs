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
    public class DeleteTicketProperty : IRequest<List<TicketModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteTicketPropertyHandler : IRequestHandler<DeleteTicketProperty, List<TicketModel>>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        private readonly IMapper _mapper;

        public DeleteTicketPropertyHandler(IApplicationDBContext applicationDBContext, IMapper mapper)
        {
            _applicationDBContext = applicationDBContext;
            _mapper = mapper;
        }

       async Task<List<TicketModel>> IRequestHandler<DeleteTicketProperty, List<TicketModel>>.Handle(DeleteTicketProperty request, CancellationToken cancellationToken)
        {
            var paymentDetail = await _applicationDBContext.TicketTable.FindAsync(request.Id);
            _applicationDBContext.TicketTable.Remove(paymentDetail);
            await _applicationDBContext.SaveChangesAsync();
            return (await _applicationDBContext.TicketTable.ToListAsync());

        }
    }
}
