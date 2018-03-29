using Dipsy.VeracodeReport.Converter.Schema;

namespace Dipsy.VeracodeReport.Converter.Interfaces
{
    public interface ICSVWriter
    {
        void Write(detailedreport detailedXml, Options options);
    }
}