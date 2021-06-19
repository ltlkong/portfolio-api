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
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public string Article { get; set; }
        [MaxLength(120)]
        public string ImgUrl { get; set; }
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
