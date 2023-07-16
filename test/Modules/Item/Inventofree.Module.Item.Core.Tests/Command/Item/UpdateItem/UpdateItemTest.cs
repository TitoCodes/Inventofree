using AutoMapper;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Item.DeleteItem;
using Inventofree.Module.Item.Core.Command.Item.UpdateItem;
using Inventofree.Module.Item.Core.Dto.Price;
using Inventofree.Module.Item.Core.Enums;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Shared.Core.Exceptions;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.UpdateItem;

public class UpdateItemTest
{
    [Fact]
    public void ShouldUpdateItem()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        var mediatorMock = new Mock<IMediator>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>()
            {
                new()
                {
                    Id = 1, 
                    Name = "Name", 
                    Detail = "Details", 
                    CreatedBy = 1, 
                    UpdatedBy = 1,
                    CreatedDate = DateTimeOffset.Now, 
                    ModifiedDate = DateTimeOffset.Now,
                    Price = new Core.Entities.Price()
                    {
                        Amount = 1,
                        CreatedDate = DateTimeOffset.Now,
                        Currency = CurrencyType.Php
                    }
                }
            });
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Item>(It.IsAny<UpdateItemCommand>()))
            .Returns(new Core.Entities.Item() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        mapperMock.Setup(a => a.Map<Core.Entities.Price>(It.IsAny<PriceDto>()))
            .Returns(new Core.Entities.Price() { Amount = 10000 });
        mediatorMock.Setup(a => a.Send(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>()));
        mediatorMock.Setup(a => a.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()));
        var handler = new UpdateItemCommandHandler(itemDbContextMock.Object, userDbContextMock.Object, mediatorMock.Object, mapperMock.Object);
        //Act
        var result = handler.Handle(new UpdateItemCommand() { Id = 1, UpdatedBy = 1, Name = "Names", Price = new PriceDto(){ Amount = 15000}},
            new CancellationToken(false));
        //Assert
        itemDbContextMock.Verify(a => a.Items.Update( It.IsAny<Core.Entities.Item>()), Times.Once);
        itemDbContextMock.Verify(a => a.SaveChangesAsync( It.IsAny<CancellationToken>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowDuplicateNameException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        var mediatorMock = new Mock<IMediator>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>()
            {
                new()
                {
                    Id = 1, 
                    Name = "Name", 
                    Detail = "Details", 
                    CreatedBy = 1, 
                    UpdatedBy = 1,
                    CreatedDate = DateTimeOffset.Now, 
                    ModifiedDate = DateTimeOffset.Now,
                    Price = new Core.Entities.Price()
                    {
                        Amount = 1,
                        CreatedDate = DateTimeOffset.Now,
                        Currency = CurrencyType.Php
                    }
                },
                new()
                {
                    Id = 2, 
                    Name = "Name", 
                    Detail = "Details 2", 
                    CreatedBy = 1, 
                    UpdatedBy = 1,
                    CreatedDate = DateTimeOffset.Now, 
                    ModifiedDate = DateTimeOffset.Now,
                    Price = new Core.Entities.Price()
                    {
                        Amount = 1,
                        CreatedDate = DateTimeOffset.Now,
                        Currency = CurrencyType.Php
                    }
                }
            });
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Item>(It.IsAny<UpdateItemCommand>()))
            .Returns(new Core.Entities.Item() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        mapperMock.Setup(a => a.Map<Core.Entities.Price>(It.IsAny<PriceDto>()))
            .Returns(new Core.Entities.Price() { Amount = 10000 });
        mediatorMock.Setup(a => a.Send(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>()));
        mediatorMock.Setup(a => a.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()));
        var handler = new UpdateItemCommandHandler(itemDbContextMock.Object, userDbContextMock.Object, mediatorMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<DuplicateNameException>(() => handler.Handle(new UpdateItemCommand() { Id = 2, UpdatedBy = 1, Name = "Name", Price = new PriceDto(){ Amount = 15000}},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowUserNullInvalidOperationException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        var mediatorMock = new Mock<IMediator>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>()
            {
                new()
                {
                    Id = 1, 
                    Name = "Name", 
                    Detail = "Details", 
                    CreatedBy = 1, 
                    UpdatedBy = 1,
                    CreatedDate = DateTimeOffset.Now, 
                    ModifiedDate = DateTimeOffset.Now,
                    Price = new Core.Entities.Price()
                    {
                        Amount = 1,
                        CreatedDate = DateTimeOffset.Now,
                        Currency = CurrencyType.Php
                    }
                }
            });
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Item>(It.IsAny<UpdateItemCommand>()))
            .Returns(new Core.Entities.Item() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        mapperMock.Setup(a => a.Map<Core.Entities.Price>(It.IsAny<PriceDto>()))
            .Returns(new Core.Entities.Price() { Amount = 10000 });
        mediatorMock.Setup(a => a.Send(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>()));
        mediatorMock.Setup(a => a.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()));
        var handler = new UpdateItemCommandHandler(itemDbContextMock.Object, userDbContextMock.Object, mediatorMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new UpdateItemCommand() { Id = 1, UpdatedBy = 5, Name = "Names", Price = new PriceDto(){ Amount = 15000}},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowItemNullInvalidOperationException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        var mediatorMock = new Mock<IMediator>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>()
            {
                new()
                {
                    Id = 1, 
                    Name = "Name", 
                    Detail = "Details", 
                    CreatedBy = 1, 
                    UpdatedBy = 1,
                    CreatedDate = DateTimeOffset.Now, 
                    ModifiedDate = DateTimeOffset.Now,
                    Price = new Core.Entities.Price()
                    {
                        Amount = 1,
                        CreatedDate = DateTimeOffset.Now,
                        Currency = CurrencyType.Php
                    }
                }
            });
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Item>(It.IsAny<UpdateItemCommand>()))
            .Returns(new Core.Entities.Item() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        mapperMock.Setup(a => a.Map<Core.Entities.Price>(It.IsAny<PriceDto>()))
            .Returns(new Core.Entities.Price() { Amount = 10000 });
        mediatorMock.Setup(a => a.Send(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>()));
        mediatorMock.Setup(a => a.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()));
        var handler = new UpdateItemCommandHandler(itemDbContextMock.Object, userDbContextMock.Object, mediatorMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new UpdateItemCommand() { Id = 5, UpdatedBy = 1, Name = "Names", Price = new PriceDto(){ Amount = 15000}},
            new CancellationToken(false)));
    }
}