# PoorMansLocalization
Simple Localization (language) class

## About PoorMansLocalization
Provides a class to facilitate easy use of Language Resource files for basic Localisation of your code.
The resource files are text files (usually named .restext) following the format outlined at [Create resource files](https://learn.microsoft.com/en-us/dotnet/core/extensions/create-resource-files)
This leaves a migration path to the full Resource File of .NET should you project later need it.

The code was written in 'Visual Studio Code' and uses .NET 8.  The use of .Net is extremely small so this should work under other releases as well.
The code has been tested on Windows 10 and Ubuntu 22.04

The reason for going my own way is that the full blown Resource usage relies on 'Visual Studio' and cannot be implemented on Linux.

The software is distributed in C# as a [CodeBit](http://filemeta.org/CodeBit.html) located [here](https://raw.githubusercontent.com/FileMeta/MetaTag/master/MetaTag.cs). It is released under a [BSD 3-Clause](https://opensource.org/licenses/BSD-3-Clause) open source license.

This project includes the master copy of the CodeBit plus a set of unit tests which may also serve as sample code.

## About CodeBits
A [CodeBit](https://www.FileMeta.org/CodeBit) is very lightweight way to share common code. Each CodeBit consists of a single source code file. A structured comment at the beginning of the file indicates where to find the master copy so that automated tools can retrieve and update CodeBits to the latest version.
