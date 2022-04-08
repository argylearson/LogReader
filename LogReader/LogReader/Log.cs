using System.Globalization;

namespace LogReader
{
    internal class Log
    {
        public DateTime timeStamp;
        public string source;
        public string level;
        public Guid id;
        public string message;

        private const string timeFormat = "yyyy-MM-dd HH:mm:ss,fff";
        private static readonly IFormatProvider culture = new CultureInfo("en-US");

        //current log format
        //2021-08-02 12:18:54,549 templogger   WARNING  "d9729770-87a6-452d-a8d2-c3f7dc354db8" "Articles api starts"
        public Log(string log)
        {
            var datum = log.Split(' ');
            var index = 0;
            timeStamp = DateTime.ParseExact($"{datum[index++]} {datum[index++]}", timeFormat, culture);
        }

        public override string ToString()
        {
            return timeStamp.ToString("yyyy-MM-dd HH:mm:ss,fff");
        }
    }
}
