using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace AwsDotnetCsharp
{
    public class ResponseDataResult
    {
        public static APIGatewayProxyResponse CreateResponse(IDictionary<string, string> result)
        {
                int statusCode = ValidStatusResult(result);

                string body = ValidSerializeResult(result);

                return Respuesta(statusCode, body);           
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

        private static int ValidStatusResult(IDictionary<string, string> result)
        {
            return (result != null) ?
                (int)HttpStatusCode.OK :
                (int)HttpStatusCode.InternalServerError;
        }

        private static string ValidSerializeResult(IDictionary<string, string> result)
        {
            return (result != null) ?
                JsonConvert.SerializeObject(result) : string.Empty;
        }
    }
}