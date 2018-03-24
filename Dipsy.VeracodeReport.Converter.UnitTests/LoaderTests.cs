using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Dipsy.VeracodeReport.Converter.UnitTests
{
    [TestClass]
    public class LoaderTests
    {
        [TestMethod, DeploymentItem("./xsd/LoadValidFileTest.xml")]
        public void LoadValidFileTest()
        {
            var loader = new Loader();
            var results = loader.Parse("LoadValidFileTest.xml");

            // Typically we should aim for one assertion per test, but 
            // it's all part of one big XML read and I'll cut it short.
            results.app_id.ShouldBe(405364);
            results.app_name.ShouldBe("Encoding Test");
            results.staticanalysis.modules.Count.ShouldBe(1);
            results.severity.Count.ShouldBe(6);
        }

        [TestMethod, DeploymentItem("./xsd/LoadInvalidFileTest.xml")]
        public void LoadInvalidFileTest()
        {
            var loader = new Loader();
            Should.Throw<InvalidOperationException>(() => loader.Parse("LoadInvalidFileTest.xml"));
        }
    }
}