using System.Collections.Generic;
using Inventofree.Module.User.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Inventofree.Module.User.UnitTest;

public class ModuleExtenstionTest
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
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new UserDbContext(options);
        serviceCollection.AddDbContext<UserDbContext>(option =>
            option.UseInMemoryDatabase("TestDb"));
        // Assert
        var result = serviceCollection.AddUserModule(configuration);
        // Act
        result.ShouldNotBeNull();
    }
}