using Microsoft.EntityFrameworkCore;
using WebFlashCards.Models;

namespace WebFlashCards.Data
{
    public class WebFlashCardsContext : DbContext
    {
        public WebFlashCardsContext(DbContextOptions<WebFlashCardsContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<AnswerScore> AnswerScores { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // For now, it's empty, but this is where you can configure relationships, constraints, etc.
        }
    }
}

