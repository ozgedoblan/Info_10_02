using System.ComponentModel.DataAnnotations;

namespace WebAppIdentity.Models.Identities
{
        public class LoginUserModel
        {
            [Required]
            public string? Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

            public bool RememberMe { get; set; }
        }
    }

