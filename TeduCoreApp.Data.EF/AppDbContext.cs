using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TeduCoreApp.Data.EF.Configurations;
using TeduCoreApp.Data.EF.Extensions;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Interfaces;

namespace TeduCoreApp.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        // Đặt tên các bảng để  gen ra CSDL
        public DbSet<Advertistment> Advertistments { get; set; }
        public DbSet<Language> Languages { set; get; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Function> Functions { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

        public DbSet<Blog> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<Blog> Blogs { set; get; }
        public DbSet<BlogTag> BlogTags { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }

        public DbSet<Size> Sizes { set; get; }
        public DbSet<Slide> Slides { set; get; }

        public DbSet<Tag> Tags { set; get; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<WholePrice> WholePrices { get; set; }

        public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }

        public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }


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

        public override int SaveChanges()
        {
            // Lấy các trạng thái chỉnh sửa hoặc thêm mới để tự động add vào ngày tạo mà ngày chỉnh sửa.
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                var changedOrAddItem = item.Entity as IDateTracking;
                // Nếu changedOrAddItem != null có nghĩa là item này có triển khai IDateTracking
                if (changedOrAddItem != null)
                {
                    // Mỗi khi save sẽ tiến hành kiểm tra đê có thể tự kiểm tra để thêm vào thời gian tạo và chỉnh sữa mẫu tin.
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddItem.DateCreated  = DateTime.Now;
                    }
                    
                    changedOrAddItem.DateModified = DateTime.Now;
                }

            }
            return base.SaveChanges();
        }
    }
}
