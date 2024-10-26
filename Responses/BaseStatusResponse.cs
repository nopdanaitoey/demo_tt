using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.Responses
{
    public class BaseStatusResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
    }
}