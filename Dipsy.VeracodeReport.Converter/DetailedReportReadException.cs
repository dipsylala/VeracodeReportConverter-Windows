using System;
using System.IO;

namespace Dipsy.VeracodeReport.Converter
{
    internal class DetailedReportReadException : IOException
    {
        public DetailedReportReadException(string message)
            : base(message)
        {
        }
    }
}
