using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Blog.Platform2.Models
{
    public class Keyword
    {
        public int KeywordId { get; set; }

        [DisplayName("Keyword")]
        public string KeywordText { get; set; }

        public bool Approved { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }

}