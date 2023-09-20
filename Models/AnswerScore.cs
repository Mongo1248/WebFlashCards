using System.ComponentModel.DataAnnotations;

namespace WebFlashCards.Models
{
    public class AnswerScore
    {
        [Key]
        public Guid AnswerScoreId { get; set; }

        [Required]
        public Guid CardID { get; private set; }
        public int ScoreCount { get; private set; }

        private AnswerScore() { }

        public AnswerScore(Guid cardID)
        {
            CardID = cardID;
        }

        public void IncrementScore()
        {
            ScoreCount += 1;
        }

        public void ResetScore()
        {
            ScoreCount = 0;
        }
    }
}

