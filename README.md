# LogReader

Here is the initial release of the log reader program. It is built on [.NET 6,](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). This program can be run by either using the executable file directly, or through the command line. To run this program in the command line, simply navigate to the directory of the executable and run `.\LogReader`.

By default, the program looks for a file called `api.log` in the base directory of the C drive, and outputs a file called `api.csv` in the executable's directory. However, the command line enables you customize the following options:
```
  -i, --inputFile            The path to the log file you would like to process
  -o, --outputFile           The path to the log file you would like to write
  --includeMessage           Include the message for the event
  --includeStartTime         Include the start time of the event
  --includeEndTime           Include the end time of the event
  --includeTimeDifference    Include the time between the start event and end event
  --includeStartMessage      Include the start message for the event
  --includeEndMessage        Include the end message for the event
  --includeSeverity          Include the severity of the event
  --includeSource            Include the source of the event
  --includeIdentifier        Include the unique identifier of the event
  --help                     Display this help screen.
  --version                  Display version information.
```
The `includeMessage`, `includeStartTime`, `includeEndTime`, and `includeTimeDifference` are defaulted to true, with the other "include" options are defaulted to false.