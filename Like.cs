using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Start.Models
{
    public class Like
    {
        public int id { get; set; }
        public int PersonId { get; set; }
        public int ImageId { get; set; }
        public int LikeValue { get; set; }

        public int DisLikeValue { get; set; }
    }
}