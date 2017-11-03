using System;
using LinqToDB;
using LinqToDB.Data;

namespace ServiceStackLinq2DbSample
{
    // Script to create test schema and user for the project
    // CREATE SCHEMA `test_linq2db` DEFAULT CHARACTER SET utf8 ;
    // CREATE USER 'testlinq2dbuser'@'%' IDENTIFIED BY 'password';
    // GRANT ALL on test_linq2db.* to 'testlinq2dbuser'@'%';
public class ExampleDb : DataConnection
    {
        public ExampleDb() : base("TestLinq2Db") {}

        public ITable<ExampleEntity> ExampleEntities {
            get { return GetTable<ExampleEntity>(); }
        }
    }
}
