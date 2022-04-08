using CommandLine;

namespace LogReader
{
    internal class CommandLineOptions
    {
        [Option('i', "inputFile", Required = false, HelpText = "The path to the log file you would like to process")]
        public string? InputLogPath { get; set; }

        [Option('o', "outputFile", Required = false, HelpText = "The path to the log file you would like to write")]
        public string? OutputLogPath { get; set; }



        [Option("includeMessage", Required = false, HelpText = "Include the message for the event")]
        public bool? IncludeMessage { get; set; }

        [Option("includeStartTime", Required = false, HelpText = "Include the start time of the event")]
        public bool? IncludeStartTime { get; set; }

        [Option("includeEndTime", Required = false, HelpText = "Include the end time of the event")]
        public bool? IncludeEndTime { get; set; }

        [Option("includeTimeDifference", Required = false, HelpText = "Include the time between the start event and end event")]
        public bool? IncludeTimeDifference { get; set; }



        [Option("includeStartMessage", Required = false, HelpText = "Include the start message for the event")]
        public bool? IncludeStartMessage { get; set; }

        [Option("includeEndMessage", Required = false, HelpText = "Include the end message for the event")]
        public bool? IncludeEndMessage { get; set; }

        [Option("includeSeverity", Required = false, HelpText = "Include the severity of the event")]
        public bool? IncludeSeverity { get; set; }

        [Option("includeSource", Required = false, HelpText = "Include the source of the event")]
        public bool? IncludeSource { get; set; }

        [Option("includeIdentifier", Required = false, HelpText = "Include the unique identifier of the event")]
        public bool? IncludeIdentifier { get; set; }
    }
}
