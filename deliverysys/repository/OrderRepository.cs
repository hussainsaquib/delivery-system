using Dapper;
using deliverysys.Models;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace deliverysys.repository
{
    public class OrderRepository
    {
        private readonly string? _connectionString;
        private IDbConnection Connection
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    throw new InvalidOperationException("Connection string has not been initialized.");
                }
                return new SqlConnection(_connectionString);
            }
        }

        public OrderRepository(IOptions<DatabaseSettings> settings)
        {
            _connectionString = settings.Value.ConnectionString;
            Console.WriteLine(_connectionString);  // For temporary debugging purposes
        }

        public int InsertOrder(OrderRequest req)
        {
            var query = "INSERT INTO Orders(CustomerName, OrderDetails) VALUES(@CustomerName, @OrderDetails); SELECT CAST(SCOPE_IDENTITY() as int);";
            using var db = Connection;
            return db.Query<int>(query, req).Single();
        }

        public void UpdateOrderWithDriver(int orderId, int driverId)
        {
            using var db = Connection;

            db.Open();  // Explicitly open the connection

            using var transaction = db.BeginTransaction(); // Start a transaction

            try
            {
                // Update the order with the driver
                var queryOrder = "UPDATE Orders SET DriverID = @driverId, OrderStatus = 'Assigned' WHERE OrderID = @orderId;";
                db.Execute(queryOrder, new { driverId, orderId }, transaction);

                // Update the driver's status to 'Busy'
                var queryDriver = "UPDATE Drivers SET DriverStatus = 'Busy' WHERE DriverID = @driverId;";
                db.Execute(queryDriver, new { driverId }, transaction);

                transaction.Commit(); // Commit the transaction
            }
            catch
            {
                transaction.Rollback(); // Rollback if any of the queries fail
                throw; // Re-throw the exception to be handled outside
            }
        }


        public IEnumerable<AvailableDriverResponse> GetAvailableDrivers()
        {
            var query = "SELECT DriverID, DriverName FROM Drivers WHERE DriverStatus = 'Available' ORDER BY NEWID();";
            using var db = Connection;
            return db.Query<AvailableDriverResponse>(query);
        }

    }
}
