# Veracode Report Converter
Takes a detailed xml report from the Veracode platform and generates a CSV file containing typical fields that are useful in day-to-day reporting.

The Veracode Help Center provides guidance on how to retrieve the detailed xml report here: https://help.veracode.com/reader/DGHxSJy3Gn3gtuSIN2jkRQ/wtJ0ZMLZcYuRd22PmK5vxg

This is a build for .NET Framework, for Windows users who don't want to have to install the .NET Core Runtime

## Usage
``` Dipsy.VeracodeReport.Converter -i <input filename> [-o <output filename>] [-f]```

* -i, --input     Required. Detailed XML file to be processed
* -o, --output    Output filename
* -f, --fixed     Include fixed flaws in the output
* --help          Display this help screen.
* --version       Display version information.

## Examples

``` Dipsy.VeracodeReport.Converter -i LoadValidFileTest.xml```

``` Dipsy.VeracodeReport.Converter -i LoadValidFileTest.xml -o myoutput.csv```
