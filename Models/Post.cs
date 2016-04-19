using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Blog.Platform2.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [DisplayName("Title")]
        public string PostTitle { get; set; }

        [DisplayName("Content")]
        public string PostContent { get; set; }

        public string CreatedBy { get; set; }

        public bool Approved { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }
    }

}