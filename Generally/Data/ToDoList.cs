using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Generally.Data
{
    public class ToDoList
    {
        public int Id { get; set; }

        [MaxLength(50,ErrorMessage ="標題限制50字內")]
        [Required]
        public string Title { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "內容限制500字內")]
        public string? Description { get; set; }

        [Required]
        public DateTime? Plan_Date { get; set; }
        public DateTime Add_Date { get; set; }
        public DateTime? Update_Date { get; set; }
        public string Add_Mid { get; set; } = null!;
        public string? Update_Mid { get; set; }
        public byte Iscomplete { get; set; }
    }
}
