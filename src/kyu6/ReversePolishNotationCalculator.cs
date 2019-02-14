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

        public double evaluate(String expr, bool infix = false)
        {

            Queue<Token> rpnQueue = infix ? ToRPN(expr.Replace(" ", String.Empty)) : FromStringToRPN(expr);
            Stack<double> evaluateStack = new Stack<double>();
            evaluateStack.Push(0);
            while (rpnQueue.Count > 0)
            {
                var currToken = rpnQueue.Dequeue();
                switch (currToken.TermType)
                {
                    case TermType.Operator:
                        var op1 = evaluateStack.Pop();
                        var op2 = evaluateStack.Pop();
                        evaluateStack.Push(
                            GrammarCatalog.BinaryOperators[currToken.Value](op2, op1)
                        );
                        break;
                    case TermType.Operand:
                        IsDouble(currToken.Value, out var tokenValue);
                        evaluateStack.Push(tokenValue);
                        break;
                }
            }
            return evaluateStack.Pop();
        }

        public Queue<Token> FromStringToRPN(string rpnTerm)
        {
            Queue<Token> outputQueue = new Queue<Token>();
            foreach (var token in new TokenReader(rpnTerm))
            {
                outputQueue.Enqueue(token);
            }
            return outputQueue;
        }
        public Queue<Token> ToRPN(string infixTerm)
        {
            Queue<Token> outputQueue = new Queue<Token>();
            Stack<Token> operators = new Stack<Token>();
            Action transition = () => outputQueue.Enqueue(operators.Pop());
            foreach (var token in new TokenReader(infixTerm))
            {
                var tokenTermType = token.TermType;
                switch (tokenTermType)
                {
                    case TermType.Operand:
                        outputQueue.Enqueue(token);
                        break;
                    case TermType.Function:
                        operators.Push(token);
                        break;
                    case TermType.Operator:
                        Token topOperator;
                        bool terminate = false;
                        while (!terminate && operators.Count > 0)
                        {
                            topOperator = operators.Peek();
                            switch (topOperator.TermType)
                            {
                                case TermType.Function:
                                    //TODO: (probably multiple pops)
                                    transition();
                                    break;
                                case TermType.Operator when topOperator.Priority > token.Priority:
                                    transition();
                                    break;
                                default:
                                    terminate = true;
                                    break;
                            }
                        }
                        operators.Push(token);
                        break;
                    case TermType.Parenthesis when token.Value == "(":
                        operators.Push(token);
                        break;
                    case TermType.Parenthesis when token.Value == ")":
                        while (operators.Count > 0 && operators.Peek().Value != "(")
                        {
                            transition();
                        }
                        while (operators.Count > 0 && operators.Peek().Value == "(")
                        {
                            operators.Pop();
                        }
                        break;
                }
            }
            while (operators.Count > 0)
            {
                transition();
            }

            return outputQueue;
        }



        public enum TermType { Operand, Operator, Function, Parenthesis }

        public class Token
        {
            public TermType TermType { get; set; }
            public string Value { get; set; }
            public int Priority { get; set; }
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
                for (; i < this._source.Length; i++)
                {
                    char item = this._source[i];
                    sb.Append(item);
                    if (!IsInGrammar(sb.ToString(), out _))
                    {
                        sb.Remove(sb.Length - 1, 1);
                        break;
                    }
                }
                _pointer = i;
                _current = CreateTokenFactoryMethod(sb.ToString());
                return true;
            }

            public void Reset()
            {
                _pointer = 0;
            }
        }

        private class GrammarCatalog
        {
            public static Dictionary<string, Func<double, double, double>> BinaryOperators { get; } = new Dictionary<string, Func<double, double, double>>
            {
                ["+"] = (op1, op2) => op1 + op2,
                ["-"] = (op1, op2) => op1 - op2,
                ["*"] = (op1, op2) => op1 * op2,
                ["/"] = (op1, op2) => op1 / op2,
            };

            public static Dictionary<string, Func<double, double>> UnaryFunctions { get; } = new Dictionary<string, Func<double, double>>
            {
                ["sin"] = op => Math.Sin(op),
                ["cos"] = op => Math.Cos(op)
            };
            public static Dictionary<string, int> PriorityOfOperations { get; } = new Dictionary<string, int>
            {
                ["+"] = 10,
                ["-"] = 11,
                ["*"] = 100,
                ["/"] = 101,
            };

            public static HashSet<String> Grammar { get; } = new HashSet<string>
            {
                "sin", "cos"
            };

        }

        private static Token CreateTokenFactoryMethod(string token)
        {
            if (String.IsNullOrEmpty(token) || !IsInGrammar(token, out var currentTermType))
            {
                throw new InvalidOperationException($"TokenReader wasn't able to read next token from input: {token}");
            }
            return new Token()
            {
                Value = token.Trim(),
                TermType = currentTermType,
                Priority = GrammarCatalog.PriorityOfOperations.ContainsKey(token) ? GrammarCatalog.PriorityOfOperations[token] : 1
            };
        }
        private static bool IsInGrammar(string token, out TermType type)
        {
            bool result = false;
            type = TermType.Operand;
            switch (token.Trim())
            {
                case string s when IsDouble(token, out var _) && GrammarCatalog.BinaryOperators.Keys.Any(op => !token.StartsWith(op)):
                    type = TermType.Operand;
                    result = true;
                    break;
                case string s when GrammarCatalog.BinaryOperators.ContainsKey(token):
                    type = TermType.Operator;
                    result = true;
                    break;
                case string s when new string[] { "(", ")" }.Contains(s):
                    type = TermType.Parenthesis;
                    result = true;
                    break;
                case string s when GrammarCatalog.Grammar.Any(t => t.StartsWith(s)):
                    type = TermType.Function;
                    result = true;
                    break;
            }
            return result;
        }

        private static bool IsDouble(string token, out double value)
        {
            NumberStyles numberStyles =
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingWhite |
                NumberStyles.AllowTrailingWhite;
            return double.TryParse(
                token.EndsWith(".") ? token + "0" : token, numberStyles,
                CultureInfo.InvariantCulture,
                out value
            );
        }
    }

}