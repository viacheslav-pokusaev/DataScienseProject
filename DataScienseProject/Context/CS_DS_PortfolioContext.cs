using DataScienseProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#nullable disable

namespace DataScienseProject.Context
{
    public partial class CS_DS_PortfolioContext : DbContext
    {
        
        public CS_DS_PortfolioContext()
        {
        }

        public CS_DS_PortfolioContext(DbContextOptions<CS_DS_PortfolioContext> options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.HasKey(e => e.DirectionKey)
                    .HasName("PK__Directio__D7F5391A1458C355");

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.ElementKey)
                    .HasName("PK__Elements__5616FCCF67B317E9");

                entity.Property(e => e.ElementName).IsUnicode(false);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.Property(e => e.Text).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.ElementTypeKeyNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.ElementTypeKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Elements__Elemen__2C3393D0");

                entity.HasOne(d => d.LinkTypeKeyNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.LinkTypeKey)
                    .HasConstraintName("FK__Elements__LinkTy__2D27B809");
            });

            modelBuilder.Entity<ElementParameter>(entity =>
            {
                entity.HasKey(e => e.ElementParameterKey)
                    .HasName("PK__ElementP__43326167F1BB6E11");

                entity.Property(e => e.Key).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.ElementKeyNavigation)
                    .WithMany(p => p.ElementParameters)
                    .HasForeignKey(d => d.ElementKey)
                    .HasConstraintName("FK__ElementPa__Eleme__300424B4");
            });

            modelBuilder.Entity<ElementType>(entity =>
            {
                entity.HasKey(e => e.ElementTypeKey)
                    .HasName("PK__ElementT__49CB5506B482EAB1");

                entity.Property(e => e.ElementTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Executor>(entity =>
            {
                entity.HasKey(e => e.ExecutorKey)
                    .HasName("PK__Executor__D96DE1014D93BFAE");

                entity.Property(e => e.ExecutorName).IsUnicode(false);

                entity.Property(e => e.ExecutorProfileLink).IsUnicode(false);
            });

            modelBuilder.Entity<ExecutorRole>(entity =>
            {
                entity.HasKey(e => e.ExecutorRoleKey)
                    .HasName("PK__Executor__3AE9E647FA2BE19B");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.FeedbackKey)
                    .HasName("PK__Feedback__AAB8A7CBF045E612");

                entity.ToTable("Feedback");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Text).IsUnicode(false);

                entity.HasOne(d => d.VisitKeyNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.VisitKey)
                    .HasConstraintName("FK__Feedback__VisitK__571DF1D5");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GroupKey)
                    .HasName("PK__Groups__36BB80D342725CBA");

                entity.Property(e => e.GroupName).IsUnicode(false);
            });

            modelBuilder.Entity<GroupView>(entity =>
            {
                entity.HasKey(e => e.GroupViewKey)
                    .HasName("PK__GroupVie__32CB5A7FB10D0124");

                entity.HasOne(d => d.GroupKeyNavigation)
                    .WithMany(p => p.GroupViews)
                    .HasForeignKey(d => d.GroupKey)
                    .HasConstraintName("FK__GroupView__Group__4AB81AF0");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.GroupViews)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__GroupView__ViewK__49C3F6B7");
            });

            modelBuilder.Entity<LinkType>(entity =>
            {
                entity.HasKey(e => e.LinkTypeKey)
                    .HasName("PK__LinkType__9FAC22697C42D49E");

                entity.Property(e => e.LinkTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Password>(entity =>
            {
                entity.HasKey(e => e.PasswordKey)
                    .HasName("PK__Password__B0327011FEBBCC4E");

                entity.HasOne(d => d.GroupKeyNavigation)
                    .WithMany(p => p.Passwords)
                    .HasForeignKey(d => d.GroupKey)
                    .HasConstraintName("FK__Passwords__Group__4D94879B");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagKey)
                    .HasName("PK__Tags__CA370A7A443D9C8F");

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DirectionKeyNavigation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.DirectionKey)
                    .HasConstraintName("FK__Tags__DirectionK__38996AB5");
            });

            modelBuilder.Entity<View>(entity =>
            {
                entity.HasKey(e => e.ViewKey)
                    .HasName("PK__Views__93DADE713C8EBBB0");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.LogoPath).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ViewName).IsUnicode(false);

                entity.HasOne(d => d.ViewTypeKeyNavigation)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.ViewTypeKey)
                    .HasConstraintName("FK__Views__ViewTypeK__25869641");
            });

            modelBuilder.Entity<ViewElement>(entity =>
            {
                entity.HasKey(e => e.ViewElementKey)
                    .HasName("PK__ViewElem__01D9A696546BC718");

                entity.HasOne(d => d.ElementKeyNavigation)
                    .WithMany(p => p.ViewElements)
                    .HasForeignKey(d => d.ElementKey)
                    .HasConstraintName("FK__ViewEleme__Eleme__33D4B598");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewElements)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewEleme__ViewK__32E0915F");
            });

            modelBuilder.Entity<ViewExecutor>(entity =>
            {
                entity.HasKey(e => e.ViewExecutorKey)
                    .HasName("PK__ViewExec__C9628F40351126F0");

                entity.HasOne(d => d.ExecutorKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ExecutorKey)
                    .HasConstraintName("FK__ViewExecu__Execu__440B1D61");

                entity.HasOne(d => d.ExecutorRoleKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ExecutorRoleKey)
                    .HasConstraintName("FK__ViewExecu__Execu__44FF419A");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewExecutors)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewExecu__ViewK__4316F928");
            });

            modelBuilder.Entity<ViewTag>(entity =>
            {
                entity.HasKey(e => e.ViewTagKey)
                    .HasName("PK__ViewTags__C5C13BB354279B8A");

                entity.HasOne(d => d.TagKeyNavigation)
                    .WithMany(p => p.ViewTags)
                    .HasForeignKey(d => d.TagKey)
                    .HasConstraintName("FK__ViewTags__TagKey__3C69FB99");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.ViewTags)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__ViewTags__ViewKe__3B75D760");
            });

            modelBuilder.Entity<ViewType>(entity =>
            {
                entity.HasKey(e => e.ViewTypeKey)
                    .HasName("PK__ViewType__34E66869DF3703AB");

                entity.Property(e => e.ViewTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<VisitLog>(entity =>
            {
                entity.HasKey(e => e.VisitKey)
                    .HasName("PK__VisitLog__DC99CD12AAD0AB8C");

                entity.Property(e => e.IpAddress).IsUnicode(false);

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.Property(e => e.VisitLastClickDate).HasColumnType("datetime");

                entity.HasOne(d => d.PasswordKeyNavigation)
                    .WithMany(p => p.VisitLogs)
                    .HasForeignKey(d => d.PasswordKey)
                    .HasConstraintName("FK__VisitLogs__Passw__5070F446");
            });

            modelBuilder.Entity<VisitView>(entity =>
            {
                entity.HasKey(e => e.VisitViewKey)
                    .HasName("PK__VisitVie__C5B0538B2027B5FA");

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.HasOne(d => d.ViewKeyNavigation)
                    .WithMany(p => p.VisitViews)
                    .HasForeignKey(d => d.ViewKey)
                    .HasConstraintName("FK__VisitView__ViewK__5441852A");

                entity.HasOne(d => d.VisitKeyNavigation)
                    .WithMany(p => p.VisitViews)
                    .HasForeignKey(d => d.VisitKey)
                    .HasConstraintName("FK__VisitView__Visit__534D60F1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
