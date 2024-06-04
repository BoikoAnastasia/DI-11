using Microsoft.EntityFrameworkCore;

namespace DI_11
{
    public class TestContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("your_connection_string"); // Используем метод UseMySQL для указания использования MySQL вместо UseSqlServer
        }
    }

    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
