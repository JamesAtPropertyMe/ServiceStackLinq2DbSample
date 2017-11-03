using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Funq;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using ServiceStack;

namespace ServiceStackLinq2DbSample
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("test", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            using (var db = new DataConnection("TestLinq2Db")) {
                var dbSchema = db.DataProvider.GetSchemaProvider().GetSchema(db);
                if (!dbSchema.Tables.Any(T => T.TableName.ToLower() == "exampleentity")) {
                    db.CreateTable<ExampleEntity>();
                }
            }

            MappingSchema.Default.SetDefaultFromEnumType(typeof(Enum), typeof(string));
            DataConnection.TurnTraceSwitchOn();
            DataConnection.WriteTraceLine = (s, s1) => Debug.WriteLine(s, s1);
        }
    }
}
