using System.IO;

namespace Dipsy.VeracodeReport.Converter
{
    using System;

    internal class CSVWriteException : IOException
    {
        public CSVWriteException(string message)
            : base(message)
        {
        }
    }
}
