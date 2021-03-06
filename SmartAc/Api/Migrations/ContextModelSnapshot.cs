// <auto-generated />
using System;
using Api.Auxiliaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Api.Models.Domain.Alert", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeasureDeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("MeasureRecordedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecognizedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Resolution")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResolvedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("View")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeasureDeviceId", "MeasureRecordedOn");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("Api.Models.Domain.Device", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("InitedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Major")
                        .HasColumnType("int");

                    b.Property<int>("Minor")
                        .HasColumnType("int");

                    b.Property<int>("Patch")
                        .HasColumnType("int");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Api.Models.Domain.Junk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Payload")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Junks");
                });

            modelBuilder.Entity("Api.Models.Domain.Measure", b =>
                {
                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RecordedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("Carbon")
                        .HasColumnType("float");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<double>("Humidity")
                        .HasColumnType("float");

                    b.Property<DateTime>("ReportedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.HasKey("DeviceId", "RecordedOn");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("Api.Models.Domain.Alert", b =>
                {
                    b.HasOne("Api.Models.Domain.Measure", "Measure")
                        .WithMany()
                        .HasForeignKey("MeasureDeviceId", "MeasureRecordedOn");

                    b.Navigation("Measure");
                });
#pragma warning restore 612, 618
        }
    }
}
