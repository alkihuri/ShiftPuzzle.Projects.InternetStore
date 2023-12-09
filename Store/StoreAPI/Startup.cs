using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStore.Services;  
using MyStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Data.SQLite;

namespace MyStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    public async Task InitializeDatabaseAsync()
    {
        using (var connection = new SQLiteConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();

            // Создание таблицы
            var createTableCommand = new SQLiteCommand(
                @"CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Price REAL,
                    Stock INTEGER
                );", connection);

            await createTableCommand.ExecuteNonQueryAsync();

            // Добавление примерных данных
            var insertCommand = new SQLiteCommand(
                @"INSERT INTO Products (Name, Description, Price, Stock) VALUES 
                ('ProductName1', 'ProductDescription1', 99.99, 100),
                ('ProductName2', 'ProductDescription2', 89.99, 150), 
                ('ProductName10', 'ProductDescription10', 59.99, 200);", connection);

            await insertCommand.ExecuteNonQueryAsync();
        }
}

        // Этот метод вызывается средой выполнения. Используйте его для добавления сервисов в контейнер.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); 
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<ProductService>(provider => new ProductService(connectionString));
        }

        // Этот метод вызывается средой выполнения. Используйте его для настройки конвейера HTTP-запросов.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = InitializeDatabaseAsync();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
 
        }
    }
}
