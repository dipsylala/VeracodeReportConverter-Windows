using System;
using CommandLine;

using Dipsy.VeracodeReport.Converter.Interfaces;

namespace Dipsy.VeracodeReport.Converter
{
    using System.IO;
    using System.Text;

    public class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(HandleParsed);
        }

        private static void HandleParsed(Options options)
        {
            ILoader loader = new Loader();
            ICSVFormatter csvFormatter = new CSVFormatter();
            ICSVWriter csvFlawWriter = new CSVFlawWriter(csvFormatter);
            ICSVWriter csvAnalysisWriter = new CSVAnalysisWriter(csvFormatter);

            try
            {
                var detailedXml = loader.Parse(options.InputFileName);
                var flawOutputFilename = csvFlawWriter.GetOutputFilename(detailedXml, options);

                try
                {
                    using (var outFile = new StreamWriter(flawOutputFilename, false, Encoding.UTF8))
                    {
                        csvFlawWriter.Write(outFile, detailedXml, options);
                    }
                }
                catch (IOException)
                {
                    Console.Error.WriteLine($"Error writing to {flawOutputFilename}");
                }

                var scaOutputFilename = csvAnalysisWriter.GetOutputFilename(detailedXml, options);

                try
                {
                    using (var outFile = new StreamWriter(flawOutputFilename, false, Encoding.UTF8))
                    {
                        csvAnalysisWriter.Write(outFile, detailedXml, options);
                    }
                }
                catch (IOException)
                {
                    Console.Error.WriteLine($"Error writing to {scaOutputFilename}");
                }
            }
            catch (DetailedReportReadException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (CSVWriteException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
