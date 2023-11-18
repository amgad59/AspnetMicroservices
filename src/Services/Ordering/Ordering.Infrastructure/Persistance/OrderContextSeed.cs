using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext,ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(
                    new List<Order>
                    {
                        new Order() {UserName = "swn", FirstName = "amgad", LastName = "Aly", EmailAddress = "amgad@gmail.com", AddressLine = "Gesr elsuiz", Country = "Egypt", TotalPrice = 350 }
                    }
                );
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Seed database associated with context {typeof(OrderContext).Name}");
            }
        }
    }
}
