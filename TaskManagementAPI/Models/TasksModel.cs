using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class TasksModel
    {
        
        public int Id { get; set; }

        [Required (ErrorMessage = "Favor informar o Title.")]
        [MinLength(10, ErrorMessage = "O Title deve conter no mínimo 10 caracteres.")]
        [MaxLength(50, ErrorMessage = "O Title deve conter no máximo 50 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Favor informar a Description.")]
        [MinLength(20, ErrorMessage = "O Description deve conter no mínimo 20 caracteres.")]
        [MaxLength(100, ErrorMessage = "O Description deve conter no máximo 100 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Favor informar a DueDate.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Favor informar se IsCompleted.")]
        public bool IsCompleted { get; set; }
    }
}
