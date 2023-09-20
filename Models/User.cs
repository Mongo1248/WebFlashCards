using System.ComponentModel.DataAnnotations;

namespace WebFlashCards.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; private set; }

        [Required]
        public string PasswordHash { get; private set; }

        public bool IsAdmin { get; private set; }

        // Navigation property for Cards
        public virtual ICollection<Card> Cards { get; set; }

        private User() { }

        public User(Guid userId, string username, string passwordHash, bool isAdmin)
        {
            this.UserID = userId;
            this.Username = username;
            this.PasswordHash = passwordHash;
            this.IsAdmin = isAdmin;
        }

        // ... rest of the methods
    }
}
