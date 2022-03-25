global using Blazored.LocalStorage;

global using CEo.Pokemon.HomeBalls.Collections;
global using CEo.Pokemon.HomeBalls.Comparers;
global using CEo.Pokemon.HomeBalls.Entities;
global using CEo.Pokemon.HomeBalls.ProtocolBuffers;
global using CEo.Pokemon.HomeBalls.Tests;
global using static CEo.Pokemon.HomeBalls.App._Values;
global using static CEo.Pokemon.HomeBalls.App.Tests._Values;

global using FluentAssertions;
global using FluentAssertions.Events;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.JSInterop;

global using NSubstitute;
global using NSubstitute.Extensions;

global using ProtoBuf;

global using System;
global using System.Collections.Generic;
global using System.IO.Abstractions;
global using System.Linq;
global using System.Net;
global using System.Net.Http;
global using System.Reflection;
global using System.Threading;
global using System.Threading.Tasks;

global using Xunit;
global using Xunit.Abstractions;