using System.Collections.Generic;

namespace demo.ioc._05_Modeling
{
    public class AnInventoryController
    {
        private readonly IInventory _inventory;

        public AnInventoryController(IInventory inventory)
        {
            _inventory = inventory;
        }

        public InventoryResponse Get(InventoryRequest req)
        {
            //map HTTP Inv Req -> the intenal model
            var result = _inventory.GetInventory(req);

            return result;
        }
    }

    public class InventoryResponse
    {
    }

    public class InventoryRequest
    {
    }

    public interface IInventory
    {
        InventoryResponse GetInventory(InventoryRequest req);
    }

    public interface IDatabase
    {
        IList<T> ExecuteSql<T>(string sql);
    }

    public class Inventory : IInventory
    {
        private readonly IDatabase _database;

        public Inventory(IDatabase database)
        {
            _database = database;
        }

        public InventoryResponse GetInventory(InventoryRequest req)
        {
            var records = _database.ExecuteSql<InventoryRecord>("SELECT * FROM [Inventory]");

            return Convert(records);
        }

        public InventoryResponse Convert(IEnumerable<InventoryRecord> records)
        {
            //convert records -> InventoryResponse (by hand / AutoMapper)
            return new InventoryResponse();
        }
    }

    public class InventoryRecord
    {
        
    }
}