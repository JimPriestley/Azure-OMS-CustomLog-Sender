# Azure OMS Custom Log Sender

## Introduction

This a sample class library for sending JSON obects to Log Analytics as a Custom Log event. The API will accept a single event, or an array of events within the JSON.

## Getting Started

1. This sample is built in .NET Standard 2.0, using Visual Studio 2017, but can be backported without change to older versions of .NET and VS.
2. This sample has no Nuget dependencies.
3. This sample requires refferences to System.Net.HTTP, and System.Text, and System.Security.Cryptograpy.

## Azure OMS Log Analytics

Currently, the Custom Log feature in Log Analytics is in preview, and you must enable preview features within your Workspace to use this code.

## Usage

````c#
string response = OMSCustomLog.LogSender.Send(WorkSpaceId, WorkSpaceKey, CustomLogName, EventJson);
````

## Contributing

I welcome feedback and contributions on this sample.

Opportunities for extending this class include:

- Exposing the sender as an async method
- Chunking large batches of records to fit within the batch send limit on the API

## License

**MIT License**
*Copyright (c) 2018 Jim Priestley*

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.