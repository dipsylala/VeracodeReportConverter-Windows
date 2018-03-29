﻿using System;
using System.Collections.Generic;
using System.IO;

using Dipsy.VeracodeReport.Converter.Interfaces;
using Dipsy.VeracodeReport.Converter.Schema;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Shouldly;

namespace Dipsy.VeracodeReport.Converter.UnitTests
{
    [TestClass]
    public class CSVFlawWriterTests
    {
        [TestMethod]
        public void ExceptionShouldBeThrownWhenNullReport()
        {
            var mockFormatter = new Mock<ICSVFormatter>();
            var options = new Options { OutputFileName = "output.csv" };
            var sut = new CSVFlawWriter(mockFormatter.Object);

            Should.Throw<ArgumentNullException>(() => sut.Write(null, options));
        }

        [TestMethod]
        public void ExceptionShouldBeThrownWhenNullOptions()
        {
            var mockFormatter = new Mock<ICSVFormatter>();
            var detailedReport = new detailedreport();
            var sut = new CSVFlawWriter(mockFormatter.Object);

            Should.Throw<ArgumentNullException>(() => sut.Write(detailedReport, null));
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
