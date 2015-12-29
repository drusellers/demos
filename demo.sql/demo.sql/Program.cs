using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ploeh.AutoFixture;

namespace demo.sql
{
    class Program
    {
        private static string connection = "server=localhost;database=demo_sql;Trusted_Connection=true;Application Name=Fulfillment;";
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            var f = new Fixture();
            int oneHundredThousand=1000*100;
            int tenThousand=1000*10;
            var rows = f.CreateMany<Row>(oneHundredThousand);

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                truncate(conn);
                Console.WriteLine("Starting {0}", DateTime.Now);
                stopwatch.Start();
                insertViaSql(conn, rows);
                stopwatch.Stop();
                Console.WriteLine("Stopping {0}", DateTime.Now);

            }

            Console.WriteLine(stopwatch.Elapsed.TotalSeconds+"sec");
            Console.ReadKey();
        }

        private static void truncate(SqlConnection conn)
        {
            conn.Execute("TRUNCATE TABLE dbo.OrderHeaders");
        }

        static void insertViaSql(IDbConnection conn, IEnumerable<Row> rows)
        {

            using (var trx = conn.BeginTransaction())
            {

                var sql = @"INSERT INTO [dbo].[OrderHeaders]
           ([DateEntered]
           ,[ShipToFirstName]
           ,[ShipToLastName]
           ,[ShipToStreetAddress]
           ,[ShipToCity]
           ,[ShipToState]
           ,[ShipToZipCode]
           ,[PurchaseOrderNumber]
           ,[RetailerReferenceNumber]
           ,[OrderStatusId]
           ,[RetailerId]
           ,[DistributorId]
           ,[ShipmentMethodId])
     VALUES
           (@DateEntered
           ,@ShipToFirstName
           ,@ShipToLastName
           ,@ShipToStreetAddress
           ,@ShipToCity
           ,@ShipToState
           ,@ShipToZipCode
           ,@PurchaseOrderNumber
           ,@RetailerReferenceNumber
           ,@OrderStatusId
           ,@RetailerId
           ,@DistributorId
           ,@ShipmentMethodId)";
                conn.Execute(sql, rows, transaction: trx);

                trx.Commit();
            }
        }

        static void insertViaTableParameter(IDbConnection conn, IEnumerable<Row> rows)
        {
            var dt = ToDataTable(rows);
            dt.Columns.Remove("OrderHeaderId");
            using (var trx = conn.BeginTransaction())
            {
                var p = new
                {
                    TheDataList = dt.AsTableValuedParameter("InsertList")
                };
                var sql = @"Insert_OrderHeaders";
                conn.Execute(sql, p, transaction: trx, commandType: CommandType.StoredProcedure);
            }
        }

        public static DataTable ToDataTable<T>( IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }

    public class Row
    {
        public int OrderHeaderId { get; set; }
        public DateTime DateEntered { get; set; }
        public string ShipToFirstName { get; set; }
        public string ShipToLastName { get; set; }
        public string ShipToStreetAddress { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZipCode { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string RetailerReferenceNumber { get; set; }
        public int OrderStatusId { get; set; }
        public int RetailerId { get; set; }
        public int DistributorId { get; set; }
        public int ShipmentMethodId { get; set; }
    }
}


