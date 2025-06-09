namespace IoC_DI.SongDirectory.output
{

    public interface IOutput
    {
        void Write(string value);
    }

    public class FileOutput : IOutput
    {
        public void Write(string value)
        {
            File.AppendAllText("song.txt", value + Environment.NewLine);
        }
    }

    public class ConsoleOutput : IOutput
    {
        public void Write(string value)
        {
            Console.WriteLine(value);
        }
    }
}
