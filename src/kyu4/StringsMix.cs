using System;
using System.Collections.Generic;
using System.Linq;
namespace CodeWars.Kyu4.StringsMix {
    public class Mixing {
        public static string Mix (string s1, string s2) {
            var _tokenSource = new List<Dictionary<char, FrequencyToken>> (2) {
                    FrequencyToken.Parse (s1, 1),
                    FrequencyToken.Parse (s2, 2)
                };
            List<FrequencyToken> tokens = MergeTokenSources (_tokenSource)
                .OrderByDescending (t => t.ToString ().Length)
                .ThenBy (t => t.ParentTokenSource)
                .ThenBy (t => t.Id)
                .ToList ();
            return Display (tokens);

            List<FrequencyToken> MergeTokenSources (List<Dictionary<char, FrequencyToken>> tokenSource) {
                List<FrequencyToken> result = new List<FrequencyToken> ();
                var uniqueKeys = tokenSource.SelectMany (i => i.Keys).Distinct ();
                foreach (var key in uniqueKeys) {
                    var candidates = tokenSource.Where (dictionary => dictionary.ContainsKey (key))
                        .Select (dictionary => dictionary[key]);
                    FrequencyToken tokenWithMaxValue = candidates.Max ();
                    if (tokenWithMaxValue.Count != 1) {
                        if (candidates.Count (ts => ts.Count == tokenWithMaxValue.Count) == tokenSource.Count ()) {
                            tokenWithMaxValue.DiscardTokenParent ();
                        }
                        result.Add (tokenWithMaxValue);
                    }
                }
                return result;
            }
            string Display (List<FrequencyToken> items) => String.Join ("/", items);
        }
        private class FrequencyToken : IComparable<FrequencyToken> {
            private static int ID_OF_TOKEN_WITHOUT_PARENT = 100;
            private FrequencyToken (char id, int parentTokenSource, int count = 1) {
                this.Id = id;
                this.ParentTokenSource = parentTokenSource;
                this.Count = count;

            }
            public int Count { get; set; }
            public char Id { get; }
            public int ParentTokenSource { get; private set; }

            public FrequencyToken (int tokenSource) {

            }
            public int CompareTo (FrequencyToken other) {
                if (other == null) {
                    return 1;
                }
                return this.Count.CompareTo (other.Count);
            }

            public void DiscardTokenParent () {
                this.ParentTokenSource = ID_OF_TOKEN_WITHOUT_PARENT;
            }

            public static Dictionary<char, FrequencyToken> Parse (string s, int tokenSourceId) {
                var chars = s.ToCharArray ().Where (c => Char.IsLower (c));
                var charsSet = chars.Distinct ();
                return charsSet
                    .Select (c => new FrequencyToken (c, tokenSourceId, chars.Count (cc => c.Equals (cc)))).ToDictionary (ft => ft.Id);
            }

            public override string ToString () {
                string id = ParentTokenSource != ID_OF_TOKEN_WITHOUT_PARENT ? ParentTokenSource.ToString () : "=";
                return $"{id}:{new string(Id, Count)}";
            }
        }

    }
}
