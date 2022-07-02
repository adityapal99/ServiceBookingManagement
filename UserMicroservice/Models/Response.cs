using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class Response
    {
        public string Message { get; set; }
        public bool Status { get; set; }

        #nullable enable
        public object? Payload { get; set; }

        public Response() { }

        public Response(string message, bool status, object payload)
        {
            Message = message;
            Status = status;
            Payload = payload;
        }

        public Response(string message, bool status)
        {
            Message = message;
            Status = status;
        }
    }

    public class Response<T> : Response
    {
        public new T? Payload { get; set; }

        
        public Response(string message, bool status, T payload)
        {
            Message = message;
            Status = status;
            Payload = payload;
        }
    }
}
