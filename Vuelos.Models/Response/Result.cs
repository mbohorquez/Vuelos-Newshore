using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Models.Response
{
    public class Result
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
