using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VernumBlog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime creationTime { get; set; }

        public string imagePath { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
