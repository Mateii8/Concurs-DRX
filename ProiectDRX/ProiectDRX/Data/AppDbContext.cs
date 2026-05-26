using Microsoft.EntityFrameworkCore;
using ProiectDRX.Models;

namespace ProiectDRX.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Asset> Assets { get; set; }
    public virtual DbSet<Complaint> Complaints { get; set; }
    public virtual DbSet<ComplaintComment> ComplaintComments { get; set; }
    public virtual DbSet<ComplaintWorkflow> ComplaintWorkflows { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Asset__D28B561DE9362293");
            entity.ToTable("Asset");
            entity.HasIndex(e => e.SerialNumber, "UQ__Asset__BED14FEE2368C22E").IsUnique();

            entity.Property(e => e.AssetId).HasColumnName("asset_id");
            entity.Property(e => e.EmplId).HasColumnName("empl_id");
            entity.Property(e => e.Name).HasMaxLength(150).IsUnicode(false).HasColumnName("name");
            entity.Property(e => e.SerialNumber).HasMaxLength(100).IsUnicode(false).HasColumnName("serial_number");

            entity.HasOne(d => d.Empl).WithMany(p => p.Assets)
                .HasForeignKey(d => d.EmplId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_Employee");
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("PK__Complain__A771F61CFCAB4A73");
            entity.ToTable("Complaint");

            entity.Property(e => e.ComplaintId).HasColumnName("complaint_id");
            entity.Property(e => e.AssetId).HasColumnName("asset_id");
            entity.Property(e => e.EmplId).HasColumnName("empl_id");
            entity.Property(e => e.Title).HasMaxLength(150).IsUnicode(false).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnType("text").HasColumnName("description");
            entity.Property(e => e.Status).HasMaxLength(50).IsUnicode(false).HasColumnName("status");

            entity.HasOne(d => d.Asset).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Complaint_Asset");

            entity.HasOne(d => d.Empl).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.EmplId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Complaint_Employee");
        });

        modelBuilder.Entity<ComplaintComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Complain__E7957687BABBCE6C");
            entity.ToTable("Complaint_Comment");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.ComplaintId).HasColumnName("complaint_id");
            entity.Property(e => e.EmplId).HasColumnName("empl_id");
            entity.Property(e => e.Message).HasColumnType("text").HasColumnName("message");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime").HasColumnName("created_at");

            entity.HasOne(d => d.Complaint).WithMany(p => p.ComplaintComments)
                .HasForeignKey(d => d.ComplaintId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Complaint");

            entity.HasOne(d => d.Empl).WithMany(p => p.ComplaintComments)
                .HasForeignKey(d => d.EmplId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Employee");
        });

        modelBuilder.Entity<ComplaintWorkflow>(entity =>
        {
            entity.HasKey(e => e.WorkflowId).HasName("PK__Complain__64A76B703D7AE0D9");
            entity.ToTable("Complaint_Workflow");

            entity.Property(e => e.WorkflowId).HasColumnName("workflow_id");
            entity.Property(e => e.ComplaintId).HasColumnName("complaint_id");
            entity.Property(e => e.EmplId).HasColumnName("empl_id");
            entity.Property(e => e.OldStatus).HasMaxLength(50).IsUnicode(false).HasColumnName("old_status");
            entity.Property(e => e.CurrentStatus).HasMaxLength(50).IsUnicode(false).HasColumnName("current_status");
            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime").HasColumnName("changed_at");

            entity.HasOne(d => d.Complaint).WithMany(p => p.ComplaintWorkflows)
                .HasForeignKey(d => d.ComplaintId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workflow_Complaint");

            entity.HasOne(d => d.Empl).WithMany(p => p.ComplaintWorkflows)
                .HasForeignKey(d => d.EmplId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workflow_Employee");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__Departme__DCA65974360E686F");
            entity.ToTable("Department");
            entity.HasIndex(e => e.Name, "UQ__Departme__72E12F1B2C85BED3").IsUnique();

            entity.Property(e => e.DeptId).HasColumnName("dept_id");
            entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false).HasColumnName("name");
            entity.Property(e => e.ResponsibleEmplId).HasColumnName("responsible_empl_id");

            entity.HasOne(d => d.ResponsibleEmpl).WithMany(p => p.Departments)
                .HasForeignKey(d => d.ResponsibleEmplId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Department_ResponsibleEmployee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmplId).HasName("PK__Employee__47739219DA33166B");
            entity.ToTable("Employee");
            entity.HasIndex(e => e.Email, "UQ__Employee__AB6E6164B0EB0EF7").IsUnique();

            entity.Property(e => e.EmplId).HasColumnName("empl_id");
            entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false).HasColumnName("name");
            entity.Property(e => e.Email).HasMaxLength(150).IsUnicode(false).HasColumnName("email");
            entity.Property(e => e.Role).HasMaxLength(100).IsUnicode(false).HasColumnName("role");
            entity.Property(e => e.PasswordHash).HasMaxLength(255).IsUnicode(true).HasColumnName("password_hash");
            entity.Property(e => e.DeptId).HasColumnName("dept_id");
            entity.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
            entity.Property(e => e.LastLogin).HasColumnType("datetime").HasColumnName("last_login");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime").HasColumnName("created_at");

            entity.HasOne(d => d.Dept).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
