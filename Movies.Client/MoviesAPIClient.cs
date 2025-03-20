using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Client
{
   public class MoviesAPIClient
    {
        public HttpClient Client { get; }

    public MoviesAPIClient(HttpClient client)
        {
            Client = client;
        }
}
}
