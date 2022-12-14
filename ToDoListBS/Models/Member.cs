using System;
using System.Collections.Generic;

namespace ToDoListBS.Models
{
    public partial class Member
    {
        /// <summary>
        /// 格式:MEM-年月-5位數,ex:MEM-20220500001
        /// </summary>
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
    }
}
