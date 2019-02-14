namespace CodeWars.Kyu6.ReversePolishNotationCalculator
{
    /// <summary>
    /// https://www.codewars.com/kata/reverse-polish-notation-calculator/
    /// </summary>
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class Calc
    {
        private static Dictionary<string, Func<double, double, double>> _binaryOperators = new Dictionary<string, Func<double, double, double>>
        {
            ["+"] = (op1, op2) => op1 + op2,
            ["-"] = (op1, op2) => op1 - op2,
            ["*"] = (op1, op2) => op1 * op2,
            ["/"] = (op1, op2) => op1 / op2
        };
        public double evaluate(String expr)
        {
            return 0.0;
        }

        public Queue<Token> ToRPN(string infixTerm)
        {
            Queue<Token> outputQueue = new Queue<Token>();


            foreach (var token in new TokenReader(infixTerm))
            {
                outputQueue.Enqueue(token);
            }
            return outputQueue;
        }



        public enum TermType { Operand, Operator }

        public class Token
        {
            public TermType TermType { get; set; }
            public string Value { get; set; }
        }

        public class TokenReader : IEnumerable<Token>
        {
            private readonly string _source;

            public TokenReader(string source)
            {
                this._source = source;
            }
            public IEnumerator<Token> GetEnumerator()
            {
                return new TokenReaderEnumerator(_source);
            }

            private IEnumerator GetEnumerator1()
            {
                return this.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator1();
            }
        }

        //could also use yield keyword in this case
        public class TokenReaderEnumerator : IEnumerator<Token>
        {
            private readonly string _source;
            private Token _current;
            private int _pointer;
            public Token Current => this._current;
            private object Current1 => this.Current;
            object IEnumerator.Current => this.Current1;

            public TokenReaderEnumerator(string source)
            {
                this._source = source;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_pointer > _source.Length - 1)
                {
                    return false;
                }
                StringBuilder sb = new StringBuilder(_source.Length);
                int i = _pointer;
                TermType currentTermType = TermType.Operand;
                for (; i < this._source.Length; i++)
                {
                    char item = this._source[i];
                    sb.Append(item);
                    if (!IsInGrammar(sb.ToString(), out currentTermType))
                    {
                        sb.Remove(sb.Length - 1, 1);
                        break;
                    }
                }
                string currentTerm = sb.ToString();
                if (String.IsNullOrEmpty(currentTerm) || !IsInGrammar(currentTerm, out currentTermType))
                {
                    throw new InvalidOperationException("TokenReader wasn't able to read next token from input");
                }
                _pointer = i;
                _current = new Token()
                {
                    Value = currentTerm,
                    TermType = currentTermType
                };
                return true;
            }

            public void Reset()
            {
                _pointer = 0;
            }
        }

        private static bool IsInGrammar(string token, out TermType type)
        {
            bool result = false;
            type = TermType.Operand;
            switch (token)
            {
                case string s when IsDouble(token, out var _) && _binaryOperators.Keys.Any(op => !token.StartsWith(op)):
                    type = TermType.Operand;
                    result = true;
                    break;
                case string s when _binaryOperators.ContainsKey(token):
                    type = TermType.Operator;
                    result = true;
                    break;
            }
            return result;
        }

        private static bool IsDouble(string token, out double value)
        {

            NumberStyles numberStyles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite; 
            return double.TryParse(token.EndsWith(".") ? token + "0" : token, numberStyles, CultureInfo.InvariantCulture, out value);
        }
    }

}