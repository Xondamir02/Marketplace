﻿// <auto-generated />
using System;
using System.Collections.Generic;
using ChatCore1.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatCore1.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    partial class ChatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatApi.Entities.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<Guid>>("UserIds")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.HasKey("Id");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("ChatApi.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("FromUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatApi.Entities.Message", b =>
                {
                    b.HasOne("ChatApi.Entities.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("ChatApi.Entities.Conversation", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
