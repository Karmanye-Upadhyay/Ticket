using Application.AllQuery.TicketCQRS.Commend;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.AllQuery.ImageCQRS.Commend
{
    public class DeleteImageProperty : IRequest<List<TicketModel>>
    {
        public string ImgName { get; set; }
    }

    public class DeleteTicketPropertyHandler : IRequestHandler<DeleteImageProperty, List<TicketModel>>
    {
        private readonly IApplicationDBContext _applicationDBContext;
        private readonly IMapper _mapper;

        public DeleteTicketPropertyHandler(IApplicationDBContext applicationDBContext, IMapper mapper)
        {
            _applicationDBContext = applicationDBContext;
            _mapper = mapper;
        }

        async Task<List<TicketModel>> IRequestHandler<DeleteImageProperty, List<TicketModel>>.Handle(DeleteImageProperty request, CancellationToken cancellationToken)
        {
            var paymentDetail = await _applicationDBContext.MultiAttachmentTable
                                                .SingleOrDefaultAsync(x => x.NewName == request.ImgName);
            _applicationDBContext.MultiAttachmentTable.Remove(paymentDetail);
            await _applicationDBContext.SaveChangesAsync();
            return (await _applicationDBContext.TicketTable.ToListAsync());

        }
    }
}
