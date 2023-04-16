using System.Collections.Generic;
using Inventofree.Module.Item.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.UnitTest;

public class ModuleExtensionTest
{
    [Fact]
    public void ShouldReturnServiceCollection()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();        
        serviceCollection.BuildServiceProvider();
        var configForDb = new Dictionary<string, string>
        {
            {"ConnectionStrings:Default", "Server=.,1433\\Catalog=TestDb;Database=TestDb;TrustServerCertificate=True"},
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configForDb)
            .Build();
        var options = new DbContextOptionsBuilder<ItemDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new ItemDbContext(options);
        serviceCollection.AddDbContext<ItemDbContext>(option =>
            option.UseInMemoryDatabase("TestDb"));
        // Assert
        var result = serviceCollection.AddItemModule(configuration);
        // Act
        result.ShouldNotBeNull();
    }
}