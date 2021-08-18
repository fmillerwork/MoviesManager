namespace MoviesManager
{
    public class MovieModel
    {
        public string Title { get; set; }
        public string FileName { get => $"{Title}({Year})_{Quality}_{Language}"; }
        public string OldFileName { get; set; }
        public string Language { get; set; }
        public string Year { get; set; }
        public string Quality { get; set; }
        public string Extension { get; set; }
    }
}
