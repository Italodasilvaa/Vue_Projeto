﻿// <auto-generated />
using Api_ProjetoEscola.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api_ProjetoEscola.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Api_ProjetoEscola.Models.Aluno", b =>
                {
                    b.Property<int>("AlunoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("TEXT");

                    b.HasKey("AlunoId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Alunos");

                    b.HasData(
                        new
                        {
                            AlunoId = 1,
                            DataNascimento = "10/05/1993",
                            Nome = "Italo",
                            ProfessorId = 1,
                            Sobrenome = "Silva"
                        },
                        new
                        {
                            AlunoId = 2,
                            DataNascimento = "05/03/2002",
                            Nome = "Igor",
                            ProfessorId = 3,
                            Sobrenome = "Cavalcante"
                        },
                        new
                        {
                            AlunoId = 3,
                            DataNascimento = "10/05/1973",
                            Nome = "Jaime",
                            ProfessorId = 3,
                            Sobrenome = "Ferreira"
                        });
                });

            modelBuilder.Entity("Api_ProjetoEscola.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Professores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Vinicius Prof"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Marco Prof"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Fernando Prof"
                        });
                });

            modelBuilder.Entity("Api_ProjetoEscola.Models.Aluno", b =>
                {
                    b.HasOne("Api_ProjetoEscola.Models.Professor", "Professor")
                        .WithMany("Alunos")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Api_ProjetoEscola.Models.Professor", b =>
                {
                    b.Navigation("Alunos");
                });
#pragma warning restore 612, 618
        }
    }
}
