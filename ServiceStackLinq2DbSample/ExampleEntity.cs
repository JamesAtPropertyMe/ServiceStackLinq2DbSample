using System;
using LinqToDB.Mapping;

namespace ServiceStackLinq2DbSample
{
    [Table("ExampleEntity")]
    public class ExampleEntity
    {
        [PrimaryKey, Column(DbType = "char(36)"), NotNull]
        public Guid Id { get; set; }

        [Column(DbType = "varchar(50)"), NotNull]
        public EntityType Type { get; set; }

        [Column(DbType = "varchar(255)")]
        public string Detail { get; set; }
    }

    public enum EntityType
    {
        Unknown,
        TypeA,
        TypeB
    }
}
