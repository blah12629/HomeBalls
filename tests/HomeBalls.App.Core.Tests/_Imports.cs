global using Blazored.LocalStorage;

global using CEo.Pokemon.HomeBalls.ProtocolBuffers;
global using CEo.Pokemon.HomeBalls.Tests;
global using static CEo.Pokemon.HomeBalls.App.Core._Values;
global using static CEo.Pokemon.HomeBalls.App.Core.Tests._Values;

global using FluentAssertions;
global using FluentAssertions.Events;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

global using NSubstitute;
global using NSubstitute.Extensions;

global using System;
global using System.Collections.Generic;
global using System.IO.Abstractions;
global using System.Net;
global using System.Net.Http;
global using System.Threading;
global using System.Threading.Tasks;

global using Xunit;