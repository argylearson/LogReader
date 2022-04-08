using CommandLine;

namespace LogReader
{
    internal class CommandLineOptions
    {
        [Option('i', "inputFile", Required = false, HelpText = "The path to the log file you would like to process")]
        public string? InputLogPath { get; set; }

        [Option('o', "outputFile", Required = false, HelpText = "The path to the log file you would like to write")]
        public string? OutputLogPath { get; set; }
    }
}
