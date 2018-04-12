using System;

namespace CoreStudyComm
{
    public class People:IPeople
    {
        public People(string word)
        {
            _word = word;
        }

        private string _word;
        public void SayHello()
        {
            Console.WriteLine($"Hello World{_word}");
        }
    }

    public interface IPeople
    {
        void SayHello();
    }
}
