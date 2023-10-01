using System;
using System.Runtime.Serialization;

namespace mc
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true){
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;
                    
                if (line == "1 + 2 * 3")
                    Console.WriteLine("7");
                else
                    Console.WriteLine("ERROR");
            }
        }
    }
    enum SyntaxKind
    {        
    }
    class SyntaxToken
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public SyntaxKind Kind {get;}
        public int Position {get;}
        public string Text {get;}
        public object Value {get;}
    }
    class Lexer
    {
        private readonly string _text;
        private int _position;
        public Lexer(string text)
        {
            _text = text;
        }

        private char Current
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';
                return _text[_position];
            }
        }

        private void Next()
        {
            _position++;
        }
        public SyntaxToken NextToken()
        {
            // is it a number
            if (char.IsDigit(Current))
            {
                var start = _position;
                // read whole number
                while(char.IsDigit(Current)) 
                    Next();
            
                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }
            // is it a white space?
            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                // read whole white space
                while(char.IsDigit(Current)) 
                    Next();
            
                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);

                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, value);
            }
            if (Current == '+')
            {
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null); 
            }
            if (Current == '-')
            {
                return new SyntaxToken(SyntaxKind.MinusToken, _position--, "-", null); 
            }
        }
    }
}