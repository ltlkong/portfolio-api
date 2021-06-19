using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Models
{
    [Index(nameof(PageName),IsUnique = true)]
    public class View
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string PageName { get; set; }
        [Required]
        public int NumOfViews { get; set; }
        [Required]
        public DateTime LastViewed { get; set; }
    }
}
