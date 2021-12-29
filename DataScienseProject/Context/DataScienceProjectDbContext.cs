using DataScienseProject.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataScienseProject.Context
{
    public partial class DataScienceProjectDbContext : DbContext
    {
        public DataScienceProjectDbContext()
        {
        }

        public DataScienceProjectDbContext(DbContextOptions<DataScienceProjectDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConfigValue> ConfigValues { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Element> Elements { get; set; }
        public virtual DbSet<ElementParameter> ElementParameters { get; set; }
        public virtual DbSet<ElementType> ElementTypes { get; set; }
        public virtual DbSet<Executor> Executors { get; set; }
        public virtual DbSet<ExecutorRole> ExecutorRoles { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupView> GroupViews { get; set; }
        public virtual DbSet<LinkType> LinkTypes { get; set; }
        public virtual DbSet<Password> Passwords { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<ViewElement> ViewElements { get; set; }
        public virtual DbSet<ViewExecutor> ViewExecutors { get; set; }
        public virtual DbSet<ViewTag> ViewTags { get; set; }
        public virtual DbSet<ViewType> ViewTypes { get; set; }
        public virtual DbSet<VisitLog> VisitLogs { get; set; }
        public virtual DbSet<VisitView> VisitViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\KALININSERWER;Database=CS_DS_Portfolio;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<ConfigValue>(entity =>
            {
                entity.HasKey(e => e.ConfigValuesKey)
                    .HasName("PK__ConfigVa__48F7A58097C36589");

                entity.Property(e => e.Key)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.HasKey(e => e.DirectionKey)
                    .HasName("PK__Directio__D7F5391AFE6232B7");

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.ElementKey)
                    .HasName("PK__Elements__5616FCCF3748CE5D");

                entity.Property(e => e.ElementName).IsUnicode(false);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.Property(e => e.Text).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.ElementTypeKeyNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.ElementTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Elements__Elemen__403A8C7D");

                entity.HasOne(d => d.LinkTypeKeyNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.LinkTypeKey)
                    .HasConstraintName("FK__Elements__LinkTy__412EB0B6");
            });

            modelBuilder.Entity<ElementParameter>(entity =>
            {
                entity.HasKey(e => e.ElementParameterKey)
                    .HasName("PK__ElementP__43326167528C5C3B");

                entity.Property(e => e.Key).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.ElementKeyNavigation)
                    .WithMany(p => p.ElementParameters)
                    .HasForeignKey(d => d.ElementKey)
                    .HasConstraintName("FK__ElementPa__Eleme__440B1D61");
            });

            modelBuilder.Entity<ElementType>(entity =>
            {
                entity.HasKey(e => e.ElementTypeKey)
                    .HasName("PK__ElementT__49CB550645C37754");

                entity.Property(e => e.ElementTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Executor>(entity =>
            {
                entity.HasKey(e => e.ExecutorKey)
                    .HasName("PK__Executor__D96DE10144051664");

                entity.Property(e => e.ExecutorName).IsUnicode(false);

                entity.Property(e => e.ExecutorProfileLink).IsUnicode(false);
            });

            modelBuilder.Entity<ExecutorRole>(entity =>
            {
                entity.HasKey(e => e.ExecutorRoleKey)
                    .HasName("PK__Executor__3AE9E647250B5DA8");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.FeedbackKey)
                    .HasName("PK__Feedback__AAB8A7CB53ED61DA");

                entity.ToTable("Feedback");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Text).IsUnicode(false);

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__Feedback__ViewKe__6C190EBB");

                entity.HasOne(d => d.VisitKeyNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.VisitKey)
                    .HasConstraintName("FK__Feedback__VisitK__6B24EA82");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GroupKey)
                    .HasName("PK__Groups__36BB80D3410BFF59");

                entity.Property(e => e.GroupName).IsUnicode(false);
            });

            modelBuilder.Entity<GroupView>(entity =>
            {
                entity.HasKey(e => e.GroupViewKey)
                    .HasName("PK__GroupVie__32CB5A7FF5073D06");

                entity.HasOne(d => d.GroupKeyNavigation)
                    .WithMany(p => p.GroupViews)
                    .HasForeignKey(d => d.GroupKey)
                    .HasConstraintName("FK__GroupView__Group__5EBF139D");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.GroupViews)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__GroupView__ViewK__5DCAEF64");
            });

            modelBuilder.Entity<LinkType>(entity =>
            {
                entity.HasKey(e => e.LinkTypeKey)
                    .HasName("PK__LinkType__9FAC2269E1B8EFC7");

                entity.Property(e => e.LinkTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Password>(entity =>
            {
                entity.HasKey(e => e.PasswordKey)
                    .HasName("PK__Password__B03270116520AC16");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.PasswordValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.GroupKeyNavigation)
                    .WithMany(p => p.Passwords)
                    .HasForeignKey(d => d.GroupKey)
                    .HasConstraintName("FK__Passwords__Group__619B8048");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagKey)
                    .HasName("PK__Tags__CA370A7AAC7B077A");

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DirectionKeyNavigation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.DirectionKey)
                    .HasConstraintName("FK__Tags__DirectionK__4CA06362");
            });

            modelBuilder.Entity<View>(entity =>
            {
                entity.HasKey(e => e.ViewKey)
                    .HasName("PK__Views__93DADE71737E8444");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.LogoPath).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ViewName).IsUnicode(false);

                entity.HasOne(d => d.ViewTypeKeyNavigation)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.ViewTypeKey)
                    .HasConstraintName("FK__Views__ViewTypeK__398D8EEE");
            });

            modelBuilder.Entity<ViewElement>(entity =>
            {
                entity.HasKey(e => e.ViewElementKey)
                    .HasName("PK__ViewElem__01D9A6962AA949CA");

                entity.HasOne(d => d.ElementKeyNavigation)
                    .WithMany(p => p.ViewElements)
                    .HasForeignKey(d => d.ElementKey)
                    .HasConstraintName("FK__ViewEleme__Eleme__47DBAE45");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewElements)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewEleme__ViewK__46E78A0C");
            });

            modelBuilder.Entity<ViewExecutor>(entity =>
            {
                entity.HasKey(e => e.ViewExecutorKey)
                    .HasName("PK__ViewExec__C9628F40D031AB7D");

                entity.HasOne(d => d.ExecutorKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ExecutorKey)
                    .HasConstraintName("FK__ViewExecu__Execu__5812160E");

                entity.HasOne(d => d.ExecutorRoleKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ExecutorRoleKey)
                    .HasConstraintName("FK__ViewExecu__Execu__59063A47");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewExecu__ViewK__571DF1D5");
            });

            modelBuilder.Entity<ViewTag>(entity =>
            {
                entity.HasKey(e => e.ViewTagKey)
                    .HasName("PK__ViewTags__C5C13BB3748E962A");

                entity.HasOne(d => d.TagKeyNavigation)
                    .WithMany(p => p.ViewTags)
                    .HasForeignKey(d => d.TagKey)
                    .HasConstraintName("FK__ViewTags__TagKey__5070F446");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewTags)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewTags__ViewKe__4F7CD00D");
            });

            modelBuilder.Entity<ViewType>(entity =>
            {
                entity.HasKey(e => e.ViewTypeKey)
                    .HasName("PK__ViewType__34E66869C3064045");

                entity.Property(e => e.ViewTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<VisitLog>(entity =>
            {
                entity.HasKey(e => e.VisitKey)
                    .HasName("PK__VisitLog__DC99CD125E7923F7");

                entity.Property(e => e.IpAddress).IsUnicode(false);

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.Property(e => e.VisitLastClickDate).HasColumnType("datetime");

                entity.HasOne(d => d.PasswordKeyNavigation)
                    .WithMany(p => p.VisitLogs)
                    .HasForeignKey(d => d.PasswordKey)
                    .HasConstraintName("FK__VisitLogs__Passw__6477ECF3");
            });

            modelBuilder.Entity<VisitView>(entity =>
            {
                entity.HasKey(e => e.VisitViewKey)
                    .HasName("PK__VisitVie__C5B0538B585EEEEB");

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.VisitViews)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__VisitView__ViewK__68487DD7");

                entity.HasOne(d => d.VisitKeyNavigation)
                    .WithMany(p => p.VisitViews)
                    .HasForeignKey(d => d.VisitKey)
                    .HasConstraintName("FK__VisitView__Visit__6754599E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
