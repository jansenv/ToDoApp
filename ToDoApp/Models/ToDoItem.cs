using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDoItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public int TodoStatusId { get; set; }

        public ToDoStatus ToDoStatus { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}