using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TicketDto
    {
        public string? Ticket_Name { get; set; }

        public string? Category { get; set; }

        public string? Priority_Level { get; set; }

        public string? Issues { get; set; }


        public string? Ticket_Status { get; set; }
        public string? Comment { get; set; } = string.Empty;

    }
}
