using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Car_Galllery_Auction.Core.Modals
{
    public class ApiResponse
    {

        public ApiResponse()
        {
            ErrorMessages = new List<string>(); // her tetiklemede boş bir mesaj listesi oluşturmak için constratra yadık
        }


        public HttpStatusCode StatusCode { get; set; }

        public bool isSuccess { get; set; }

        public List<string> ErrorMessages { get; set; }

        public object Result { get; set; }

    }
}
