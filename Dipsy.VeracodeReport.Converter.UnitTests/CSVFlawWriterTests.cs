using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dipsy.VeracodeReport.Converter.UnitTests
{
    using System.Collections.Generic;
    using System.IO;

    using Dipsy.VeracodeReport.Converter.Schema;

    using Moq;

    using Shouldly;

    [TestClass]
    public class CSVFlawWriterTests
    {
        [TestMethod]
        public void ExceptionShouldBeThrownWhenNullReport()
        {
            var mockFormatter = new Mock<ICSVFormatter>();
            var sut = new CSVFlawWriter(mockFormatter.Object);

            Should.Throw<ArgumentNullException>(() => sut.Write(null, "output.csv", false));
        }

        [TestMethod]
        public void ExceptionShouldBeThrownWhenNullOutput()
        {
            var mockFormatter = new Mock<ICSVFormatter>();
            var detailedReport = new detailedreport();
            var sut = new CSVFlawWriter(mockFormatter.Object);

            Should.Throw<ArgumentNullException>(() => sut.Write(detailedReport, null, false));
        }

        [TestMethod, DeploymentItem("./xml/LoadValidStaticFileTest.xml")]
        public void ShouldLoadStaticResults()
        {
            var detailedReport = detailedreport.LoadFromFile("LoadValidStaticFileTest.xml");
            var mockFormatter = new Mock<ICSVFormatter>();

            using (var resultStream = new MemoryStream())
            using (var resultWriter = new StreamWriter(resultStream))
            {
                var sut = new CSVFlawWriter(mockFormatter.Object);

                sut.Write(resultWriter, detailedReport, false);
                resultWriter.Flush();
            }

            // 1 for header, 5 for static results
            mockFormatter.Verify(x => x.FormatLine(It.IsAny<List<string>>()), Times.Exactly(6));
        }
    }
}
