namespace IoC_DI.SongDirectory.models
{
    public interface ISong
    {
        string Title { get; }
        string Artist { get; }
        int Year { get; }
        string FirstLine { get; }

        string GetFullInfo();
    }
}
