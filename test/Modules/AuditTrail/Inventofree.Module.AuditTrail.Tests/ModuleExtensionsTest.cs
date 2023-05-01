using Inventofree.Module.AuditTrail.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace Inventofree.Module.AuditTrail.Tests;

public class ModuleExtensionsTest
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
        var options = new DbContextOptionsBuilder<AuditTrailDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new AuditTrailDbContext(options);
        serviceCollection.AddDbContext<AuditTrailDbContext>(option =>
            option.UseInMemoryDatabase("TestDb"));
        // Assert
        var result = serviceCollection.AddAuditTrailModule(configuration);
        // Act
        result.ShouldNotBeNull();
    }
}