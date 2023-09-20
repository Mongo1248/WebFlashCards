using System.ComponentModel.DataAnnotations;

namespace WebFlashCards.Models
{
    public class Card
    {
        [Key]
        public Guid CardID { get; set; }

        [Required]
        [MaxLength(500)] // You can adjust the max length as needed
        public string QuestionText { get; private set; }

        [Required]
        [MaxLength(500)] // Adjust this too
        public string AnswerText { get; private set; }

        public byte[] QuestionMedia { get; private set; }
        public string QuestionMediaType { get; private set; }
        public byte[] AnswerMedia { get; private set; }
        public string AnswerMediaType { get; private set; }

        [Required]
        public Guid OwnerUserID { get; private set; }

        public Card(Guid cardID, Guid ownerUserID)
        {
            CardID = cardID;
            OwnerUserID = ownerUserID;
        }

        // ... other methods remain the same
    }
}
