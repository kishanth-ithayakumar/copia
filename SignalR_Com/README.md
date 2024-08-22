# SignalR_Com - Monitoring CPU Load and Memory Usage with a C# SignalR Application


## Introduction
In this guide, we’ll build a small C# SignalR application that monitors CPU load and memory usage across all devices. SignalR allows real-time communication between clients and servers, making it ideal for tracking system performance.

## Prerequisites
Before we begin, ensure you have the following:

- VS Code
- C# Dev Kit (see https://code.visualstudio.com/docs/languages/csharp)
- .NET 6 SDK


## Install SignalR
In your project, add the SignalR NuGet package:

`Install-Package Microsoft.AspNetCore.SignalR`

## Starting the Client App

### Command Line Argument

`Client.exe <Manager-IP-Address>`