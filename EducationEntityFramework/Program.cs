using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EducationEntityFramework {
  class Program {
    private const string CONNECTION_STRING = "Data Source=IF979;Integrated Security=True;Database=Education";

    static void Main(string[] args) {
      //Context context = new Context();

      //foreach(var temp in context.Nodes) {
      //  Console.WriteLine(temp.Title);
      //}

      DataReaders data = new DataReaders(CONNECTION_STRING);

      //data.DataReaderProcess();
      //data.DataAdapterProcess();
      data.DataSetProcess();

      Console.ReadKey();
    }
  }

  class Context : DbContext {
    private readonly string _connString;

    public Context(string connString) {
      _connString = connString;
    }

    public DbSet<Node> Nodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlServer(_connString);
    }
  }

  [Table("Users")]
  class User {
    public int Id { get; set; }

    public string Login { get; set; }
  }

  [Table("Nodes")]
  class Node {
    public int Id { get; set; }

    public string Title { get; set; }
  }
}
