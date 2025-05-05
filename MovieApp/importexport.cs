using System.IO;
using System.Text;
using domain;
using model;

namespace importExport
{
    class importExport
    {
        public static void saveJSON(LinkedList<Movie> movieCollection, string path)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.AppendLine("[");

            int lineCount = movieCollection.Count;
            int currentIndex = 0;

            foreach (Movie movie in movieCollection)
            {
                
                jsonBuilder.AppendLine("  {");
                jsonBuilder.AppendLine($"    \"MovieID\": \"{movie.MovieID}\",");
                jsonBuilder.AppendLine($"    \"Title\": \"{movie.Title}\",");
                jsonBuilder.AppendLine($"    \"Director\": \"{movie.Director}\",");
                jsonBuilder.AppendLine($"    \"Genre\": \"{movie.Genre}\",");
                jsonBuilder.AppendLine($"    \"RelYear\": {movie.RelYear},");
                jsonBuilder.AppendLine($"    \"Availability\": \"{movie.Availability}\"");
                jsonBuilder.Append("  }");

                if (currentIndex < lineCount - 1)
                {
                    jsonBuilder.AppendLine(",");
                }
                currentIndex++;
            }

            jsonBuilder.AppendLine("]"); 

            File.WriteAllText(path, jsonBuilder.ToString());
        }


        public static LinkedList<Movie> LoadJson(string path)
        {
            
            string jsonString = File.ReadAllText(path);
            List<Movie> movieList = System.Text.Json.JsonSerializer.Deserialize<List<Movie>>(jsonString);
            LinkedList<Movie> movieCollection = new LinkedList<Movie>(movieList);            
            return movieCollection;
            
            
        }
    }
}

