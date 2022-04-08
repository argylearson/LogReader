using CommandLine;
using System.Text;

namespace LogReader
{
    public class Program
    {
        private static string inputFilePath = @"c:\api.log";
        private static string outputFilePath = $"{Directory.GetCurrentDirectory()}\\api.csv";
        private static string headers = "Log Message, Start Time, End Time, Time Diff\n";
        private static Dictionary<Guid, Log> startActivities = new Dictionary<Guid, Log>();

        public async static Task Main(string[] args)
        {
            try
            {
                //check for command line arguments
                ParseOptions(args);

                using var file = File.Create(outputFilePath);
                await file.WriteAsync(new UTF8Encoding(true).GetBytes(headers));

                foreach (var row in File.ReadLines(inputFilePath))
                {
                    var log = new Log(row);

                    //we found a match
                    if (startActivities.TryGetValue(log.id, out var existingActivity))
                    {
                        //we would expect 'start' entries to always come first. however, we'll account for weirdness in case we make this async
                        var line = log.isStart ? WriteActivity(log, existingActivity) : WriteActivity(existingActivity, log);
                        await file.WriteAsync(new UTF8Encoding(true).GetBytes(line));
                    }
                    else
                        startActivities.Add(log.id, log);
                }

                file.Close();
            }
            catch
            {
                throw;
            }
        }

        private static void ParseOptions(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed<CommandLineOptions>(o =>
            {
                if (!string.IsNullOrEmpty(o.InputLogPath))
                    inputFilePath = o.InputLogPath;
                if (!string.IsNullOrEmpty (o.OutputLogPath))
                    outputFilePath = o.OutputLogPath;
            }
            );
        }

        private static string WriteActivity(Log startLog, Log endLog)
        {
            return $"{startLog.message} - {endLog.message}, {startLog.timeStampString}, {endLog.timeStampString}, {(endLog.timeStamp - startLog.timeStamp).TotalSeconds}\n";
        }
    }
}
