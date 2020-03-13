using System.Text;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace CodeWars.Kyu3.BinomialExpansion
{

    public class KataSolution
    {
        public static string Expand(string expr)
        {
            var binom = new BinomialExpression(expr)
                .Compile();
            return binom.ToString();
        }
    }

    public class PolynomialExpression : Expression
    {

        public PolynomialExpression(IEnumerable<PolynomTerm> members, ParameterExpression compiledParam)
        {
            this.Expr = FromPolynomTermsToExpression(members, compiledParam);
        }
        public PolynomialExpression(string expr)
        {
            var compiledParam = GeneratePolynomialParameter();
            var matches = new Regex(@"(?<mul>-{0,1}\d*)?(?<base>[a-z]+)?\^?(?<pow>\d)?").Matches(expr);
            var terms = from match in matches
                        where !string.IsNullOrEmpty(match.Value)
                        let tokens = match.Groups
                        let multiplier = !string.IsNullOrEmpty(tokens["mul"].Value)
                            ? double.Parse(
                                tokens["mul"].Value == "-" ? "-1" : tokens["mul"].Value)
                            : 1
                        let pow = !string.IsNullOrEmpty(tokens["pow"].Value)
                            ? double.Parse(tokens["pow"].Value) : 1
                        let param = !string.IsNullOrEmpty(tokens["base"].Value)
                            ? compiledParam : null
                        select new PolynomTerm()
                        {
                            Pow = pow,
                            Param = param,
                            Multiplier = multiplier,
                        };
            Expr = FromPolynomTermsToExpression(terms.ToList(), compiledParam);
        }

        private LambdaExpression FromPolynomTermsToExpression(IEnumerable<PolynomTerm> terms, ParameterExpression compiledParam)
        {
            // TODO: it could be really convenient if it is possible to subclass Expression and pass it to lambda expression as DTO
            LambdaExpression polynom = Expression.Lambda(
                terms.First().BuildInnerExpression(), compiledParam);
            foreach (var term in terms.Skip(1))
            {
                var shouldAdd = true; //term.Multiplier > 0;
                var innerExpr = term.BuildInnerExpression();

                var operation = shouldAdd
                    ? Expression.Add(polynom.Body, innerExpr)
                    : Expression.Subtract(polynom.Body, innerExpr);
                polynom = Expression.Lambda(operation, compiledParam);
            }
            return polynom;
        }

        public string Body { get => Expr.Body.ToString(); }
        public LambdaExpression Expr { get; private set; }

        public PolynomialExpression Multiply(PolynomialExpression polynomialExpression)
        {
            var pol1 = FetchPolynomialMembers(this.Expr.Body);
            var pol2 = FetchPolynomialMembers(polynomialExpression.Expr.Body);
            var sums = from member1 in pol1
                       from member2 in pol2
                       let md1 = FetchMemberDetails(member1)
                       let md2 = FetchMemberDetails(member2)
                       select (multiplier: md1.multiplier * md2.multiplier,
                           pow: md1.pow + md2.pow);

            var reducedSum = from member in sums
                             group member by member.pow
                             into g
                             select (multiplier: aggregateMultipliers(g), pow: g.Key)
                             into aggregatedSums
                             orderby aggregatedSums.pow descending
                             where aggregatedSums.multiplier != 0
                             select aggregatedSums;
            var compiledParam = GeneratePolynomialParameter();

            var polynomTerms = reducedSum.Select(m => new PolynomTerm()
            {
                Multiplier = m.multiplier,
                Pow = m.pow,
                Param = m.pow != 0 ? compiledParam : null
            });
            return new PolynomialExpression(polynomTerms.ToList(), compiledParam);

            double aggregateMultipliers(IEnumerable<(double multiplier, int pow)> members) =>
                members.Select(m => m.multiplier).Aggregate((acc, curr) => acc + curr);
        }

        private static (double multiplier, int pow) FetchMemberDetails(Expression expr)
        {
            switch (expr)
            {

                case BinaryExpression me when me.NodeType == ExpressionType.Multiply:
                    var mul = parseConstant(me.Left as ConstantExpression);
                    return (mul, getRightPartOfExpression(me.Right));
                case BinaryExpression me when me.NodeType == ExpressionType.Power:
                    return (1, getRightPartOfExpression(me));
                case ConstantExpression ce:
                    return (parseConstant(ce), 0);
                case ParameterExpression _:
                    return (1, 1);
                default:
                    throw new ArgumentException("Not known expression type: " + expr);
            };

            static double parseConstant(ConstantExpression e) =>
                double.Parse(e.Value.ToString());
            static int getRightPartOfExpression(Expression e)
            {
                switch (e)
                {
                    case BinaryExpression be:
                        var rightExpression = be.Right as ConstantExpression;
                        var pow = int.Parse(rightExpression.Value.ToString());
                        return pow;
                    case ParameterExpression _:
                        return 1;
                    case ConstantExpression ce:
                        return int.Parse(ce.Value.ToString());
                    default:
                        throw new ArgumentException("Not known expression type: " + e);
                }
            }
        }

        private static IEnumerable<Expression> FetchPolynomialMembers(Expression expr)
        {

            var res = new List<Expression>();
            switch (expr)
            {
                case BinaryExpression ae when ae.NodeType == ExpressionType.Add:
                    res.Add(ae.Right);
                    res.AddRange(FetchPolynomialMembers(ae.Left));
                    break;
                case BinaryExpression me when
                    me.NodeType == ExpressionType.Multiply
                    || me.NodeType == ExpressionType.Power:
                    res.Add(me);
                    break;
                case ParameterExpression pe:
                    res.Add(pe);
                    break;
                case ConstantExpression ce:
                    res.Add(ce);
                    break;
                default:
                    throw new ArgumentException("Not known expression type: " + expr);
            }
            return res;
        }

        private static ParameterExpression GeneratePolynomialParameter()
        {
            return Expression.Parameter(typeof(double));
        }

        public class PolynomTerm
        {
            public double Multiplier { get; set; }
            public double Pow { get; set; }

            public ParameterExpression Param { get; set; }

            public Expression BuildInnerExpression()
            {
                var mulExpr = Constant(Multiplier);
                var powArgExpr = Constant(Pow);
                var baseExpr = powArgExpr as Expression;
                if (Param != null && Pow > 1)
                {
                    baseExpr = Power(Param, powArgExpr);
                }
                else if (Param != null && Pow == 1)
                {
                    baseExpr = Param;
                }
                return Param switch
                {
                    null => mulExpr,
                    _ => Multiplier != 1 ? Multiply(Constant(Multiplier), baseExpr) : baseExpr
                };
            }
        }
    }

    public class BinomialExpression
    {
        public PolynomialExpression Expr { get; private set; }
        public string _var;

        public string Source { get; }

        public BinomialExpression(string expr)
        {
            Source = expr;
            _var = expr.First(char.IsLetter).ToString();
        }

        public BinomialExpression Compile()
        {
            this.Expr = this.Reduce();
            return this;
        }

        public override string ToString()
        {
            var replacements = new Dictionary<string, string>
            {
                ["**"] = "^",
                [" "] = string.Empty,
                // ["^1"] = "",
                ["("] = "",
                [")"] = "",
                // ["\b1*"] = "",
                ["-1*"] = "-",
                ["+-"] = "-",
                ["*"] = "",
                ["Param_0"] = _var,
            };
            var sb = new StringBuilder(Expr.Body);
            foreach (var kvp in replacements)
            {
                sb.Replace(kvp.Key, kvp.Value);
            }
            return sb.ToString();
        }

        public double Compute(double x)
        {
            var lambda = Expr.Expr.Compile() as Func<double, double>;
            return lambda(x);
        }

        private PolynomialExpression Reduce()
        {
            var @base = new Regex(@"(?<=\().+?(?=\))").Match(this.Source).Value;
            int pow = int.Parse(this.Source.Split('^')[1]);
            return pow switch
            {
                0 => new PolynomialExpression("1"),
                1 => new PolynomialExpression(@base),
                _ => Enumerable.Range(1, pow)
                    .Select(_ => new PolynomialExpression(@base))
                    .Aggregate((acc, curr) => acc.Multiply(curr))
            };
        }
    }
}
