using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AplicationMessanger.Data;

public class AplicationMessangerContext : IdentityDbContext<AplicationMessangerUser>
{
    public AplicationMessangerContext(DbContextOptions<AplicationMessangerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AplicationMessangerUser>()
            .HasMany(u => u.Chats)
            .WithMany(c => c.Users);
        builder.Entity<Chat>()
            .HasMany(c => c.Users)
            .WithMany(c => c.Chats);


        //builder.Entity<ChatUsers>()
        //    .HasOne(C => C.Сhat)
        //    .WithMany(u => u.UserChat)
        //    .HasForeignKey(i => i.ChatId);
        //builder.Entity<ChatUsers>()
        //    .HasOne(C => C.User)
        //    .WithMany(u => u.UserChat)
        //    .HasForeignKey(i => i.UserId);
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
}
