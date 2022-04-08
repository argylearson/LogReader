namespace LogReader
{
    public class Program
    {
        private static string filePath = @"c:\api.log";
        private static Dictionary<string, Log> startActivities = new Dictionary<string, Log>();

        public static void Main(string[] args)
        {
            try
            {
                foreach (var row in File.ReadLines(filePath))
                {
                    var log = new Log(row);

                    var activityExists = startActivities.TryGetValue(log.message, out var existingActivity);

                    //we expect start activities to always preceed end activities
                    //so this will write to our changed file
                    if (activityExists)
                        Console.WriteLine(WriteActivity(existingActivity, log));
                    else
                        startActivities.Add(log.message, log);
                }
            } catch
            {
                throw;
            }
        }

        private static string WriteActivity(Log startLog, Log endLog)
        {
            return $"{startLog.message} - {endLog.message}, {startLog.timeStampString}, {endLog.timeStampString}, {(endLog.timeStamp - startLog.timeStamp).TotalSeconds}";
        }
    }
}
