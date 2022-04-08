using System.Text;

namespace LogReader
{
    public class Program
    {
        private static string inputFilePath = @"c:\api.log";
        private static string outputFilePath = $"{Directory.GetCurrentDirectory()}\\api.csv";
        private static Dictionary<string, Log> startActivities = new Dictionary<string, Log>();

        public async static Task Main(string[] args)
        {
            try
            {
                using var file = File.Create(outputFilePath);
                var linesToWrite = new List<string>();

                foreach (var row in File.ReadLines(inputFilePath))
                {
                    var log = new Log(row);

                    var activityExists = startActivities.TryGetValue(log.message, out var existingActivity);

                    //we expect start activities to always preceed end activities
                    //so this will write to our changed file
                    if (activityExists)
                        await file.WriteAsync(new UTF8Encoding(true).GetBytes(WriteActivity(existingActivity, log)));
                    //linesToWrite.Add(WriteActivity(existingActivity, log));
                    else
                        startActivities.Add(log.message, log);
                }

                //using var file = File.Create(outputFilePath);
                //await file.w//.WriteAllLinesAsync(linesToWrite);
                file.Close();
            }
            catch
            {
                throw;
            }
        }

        private static string WriteActivity(Log startLog, Log endLog)
        {
            return $"{startLog.message} - {endLog.message}, {startLog.timeStampString}, {endLog.timeStampString}, {(endLog.timeStamp - startLog.timeStamp).TotalSeconds}\n";
        }
    }
}
