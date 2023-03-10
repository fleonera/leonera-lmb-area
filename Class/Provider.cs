using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;
using AwsDotnetCsharp.Models.Domain;

namespace AwsDotnetCsharp.Connection
{
    public static class ConnectingProvider{
        private const string ProviderConnectionDev = "mongodb+srv://leonera01:1VHenEUKvZnOTjS7@leonera.9gdlagt.mongodb.net/test";
        private static MongoDBConnection connDB = null;
        public static void GetProviderDev() { connDB = new MongoDBConnection() { ConnectionString = ProviderConnectionDev }; }
        public static IMongoCollection<Area> GetCollection(){
            connDB.OpenConnection();
            return connDB.getCollection();
        }
    }
}