using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace AwsDotnetCsharp.Models.Domain
{

    public class Area
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonPropertyName("_id")]
        public string ID { get; set; } = null!;

        [BsonElement("nombre")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;  

        [BsonElement("estado")]
        [JsonPropertyName("estado")]
        public int Estado { get; set; }


        [BsonElement("empresa")]
        [JsonPropertyName("empresa")]
        public EmpresaArea Empresa { get; set; } = null!;  

    }

    public class EmpresaArea{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonPropertyName("_id")]
        public string ID { get; set; } = null!;

        [BsonElement("nombre")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!; 
    }
} 