using System;
using CommandLine;

using Dipsy.VeracodeReport.Converter.Interfaces;

namespace Dipsy.VeracodeReport.Converter
{
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

                csvFlawWriter.Write(detailedXml, options);
                csvAnalysisWriter.Write(detailedXml, options);
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
