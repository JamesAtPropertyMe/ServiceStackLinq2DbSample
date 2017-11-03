using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using LinqToDB;
using ServiceStack;
using ServiceStack.Web;

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
            using (var db = new ExampleDb())
            {
                var data1 = new ExampleEntity() {
                    Id = Guid.NewGuid(),
                    Type = EntityType.TypeA,
                    Detail = "A TypeA record"
                };
                db.Insert(data1);
                // Generated SQL:
                // DECLARE @Id Guid
                // SET     @Id = 'e99c95f9-a54d-40f1-82b3-7d47c2a9461b'
                // DECLARE @Type1 VarChar(5) -- String
                // SET     @Type1 = 'TypeA'
                // DECLARE @Detail VarChar(14) -- String
                // SET     @Detail = 'A TypeA record'

                // INSERT INTO `ExampleEntity`
                // (
                //     `Id`,
                //     `Type`,
                //     `Detail`
                // )
                // VALUES
                // (
                //     @Id,
                //     @Type1,
                //     @Detail
                // )

                var data2 = new ExampleEntity() {
                    Id = Guid.NewGuid(),
                    Type = EntityType.TypeB,
                    Detail = "A TypeB record"
                };
                db.Insert(data2);
                // Generated SQL:
                // DECLARE @Id Guid
                // SET     @Id = 'b2e28b84-3f3a-4fd4-9483-ac57720f43e8'
                // DECLARE @Type1 VarChar(5) -- String
                // SET     @Type1 = 'TypeB'
                // DECLARE @Detail VarChar(14) -- String
                // SET     @Detail = 'A TypeB record'

                // INSERT INTO `ExampleEntity`
                // (
                //     `Id`,
                //     `Type`,
                //     `Detail`
                // )
                // VALUES
                // (
                //     @Id,
                //     @Type1,
                //     @Detail
                // )


                var typeACount = db.ExampleEntities.Where(E => E.Type == EntityType.TypeA).Count();
                // Generated SQL:
                // SELECT
                // Count(*) as `cnt`
                // FROM
                // `ExampleEntity` `t1`
                // WHERE
                // `t1`.`Type` = '1'

                var typeBCount = db.ExampleEntities.Where(E => E.Type == EntityType.TypeB).Count();
                // Generated SQL:
                // SELECT
                // Count(*) as `cnt`
                // FROM
                // `ExampleEntity` `t1`
                // WHERE
                // `t1`.`Type` = '2'

                return new TestResponse() {
                    Message = $"TypeA Count: {typeACount}, TypeB Count: {typeBCount}"
                };
            }
        }
    }
}
