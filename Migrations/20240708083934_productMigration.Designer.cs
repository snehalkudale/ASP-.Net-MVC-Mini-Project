﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoleMates.Models;

#nullable disable

namespace SoleMates.Migrations
{
    [DbContext(typeof(myContext))]
    [Migration("20240708083934_productMigration")]
    partial class productMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SoleMates.Models.Admin", b =>
                {
                    b.Property<int>("admin_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("admin_Id"), 1L, 1);

                    b.Property<string>("admin_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("admin_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("admin_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("admin_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("admin_Id");

                    b.ToTable("tbl_admin");
                });

            modelBuilder.Entity("SoleMates.Models.Category", b =>
                {
                    b.Property<int>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("category_id"), 1L, 1);

                    b.Property<string>("category_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("category_id");

                    b.ToTable("tbl_category");
                });

            modelBuilder.Entity("SoleMates.Models.Customer", b =>
                {
                    b.Property<int>("customer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customer_id"), 1L, 1);

                    b.Property<string>("customer_address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customer_id");

                    b.ToTable("tbl_customer");
                });

            modelBuilder.Entity("SoleMates.Models.Product", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("product_id"), 1L, 1);

                    b.Property<string>("product_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_price")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("product_id");

                    b.ToTable("tbl_product");
                });
#pragma warning restore 612, 618
        }
    }
}
