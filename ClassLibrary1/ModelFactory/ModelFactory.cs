using System.Net.Http;
using System.Web.Http.Routing;

namespace ClassLibrary1.ModelFactory
{
    public class ModelFactory
    {
        protected UrlHelper UrlHelper;
        public ModelFactory(HttpRequestMessage request)
        {
            UrlHelper = new UrlHelper(request);
        }
    }
}