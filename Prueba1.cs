using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;
using System.Net;

namespace AwsDotnetCsharp
{
    public class Prueba1
    {
        public static APIGatewayProxyResponse CreateResponse()
        {
           return Respuesta(200, "hola");    
        }

        private static APIGatewayProxyResponse Respuesta(int statusCode, string body)
        {
            var response = new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
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