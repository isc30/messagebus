[![Build Status](https://travis-ci.org/isc30/messagebus.svg?branch=master)](https://travis-ci.org/isc30/messagebus)

A simple MessageBus implementation for .NET Standard 2.0
<br/><br/>

# Work in Progress

## ✔️ Multiple Handlers

Ability to subscribe multiple dispatchers for the same message type.<br/>
`Dispatch` will synchronously call all the (0..N) handlers.

```csharp
var messageBus = new MessageBus<int>();
messageBus.Subscribe((int n) => Console.WriteLine($"Yay! I got {n}!");
messageBus.Subscribe((int n) => Console.WriteLine($"I got {n} too!");
```

## ❌ Polymorphic Handlers

Allow handling specific abstractions of the message

```csharp
var messageBus = new MessageBus<Exception>();
messageBus.Subscribe((Exception ex) => Console.WriteLine($"OH NO!");
messageBus.Subscribe((NotImplementedException n) => Console.WriteLine($"HAHAHA, ignore this :P");
```

## ❌ Bubbling

Dispatch every **unhandled** message to the parent Bus.

```csharp
var parentBus = new MessageBus<int>();
parentBus.Subscribe((int n) => Console.WriteLine($"I got a value! {n}");

var childBus = parentBus.CreatePipe<int>();
childBus.Dispatch(1234);
```
