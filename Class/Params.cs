using Amazon.Lambda.APIGatewayEvents;
namespace AwsDotnetCsharp
{
    public class Params{
        public static string GetValue(APIGatewayProxyRequest request, string param)
        {
            if (request.QueryStringParameters != null)
            {                
                foreach (var item in request.QueryStringParameters)
                {
                    if(param == item.Key) return item.Value;            
                }
            }

            return "";
        }
    }
}