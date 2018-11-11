﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bkService.DataAccess;

namespace bkService.Migrations
{
    [DbContext(typeof(bkContext))]
    [Migration("20181111161104_addMessage")]
    partial class addMessage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("bkService.DataAccess.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("EventId")
                        .HasColumnName("EventId");

                    b.Property<string>("jsonData")
                        .IsRequired()
                        .HasColumnName("data");

                    b.HasKey("Id");

                    b.ToTable("EventSet");
                });

            modelBuilder.Entity("bkService.DataAccess.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("MessageBody")
                        .HasColumnName("MessageBody");

                    b.Property<bool>("Sent")
                        .HasColumnName("Sent");

                    b.HasKey("Id");

                    b.ToTable("MessageSet");
                });
#pragma warning restore 612, 618
        }
    }
}
