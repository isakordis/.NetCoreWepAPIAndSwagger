using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPIExample.Models;

namespace UnitTest
{
    public static class DbContextMocker
    {
        public static WebAPIContext GetWideWorldImportersDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<WebAPIContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new WebAPIContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
