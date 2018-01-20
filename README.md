# PocoOrm

This is an simple ORM for .NET.

## Instalation

Today clone the repository & build the PocoOrm.Core & PocoOrm.SqlServer

## Usage

Create First you context, it should herit from PocoOrm.SqlServer.SqlContext.

````csharp
internal class Context : SqlContext
{
    public Context(SqlConnection connection, Options options) : base(connection, options)
    {
    }
}
````

Create after your table, define your table name with the Table attribute and your column with column attribute.
```csharp
[Table("Test")]
internal class Test
{
    [Column("Content", DbType.String)]
    public string Content { get; set; }

    [Column("Id", DbType.Int32)]
    public int Id { get; set; }
}
```

Add after to your context your differrent Repository. Each repository create a request on a specific table. The repository will be set in the constructor.

```csharp
    public IRepository<Test> Test { get; set; }
```

You could after create your request like

```csharp

IEnumerable<TestTable> result = await Context
                                        .Test
                                        .Select()
                                        .ExecuteAsync();

//create a request with a where clause
IEnumerable<TestTable> result = await Context
                                        .Test
                                        .Select()
                                        .Where(t => t.Content == "Salut")
                                        .ExecuteAsync();

```

## Exemple

See the project PocoOrm.Console
