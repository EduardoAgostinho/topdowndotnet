using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Favor informar o E-mail.")]
        [EmailAddress (ErrorMessage = "E-mail inválido.")]
        [MaxLength(50, ErrorMessage = "O e-mail deve conter no máximo 50 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Favor informar a Senha.")]
        [PasswordPropertyText(true)]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres e conter: letras maiúsculas e minúsculas, números e caracteres especiais.")]
        [MaxLength(10, ErrorMessage = "A senha deve conter no máximo 10 caracteres e conter: letras maiúsculas e minúsculas, números e caracteres especiais.")]
        public string Password { get; set; }
    }
}
