using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class TransferUnitTest
{
    private ModelContext Context;

    public TransferUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetTransferTest()
    {
        // Given
        Context.Transfers.Add(new Transfer
        {
            Id = 2,
            Reference = "TR00002",
            TransferFrom = 9229,
            TransferTo = 9284,
            TransferStatus = "Completed",
            CreatedAt = DateTime.Parse("2017-09-19T00:33:14Z").ToString(),
            UpdatedAt = DateTime.Parse("2017-09-20T01:33:14Z").ToString(),
            Items = new List<TransferItem>
            {
                new TransferItem
                {
                    id = 5,
                    tranfer_item_id = "5",
                    amount = 100,
                    TransferId = 2

                }
            }
        });

        Context.SaveChanges();

        var transferService = new TransferService(Context);

        // When
        Transfer? transfer = transferService.Get(2);

        // Then
        Assert.Equal("TR00002", transfer.Reference);
    }

    [Fact]
    public void GetAllTransferTest()
    {
        // Given
        Context.Transfers.Add(new Transfer
        {
            Id = 3,
            Reference = "TR00002",
            TransferFrom = 9229,
            TransferTo = 9284,
            TransferStatus = "Completed",
            CreatedAt = DateTime.Parse("2017-09-19T00:33:14Z").ToString(),
            UpdatedAt = DateTime.Parse("2017-09-20T01:33:14Z").ToString(),
            Items = new List<TransferItem>
            {
                new TransferItem
                {
                    id = 5,
                    tranfer_item_id = "5",
                    amount = 100,
                    TransferId = 2

                }
            }
        });
        Context.Transfers.Add(new Transfer
        {
            Id = 4,
            Reference = "TR00002",
            TransferFrom = 9229,
            TransferTo = 9284,
            TransferStatus = "Completed",
            CreatedAt = DateTime.Parse("2017-09-19T00:33:14Z").ToString(),
            UpdatedAt = DateTime.Parse("2017-09-20T01:33:14Z").ToString(),
            Items = new List<TransferItem>
            {
                new TransferItem
                {
                    id = 6,
                    tranfer_item_id = "5",
                    amount = 100,
                    TransferId = 2

                }
            }
        });
        Context.SaveChanges();

        var transferService = new TransferService(Context);

        // When
        List<Transfer> transfers = transferService.GetAll();

        // Then
        Assert.Equal(2, transfers.Count);
    }

    [Fact]
    public void CreateTransferTest()
    {
        // Given
        Transfer transfer = new Transfer
        {
            Id = 4,
            Reference = "TR00002",
            TransferFrom = 9229,
            TransferTo = 9284,
            TransferStatus = "Success",
            CreatedAt = DateTime.Parse("2017-09-19T00:33:14Z").ToString(),
            UpdatedAt = DateTime.Parse("2017-09-20T01:33:14Z").ToString(),
            Items = new List<TransferItem>
            {
                new TransferItem
                {
                    id = 5,
                    tranfer_item_id = "5",
                    amount = 100,
                    TransferId = 2

                }
            }
        };
        var transferService = new TransferService(Context);

        // When
        transferService.Post(transfer);
        Context.SaveChanges();

        // Then
        Transfer? transfer1 = transferService.Get(transfer.Id);
        Assert.Equal("Success", transfer1.TransferStatus);
    }

    [Fact]
    public void UpdateTransferTest()
    {
        // Given
        Context.Transfers.Add(new Transfer
        {
            Id = 4,
            Reference = "TR00002",
            TransferFrom = 9229,
            TransferTo = 9284,
            TransferStatus = "Completed",
            CreatedAt = DateTime.Parse("2017-09-19T00:33:14Z").ToString(),
            UpdatedAt = DateTime.Parse("2017-09-20T01:33:14Z").ToString(),
            Items = new List<TransferItem>
            {
                new TransferItem
                {
                    id = 5,
                    tranfer_item_id = "5",
                    amount = 100,
                    TransferId = 2

                }
            }
        });
        Context.SaveChanges();

        var transferService = new TransferService(Context);

        // When
        transferService.Put(new Transfer
        {
            Id = 4,
            Reference = "TR00002",
            TransferFrom = 9229,
            TransferTo = 9284,
            TransferStatus = "new Status",
            CreatedAt = DateTime.Parse("2017-09-19T00:33:14Z").ToString(),
            UpdatedAt = DateTime.Parse("2017-09-20T01:33:14Z").ToString(),
            Items = new List<TransferItem>
            {
                new TransferItem
                {
                    id = 5,
                    tranfer_item_id = "5",
                    amount = 100,
                    TransferId = 2

                }
            }
        });
        Context.SaveChanges();

        // Then

        Transfer transfer = transferService.Get(4)!;
        Assert.Equal("new Status", transfer.TransferStatus);
    }

    [Fact]
    public void DeleteTransferTest()
    {
        // Given
        Context.Transfers.Add(new Transfer
        {
            Id = 4,
            Reference = "TR00002",
            TransferFrom = 9229,
            TransferTo = 9284,
            TransferStatus = "Completed",
            CreatedAt = DateTime.Parse("2017-09-19T00:33:14Z").ToString(),
            UpdatedAt = DateTime.Parse("2017-09-20T01:33:14Z").ToString(),
            Items = new List<TransferItem>
            {
                new TransferItem
                {
                    id = 5,
                    tranfer_item_id = "5",
                    amount = 100,
                    TransferId = 2

                }
            }
        });
        Context.SaveChanges();

        var transferService = new TransferService(Context);

        // When
        bool result = transferService.Delete(4);

        // Then
        Assert.True(result);
        Assert.Null(transferService.Get(4));
    }


    [Fact]
    public void GetItemsInTransferTest()
    {
        // Given
        Context.Transfers.Add(new Transfer
        {
            Id = 4,
            Reference = "TR00002",
            TransferFrom = 9229,
            TransferTo = 9284,
            TransferStatus = "Completed",
            CreatedAt = DateTime.Parse("2017-09-19T00:33:14Z").ToString(),
            UpdatedAt = DateTime.Parse("2017-09-20T01:33:14Z").ToString(),
            Items = new List<TransferItem>
            {
                new TransferItem
                {
                    id = 5,
                    tranfer_item_id = "5",
                    amount = 100,
                    TransferId = 2

                }
            }
        });
        Context.SaveChanges();
    
        // When
        var transferService = new TransferService(Context);

        List<TransferItem>? items = transferService.GetItemsInTransfer(4);

        // Then
        Assert.Single(items);
        Assert.Equal(5, items[0].id);

    }
}
