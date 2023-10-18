﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iLearn.iLearnDbModels;

#nullable disable

namespace iLearn.Migrations.iLearnApp
{
    [DbContext(typeof(iLearnAppContext))]
    [Migration("20231014180858_mssql.local_migration_123")]
    partial class mssqllocal_migration_123
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("iLearn.iLearnDbModels.Course", b =>
                {
                    b.Property<string>("CourseCode")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("CourseDescription")
                        .HasMaxLength(8000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8000)");

                    b.Property<string>("CourseTitle")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsCertificationProvided")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsEvaluationRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.HasKey("CourseCode")
                        .HasName("PK__Course__FC00E001B39E4CF6");

                    b.HasIndex("InstructorId");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.CourseSection", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SectionId"), 1L, 1);

                    b.Property<string>("CourseCode")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("CourseContentPath")
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("EvalutationId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsEvaluationMandotry")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("SectionDescription")
                        .HasMaxLength(8000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8000)");

                    b.Property<int?>("SectionDuration")
                        .HasColumnType("int");

                    b.Property<string>("SectionTitle")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("SectionId")
                        .HasName("PK__CourseSe__80EF08723F362C42");

                    b.HasIndex("CourseCode");

                    b.HasIndex("EvalutationId");

                    b.ToTable("CourseSections");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.Evaluation", b =>
                {
                    b.Property<int>("EvaluationId")
                        .HasColumnType("int");

                    b.Property<string>("EvaluationDesc")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1)");

                    b.HasKey("EvaluationId");

                    b.ToTable("Evaluation", (string)null);
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.EvaluationQuestionMapping", b =>
                {
                    b.Property<int>("EvaluationQuestionMappingId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatdOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("EvaluationId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("EvaluationQuestionMappingId");

                    b.HasIndex("EvaluationId");

                    b.HasIndex("QuestionId");

                    b.ToTable("EvaluationQuestionMapping", (string)null);
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructorId"), 1L, 1);

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("InstructorCertifications")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("InstructorDescription")
                        .HasMaxLength(8000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8000)");

                    b.Property<string>("InstructorEmailId")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("InstructorExprerience")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("InstructorFirstName")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("InstructorLastName")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("InstructorId");

                    b.ToTable("Instructor", (string)null);
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<short?>("QuestionAnswerOptionId")
                        .HasColumnType("smallint");

                    b.Property<string>("QuestionDesc")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.HasKey("QuestionId");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.QuestionOptionsMapping", b =>
                {
                    b.Property<int>("QuestionOptionsMapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionOptionsMapId"), 1L, 1);

                    b.Property<DateTime?>("CreatdOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("OptionDesc")
                        .HasColumnType("int");

                    b.Property<short?>("OptionId")
                        .HasColumnType("smallint");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("QuestionOptionsMapId")
                        .HasName("PK__Question__5F596537A035B7F9");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionOptionsMapping", (string)null);
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.UserCourseMapping", b =>
                {
                    b.Property<int>("UserCourseMappingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserCourseMappingId"), 1L, 1);

                    b.Property<string>("CourseCode")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("CurrentSectionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EnrolledOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserCourseMappingId");

                    b.HasIndex("CourseCode");

                    b.HasIndex("CurrentSectionId");

                    b.ToTable("UserCourseMapping", (string)null);
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.Course", b =>
                {
                    b.HasOne("iLearn.iLearnDbModels.Instructor", "Instructor")
                        .WithMany("Courses")
                        .HasForeignKey("InstructorId")
                        .HasConstraintName("FK__Course__Instruct__1EA48E88");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.CourseSection", b =>
                {
                    b.HasOne("iLearn.iLearnDbModels.Course", "CourseCodeNavigation")
                        .WithMany("CourseSections")
                        .HasForeignKey("CourseCode")
                        .HasConstraintName("FK__CourseSec__Cours__2180FB33");

                    b.HasOne("iLearn.iLearnDbModels.Evaluation", "Evalutation")
                        .WithMany("CourseSections")
                        .HasForeignKey("EvalutationId")
                        .HasConstraintName("FK__CourseSec__Evalu__22751F6C");

                    b.Navigation("CourseCodeNavigation");

                    b.Navigation("Evalutation");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.EvaluationQuestionMapping", b =>
                {
                    b.HasOne("iLearn.iLearnDbModels.Evaluation", "Evaluation")
                        .WithMany("EvaluationQuestionMappings")
                        .HasForeignKey("EvaluationId")
                        .HasConstraintName("FK__Evaluatio__Evalu__1AD3FDA4");

                    b.HasOne("iLearn.iLearnDbModels.Question", "Question")
                        .WithMany("EvaluationQuestionMappings")
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("FK__Evaluatio__Quest__1BC821DD");

                    b.Navigation("Evaluation");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.QuestionOptionsMapping", b =>
                {
                    b.HasOne("iLearn.iLearnDbModels.Question", "Question")
                        .WithMany("QuestionOptionsMappings")
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("FK__QuestionO__Quest__160F4887");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.UserCourseMapping", b =>
                {
                    b.HasOne("iLearn.iLearnDbModels.Course", "CourseCodeNavigation")
                        .WithMany("UserCourseMappings")
                        .HasForeignKey("CourseCode")
                        .HasConstraintName("FK__UserCours__Cours__2645B050");

                    b.HasOne("iLearn.iLearnDbModels.CourseSection", "CurrentSection")
                        .WithMany("UserCourseMappings")
                        .HasForeignKey("CurrentSectionId")
                        .HasConstraintName("FK__UserCours__Curre__2739D489");

                    b.Navigation("CourseCodeNavigation");

                    b.Navigation("CurrentSection");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.Course", b =>
                {
                    b.Navigation("CourseSections");

                    b.Navigation("UserCourseMappings");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.CourseSection", b =>
                {
                    b.Navigation("UserCourseMappings");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.Evaluation", b =>
                {
                    b.Navigation("CourseSections");

                    b.Navigation("EvaluationQuestionMappings");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.Instructor", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("iLearn.iLearnDbModels.Question", b =>
                {
                    b.Navigation("EvaluationQuestionMappings");

                    b.Navigation("QuestionOptionsMappings");
                });
#pragma warning restore 612, 618
        }
    }
}
