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
using AwsDotnetCsharp.Models.DataTransfer;
using AwsDotnetCsharp.Connection;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(
typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {
        
        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("Error Consulta Http", "error Test!!");
            //dict.Add("Test", "test correcto!!");
            
           
            APIGatewayProxyResponse response = null;
            LogMessage(context, "Endpoint GetByFilters llamado.");

            try
            {
                
                ConnectingProvider.GetProviderDev();              
               
                var  collection = ConnectingProvider.GetCollection();
                
                //Solicitud solicitud = collection.Find(a => a.NroSolicitud == Params.GetValue(request, "NroPedido")).ToList().First();
                List<Area> AreaList = collection.Find(_ => true).ToList();                    
                
                if (AreaList == null)
                {
                    LogMessage(context, $"No se han encontrado los datos solicitados.");
                    dict.Add("Error Consulta Http", "No se han encontrado los datos solicitados");
                    return ResponseDataResult.CreateResponse(dict);
                   
                }
                
                 List<AreaDTO> lista = new List<AreaDTO>();
                foreach(var oItem in AreaList)
                {                   
                    lista.Add(GetDTO(oItem));
                }

                response = CreateResponse(lista, HttpStatusCode.OK);
                //LogMessage(context, "La solicitud se ha procesado exitosamente.");
                return response;


                /* dict.Add("Evaluacion", "hasta aqui bien");
                return ResponseDataResult.CreateResponse(dict); */

            }
            catch(Exception e)
            {
               dict.Add("Error Consulta Http", e.Message);
               return ResponseDataResult.CreateResponse(dict);
            }          
        }  
        
        public APIGatewayProxyResponse GetById(APIGatewayProxyRequest request, ILambdaContext context)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("Error Consulta Http", "error Test!!");
            //dict.Add("Test", "test correcto!!");
            
           
            APIGatewayProxyResponse response = null;
            LogMessage(context, "Endpoint GetByFilters llamado.");

            try
            {
                bool respuesta = false;
                ValidationInput(request, response, ref respuesta, context);
                if(respuesta) return response;
                ConnectingProvider.GetProviderDev();              
               
                var  collection = ConnectingProvider.GetCollection();
                
                //Solicitud solicitud = collection.Find(a => a.NroSolicitud == Params.GetValue(request, "NroPedido")).ToList().First();
                List<Area> AreaList = collection.Find(_ => true).ToList().
                    Where(x=>x.ID.Contains(Params.GetValue(request, "IdArea"))).ToList();
                
                if (AreaList == null)
                {
                    LogMessage(context, $"No se han encontrado los datos solicitados.");
                    dict.Add("Error Consulta Http", "No se han encontrado los datos solicitados");
                    return ResponseDataResult.CreateResponse(dict);
                   
                }
                
                 List<AreaDTO> lista = null;
                foreach(var oItem in AreaList)
                {
                    lista = new List<AreaDTO>();
                    lista.Add(GetDTO(oItem));
                }

                response = CreateResponse(lista, HttpStatusCode.OK);
                //LogMessage(context, "La solicitud se ha procesado exitosamente.");
                return response;


                /* dict.Add("Evaluacion", "hasta aqui bien");
                return ResponseDataResult.CreateResponse(dict); */

            }
            catch(Exception e)
            {
               dict.Add("Error Consulta Http", e.Message);
               return ResponseDataResult.CreateResponse(dict);
            }          
        }  
        
        public APIGatewayProxyResponse Create(APIGatewayProxyRequest request, ILambdaContext context)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("Error Consulta Http", "error Test!!");
            //dict.Add("Test", "test correcto!!");
            
           
            APIGatewayProxyResponse response = null;
            LogMessage(context, "Endpoint Create llamado.");

            try
            {
                ConnectingProvider.GetProviderDev();              
               
                var AreaDTO = JsonConvert.DeserializeObject<AreaDTO>(request.Body);
                if (AreaDTO == null)
                {
                    LogMessage(context, $"Error en el procesamiento de la solicitud - El cuerpo de la solicitud no es válido.");
                    response = CreateResponse(null, HttpStatusCode.BadRequest);
                    return response;
                }

                //Mappeamos para insertar en BD
                var Area = GetDomain(AreaDTO);

                var  collection = ConnectingProvider.GetCollection();
                collection.InsertOne(Area);
                

                response = CreateResponse(null, HttpStatusCode.OK);
                LogMessage(context, "La solicitud se ha procesado exitosamente.");
                return response;              

            }
            catch(Exception e)
            {
               dict.Add("Error Consulta Http", e.Message);
               return ResponseDataResult.CreateResponse(dict);
            }          
        }  

        public APIGatewayProxyResponse Update(APIGatewayProxyRequest request, ILambdaContext context)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("Error Consulta Http", "error Test!!");
            //dict.Add("Test", "test correcto!!");
            
           
            APIGatewayProxyResponse response = null;
            LogMessage(context, "Endpoint Create llamado.");

            try
            {
                bool respuesta = false;
                ValidationInput(request, response, ref respuesta, context);
                if(respuesta) return response;
                ConnectingProvider.GetProviderDev();              
               
                var id = Params.GetValue(request, "IdArea"); 

                var AreaDTO = JsonConvert.DeserializeObject<AreaDTO>(request.Body);
                if (AreaDTO == null)
                {
                    LogMessage(context, $"Error en el procesamiento de la solicitud - El cuerpo de la solicitud no es válido.");
                    response = CreateResponse(null, HttpStatusCode.BadRequest);
                    return response;
                }

                //Mappeamos para insertar en BD
                var Area = GetDomain(AreaDTO);
                Area.ID = id;



                var  collection = ConnectingProvider.GetCollection();
                LogMessage(context, "Actualizando datos...");
                collection.ReplaceOne(a => a.ID == id, Area);
                LogMessage(context, $"Se ha actualizado el registro.");
  

                response = CreateResponse(null, HttpStatusCode.NoContent);
                LogMessage(context, "La solicitud se ha procesado exitosamente.");
                return response;    
            }
            catch(Exception e)
            {
               dict.Add("Error Consulta Http", e.Message);
               return ResponseDataResult.CreateResponse(dict);
            }          
        }  

        public APIGatewayProxyResponse Delete(APIGatewayProxyRequest request, ILambdaContext context)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("Error Consulta Http", "error Test!!");
            //dict.Add("Test", "test correcto!!");
            
           
            APIGatewayProxyResponse response = null;
            LogMessage(context, "Endpoint Create llamado.");

            try
            {
                bool respuesta = false;
                ValidationInput(request, response, ref respuesta, context);
                if(respuesta) return response;
                ConnectingProvider.GetProviderDev();              
               
                var id = Params.GetValue(request, "IdArea"); 

                
                var  collection = ConnectingProvider.GetCollection();
                LogMessage(context, "Eliminando datos...");
                collection.DeleteOne(a => a.ID == id);
                LogMessage(context, $"Se ha eliminado el registro.");
  

                response = CreateResponse(null, HttpStatusCode.NoContent);
                LogMessage(context, "La solicitud se ha procesado exitosamente.");
                return response;
            }
            catch(Exception e)
            {
               dict.Add("Error Consulta Http", e.Message);
               return ResponseDataResult.CreateResponse(dict);
            }          
        }  
        
        private static Area GetDomain(AreaDTO AreaDTO) =>
            new Area
            {
                ID = AreaDTO.DTOId,
                Nombre = AreaDTO.DTONombre,
                Estado = AreaDTO.DTOEstado,
                Empresa = new EmpresaArea()
                {
                    ID = AreaDTO.DTOEmpresa.ID,
                    Nombre = AreaDTO.DTOEmpresa.Nombre
                }
            };


        private static AreaDTO GetDTO(Area area) =>
                new AreaDTO
                {
                    DTOId = area.ID,
                    DTONombre = area.Nombre,
                    DTOEstado = area.Estado,
                    DTOEmpresa = new DTOEmpresaArea()
                    {
                        ID = area.ID,
                        Nombre = area.Nombre
                    }            
                };
            

        APIGatewayProxyResponse ValidationInput(APIGatewayProxyRequest request, APIGatewayProxyResponse response,ref bool respuesta, ILambdaContext context)
        {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                if (request == null || request.QueryStringParameters == null)
                {
                    LogMessage(context, $"Error en el procesamiento de la solicitud - Debe ingresar el parametros via http(s).");
                    response = CreateResponse(null, HttpStatusCode.BadRequest);


                    respuesta = true;
                    dict.Add("Error Validaciones", "Error en el procesamiento de la solicitud - Debe ingresar parametros via http(s).");
                    return ResponseDataResult.CreateResponse(dict);
                    
                }
                
                 if (request == null || request.QueryStringParameters == null || !request.QueryStringParameters.ContainsKey("IdArea"))
                {
                    LogMessage(context, $"Error en el procesamiento de la solicitud - Debe ingresar el parametro 'IdArea' a su solicitud.");
                    response = CreateResponse(null, HttpStatusCode.BadRequest);
                    
                    dict.Add("Error Validaciones", "Error en el procesamiento de la solicitud - Debe ingresar el parametro 'IdArea' a su solicitud.");
                    return ResponseDataResult.CreateResponse(dict);
                }
                
                response = CreateResponse(null, HttpStatusCode.BadRequest);
                return response; 
        }
        void LogMessage(ILambdaContext ctx, string msg)
        {   
            ctx.Logger.LogLine(
                string.Format("{0}:{1} - {2}",
                    ctx.AwsRequestId,
                    ctx.FunctionName,
                    msg));
        }
       
       
        private APIGatewayProxyResponse CreateResponse(object result = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            var body = (result != null) ?
                JsonConvert.SerializeObject(result) :
                string.Empty;

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)statusCode,
                Body = body,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };
            return response;
        } 

    }

}
