using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HeplerClass
{
    public class ApiResponse
    {
        public int ResponseCode { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
        public string Extension { get; set; }
        public string ImagePath { get; set; }
    }
}
