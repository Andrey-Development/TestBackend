using ApiAgenda.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiAgenda.Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<Agenda> Agenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var a = modelBuilder.Entity<Agenda>();
            a.ToTable("agenda");
            a.HasKey(x => x.Id);
            a.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            a.Property(x => x.Titulo).HasColumnName("titulo").IsRequired();
            a.Property(x => x.Descricao).HasColumnName("descricao");
            a.Property(x => x.DataPrazo).HasColumnName("data_prazo").IsRequired();
            a.Property(x => x.DataInicio).HasColumnName("data_inicio").IsRequired();
            a.Property(x => x.DataConclusao).HasColumnName("data_conclusao").IsRequired();
            a.Property(x => x.Status).HasColumnName("status").IsRequired();
            a.Property(x => x.Responsavel).HasColumnName("responsavel").IsRequired();
            a.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
            a.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        }
    }
}