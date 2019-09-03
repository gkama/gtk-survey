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

        public virtual DbSet<Workspace> Workspaces { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<QuestionTypeAnswer> QuestionTypeAnswers { get; set; }
        public virtual DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workspace>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Survey>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Name).HasMaxLength(300);
                e.Property(x => x.CreatedBy).HasMaxLength(100);
                e.Property(x => x.LastUpdatedBy).HasMaxLength(100);

                e.HasMany(x => x.SurveyQuestions)
                    .WithOne()
                    .HasForeignKey(x => x.SurveyId)
                    .IsRequired();

                e.HasOne(x => x.Workspace)
                    .WithMany()
                    .HasForeignKey(x => x.WorkspaceId);
            });

            modelBuilder.Entity<Question>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Name).HasMaxLength(300);
                e.Property(x => x.Text).HasMaxLength(1000);

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeId)
                    .IsRequired();
            });

            modelBuilder.Entity<SurveyQuestion>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.Question)
                    .WithMany()
                    .HasForeignKey(x => x.QuestionId)
                    .IsRequired();
            });

            modelBuilder.Entity<QuestionType>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Name).HasMaxLength(300);

                e.HasMany(x => x.Answers)
                    .WithOne()
                    .HasForeignKey(x => x.TypeId)
                    .IsRequired();
            });

            modelBuilder.Entity<QuestionTypeAnswer>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Answer).HasMaxLength(1000);
            });

            modelBuilder.Entity<Response>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.SurveyQuestion)
                    .WithMany()
                    .HasForeignKey(x => x.SurveyQuestionId)
                    .IsRequired();

                e.HasOne(x => x.QuestionTypeAnswer)
                    .WithMany()
                    .HasForeignKey(x => x.QuestionTypeAnswerId)
                    .IsRequired();
            });
        }
    }
}
