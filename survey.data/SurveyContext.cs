using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace survey.data
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options)
            : base(options)
        { }

        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<QuestionTypeAnswer> QuestionTypeAnswers { get; set; }
        public virtual DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasMany(x => x.Questions)
                    .WithOne()
                    .HasForeignKey(x => x.SurveyId)
                    .IsRequired();
            });

            modelBuilder.Entity<Question>(e =>
            {
                e.HasKey(x => x.Id);

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

                e.HasOne(x => x.Question)
                    .WithOne()
                    .HasForeignKey<Response>(x => x.QuestionId)
                    .IsRequired();
            });
        }
    }
}
