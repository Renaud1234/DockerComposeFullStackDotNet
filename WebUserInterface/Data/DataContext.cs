using Microsoft.EntityFrameworkCore;
using WebUserInterface.Models;

namespace WebUserInterface.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecast { get; set; }
    }
}