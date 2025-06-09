namespace IoC_DI.models
{
    public interface IOutputWriter
    {
        void Write(string value);
    }

    public class FileOutputWriter : IOutputWriter
    {
        public void Write(string value)
        {
            File.AppendAllText("drinks.txt", value +  Environment.NewLine);
        }
    }

    public class ConsoleOutputWriter : IOutputWriter
    {
        public void Write(string value)
        {
            Console.WriteLine(value);
        }
    }
}
