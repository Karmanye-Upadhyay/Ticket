using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class TicketModel
    {
        [Key]
        public int Ticket_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Ticket_Name { get; set; }

        [Column(TypeName = "varchar(16)")]
        public string? Category { get; set; }

        public string? Priority_Level { get; set; }

        public string? Issues { get; set; }


        public string? Ticket_Status { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public List<MultiAttachment>? MultiAttachments { get; set; }
    }
}
