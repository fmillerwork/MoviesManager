using System;
using System.IO;
using System.Threading;

namespace MoviesManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Localisation des filmes...\n");

            var savingPath = @"D:\Films";
            var filesPathes = Directory.GetFiles(savingPath);

            foreach (var filePath in filesPathes)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var movie = new MovieModel() { OldFileName = fileName };

                // Extension
                movie.Extension = Path.GetExtension(filePath);

                // Qualité
                if (fileName.Contains("2160"))
                    movie.Quality = "2160p";
                else if (fileName.Contains("1080"))
                    movie.Quality = "1080p";
                else if (fileName.Contains("720"))
                    movie.Quality = "720p";
                else if (fileName.Contains("BDRip") || fileName.Contains("BDRIP"))
                    movie.Quality = "BDRip";

                // Langue
                var language = "";
                var languageWritings = new string[] { "MULTI", "multi", "Multi", "MULTi" };
                if (fileName.Contains(languageWritings[0]))
                {
                    language = movie.Language = languageWritings[0];
                }
                else if (fileName.Contains(languageWritings[1]))
                {
                    language = movie.Language = languageWritings[1];
                }
                else if (fileName.Contains(languageWritings[2]))
                {
                    language = movie.Language = languageWritings[2];
                }
                else if (fileName.Contains(languageWritings[3]))
                {
                    language = movie.Language = languageWritings[3];
                }

                /// TODO VFQ VFF Truefrench (différentes écritures)

                // Titre
                // Replace "." et "-" et " " 
                int qualityIndex = int.MaxValue;
                int languageIndex = int.MaxValue;

                if (!string.IsNullOrEmpty(movie.Quality))
                    qualityIndex = fileName.IndexOf(movie.Quality);
                if (!string.IsNullOrEmpty(language))
                    languageIndex = fileName.IndexOf(language);

                string title;
                if (qualityIndex != int.MaxValue || languageIndex != int.MaxValue)
                    title = fileName.Remove(Math.Min(qualityIndex, languageIndex));
                else
                    title = fileName;

                if (title.Contains("("))
                {
                    var openParenthesisIndex = title.IndexOf("(");
                    var closeParenthesisIndex = title.IndexOf(")");
                    movie.Year = title.Substring(openParenthesisIndex + 1, closeParenthesisIndex - openParenthesisIndex - 1);
                    title = title.Remove(openParenthesisIndex);
                }
                    
                title = title.Replace(" ", ".");
                title = title.Replace("_", ".");
                title = title.Replace("[", "");
                title = title.Replace("]", "");
                title = title.Replace("{", "");
                title = title.Replace("}", "");
                title = title.Replace("-", "");
                if (!Char.IsNumber(title[0]))
                    title = title[0].ToString().ToUpper() + title[1..];
                if (Char.IsPunctuation(title[^1]))
                    title = title.Remove(title.Length - 1);
                movie.Title = title;

                // Année
                // TODO Call API

                // Demande Langue et qualité
                // TODO

                // Remplacement titre
                //File.Move(filePath, movie.FileName);

                Console.WriteLine("\n***********\n");
                Console.WriteLine($"{movie.OldFileName} => {movie.FileName}");
                Thread.Sleep(1000);
            }
        }
    }
}
