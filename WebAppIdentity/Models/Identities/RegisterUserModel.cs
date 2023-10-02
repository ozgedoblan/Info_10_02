using System.ComponentModel.DataAnnotations;

namespace WebAppIdentity.Models.Identities
{
    public class RegisterUserModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string? Fullname { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "İki defa doğru giriniz")]
        public string PasswordConfirm { get; set; } = default!;
    }
}