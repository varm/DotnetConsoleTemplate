using DotnetConsoleTemplate.DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DotnetConsoleTemplate.Services
{
    public class CustomerService(ILogger logger, GenerDbContext dbContext)
    {
        private readonly GenerDbContext _dbContext = dbContext;
        private readonly ILogger _logger = logger;
        public async Task GetCustomerList()
        {
            _logger.Information("Get customer list: ");
            var customerList = await _dbContext.Customer.Where(a => a.CusID > 0).OrderByDescending(a => a.CusID).Take(10).ToListAsync();
            System.Console.WriteLine(String.Format("╭{0,10}¡{1,15}╮", "----------", "---------------"));
            System.Console.WriteLine(String.Format("|{0,10}|{1,15}|", "ID", "Name"));
            System.Console.WriteLine(String.Format("|{0,10}|{1,15}|", "----------", "---------------"));
            var stringBuilder = new System.Text.StringBuilder();
            foreach (var item in customerList)
            {
                stringBuilder.Append(String.Format("|{0,10}|{1,15}|\n", item.CusID, item.CusName));
                stringBuilder.Append(String.Format("|{0,10}|{1,15}|\n", "----------", "---------------"));
            }
            System.Console.WriteLine(stringBuilder.ToString());
            _logger.Information("Get customer list end.");
        }
    }
}