using CommandLine;
using System.Text;

namespace LogReader
{
    public class Program
    {
        private static string inputFilePath = @"c:\api.log";
        private static string outputFilePath = $"{Directory.GetCurrentDirectory()}\\api.csv";
        private static string headers = "";
        private static Dictionary<Guid, Log> startActivities = new Dictionary<Guid, Log>();

        private static bool IncludeMessage = true;
        private static bool IncludeStartTime = true;
        private static bool IncludeEndTime = true;
        private static bool IncludeTimeDifference = true;
        private static bool IncludeStartMessage = false;
        private static bool IncludeEndMessage = false;
        private static bool IncludeSeverity = false;
        private static bool IncludeSource = false;
        private static bool IncludeIdentifier = false;

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

                if (IncludeMessage = o.IncludeMessage ?? IncludeMessage)
                {
                    headers += "Log Message, ";
                }
                if (IncludeStartTime = o.IncludeStartTime ?? IncludeStartTime)
                {
                    headers += "Start Time, ";
                }
                if (IncludeEndTime = o.IncludeEndTime ?? IncludeEndTime)
                {
                    headers += "End Time, ";
                }
                if (IncludeTimeDifference = o.IncludeTimeDifference ?? IncludeTimeDifference)
                {
                    headers += "Time Diff, ";
                }


                if (IncludeStartMessage = o.IncludeStartMessage ?? IncludeStartMessage)
                {
                    headers += "Start Message, ";
                }
                if (IncludeEndMessage = o.IncludeEndMessage ?? IncludeEndMessage)
                {
                    headers += "End Message, ";
                }
                if (IncludeSeverity = o.IncludeSeverity ?? IncludeSeverity)
                {
                    headers += "Severity, ";
                }
                if (IncludeSource = o.IncludeSource ?? IncludeSource)
                {
                    headers += "Source, ";
                }
                if (IncludeIdentifier = o.IncludeIdentifier ?? IncludeIdentifier)
                {
                    headers += "Identifier, ";
                }

                headers = $"{headers.Trim().TrimEnd(',')}\n";
            }
            );
        }

        private static string WriteActivity(Log startLog, Log endLog)
        {
            var result = "";
            if (IncludeMessage)
                result += $"{startLog.message} - {endLog.message}, ";
            if (IncludeStartTime)
                result += $"\"{startLog.timeStampString}\", ";
            if (IncludeEndTime)
                result += $"\"{endLog.timeStampString}\", ";
            if (IncludeTimeDifference)
                result += $"{(endLog.timeStamp - startLog.timeStamp).TotalSeconds}, ";
            if (IncludeStartMessage)
                result += $"{startLog.message}, ";
            if (IncludeEndMessage)
                result += $"{endLog.message}, ";
            //worry about if the severities are different?
            if (IncludeSeverity)
                result += $"{endLog.level}, ";
            //same question here
            if (IncludeSource)
                result += $"{endLog.source}, ";
            if (IncludeIdentifier)
                result += $"{endLog.id}, ";

            return $"{result.Trim().TrimEnd(',')}\n";
        }
    }
}
