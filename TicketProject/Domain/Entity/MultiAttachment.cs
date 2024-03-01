using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class MultiAttachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public string? Attachment_Name { get; set; }

        public string? OrignalName { get; set; }
        public string? Extension { get; set; }
        public string? NewName { get; set; }


        public TicketModel? TicketModels { get; set; }
        public int TickId { get; set; }



        [NotMapped]
        public byte[]? ImageByte { get; set; }
    }
}
