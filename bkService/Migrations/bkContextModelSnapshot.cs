﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bkService.DataAccess;

namespace bkService.Migrations
{
    [DbContext(typeof(bkContext))]
    partial class bkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("HasMessage")
                        .HasColumnName("HasMessage");

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

            modelBuilder.Entity("bkService.DataAccess.updInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime>("lastUpd")
                        .HasColumnName("lastUpd");

                    b.Property<int>("updDuration")
                        .HasColumnName("updDuration");

                    b.HasKey("Id");

                    b.ToTable("updInfoSet");
                });
#pragma warning restore 612, 618
        }
    }
}
