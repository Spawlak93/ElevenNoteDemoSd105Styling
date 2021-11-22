using System.Net.Http.Headers;
using System.Web.Http;

namespace ElevenNoteDemo.WebMVC
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            GlobalConfiguration
                .Configure(
                    x =>
                    {
                        x
                            .Formatters
                            .JsonFormatter
                            .SupportedMediaTypes
                            .Add(new MediaTypeHeaderValue("text/html"));

                        x.MapHttpAttributeRoutes();
                    }
                );
        }
    }
}