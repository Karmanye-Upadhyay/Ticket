using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class MultiAttachmentDto
    {
        public int AttachmentId { get; set; }
        public string Attachment_Name { get; set; }
        public string? OrignalName { get; set; }
        public string? Extension { get; set; }
        public string? NewName { get; set; }

        public int TickId { get; set; }

    }
}
