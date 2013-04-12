using System;

namespace AngluarTutorialWithNancy.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Todo { get; set; }
        public byte Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}