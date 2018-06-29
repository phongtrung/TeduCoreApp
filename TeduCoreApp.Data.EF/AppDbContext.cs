using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF.Configurations;
using TeduCoreApp.Data.EF.Extensions;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        // Đặt tên các bảng để  gen ra CSDL
        public DbSet<Advertistment> Advertistments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Indentity Config (Đổi tên các bảng mặc định)

            builder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogin").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<string>>().ToTable("AppUserRole").HasKey(x => new {x.RoleId, x.UserId});
            builder.Entity<IdentityUserToken<string>>().ToTable("AppUserToken").HasKey(x => x.UserId);
            #endregion

            // Lúc khởi tạo DB sẽ áp các cấu hình key của vào các Table khi khởi tạo trong CSDL
            builder.AddConfiguration(new TagConfiguration());
            builder.AddConfiguration(new AdvertistmentPositionConfiguration());
            builder.AddConfiguration(new BlogTagConfiguration());
            builder.AddConfiguration(new ContactDetailConfiguration());
            builder.AddConfiguration(new FooterConfiguration());
            builder.AddConfiguration(new FunctionConfiguration());
            builder.AddConfiguration(new PageConfiguration());
            builder.AddConfiguration(new ProductTagConfiguration());
            builder.AddConfiguration(new SystemConfigConfiguration());
            

            base.OnModelCreating(builder);
        }
    }
}
