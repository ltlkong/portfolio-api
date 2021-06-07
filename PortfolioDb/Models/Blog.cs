using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PortfolioDb.Models
{
    public class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }    
        public string Title { get; set; }
        public string Description { get; set; }
        public string Article { get; set; }
        public string ImgUrl { get; set; }
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
