﻿// <auto-generated />
using System;
using MeetupAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeetupAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220728014909_New")]
    partial class New
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("MeetupAPI.Entities.Meetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Meetups");
                });

            modelBuilder.Entity("MeetupAPI.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MeetupUser", b =>
                {
                    b.Property<int>("MeetupsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsersRegistredId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MeetupsId", "UsersRegistredId");

                    b.HasIndex("UsersRegistredId");

                    b.ToTable("MeetupUser");
                });

            modelBuilder.Entity("MeetupUser", b =>
                {
                    b.HasOne("MeetupAPI.Entities.Meetup", null)
                        .WithMany()
                        .HasForeignKey("MeetupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeetupAPI.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersRegistredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
