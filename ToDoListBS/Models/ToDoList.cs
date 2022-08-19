using System;
using System.Collections.Generic;

namespace ToDoListBS.Models
{
    public partial class ToDoList
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Plan_Date { get; set; }
        public DateTime Add_Date { get; set; }
        public DateTime? Update_Date { get; set; }
        public string Add_Mid { get; set; } = null!;
        public string? Update_Mid { get; set; }
        public byte Iscomplete { get; set; }
    }
}
