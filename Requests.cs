﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Start.Models
{
    public class Requests
    {
        public int id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int Value { get; set; }
    }
}