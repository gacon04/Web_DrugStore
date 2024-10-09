using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int AuthorId { get; set; } // Liên kết với bảng Users

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Đường dẫn hình ảnh
        [StringLength(255)]
        public string ImagePath { get; set; } // Lưu đường dẫn ảnh
    }
}