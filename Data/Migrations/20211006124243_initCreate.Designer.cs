﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nas_Pos.Data;

namespace API.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211006124243_initCreate")]
    partial class initCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("API.Entities.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("API.Entities.BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("longtext");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("API.Entities.Ledger.Ledger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("AmountPaid")
                        .HasColumnType("double");

                    b.Property<double>("AmountRemaining")
                        .HasColumnType("double");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDebit")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Ledgers");
                });

            modelBuilder.Entity("API.Entities.Ledger.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("LedgerId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionType")
                        .HasColumnType("longtext");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("LedgerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("API.Entities.OrderAggregate.DeliveryMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeliveryTime")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ShortName")
                        .HasColumnType("longtext");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("DeliveryMethods");
                });

            modelBuilder.Entity("API.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("DeliveryMethodId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Subtotal")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryMethodId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("API.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("API.Entities.OrderAggregate.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("API.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("MinimumThreshold")
                        .HasColumnType("int");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int?>("ProductShelvesId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<double>("PurchasedPrice")
                        .HasColumnType("double");

                    b.Property<double>("Stock")
                        .HasColumnType("double");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<int>("UnitOfMeasure")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductShelvesId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("API.Entities.ProductShelves", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("ProductShelves");
                });

            modelBuilder.Entity("API.Entities.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("API.Entities.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("OwnerId")
                        .HasColumnType("longtext");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("API.Entities.BasketItem", b =>
                {
                    b.HasOne("API.Entities.Basket", null)
                        .WithMany("BasketItems")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Entities.Ledger.Ledger", b =>
                {
                    b.HasOne("API.Entities.OrderAggregate.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("API.Entities.Ledger.Transaction", b =>
                {
                    b.HasOne("API.Entities.Ledger.Ledger", null)
                        .WithMany("Transactions")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Entities.OrderAggregate.Order", b =>
                {
                    b.HasOne("API.Entities.OrderAggregate.DeliveryMethod", "DeliveryMethod")
                        .WithMany()
                        .HasForeignKey("DeliveryMethodId");

                    b.HasOne("API.Entities.OrderAggregate.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId");

                    b.OwnsOne("API.Entities.OrderAggregate.Address", "ShipToAddress", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .HasColumnType("longtext");

                            b1.Property<string>("FirstName")
                                .HasColumnType("longtext");

                            b1.Property<string>("LastName")
                                .HasColumnType("longtext");

                            b1.Property<string>("State")
                                .HasColumnType("longtext");

                            b1.Property<string>("Zipcode")
                                .HasColumnType("longtext");

                            b1.Property<string>("street")
                                .HasColumnType("longtext");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("DeliveryMethod");

                    b.Navigation("PaymentMethod");

                    b.Navigation("ShipToAddress");
                });

            modelBuilder.Entity("API.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("API.Entities.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("API.Entities.OrderAggregate.ProductItemOrdered", "ItemOrdered", b1 =>
                        {
                            b1.Property<int>("OrderItemId")
                                .HasColumnType("int");

                            b1.Property<string>("PictureUrl")
                                .HasColumnType("longtext");

                            b1.Property<int>("ProductId")
                                .HasColumnType("int");

                            b1.Property<string>("ProductName")
                                .HasColumnType("longtext");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("ItemOrdered");
                });

            modelBuilder.Entity("API.Entities.Product", b =>
                {
                    b.HasOne("API.Entities.ProductShelves", "ProductShelves")
                        .WithMany()
                        .HasForeignKey("ProductShelvesId");

                    b.HasOne("API.Entities.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductShelves");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("API.Entities.ProductShelves", b =>
                {
                    b.HasOne("API.Entities.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("API.Entities.ProductType", b =>
                {
                    b.HasOne("API.Entities.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("API.Entities.Basket", b =>
                {
                    b.Navigation("BasketItems");
                });

            modelBuilder.Entity("API.Entities.Ledger.Ledger", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("API.Entities.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("API.Entities.ProductType", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}