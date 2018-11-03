# Veracode Report Converter
Takes a detailed xml report from the Veracode platform and generates a CSV file containing typical fields that are useful in day-to-day reporting:

                Flaw ID
                CWE ID
                Category Name
                Description
                Affects Policy Compliance
                Exploit (Manual)
                Severity (Manual)
                Remediation (Manual)
                Date First Occurrence
                Module
                Source File
                Source File Path
                Attack Vector
                Function Prototype
                Line
                Function Relative Location (%)
                Scope
                Severity
                Exploitability Adjustments
                Grace Period Expires
                Remediation Status
                Mitigation Status
                Mitigation Status Description
                Mitigation Text

It can also optionally output SCA details into another file:

                Library
                Version
                Vendor
                Description
                File Paths
                Licenses
                Max CVSS Score
                Affects Policy Compliance
                Violated Policy Rules
                Vulnerabilities (Very Low)
                Vulnerabilities (Low)
                Vulnerabilities (Medium)
                Vulnerabilities (High)
                Vulnerabilities (Very High)

The Veracode Help Center provides guidance on how to retrieve the detailed xml report here: https://help.veracode.com/reader/DGHxSJy3Gn3gtuSIN2jkRQ/wtJ0ZMLZcYuRd22PmK5vxg

This is a build for .NET Framework, for Windows users who don't want to have to install the .NET Core Runtime

## Usage
``` Dipsy.VeracodeReport.Converter -i <input filename> [-o <output filename>] [-f]```

* -i, --input     Required. Detailed XML file to be processed
* -o, --output    Output filename
* -f, --fixed     Include fixed flaws in the output
* -s, --sca       Generate Software Composition Analysis report
* --help          Display this help screen.
* --version       Display version information.

## Examples

``` Dipsy.VeracodeReport.Converter -i LoadValidFileTest.xml```

``` Dipsy.VeracodeReport.Converter -i LoadValidFileTest.xml -o myoutput.csv```
