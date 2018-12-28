using System;
using System.Data;
using System.Data.SqlClient;

namespace EducationEntityFramework {
  internal class DataReaders {
    private readonly string _connString;

    public DataReaders(string connString) {
      _connString = connString;
    }

    public void DataReaderProcess() {
      SqlConnection conn = new SqlConnection(_connString);
      var com = new SqlCommand("SELECT ID, Title FROM Nodes;SELECT ID, Login FROM Users", conn);

      using (conn) {
        conn.Open();

        SqlDataReader reader = com.ExecuteReader();

        while (reader.Read()) {
          Console.WriteLine($"Id: {reader.GetInt32(0)}\tTitle: {reader.GetString(1)}");
        }

        Console.WriteLine($"\n\n");

        reader.NextResult();

        while (reader.Read()) {
          Console.WriteLine($"Id: {reader.GetInt32(0)}\tTitle: {reader.GetString(1)}");
        }
      }
    }

    public void DataAdapterProcess() {
      SqlConnection conn = new SqlConnection(_connString);

      DataTable table = new DataTable("allPrograms");

      using (conn) {
        var command = "SELECT ID, Title FROM Nodes";

        using (var cmd = new SqlCommand(command, conn)) {
          SqlDataAdapter adapt = new SqlDataAdapter(cmd);

          conn.Open();
          adapt.Fill(table);
        }
      }

      foreach (DataRow row in table.Rows) {
        foreach (DataColumn col in table.Columns) {
          Console.Write($"{col}: {row[col]} ");
        }
        Console.WriteLine($"\n");
      }
    }

    public void DataSetProcess() {
      SqlConnection conn = new SqlConnection(_connString);

      DataSet set = new DataSet("allPrograms");

      using (conn) {
        var command = "SELECT ID, Title FROM Nodes;SELECT ID, Login FROM Users";

        using (var cmd = new SqlCommand(command, conn)) {
          SqlDataAdapter adapt = new SqlDataAdapter(cmd);

          conn.Open();
          adapt.Fill(set);
        }
      }

      foreach (DataTable table in set.Tables) {
        Console.WriteLine("Table " + table);
        foreach (DataRow row in table.Rows) {
          foreach (DataColumn col in table.Columns) {
            Console.Write($"{col}: {row[col]} ");
          }
          Console.WriteLine();
        }
        Console.WriteLine("\n");
      }
    }
  }
}
