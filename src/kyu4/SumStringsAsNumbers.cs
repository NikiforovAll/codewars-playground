using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeWars.Kyu4.SumStringsAsNumbers {
    public static class Kata {
        public static string sumStrings (string a, string b) {
            return new string (
                new BigNumber(new string[] { a, b }).Reverse()
                                                    .ToArray()
            );
        }
    }

    public class BigNumber : IEnumerable<char> {
        private readonly string[] terms;
        public BigNumber (string[] terms) {
            this.terms = terms;
        }
        public IEnumerator<char> GetEnumerator () {

            byte[][] parsedTerms = terms.Select (t => ToDigit (Reverse (t))).ToArray ();
            var maxLength = parsedTerms.Select (s => s.Length).Max ();
            byte overflow = 0;
            for (int i = 0; i < maxLength || overflow != 0; i++) {
                var sum = 0;
                foreach (var term in parsedTerms.Where (t => t.Length > i)) {
                    sum += term[i];
                }
                sum += overflow;
                overflow = (byte) (sum / 10);
                yield return Convert.ToChar (sum % 10 + '0');
            }

            char[] Reverse (string s) {
                var res = s.ToCharArray()
                    .SkipWhile(d => d.Equals('0'))
                    .ToArray();
                Array.Reverse (res);
                return res;
            }
            byte[] ToDigit (char[] arr) {
                return arr
                    .Select<char, byte> (c => byte.Parse (c.ToString ()))
                    .ToArray ();
            }
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return this.GetEnumerator ();
        }
    }
}
