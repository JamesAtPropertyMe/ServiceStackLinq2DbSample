using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using LinqToDB;
using ServiceStack;
using ServiceStack.Web;
using System.Transactions;
using System.Diagnostics;

namespace ServiceStackLinq2DbSample
{
    [Route("/test", Verbs="GET")]
    public class TestRequest
    {

    }

    public class TestResponse
    {
        public string Message { get; set; }
    }

    public class TestService : Service
    {
        public TestResponse Get(TestRequest request)
        {
            using (var transaction = new TransactionScope()) {
                using (var db1 = new ExampleDb()) {
                    db1.Insert(new ExampleEntity() {
                        Id = Guid.NewGuid(),
                        Detail = "Record in db1",
                        Type = EntityType.TypeA
                    });
                }

                using (var db2 = new ExampleDb()) {

                    Debug.WriteLine($"count in db2: {db2.ExampleEntities.Count()}");

                    db2.Insert(new ExampleEntity() {
                        Id = Guid.NewGuid(),
                        Detail = "Record in db2",
                        Type = EntityType.TypeA
                    });

                    using (var db3 = new ExampleDb()) {

                        Debug.WriteLine($"count in db3: {db3.ExampleEntities.Count()}");

                        db3.Insert(new ExampleEntity() {
                            Id = Guid.NewGuid(),
                            Detail = "Record in db3",
                            Type = EntityType.TypeA
                        });
                    }
                }
            }
            using (var db = new ExampleDb())
            {
                var count = db.ExampleEntities.Count();

                return new TestResponse() {
                    Message = $"count {count}"
                };
            }
        }
    }
}
