using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace AwsDotnetCsharp.Models.DataTransfer
{

    public class AreaDTO
    { 
        public string DTOId { get; set; } = null!;
        public string DTONombre { get; set; } = null!;  

        public int DTOEstado { get; set; }
        public DTOEmpresaArea DTOEmpresa { get; set; } = null!; 
    }

    public class DTOEmpresaArea{
       
        public string ID { get; set; } = null!;        
        public string Nombre { get; set; } = null!; 
    }
}