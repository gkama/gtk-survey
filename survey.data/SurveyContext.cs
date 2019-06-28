using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace survey.data
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options)
            : base(options)
        { }

        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<QuestionTypeAnswer> QuestionTypeAnswers { get; set; }
        public virtual DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasMany(x => x.Responses)
                    .WithOne()
                    .HasForeignKey(x => x.QuestionId)
                    .IsRequired();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeId)
                    .IsRequired();
            });

            modelBuilder.Entity<QuestionType>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasMany(x => x.Answers)
                    .WithOne()
                    .HasForeignKey(x => x.TypeId)
                    .IsRequired();
            });

            modelBuilder.Entity<QuestionTypeAnswer>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Response>(e =>
            {
                e.HasKey(x => x.Id);
            });
        }
    }
}
