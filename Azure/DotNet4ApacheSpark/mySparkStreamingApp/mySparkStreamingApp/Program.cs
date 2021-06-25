using System;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Streaming;
using static Microsoft.Spark.Sql.Functions;

namespace mySparkStreamingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            // Default to running on localhost:9999
            string hostname = "localhost";
            int port = 9999;

            // User designated their own host and port
            if (args.Length == 2)
            {
                hostname = args[0];
                port = int.Parse(args[1]);
            }

            // Create Spark session
            SparkSession spark = SparkSession
                .Builder()
                .AppName("Streaming example with a UDF")
                .GetOrCreate();

            // Create initial DataFrame
            DataFrame lines = spark
                .ReadStream()
                .Format("socket")
                .Option("host", hostname)
                .Option("port", port)
                .Load();

            // UDF to produce an array
            // Array includes:
            // 1) original string
            // 2) original string + length of original string
            Func<Column, Column> udfArray =
                Udf<string, string[]>((str) => new string[] { str, $"{str} {str.Length}" });

            // Explode array to rows
            DataFrame arrayDF = lines.Select(Explode(udfArray(lines["value"])));

            // Process and display each incoming line
            StreamingQuery query = arrayDF
                .WriteStream()
                .Format("console")
                .Start();

            query.AwaitTermination();
        }
    }
}
