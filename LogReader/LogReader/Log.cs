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
        public bool isStart;

        private const string timeFormat = "yyyy-MM-dd HH:mm:ss,fff";
        private static readonly IFormatProvider culture = new CultureInfo("en-US");
        public string timeStampString {
            get {
                return timeStamp.ToString(timeFormat);
            }
        }

        //current log format
        //2021-08-02 12:18:54,549 templogger   WARNING  "d9729770-87a6-452d-a8d2-c3f7dc354db8" "Articles api starts"
        public Log(string log)
        {
            var datum = log.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var index = 0;
            timeStamp = DateTime.ParseExact($"{datum[index++]} {datum[index++]}", timeFormat, culture);
            source = datum[index++];
            level = datum[index++];
            id = Guid.Parse(datum[index++].Trim('"'));

            message = datum[index++].TrimStart('"');
            while (index < datum.Length)
                message += $" {datum[index++]}";
            message = message.TrimEnd('"');
            isStart = datum[index - 1].EndsWith("starts\"");
        }

        public override string ToString()
        {
            return $"{message}, {timeStamp}";
        }
    }
}
