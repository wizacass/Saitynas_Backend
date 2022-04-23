﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saitynas_API.Models;

#nullable disable

namespace Saitynas_API.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Saitynas_API.Models.Authentication.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RevokedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Consultation.Consultation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("FinishedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PatientDeviceToken")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("RequestedSpecialityId")
                        .HasColumnType("int");

                    b.Property<string>("SpecialistDeviceToken")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("SpecialistId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("PublicId")
                        .IsUnique();

                    b.HasIndex("RequestedSpecialityId");

                    b.HasIndex("SpecialistId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Evaluation.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("ConsultationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("SpecialistId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("Value")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsultationId");

                    b.HasIndex("SpecialistId");

                    b.HasIndex("UserId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Message.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Patient.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Role.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "None"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Patient"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Specialist"
                        });
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Specialist.Specialist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("SpecialistStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("SpecialityId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkplaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialistStatusId");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("WorkplaceId");

                    b.ToTable("Specialists");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Specialist.SpecialistStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SpecialistStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Offline"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Available"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Busy"
                        });
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Speciality.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("SpecialistId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("SpecialistId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Workplace.Workplace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Workplaces");
                });

            modelBuilder.Entity("Saitynas_API.Models.Authentication.RefreshToken", b =>
                {
                    b.HasOne("Saitynas_API.Models.Entities.User.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Consultation.Consultation", b =>
                {
                    b.HasOne("Saitynas_API.Models.Entities.Patient.Patient", "Patient")
                        .WithMany("Consultations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saitynas_API.Models.Entities.Speciality.Speciality", "RequestedSpeciality")
                        .WithMany("Consultations")
                        .HasForeignKey("RequestedSpecialityId");

                    b.HasOne("Saitynas_API.Models.Entities.Specialist.Specialist", "Specialist")
                        .WithMany("Consultations")
                        .HasForeignKey("SpecialistId");

                    b.Navigation("Patient");

                    b.Navigation("RequestedSpeciality");

                    b.Navigation("Specialist");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Evaluation.Evaluation", b =>
                {
                    b.HasOne("Saitynas_API.Models.Entities.Consultation.Consultation", "Consultation")
                        .WithMany("Evaluations")
                        .HasForeignKey("ConsultationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Saitynas_API.Models.Entities.Specialist.Specialist", "Specialist")
                        .WithMany("Evaluations")
                        .HasForeignKey("SpecialistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Saitynas_API.Models.Entities.User.User", "User")
                        .WithMany("Evaluations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consultation");

                    b.Navigation("Specialist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Specialist.Specialist", b =>
                {
                    b.HasOne("Saitynas_API.Models.Entities.Specialist.SpecialistStatus", "Status")
                        .WithMany("Specialists")
                        .HasForeignKey("SpecialistStatusId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Saitynas_API.Models.Entities.Speciality.Speciality", "Speciality")
                        .WithMany("Specialists")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saitynas_API.Models.Entities.Workplace.Workplace", "Workplace")
                        .WithMany("Specialists")
                        .HasForeignKey("WorkplaceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Speciality");

                    b.Navigation("Status");

                    b.Navigation("Workplace");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.User.User", b =>
                {
                    b.HasOne("Saitynas_API.Models.Entities.Patient.Patient", "Patient")
                        .WithOne("User")
                        .HasForeignKey("Saitynas_API.Models.Entities.User.User", "PatientId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Saitynas_API.Models.Entities.Role.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saitynas_API.Models.Entities.Specialist.Specialist", "Specialist")
                        .WithOne("User")
                        .HasForeignKey("Saitynas_API.Models.Entities.User.User", "SpecialistId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Patient");

                    b.Navigation("Role");

                    b.Navigation("Specialist");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Consultation.Consultation", b =>
                {
                    b.Navigation("Evaluations");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Patient.Patient", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Role.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Specialist.Specialist", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("Evaluations");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Specialist.SpecialistStatus", b =>
                {
                    b.Navigation("Specialists");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Speciality.Speciality", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("Specialists");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.User.User", b =>
                {
                    b.Navigation("Evaluations");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Saitynas_API.Models.Entities.Workplace.Workplace", b =>
                {
                    b.Navigation("Specialists");
                });
#pragma warning restore 612, 618
        }
    }
}
