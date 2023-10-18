using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iLearn.iLearnDbModels
{
    public partial class iLearnAppContext : DbContext
    {
        public iLearnAppContext()
        {
        }

        public iLearnAppContext(DbContextOptions<iLearnAppContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseSection> CourseSections { get; set; } = null!;
        public virtual DbSet<Evaluation> Evaluations { get; set; } = null!;
        public virtual DbSet<EvaluationQuestionMapping> EvaluationQuestionMappings { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionOptionsMapping> QuestionOptionsMappings { get; set; } = null!;
        public virtual DbSet<UserCourseMapping> UserCourseMappings { get; set; } = null!;

        public int GetNextPrimaryKeyValue<TEntity>() where TEntity : class
        {
            IKey primaryKey = Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey();

            if (primaryKey == null || primaryKey.Properties.Count != 1)
            {
                throw new InvalidOperationException($"Entity {typeof(TEntity).Name} does not have a single-column primary key.");
            }

            string primaryKeyName = primaryKey.Properties[0].Name;

            var maxId = Set<TEntity>().Max(entity => (int?)EF.Property<int>(entity, primaryKeyName)) ?? 0;

            return maxId + 1;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseCode)
                    .HasName("PK__Course__FC00E001B39E4CF6");

                entity.ToTable("Course");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CourseDescription)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.CourseTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK__Course__Instruct__1EA48E88");
            });

            modelBuilder.Entity<CourseSection>(entity =>
            {
                entity.HasKey(e => e.SectionId)
                    .HasName("PK__CourseSe__80EF08723F362C42");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CourseContentPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SectionDescription)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.SectionTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CourseCodeNavigation)
                    .WithMany(p => p.CourseSections)
                    .HasForeignKey(d => d.CourseCode)
                    .HasConstraintName("FK__CourseSec__Cours__2180FB33");

                entity.HasOne(d => d.Evalutation)
                    .WithMany(p => p.CourseSections)
                    .HasForeignKey(d => d.EvalutationId)
                    .HasConstraintName("FK__CourseSec__Evalu__22751F6C");
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.ToTable("Evaluation");

                entity.Property(e => e.EvaluationId).ValueGeneratedNever();

                entity.Property(e => e.EvaluationDesc)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EvaluationQuestionMapping>(entity =>
            {
                entity.ToTable("EvaluationQuestionMapping");

                entity.Property(e => e.EvaluationQuestionMappingId).ValueGeneratedNever();

                entity.Property(e => e.CreatdOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p.EvaluationQuestionMappings)
                    .HasForeignKey(d => d.EvaluationId)
                    .HasConstraintName("FK__Evaluatio__Evalu__1AD3FDA4");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.EvaluationQuestionMappings)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Evaluatio__Quest__1BC821DD");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructor");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.InstructorCertifications)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorDescription)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorEmailId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorExprerience)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorFirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorLastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.QuestionId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.QuestionDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuestionOptionsMapping>(entity =>
            {
                entity.HasKey(e => e.QuestionOptionsMapId)
                    .HasName("PK__Question__5F596537A035B7F9");

                entity.ToTable("QuestionOptionsMapping");

                entity.Property(e => e.CreatdOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptionsMappings)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__QuestionO__Quest__160F4887");
            });

            modelBuilder.Entity<UserCourseMapping>(entity =>
            {
                entity.ToTable("UserCourseMapping");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EnrolledOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.CourseCodeNavigation)
                    .WithMany(p => p.UserCourseMappings)
                    .HasForeignKey(d => d.CourseCode)
                    .HasConstraintName("FK__UserCours__Cours__2645B050");

                entity.HasOne(d => d.CurrentSection)
                    .WithMany(p => p.UserCourseMappings)
                    .HasForeignKey(d => d.CurrentSectionId)
                    .HasConstraintName("FK__UserCours__Curre__2739D489");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
