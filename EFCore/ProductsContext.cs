using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCore
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> products { set; get; }

        private const string connectionString = @"
                Data Source=WIN10;
                Initial Catalog=mydata;
                User ID=sa;Password=12345";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        public static async Task CreateDatabase()
        {
            using (var dbcontext = new ProductsContext())
            {
                String databaseName = dbcontext.Database.GetDbConnection().Database;

                Console.WriteLine("Tao "+ databaseName);
                bool result = await dbcontext.Database.EnsureCreatedAsync();
                string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
                Console.WriteLine($"CSDL {databaseName} : {resultstring}");
            }
        }

        public static async Task DeleteDatabase()
        {
            using (var context = new ProductsContext())
            {
                string databaseName = context.Database.GetDbConnection().Database;
                Console.WriteLine($"Co chan chan xoa {databaseName}? (y)");
                string input = Console.ReadLine();

                if (input.ToLower() == "y")
                {
                    bool deleted = await context.Database.EnsureDeletedAsync();
                    string deletionInfo = deleted ? "đã xóa" : "không xóa được";
                    Console.WriteLine($"{databaseName} {deletionInfo}");
                }
            }
        }

        public static async Task InsertProduct()
        {
            using (var context = new ProductsContext())
            {
                //await context.Products.AddAsync(new Product {
                //    Name = "San pham 1",
                //    Provider = "Cong ty 1"
                //});

                //await context.Products.AddAsync(new Product
                //{
                //    Name = "San pham 2",
                //    Provider = "Cong ty 2"
                //});

                var p1 = new Product() { Name = "Sản phẩm 3", Provider = "CTY A" };
                var p2 = new Product() { Name = "Sản phẩm 4", Provider = "CTY A" };
                var p3 = new Product() { Name = "Sản phẩm 5", Provider = "CTY B" };

                await context.AddRangeAsync(new object[] { p1, p2, p3 });

                int rows = await context.SaveChangesAsync();
                Console.WriteLine($"Da luu {rows} san pham");
            }
        }

        public static async Task ReadProducts()
        {
            using (var context = new ProductsContext())
            {
                //lay all sp
                var products = await context.products.ToListAsync();

                foreach(var product in products)
                {
                    Console.WriteLine($"{product.ProductId} {product.Name} ({product.Provider})");
                }

                Console.WriteLine();

                //Lay all sp cung cap boi cty A

                products = await (from p in context.products
                                  where (p.Provider == "CTY A")
                                  select p
                         )
                        .ToListAsync();

                Console.WriteLine("Sản phẩm CTY A");
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductId,2} {product.Name,10} - {product.Provider}");
                }
            }
        }

        public static async Task RenameProduct(int id, string newName)
        {
            using(var context = new ProductsContext())
            {
                //Timf kiem 
                var product = await (from p in context.products
                                     where (p.ProductId == id)
                                     select p).FirstOrDefaultAsync();

                //doi ten
                if(product != null)
                {
                    product.Name = newName;
                    Console.WriteLine($"{product.ProductId} co ten moi {product.Name}");
                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
