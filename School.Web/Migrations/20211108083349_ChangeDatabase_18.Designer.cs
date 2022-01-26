﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolCore;

namespace School.Web.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20211108083349_ChangeDatabase_18")]
    partial class ChangeDatabase_18
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("School.Core.Common.Entities.ErrorType", b =>
                {
                    b.Property<int>("ErrorCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ErrorReason")
                        .HasColumnType("longtext");

                    b.HasKey("ErrorCode");

                    b.ToTable("ErrorTypes");
                });

            modelBuilder.Entity("School.Core.Common.Entities.ReportCards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<double?>("Report")
                        .HasColumnType("double");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("ReportCards");
                });

            modelBuilder.Entity("School.Core.UserIndex.Entities.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NewsContent")
                        .HasColumnType("longtext");

                    b.Property<string>("NewsCoverAddressOrTitle")
                        .HasColumnType("longtext");

                    b.Property<string>("NewsCoverType")
                        .HasColumnType("longtext");

                    b.Property<string>("NewsFileAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("NewsImgsAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("NewsName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("NewsShowEndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NewsShowStartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("NewsTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("NewsWriteTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NewsWriter")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("NewsTypeId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("School.Core.UserIndex.Entities.NewsType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NewsTypeName")
                        .HasColumnType("longtext");

                    b.Property<string>("NewsTypeType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("NewsTypes");
                });

            modelBuilder.Entity("SchoolCore.Entities.AClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AClassName")
                        .HasColumnType("longtext");

                    b.Property<int?>("AcademicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AcademicId");

                    b.ToTable("AClasses");
                });

            modelBuilder.Entity("SchoolCore.Entities.Academic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AcademicName")
                        .HasColumnType("longtext");

                    b.Property<int>("AcademicParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Academics");
                });

            modelBuilder.Entity("SchoolCore.Entities.AcademicCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AcademicId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AcademicId");

                    b.HasIndex("CourseId");

                    b.ToTable("AcademicAndCourses");
                });

            modelBuilder.Entity("SchoolCore.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseCapacity")
                        .HasColumnType("int");

                    b.Property<int>("CourseChoosenNumber")
                        .HasColumnType("int");

                    b.Property<string>("CourseCode")
                        .HasColumnType("longtext");

                    b.Property<double>("CourseCredit")
                        .HasColumnType("double");

                    b.Property<double>("CourseHour")
                        .HasColumnType("double");

                    b.Property<string>("CourseName")
                        .HasColumnType("longtext");

                    b.Property<string>("CourseTime")
                        .HasColumnType("longtext");

                    b.Property<int?>("TeachingTeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeachingTeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SchoolCore.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DepartmentFunctions")
                        .HasColumnType("longtext");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("longtext");

                    b.Property<int>("DepartmentParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SchoolCore.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SchoolCore.Entities.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("RoleClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("SchoolCore.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AClassId")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HeadPictureAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("IdCardNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Sex")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserAcademicId")
                        .HasColumnType("int");

                    b.Property<string>("UserCode")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserDepartmentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AClassId");

                    b.HasIndex("UserAcademicId");

                    b.HasIndex("UserDepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SchoolCore.Entities.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("UserClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("UserClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("SchoolCore.Entities.UserCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCourses");
                });

            modelBuilder.Entity("SchoolCore.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("School.Core.Common.Entities.ReportCards", b =>
                {
                    b.HasOne("SchoolCore.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCore.Entities.User", "Student")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("School.Core.UserIndex.Entities.News", b =>
                {
                    b.HasOne("School.Core.UserIndex.Entities.NewsType", "NewsType")
                        .WithMany("News")
                        .HasForeignKey("NewsTypeId");

                    b.Navigation("NewsType");
                });

            modelBuilder.Entity("SchoolCore.Entities.AClass", b =>
                {
                    b.HasOne("SchoolCore.Entities.Academic", "Academic")
                        .WithMany("AcademicClass")
                        .HasForeignKey("AcademicId");

                    b.Navigation("Academic");
                });

            modelBuilder.Entity("SchoolCore.Entities.AcademicCourse", b =>
                {
                    b.HasOne("SchoolCore.Entities.Academic", "Academic")
                        .WithMany("AcademicCourses")
                        .HasForeignKey("AcademicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCore.Entities.Course", "Course")
                        .WithMany("CourseAcademic")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Academic");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("SchoolCore.Entities.Course", b =>
                {
                    b.HasOne("SchoolCore.Entities.User", "TeachingTeacher")
                        .WithMany()
                        .HasForeignKey("TeachingTeacherId");

                    b.Navigation("TeachingTeacher");
                });

            modelBuilder.Entity("SchoolCore.Entities.RoleClaim", b =>
                {
                    b.HasOne("SchoolCore.Entities.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SchoolCore.Entities.User", b =>
                {
                    b.HasOne("SchoolCore.Entities.AClass", null)
                        .WithMany("AclassUsers")
                        .HasForeignKey("AClassId");

                    b.HasOne("SchoolCore.Entities.Academic", "UserAcademic")
                        .WithMany("AcademicUsers")
                        .HasForeignKey("UserAcademicId");

                    b.HasOne("SchoolCore.Entities.Department", "UserDepartment")
                        .WithMany("DepartmentUsers")
                        .HasForeignKey("UserDepartmentId");

                    b.Navigation("UserAcademic");

                    b.Navigation("UserDepartment");
                });

            modelBuilder.Entity("SchoolCore.Entities.UserClaim", b =>
                {
                    b.HasOne("SchoolCore.Entities.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SchoolCore.Entities.UserCourse", b =>
                {
                    b.HasOne("SchoolCore.Entities.Course", "Course")
                        .WithMany("CourseMember")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCore.Entities.User", "User")
                        .WithMany("UserCourse")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SchoolCore.Entities.UserRole", b =>
                {
                    b.HasOne("SchoolCore.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCore.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("School.Core.UserIndex.Entities.NewsType", b =>
                {
                    b.Navigation("News");
                });

            modelBuilder.Entity("SchoolCore.Entities.AClass", b =>
                {
                    b.Navigation("AclassUsers");
                });

            modelBuilder.Entity("SchoolCore.Entities.Academic", b =>
                {
                    b.Navigation("AcademicClass");

                    b.Navigation("AcademicCourses");

                    b.Navigation("AcademicUsers");
                });

            modelBuilder.Entity("SchoolCore.Entities.Course", b =>
                {
                    b.Navigation("CourseAcademic");

                    b.Navigation("CourseMember");
                });

            modelBuilder.Entity("SchoolCore.Entities.Department", b =>
                {
                    b.Navigation("DepartmentUsers");
                });

            modelBuilder.Entity("SchoolCore.Entities.Role", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("SchoolCore.Entities.User", b =>
                {
                    b.Navigation("UserClaims");

                    b.Navigation("UserCourse");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
