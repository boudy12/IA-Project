using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Start.Models
{
    public class Images
    {
        public int id { get; set; }


        public int PersonID { get; set; }

        [DisplayName("upload file")]
        public string UploadImages { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public int Likes { get; set; }

        public int DisLikes { get; set; }

        public string Comments { get; set; }

        public string Description { get; set; }
    }
}