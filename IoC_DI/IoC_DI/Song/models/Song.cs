namespace IoC_DI.SongDirectory.models
{
    public class Song : ISong
    {
        public string Title => "walk like an egyptian";

        public string Artist => "The Bangles";

        public int Year => 1986;

        public string FirstLine => "All the old paintings on the tombs";

        public string GetFullInfo()
        {
            return $"{Title} by {Artist} ({Year})\n{FirstLine}";
        }
    }
}
