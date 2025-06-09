using System.Security.Cryptography.X509Certificates;

namespace IoC_DI.SongDirectory.output
{
    public class PlainFormatter : IFormatter
    {
        public string Format(string value) => value;
    }


    public class MorseFormatter : IFormatter
    {
        private static readonly Dictionary<char, string> Morse = new()
        {
            ['a'] = ".-",
            ['b'] = "-...",
            ['c'] = "-.-.",
            ['d'] = "-..",
            ['e'] = ".",
            ['f'] = "..-.",
            ['g'] = "--.",
            ['h'] = "....",
            ['i'] = "..",
            ['j'] = ".---",
            ['k'] = "-.-",
            ['l'] = ".-..",
            ['m'] = "--",
            ['n'] = "-.",
            ['o'] = "---",
            ['p'] = ".--.",
            ['q'] = "--.-",
            ['r'] = ".-.",
            ['s'] = "...",
            ['t'] = "-",
            ['u'] = "..-",
            ['v'] = "...-",
            ['w'] = ".--",
            ['x'] = "-..-",
            ['y'] = "-.--",
            ['z'] = "--..",
            [' '] = "/"
        };


        public string Format(string value)
        {
            return string.Join(" ", value.ToLower().Select(c => Morse.ContainsKey(c) ?  Morse[c] : c.ToString()));
        }
    }
}
