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
    public class PostTicketProperty : IRequest<List<TicketModel>>
    {
        public TicketDto? ticketDto { get; set; }
    }

    public class PostTicketPropertyHandler : IRequestHandler<PostTicketProperty, List<TicketModel>>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;

        public PostTicketPropertyHandler(IApplicationDBContext ApplicationdbContext, IMapper mapper)
        {
            _context = ApplicationdbContext;
            _mapper = mapper;
        }

        public async Task<List<TicketModel>> Handle(PostTicketProperty request, CancellationToken cancellationToken)
        {


            var driver = _mapper.Map<TicketModel>(request.ticketDto);
            await _context.TicketTable.AddAsync(driver);
            await _context.SaveChangesAsync();


            return await _context.TicketTable.ToListAsync();

        }

    }
}
