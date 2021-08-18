using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesManager
{
    public class JsonGenerator
    {
        public void Save(List<MovieModel> models)
        {
            string jsonString = JsonSerializer.Serialize(models);
            Console.WriteLine(jsonString);
        }
    }
}
