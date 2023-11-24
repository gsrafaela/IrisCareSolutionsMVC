﻿// <auto-generated />
using System;
using IrisCareSolutions.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IrisCareSolutions.Migrations
{
    [DbContext(typeof(ICSolutionsContext))]
    partial class ICSolutionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IrisCareSolutions.Models.Exame", b =>
                {
                    b.Property<int>("ExameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExameId"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<byte[]>("ResultadoData")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ResultadoFileName")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("TuteladoId")
                        .HasColumnType("int");

                    b.HasKey("ExameId");

                    b.HasIndex("TuteladoId");

                    b.ToTable("Tbl_Exame");
                });

            modelBuilder.Entity("IrisCareSolutions.Models.Lembrete", b =>
                {
                    b.Property<int>("LembreteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LembreteId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Validade")
                        .HasColumnType("datetime2");

                    b.HasKey("LembreteId");

                    b.ToTable("Tbl_Lembrete");
                });

            modelBuilder.Entity("IrisCareSolutions.Models.Responsavel", b =>
                {
                    b.Property<int>("ResponsavelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResponsavelId"));

                    b.Property<int>("Cpf")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("Parentesco")
                        .HasColumnType("int");

                    b.Property<decimal>("Telefone")
                        .IsUnicode(false)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ResponsavelId");

                    b.ToTable("Responsavels");
                });

            modelBuilder.Entity("IrisCareSolutions.Models.Tutelado", b =>
                {
                    b.Property<int>("TuteladoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TuteladoId"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnName("Dt_Nascimento");

                    b.Property<int>("ModalidadeAtendimento")
                        .HasColumnType("int")
                        .HasColumnName("Ds_Modalidade");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("TuteladoId");

                    b.ToTable("Tbl_Tutelado");
                });

            modelBuilder.Entity("IrisCareSolutions.Models.TuteladoLembrete", b =>
                {
                    b.Property<int>("TuteladoId")
                        .HasColumnType("int");

                    b.Property<int>("LembreteId")
                        .HasColumnType("int");

                    b.HasKey("TuteladoId", "LembreteId");

                    b.HasIndex("LembreteId");

                    b.ToTable("TuteladosLembretes");
                });

            modelBuilder.Entity("IrisCareSolutions.Models.Exame", b =>
                {
                    b.HasOne("IrisCareSolutions.Models.Tutelado", "Tutelado")
                        .WithMany("Exames")
                        .HasForeignKey("TuteladoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tutelado");
                });

            modelBuilder.Entity("IrisCareSolutions.Models.TuteladoLembrete", b =>
                {
                    b.HasOne("IrisCareSolutions.Models.Lembrete", "Lembrete")
                        .WithMany("TuteladosLembretes")
                        .HasForeignKey("LembreteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IrisCareSolutions.Models.Tutelado", "Tutelado")
                        .WithMany("TuteladosLembretes")
                        .HasForeignKey("TuteladoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lembrete");

                    b.Navigation("Tutelado");
                });

            modelBuilder.Entity("IrisCareSolutions.Models.Lembrete", b =>
                {
                    b.Navigation("TuteladosLembretes");
                });

            modelBuilder.Entity("IrisCareSolutions.Models.Tutelado", b =>
                {
                    b.Navigation("Exames");

                    b.Navigation("TuteladosLembretes");
                });
#pragma warning restore 612, 618
        }
    }
}
