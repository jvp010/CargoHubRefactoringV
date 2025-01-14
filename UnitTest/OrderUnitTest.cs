using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class OrderUnitTest
{
    private ModelContext Context;

    public OrderUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetOrderTest()
    {
        // Given
        Context.Orders.Add(new Order
        {
            Id = 1,
            SourceId = 33,
            Notes = "testnode",
            ReferenceExtra = "testreferenceextra",
            ShippingNotes = "testshippingnotes",
            OrderDate = DateTime.Parse("2019-04-03T11:33:15Z"),
            RequestDate = DateTime.Parse("2019-04-07T11:33:15Z"),
            Reference = "ORD00001",
            OrderStatus = "Delivered",
            TotalAmount = 9905,
            CreatedAt = DateTime.Parse("2019-04-03T11:33:15Z").ToString(),
            UpdatedAt = DateTime.Parse("2019-04-05T07:33:15Z").ToString(),
            Items = new List<OrderItem>
            {
                new OrderItem { Id = 7435, Amount = 23, OrderItemId = "4", OrderId = 1 }
            }
        });

        Context.SaveChanges();

        var orderService = new CrudService<Order>(Context);

        // When
        Order order = orderService.Get(1);

        // Then
        Assert.Equal("ORD00001", order.Reference);
        Assert.Equal(23, order.Items.First().Amount);
    }

    [Fact]
    public void GetAllOrdersTest()
    {
        // Given
        Context.Orders.Add(new Order
        {
            Id = 1,
            SourceId = 33,
            OrderDate = DateTime.Parse("2019-04-03T11:33:15Z"),
            RequestDate = DateTime.Parse("2019-04-07T11:33:15Z"),
            Notes = "testnode",
            ReferenceExtra = "testreferenceextra",
            ShippingNotes = "testshippingnotes",
            Reference = "ORD00001",
            OrderStatus = "Delivered",
            TotalAmount = 9905,
            CreatedAt = DateTime.Parse("2019-04-03T11:33:15Z").ToString(),
            UpdatedAt = DateTime.Parse("2019-04-05T07:33:15Z").ToString(),
            Items = new List<OrderItem>
            {
                new OrderItem { Id = 7435, Amount = 23, OrderItemId = "4", OrderId = 1 }
            }
        });

        Context.Orders.Add(new Order
        {
            Id = 2,
            SourceId = 34,
            OrderDate = DateTime.Parse("2019-04-04T12:35:20Z"),
            RequestDate = DateTime.Parse("2019-04-08T12:35:20Z"),
            Notes = "testnode",
            ReferenceExtra = "testreferenceextra",
            ShippingNotes = "testshippingnotes",
            Reference = "ORD00002",
            OrderStatus = "Pending",
            TotalAmount = 5300,
            CreatedAt = DateTime.Parse("2019-04-04T12:35:20Z").ToString(),
            UpdatedAt = DateTime.Parse("2019-04-06T10:22:15Z").ToString(),
            Items = new List<OrderItem>
            {
                new OrderItem { Id = 7436, Amount = 10, OrderItemId = "5", OrderId = 2 }
            }
        });

        Context.SaveChanges();

        var orderService = new CrudService<Order>(Context);

        // When
        List<Order> orders = orderService.GetAll();

        // Then
        Assert.Equal(2, orders.Count);
    }

    [Fact]
    public void DeleteOrderTest()
    {
        // Given
        Context.Orders.Add(new Order
        {
            Id = 1,
            SourceId = 33,
            OrderDate = DateTime.Parse("2019-04-03T11:33:15Z"),
            RequestDate = DateTime.Parse("2019-04-07T11:33:15Z"),
            Notes = "testnode",
            ReferenceExtra = "testreferenceextra",
            ShippingNotes = "testshippingnotes",
            Reference = "ORD00001",
            OrderStatus = "Delivered",
            TotalAmount = 9905,
            CreatedAt = DateTime.Parse("2019-04-03T11:33:15Z").ToString(),
            UpdatedAt = DateTime.Parse("2019-04-05T07:33:15Z").ToString(),
            Items = new List<OrderItem>
            {
                new OrderItem { Id = 7435, Amount = 30, OrderItemId = "4", OrderId = 1 }
            }
        });

        Context.SaveChanges();

        var orderService = new CrudService<Order>(Context);

        // When
        bool result = orderService.Delete(1);

        // Then
        Assert.True(result);
        Assert.Null(orderService.Get(1));
    }

    [Fact]
    public void UpdateOrderTest()
    {
        // Given
        Context.Orders.Add(new Order
        {
            Id = 1,
            SourceId = 33,
            OrderDate = DateTime.Parse("2019-04-03T11:33:15Z"),
            RequestDate = DateTime.Parse("2019-04-07T11:33:15Z"),
            Notes = "testnode",
            ReferenceExtra = "testreferenceextra",
            ShippingNotes = "testshippingnotes",
            Reference = "ORD00001",
            OrderStatus = "Delivered",
            TotalAmount = 9905,
            CreatedAt = DateTime.Parse("2019-04-03T11:33:15Z").ToString(),
            UpdatedAt = DateTime.Parse("2019-04-05T07:33:15Z").ToString(),
            Items = new List<OrderItem>
            {
                new OrderItem { Id = 7435, Amount = 23, OrderItemId = "4", OrderId = 1 }
            }
        });

        Context.SaveChanges();

        var orderService = new CrudService<Order>(Context);

        // When
        orderService.Put(new Order
        {
            Id = 1,
            SourceId = 33,
            OrderDate = DateTime.Parse("2019-04-03T11:33:15Z"),
            RequestDate = DateTime.Parse("2019-04-07T11:33:15Z"),
            Reference = "ORD00001",
            OrderStatus = "Shipped", // Updated status
            TotalAmount = 11000, // Updated total amount
            CreatedAt = DateTime.Parse("2019-04-03T11:33:15Z").ToString(),
            UpdatedAt = DateTime.Parse("2019-04-05T07:33:15Z").ToString(),
            Items = new List<OrderItem>
            {
                new OrderItem { Id = 7435, Amount = 30, OrderItemId = "4", OrderId = 1 }
            }
        });

        // Then
        Order updatedOrder = orderService.Get(1);
        Assert.Equal("Shipped", updatedOrder.OrderStatus);
        Assert.Equal(11000, updatedOrder.TotalAmount);
        Assert.Equal(30, updatedOrder.Items.First().Amount);
    }

    [Fact]
    public void GetItemsInOrderTest()
    {
        var holder = new List<OrderItem>
            {
                new OrderItem { Id = 7435, Amount = 23, OrderItemId = "4", OrderId = 1 }
            };
        // Given
        Context.Orders.Add(new Order
        {
            Id = 1,
            SourceId = 33,
            OrderDate = DateTime.Parse("2019-04-03T11:33:15Z"),
            RequestDate = DateTime.Parse("2019-04-07T11:33:15Z"),
            Notes = "testnode",
            ReferenceExtra = "testreferenceextra",
            ShippingNotes = "testshippingnotes",
            Reference = "ORD00001",
            OrderStatus = "Delivered",
            TotalAmount = 9905,
            CreatedAt = DateTime.Parse("2019-04-03T11:33:15Z").ToString(),
            UpdatedAt = DateTime.Parse("2019-04-05T07:33:15Z").ToString(),
            Items = holder
        });

        Context.SaveChanges();

        var orderService = new OrderService(Context);
        // When
        var result = orderService.GetItemsInOrder(1);
    
        // Then
        Assert.Equal(result[0].Id, holder[0].Id);
    }
}
