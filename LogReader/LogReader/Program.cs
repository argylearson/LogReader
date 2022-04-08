using LogReader;

Console.WriteLine("Hello, World!");

var filePath = @"c:\api.log";

foreach (var row in File.ReadLines(filePath))
{
    var log = new Log(row);
    Console.WriteLine(log);
}