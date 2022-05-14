using Start.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Start.ViewModels
{
    public class ImageComments
    {
        public Images images { get; set; }
        public List<person> person { get; set; }
        public List<Comment> comments { get; set; }

    }
}