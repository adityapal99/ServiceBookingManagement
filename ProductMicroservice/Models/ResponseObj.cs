using System;

namespace ProductMicroservice.Models
{
    public class ResponseObj
    {
        public int status { get; set; }
        public string msg { get; set; }
        public Object payload { get; set; }

    }
}
