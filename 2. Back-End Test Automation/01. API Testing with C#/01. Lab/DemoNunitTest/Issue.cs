using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace DemoNunitTest
{
    public class Issue
    {
        public long id {  get; set; }
        public int number { get; set; }

        public string title { get; set; }

       public string body { get; set; }
    }
}
