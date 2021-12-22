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
                optionsBuilder.UseSqlServer("Server=.;Database=CS_DS_Portfolio;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.HasKey(e => e.DirectionKey)
                    .HasName("PK__Directio__D7F5391AB0B39526");

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.ElementKey)
                    .HasName("PK__Elements__5616FCCFA166AA5E");

                entity.Property(e => e.ElementName).IsUnicode(false);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.Property(e => e.Text).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.ElementTypeKeyNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.ElementTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Elements__Elemen__2D27B809");

                entity.HasOne(d => d.LinkTypeKeyNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.LinkTypeKey)
                    .HasConstraintName("FK__Elements__LinkTy__2E1BDC42");
            });

            modelBuilder.Entity<ElementParameter>(entity =>
            {
                entity.HasKey(e => e.ElementParameterKey)
                    .HasName("PK__ElementP__4332616766219A23");

                entity.Property(e => e.Key).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.ElementKeyNavigation)
                    .WithMany(p => p.ElementParameters)
                    .HasForeignKey(d => d.ElementKey)
                    .HasConstraintName("FK__ElementPa__Eleme__30F848ED");
            });

            modelBuilder.Entity<ElementType>(entity =>
            {
                entity.HasKey(e => e.ElementTypeKey)
                    .HasName("PK__ElementT__49CB5506F9F892A7");

                entity.Property(e => e.ElementTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Executor>(entity =>
            {
                entity.HasKey(e => e.ExecutorKey)
                    .HasName("PK__Executor__D96DE1011359B9EA");

                entity.Property(e => e.ExecutorName).IsUnicode(false);

                entity.Property(e => e.ExecutorProfileLink).IsUnicode(false);
            });

            modelBuilder.Entity<ExecutorRole>(entity =>
            {
                entity.HasKey(e => e.ExecutorRoleKey)
                    .HasName("PK__Executor__3AE9E64787B32801");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.FeedbackKey)
                    .HasName("PK__Feedback__AAB8A7CBF72EC3B6");

                entity.ToTable("Feedback");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Text).IsUnicode(false);

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__Feedback__ViewKe__59063A47");

                entity.HasOne(d => d.VisitKeyNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.VisitKey)
                    .HasConstraintName("FK__Feedback__VisitK__5812160E");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GroupKey)
                    .HasName("PK__Groups__36BB80D37733DCF5");

                entity.Property(e => e.GroupName).IsUnicode(false);
            });

            modelBuilder.Entity<GroupView>(entity =>
            {
                entity.HasKey(e => e.GroupViewKey)
                    .HasName("PK__GroupVie__32CB5A7FC6274B2D");

                entity.HasOne(d => d.GroupKeyNavigation)
                    .WithMany(p => p.GroupViews)
                    .HasForeignKey(d => d.GroupKey)
                    .HasConstraintName("FK__GroupView__Group__4BAC3F29");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.GroupViews)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__GroupView__ViewK__4AB81AF0");
            });

            modelBuilder.Entity<LinkType>(entity =>
            {
                entity.HasKey(e => e.LinkTypeKey)
                    .HasName("PK__LinkType__9FAC22697A418CF4");

                entity.Property(e => e.LinkTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Password>(entity =>
            {
                entity.HasKey(e => e.PasswordKey)
                    .HasName("PK__Password__B032701148183C40");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.PasswordValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.GroupKeyNavigation)
                    .WithMany(p => p.Passwords)
                    .HasForeignKey(d => d.GroupKey)
                    .HasConstraintName("FK__Passwords__Group__4E88ABD4");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagKey)
                    .HasName("PK__Tags__CA370A7A4594F11B");

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DirectionKeyNavigation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.DirectionKey)
                    .HasConstraintName("FK__Tags__DirectionK__398D8EEE");
            });

            modelBuilder.Entity<View>(entity =>
            {
                entity.HasKey(e => e.ViewKey)
                    .HasName("PK__Views__93DADE7120829A87");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.LogoPath).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ViewName).IsUnicode(false);

                entity.HasOne(d => d.ViewTypeKeyNavigation)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.ViewTypeKey)
                    .HasConstraintName("FK__Views__ViewTypeK__267ABA7A");
            });

            modelBuilder.Entity<ViewElement>(entity =>
            {
                entity.HasKey(e => e.ViewElementKey)
                    .HasName("PK__ViewElem__01D9A6965B5604CB");

                entity.HasOne(d => d.ElementKeyNavigation)
                    .WithMany(p => p.ViewElements)
                    .HasForeignKey(d => d.ElementKey)
                    .HasConstraintName("FK__ViewEleme__Eleme__34C8D9D1");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewElements)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewEleme__ViewK__33D4B598");
            });

            modelBuilder.Entity<ViewExecutor>(entity =>
            {
                entity.HasKey(e => e.ViewExecutorKey)
                    .HasName("PK__ViewExec__C9628F4007FCBAF4");

                entity.HasOne(d => d.ExecutorKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ExecutorKey)
                    .HasConstraintName("FK__ViewExecu__Execu__44FF419A");

                entity.HasOne(d => d.ExecutorRoleKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ExecutorRoleKey)
                    .HasConstraintName("FK__ViewExecu__Execu__45F365D3");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewExecu__ViewK__440B1D61");
            });

            modelBuilder.Entity<ViewTag>(entity =>
            {
                entity.HasKey(e => e.ViewTagKey)
                    .HasName("PK__ViewTags__C5C13BB3F948086F");

                entity.HasOne(d => d.TagKeyNavigation)
                    .WithMany(p => p.ViewTags)
                    .HasForeignKey(d => d.TagKey)
                    .HasConstraintName("FK__ViewTags__TagKey__3D5E1FD2");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewTags)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewTags__ViewKe__3C69FB99");
            });

            modelBuilder.Entity<ViewType>(entity =>
            {
                entity.HasKey(e => e.ViewTypeKey)
                    .HasName("PK__ViewType__34E668696E59070F");

                entity.Property(e => e.ViewTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<VisitLog>(entity =>
            {
                entity.HasKey(e => e.VisitKey)
                    .HasName("PK__VisitLog__DC99CD12E81732D6");

                entity.Property(e => e.IpAddress).IsUnicode(false);

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.Property(e => e.VisitLastClickDate).HasColumnType("datetime");

                entity.HasOne(d => d.PasswordKeyNavigation)
                    .WithMany(p => p.VisitLogs)
                    .HasForeignKey(d => d.PasswordKey)
                    .HasConstraintName("FK__VisitLogs__Passw__5165187F");
            });

            modelBuilder.Entity<VisitView>(entity =>
            {
                entity.HasKey(e => e.VisitViewKey)
                    .HasName("PK__VisitVie__C5B0538B58188750");

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.VisitViews)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__VisitView__ViewK__5535A963");

                entity.HasOne(d => d.VisitKeyNavigation)
                    .WithMany(p => p.VisitViews)
                    .HasForeignKey(d => d.VisitKey)
                    .HasConstraintName("FK__VisitView__Visit__5441852A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
