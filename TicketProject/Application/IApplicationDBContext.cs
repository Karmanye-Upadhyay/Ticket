using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IApplicationDBContext
    {
      public  DbSet<TicketModel>TicketTable { get; }
       public DbSet<MultiAttachment>MultiAttachmentTable { get; }

        object Entry(TicketModel ticketModels);
        public Task SaveChangesAsync();
    }
}
