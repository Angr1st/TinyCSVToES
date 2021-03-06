﻿using CSVToESLib;
using CSVToESLib.Template;
using CSVToESLib.Types;
using Nest;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace TinyCSVToES
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //var filePath = "testData.csv";
            //var test = CsvImporterGenerator.CreateBulkPriceImporterType(GetHeaders(filePath));

            //var taskResult = await test.ImportCsv(null, filePath, 1);
            //WriteLine(taskResult);
            //var person = new CSVToESLib.Template.Person() { FirstName = "test", BirthDay = "30.12.2012", LastName = "Lol", Version = 1 };
            //var innerIndex = new CSVToESLib.Template.InnerIndex();
            //var index = new CSVToESLib.Template.Index(person);
            //File.WriteAllText("test1.json", innerIndex.ToString(), System.Text.Encoding.UTF8);
            //File.WriteAllText("test.json", person.ToString(), System.Text.Encoding.UTF8);
            //File.WriteAllText("test3.json", index.ToString(), System.Text.Encoding.UTF8);
            var typeNames = new TypeNames("Person");
            var filePath2 = "testData3.csv";
            var csvImporter = CsvImporterGenerator.CreateICsvImporterType(GetHeaders(filePath2), typeNames);
            var settings = new ConnectionSettings().DefaultIndex("persons");
            var result = await csvImporter.ImportCsv(settings, filePath2, 1);

            ReadLine();
        }

        public static string[] GetHeaders(string filePath) => File.ReadAllLines(filePath).First().Split(';').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
    }
}
