﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VectorSearch.EF.Contexts;

#nullable disable

namespace VectorSearch.EF.Migrations
{
    [DbContext(typeof(VectorSearchDbContext))]
    partial class VectorSearchDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VectorSearch.Domain.Models.DictionaryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DictionaryType");
                });

            modelBuilder.Entity("VectorSearch.Domain.Models.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DictionaryTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vector")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DictionaryTypeId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("VectorSearch.Domain.Models.Word", b =>
                {
                    b.HasOne("VectorSearch.Domain.Models.DictionaryType", "DictionaryType")
                        .WithMany("Words")
                        .HasForeignKey("DictionaryTypeId");

                    b.Navigation("DictionaryType");
                });

            modelBuilder.Entity("VectorSearch.Domain.Models.DictionaryType", b =>
                {
                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}
