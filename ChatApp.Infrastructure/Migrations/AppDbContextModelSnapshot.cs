﻿// <auto-generated />
using System;
using ChatApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatApp.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatApp.Domain.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("RoomId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatApp.Domain.Models.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("ChatApp.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatApp.Domain.Models.UserInRooms", b =>
                {
                    b.Property<Guid>("IdRoom")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("IdRoom", "IdUser");

                    b.HasIndex("IdUser");

                    b.ToTable("UserInRooms");
                });

            modelBuilder.Entity("ChatApp.Domain.Models.Message", b =>
                {
                    b.HasOne("ChatApp.Domain.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChatApp.Domain.Models.Room", "Room")
                        .WithMany("Messages")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("ChatApp.Domain.Models.Room", b =>
                {
                    b.HasOne("ChatApp.Domain.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("ChatApp.Domain.Models.UserInRooms", b =>
                {
                    b.HasOne("ChatApp.Domain.Models.Room", "Room")
                        .WithMany("Members")
                        .HasForeignKey("IdRoom")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChatApp.Domain.Models.User", "User")
                        .WithMany("JoinedRooms")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatApp.Domain.Models.Room", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("ChatApp.Domain.Models.User", b =>
                {
                    b.Navigation("JoinedRooms");
                });
#pragma warning restore 612, 618
        }
    }
}
