using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace EFCore2.Model
{
    public class ShopContext : DbContext
    {

        protected string connect_str = @"Data Source=WIN10;
                                         Initial Catalog=shopdata;
                                         User ID=sa;Password=12345";
        public DbSet<Product> products { set; get; }
        public DbSet<Category> categories { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            optionsBuilder.UseSqlServer(connect_str).UseLoggerFactory(loggerFactory).UseLazyLoadingProxies();
        }

        //Tao database
        public async Task CreateDatabase()
        {
            String databasename = Database.GetDbConnection().Database;

            Console.WriteLine("Tao" + databasename);
            bool result = await Database.EnsureCreatedAsync();

            string resulstring = result ? "Tao thanh cong" : "Da co truoc do";
            Console.WriteLine($"CSDL {databasename} : {resulstring}");
        }

        //xoa database
        public async Task DeleteDatabase()
        {
            String databasename = Database.GetDbConnection().Database;
            Console.WriteLine($"Co chan chan xoa {databasename} (y)?");
            string input = Console.ReadLine();

            if (input.ToLower() == "y")
            {
                bool deleted = await Database.EnsureDeletedAsync();
                string deletionInfo = deleted ? "đã xóa" : "không xóa được";
                Console.WriteLine($"{databasename} {deletionInfo}");
            }
        }

        //chen du lieu mau
        public async Task InsertSampleData()
        {
            // Thêm 2 danh mục vào Category
            var cate1 = new Category() { Name = "Cate1", Description = "Description1" };
            var cate2 = new Category() { Name = "Cate2", Description = "Description2" };
            await AddRangeAsync(cate1, cate2);
            await SaveChangesAsync();

            // Thêm 5 sản phẩm vào Products                       
            await AddRangeAsync(
                new Product() { Name = "Sản phẩm 1", Price = 12, Category = cate2 },
                new Product() { Name = "Sản phẩm 2", Price = 11, Category = cate2 },
                new Product() { Name = "Sản phẩm 3", Price = 33, Category = cate2 },
                new Product() { Name = "Sản phẩm 4(1)", Price = 323, Category = cate1 },
                new Product() { Name = "Sản phẩm 5(1)", Price = 333, Category = cate1 }

            );

            await SaveChangesAsync();

            // Các sản phầm chèn vào
            foreach (var item in products)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"ID: {item.ProductId}");
                stringBuilder.Append($"tên: {item.Name}");
                stringBuilder.Append($"Danh mục {item.CategoryId}({item.Category.Name})");
                Console.WriteLine(stringBuilder);

            }
        }

        //truy van lay ve san pha mtheo ID
        public async Task<Product> FindProduct(int id)
        {
            var p = await (from c in products where c.ProductId == id select c).FirstOrDefaultAsync();

            await Entry(p)
                .Reference(x => x.Category)
                .LoadAsync();
            return p;
        }

        public async Task<Category> FindCategory(int id)
        {

            var cate = await (from c in categories where c.CategoryId == id select c).FirstOrDefaultAsync();
            await Entry(cate)                     // lấy DbEntityEntry liên quan đến p
                   .Collection(cc => cc.products)  // lấy thuộc tính tập hợp, danh sách các sản phẩm
                   .LoadAsync();                   // nạp thuộc tính từ DB
            return cate;
        }
    }
}
