using System;
using System.Collections.Generic;

namespace ToDoListBS.Models
{
    public partial class Remind
    {
        public int Id { get; set; }
        public DateTime? Remind_Date { get; set; }

        public DateTime? Update_Date { get; set; }

        public int? ToDoList_Id { get; set; }
    }
}
