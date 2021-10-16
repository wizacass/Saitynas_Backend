﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saitynas_API.Models;

namespace Saitynas_API.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Saitynas_API.Models.MessageEntity.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Saitynas_API.Models.RoleEntity.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

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

            modelBuilder.Entity("Saitynas_API.Models.SpecialistEntity.Specialist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("SpecialityId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("WorkplaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("WorkplaceId");

                    b.ToTable("Specialists");
                });

            modelBuilder.Entity("Saitynas_API.Models.SpecialityEntity.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("Saitynas_API.Models.UserEntity.User", b =>
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

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Saitynas_API.Models.WorkplaceEntity.Workplace", b =>
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

            modelBuilder.Entity("Saitynas_API.Models.SpecialistEntity.Specialist", b =>
                {
                    b.HasOne("Saitynas_API.Models.SpecialityEntity.Speciality", "Speciality")
                        .WithMany("Specialists")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saitynas_API.Models.WorkplaceEntity.Workplace", "Workplace")
                        .WithMany("Specialists")
                        .HasForeignKey("WorkplaceId");

                    b.Navigation("Speciality");

                    b.Navigation("Workplace");
                });

            modelBuilder.Entity("Saitynas_API.Models.UserEntity.User", b =>
                {
                    b.HasOne("Saitynas_API.Models.RoleEntity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Saitynas_API.Models.RoleEntity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Saitynas_API.Models.SpecialityEntity.Speciality", b =>
                {
                    b.Navigation("Specialists");
                });

            modelBuilder.Entity("Saitynas_API.Models.WorkplaceEntity.Workplace", b =>
                {
                    b.Navigation("Specialists");
                });
#pragma warning restore 612, 618
        }
    }
}
