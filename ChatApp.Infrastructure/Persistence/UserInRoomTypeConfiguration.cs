using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Persistence;

public class UserInRoomTypeConfiguration : IEntityTypeConfiguration<UserInRooms>
{
    public void Configure(EntityTypeBuilder<UserInRooms> builder)
    {
        builder.HasKey(e => new { e.IdRoom, e.IdUser });
        builder.HasOne(x => x.Room)
            .WithMany(x => x.Members)
            .HasForeignKey(x=>x.IdRoom);

        builder.HasOne(x => x.User)
            .WithMany(x => x.JoinedRooms)
            .HasForeignKey(x=>x.IdUser);
    }
}