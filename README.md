# WebBanana-Server

## WebBanana

WebBanana is an application that has two parts:
- [WebBanana-Client](https://github.com/caldavsta/WebBanana-Client), which is built in Xamarin and runs on Android currently
- WebBanana-Server, which is built in .NET Core

Voicemeeter's DLL exposes information about its current configuration and allows control of its parameters. When a user changes values in the Android app, it controls Voicemeeter via the server.

## What is Voicemeeter?

[Voicemeeter](https://www.vb-audio.com/Voicemeeter/) is an audio mixer application that allows management of audio I/O to and from audio devices and applications.

## Banana what?

The version of Voicemeeter I use is called Voicemeeter Banana. It's been in my life since 2013 so I think pet names are appropriate.

## How do I use WebBanana?

Simply put, you'll have to be familiar with C# and .NET. In its current state, it won't run if you clone the repo and hit "play". For example, it has hard-coded local IP addresses that would need to be changed before building.
