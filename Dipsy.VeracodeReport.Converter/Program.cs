using System;
using System.IO;
using CommandLine;

namespace Dipsy.VeracodeReport.Converter
{
    using Dipsy.VeracodeReport.Converter.Schema;

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

            try
            {
                var detailedXml = loader.Parse(options.InputFileName);

                if (options.GenerateAnalysis)
                {
                    var outputFileName = GetFlawOutputFilename(options, detailedXml);
                    ICSVAnalysisWriter icsvWriter = new CSVAnalysisWriter(csvFormatter);
                    icsvWriter.Write(detailedXml, outputFileName);
                }

                if (options.GenerateFlaws)
                {
                    var outputFileName = GetAnalysisOutputFilename(options, detailedXml);
                    ICSVFlawWriter icsvWriter = new CSVFlawWriter(csvFormatter);
                    icsvWriter.Write(detailedXml, outputFileName, options.IncludeFixedFlaws);
                }
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine($"{options.InputFileName} not found");
            }
        }

        private static string GetFlawOutputFilename(Options options, detailedreport detailedXml)
        {
            return options.OutputFileName ?? detailedXml.app_name + ".csv";
        }

        private static string GetAnalysisOutputFilename(Options options, detailedreport detailedXml)
        {
            if (options.GenerateFlaws == false)
            {
                return options.OutputFileName ?? detailedXml.app_name + ".csv";
            }

            // If we're generating flaws and SCA, add _sca
            var newFilename = Path.GetFileNameWithoutExtension(options.OutputFileName) + "_sca."
                                                                                       + Path.GetExtension(
                                                                                           options.OutputFileName);

            return Path.Combine(Path.GetFullPath(options.OutputFileName), newFilename);
        }
    }
}
