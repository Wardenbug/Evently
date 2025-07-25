﻿// <auto-generated />
using System;
using Evently.Modules.Attendance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Evently.Modules.Attendance.Infrastructure.Database.Migrations
{
    [DbContext(typeof(AttendanceDbContext))]
    partial class AttendanceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("attendance")
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Evently.Modules.Attendance.Domain.Attendees.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_attendees");

                    b.ToTable("attendees", "attendance");
                });

            modelBuilder.Entity("Evently.Modules.Attendance.Domain.Events.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndsAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ends_at_utc");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<DateTime>("StartsAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("starts_at_utc");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.ToTable("events", "attendance");
                });

            modelBuilder.Entity("Evently.Modules.Attendance.Domain.Tickets.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AttendeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("attendee_id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("code");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<DateTime?>("UsedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("used_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_tickets");

                    b.HasIndex("AttendeeId")
                        .HasDatabaseName("ix_tickets_attendee_id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_tickets_code");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_tickets_event_id");

                    b.ToTable("tickets", "attendance");
                });

            modelBuilder.Entity("Evently.Modules.Attendance.Domain.Tickets.Ticket", b =>
                {
                    b.HasOne("Evently.Modules.Attendance.Domain.Attendees.Attendee", null)
                        .WithMany()
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tickets_attendees_attendee_id");

                    b.HasOne("Evently.Modules.Attendance.Domain.Events.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tickets_events_event_id");
                });
#pragma warning restore 612, 618
        }
    }
}
