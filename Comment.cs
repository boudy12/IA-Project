using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Start.Models
{
    public class Comment
    {
        public int id { get; set; }
        public int PersonId { get; set; }
        public int ImageId { get; set; }
        public string Comments { get; set; }
    }
}