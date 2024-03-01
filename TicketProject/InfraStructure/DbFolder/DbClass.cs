using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Application;

namespace InfraStructure.DbFolder
{
    public class DbClass : DbContext ,IApplicationDBContext
    {
        public DbClass(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

       
     

       public  DbSet<TicketModel> TicketTable => Set<TicketModel>();


        public DbSet<MultiAttachment>MultiAttachmentTable => Set<MultiAttachment>();

        public object Entry(TicketModel ticketModels)
        {
            return( base.Entry(ticketModels).State = EntityState.Modified);
           
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TicketModel>()
                    .HasKey(customer => customer.Ticket_Id);

            modelBuilder.Entity<TicketModel>()
                .HasMany(TicketModel => TicketModel.MultiAttachments)
                .WithOne(MultiAttachments => MultiAttachments.TicketModels)
                .HasForeignKey(MultiAttachments => MultiAttachments.TickId);


            modelBuilder.Entity<TicketModel>()
                .Property(t => t.Ticket_Status)
                .HasDefaultValue("Pending");
        }
    }
}