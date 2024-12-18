﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz_Maktab.APPDbContext;

#nullable disable

namespace Quiz_Maktab.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241130192106_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Quiz_Maktab.Entities.Card", b =>
                {
                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<int>("FailedAttempts")
                        .HasColumnType("int");

                    b.Property<string>("HolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CardNumber");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Quiz_Maktab.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<string>("DestinationCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<string>("SourceCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("TranceactionTime")
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionId");

                    b.HasIndex("DestinationCardNumber");

                    b.HasIndex("SourceCardNumber");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Quiz_Maktab.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Quiz_Maktab.Entities.Card", b =>
                {
                    b.HasOne("Quiz_Maktab.Entities.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quiz_Maktab.Entities.Transaction", b =>
                {
                    b.HasOne("Quiz_Maktab.Entities.Card", "DestinationCard")
                        .WithMany("ReceivedTransactions")
                        .HasForeignKey("DestinationCardNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Quiz_Maktab.Entities.Card", "SourceCard")
                        .WithMany("SentTransaction")
                        .HasForeignKey("SourceCardNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationCard");

                    b.Navigation("SourceCard");
                });

            modelBuilder.Entity("Quiz_Maktab.Entities.Card", b =>
                {
                    b.Navigation("ReceivedTransactions");

                    b.Navigation("SentTransaction");
                });

            modelBuilder.Entity("Quiz_Maktab.Entities.User", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
