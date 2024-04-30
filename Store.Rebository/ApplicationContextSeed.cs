using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Rebository
{
    public class ApplicationContextSeed
    {
        public static async Task seedAsync(ApplicationDbContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var BrandsDate = File.ReadAllText("../Store.Rebository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsDate);
                    if (brands != null)
                    {
                        await context.AddRangeAsync(brands);
                    }
                }

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typesDate = File.ReadAllText("../Store.Rebository/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesDate);
                    if (types != null)
                    {
                        await context.AddRangeAsync(types);
                    }
                }





                if (context.Products != null && !context.Products.Any())
                {
                    var productsDate = File.ReadAllText("../Store.Rebository/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsDate);
                    if (products != null)
                    {
                        await context.AddRangeAsync(products);
                    }
                }


                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var productsDate = File.ReadAllText("../Store.Rebository/SeedData/delivery.json");
                    var products = JsonSerializer.Deserialize<List<DeliveryMethod>>(productsDate);
                    if (products != null)
                    {
                        await context.AddRangeAsync(products);
                    }
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationContextSeed>();
                logger.LogError(ex.Message);
                
            }
        }
    }
}
