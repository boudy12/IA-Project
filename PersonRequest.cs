using Start.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Start.ViewModels
{
    public class PersonRequest
    {
        public List<person> person { get; set; }
        public List<Requests> Requests { get; set; }
    }
}