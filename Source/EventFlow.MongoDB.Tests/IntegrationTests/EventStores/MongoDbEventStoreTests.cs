﻿using System;
using System.Threading.Tasks;
using EventFlow.Configuration;
using EventFlow.TestHelpers;
using EventFlow.TestHelpers.Suites;
using EventFlow.MongoDB.EventStore;
using EventFlow.MongoDB.Extensions;
using NUnit.Framework;
using Mongo2Go;

namespace EventFlow.MongoDB.Tests.IntegrationTests.EventStores
{
	[Category(Categories.Integration)]
	[TestFixture]
	[NUnit.Framework.Timeout(30000)]
    public class MongoDbEventStoreTests : TestSuiteForEventStore
	{
		private MongoDbRunner _runner;
		
		protected override IRootResolver CreateRootResolver(IEventFlowOptions eventFlowOptions)
		{
		    _runner = MongoDbRunner.Start();
            var resolver = eventFlowOptions
				.ConfigureMongoDb(_runner.ConnectionString, "eventflow")
				.UseMongoDbEventStore()
				.CreateResolver();
		    var eventPersistenceInitializer = resolver.Resolve<IMongoDbEventPersistenceInitializer>();
            eventPersistenceInitializer.Initialize();
		    
			return resolver;
		}

        [TearDown]
		public void TearDown()
		{
			_runner.Dispose();
		}
	}
}
