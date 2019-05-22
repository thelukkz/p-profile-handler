using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PProfileProcessor
{
    public class OutputWriter
    {
        private string _key { get; set; }
        private string _value { get; set; }

        public OutputWriter()
        {

        }

        public OutputWriter WithKey(string key)
        {
            _key = $"> {key} :" + "\t";
            return this;
        }

        public OutputWriter WithValue(string value)
        {
            _value = value;
            return this;
        }

        public void WriteOutput()
        {
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write(_key);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(_value);
            Console.ResetColor();
        }
    }
}
