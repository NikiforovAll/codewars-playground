using System;
using System.Linq;
using System.Collections.Generic;
namespace CodeWars.Kyu4.RankingPokerHands
{
    // The first character is the value of the card: 2, 3, 4, 5, 6, 7, 8, 9, T(en), J(ack), Q(ueen), K(ing), A(ce)
    // The second character represents the suit: S(pades), H(earts), D(iamonds), C(lubs)
    using CardList = List<(string value, string suit)>;
    using Combination = Predicate<List<(string value, string suit)>>;
    using ParamCombination = Func<List<(string value, string suit)>, int, bool>;
    public enum Result
    {
        Win,
        Loss,
        Tie
    }

    public static class GameRules
    {
        private static readonly int NOT_MATCHED_COMBINATION = -1;

        private static readonly Combination Royal = (cardList) =>
            cardList.Min(GetCardScore) == 100 && cardList.Max(GetCardScore) == 60;
        private static readonly Combination Straight = (cardList) =>
        {
            var scores = cardList
            .Select(GetCardScore)
            .OrderBy(i => i).ToList();
            for (int i = 1; i < scores.Count; i++)
            {
                // TODO: handle circular ACE situation
                if (scores[i] - scores[i - 1] != 10)
                    return false;
            }
            return true;
        };
        private static readonly Combination Three = (cardList) =>
            cardList.GroupBy((c) => c.value).Any(group => group.Count() == 3);
        private static readonly ParamCombination Pair = (cardList, num) =>
            cardList.GroupBy((c) => c.value).Where(group => group.Count() == 2).Count() == num;
        private static readonly Combination Flush = (cardList) =>
            cardList.All(c => c.suit == cardList[0].suit);

        private static readonly Func<CardList, int, int> GetHandDiscriminatorScrore =
            (cardList, numCards) =>
            {
                // no combo
                if (numCards == 0)
                {
                    return cardList.Max(GetCardScore) / 10;
                }
                // straight
                if (numCards == -1)
                {
                    return cardList.Select(GetCardScore).Min() / 10;
                }
                // group of cards
                return cardList.GroupBy((c) => c.value)
                    .First(group => group.Count() == numCards).Select(GetCardScore).First() / 10;
            };
        public static readonly List<Func<CardList, int>> Rules = new List<Func<CardList, int>>
        {
            // Royal Flush
            cl => Royal(cl) && Flush(cl)
                ? 200 : NOT_MATCHED_COMBINATION,
            // Straight Flush
            cl => Straight(cl) && Flush(cl)
                ? 180 + GetHandDiscriminatorScrore(cl, 0) : NOT_MATCHED_COMBINATION,
            // Four of Kind
            cl =>  cl.GroupBy((c) => c.value).Select(group => group.Count()).Max() == 4
                ? 160 + GetHandDiscriminatorScrore(cl, 4)
                : NOT_MATCHED_COMBINATION,
            // Full house
            cl =>  Three(cl) && Pair(cl, 1)
                ? 140 : NOT_MATCHED_COMBINATION,
            // Flush
            cl => Flush(cl)
                ? 120 + GetHandDiscriminatorScrore(cl, 0): NOT_MATCHED_COMBINATION,
            // Straight
            cl => Straight(cl)
                ? 100 + GetHandDiscriminatorScrore(cl, -1) : NOT_MATCHED_COMBINATION,
            // Three
            cl => Three(cl)
                ? 80 + GetHandDiscriminatorScrore(cl, 3): NOT_MATCHED_COMBINATION,
            // Two Pairs
            cl => Pair(cl, 2)
                ? 60 : NOT_MATCHED_COMBINATION,
            // Pair
            cl => Pair(cl, 1)
                ? 40 + GetHandDiscriminatorScrore(cl, 2) : NOT_MATCHED_COMBINATION,
            // High Card
            cl => GetHandDiscriminatorScrore(cl, 0)
        };
        public static int GetCardScore((string value, string suit) card) =>
            card switch
            {
                ("A", _) => 140,
                ("K", _) => 130,
                ("Q", _) => 120,
                ("J", _) => 110,
                ("T", _) => 100,
                ("9", _) => 90,
                ("8", _) => 80,
                ("7", _) => 70,
                ("6", _) => 60,
                ("5", _) => 50,
                ("4", _) => 40,
                ("3", _) => 30,
                ("2", _) => 20,
                _ => throw new ArgumentException("Card was not recognized"),
            };
    }
    public class PokerHand : IComparable<PokerHand>
    {

        private readonly CardList _hand;
        public PokerHand(string hand)
        {
            _hand = hand.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => (s[0].ToString(), s[1].ToString()))
                .ToList();
        }

        public Result CompareWith(PokerHand hand)
        {
            int comparison = hand.CompareTo(this);
            if (comparison > 0)
            {
                return Result.Win;
            }
            else if (comparison < 0)
            {
                return Result.Loss;
            }
            return Result.Tie;
        }

        public int CompareTo(PokerHand other)
        {
            var result = this.GetHandScore() - other.GetHandScore();
            if (result == 0)
            {
                result = this.GetScoreSequence()
                    .Zip(other.GetScoreSequence(), (s1, s2) => s1 - s2)
                    .SkipWhile(s => s == 0).FirstOrDefault();
            }
            return result * -1;
        }

        public int GetHandScore()
        {
            CardList hand = this._hand;
            var combination = GameRules.Rules.First(gr => gr(hand) > 0);
            return combination(hand);
        }

        public IEnumerable<int> GetScoreSequence()
        {
            return this._hand.Select(GameRules.GetCardScore).OrderByDescending(i => i);
        }

        public override string ToString(){
            return string.Join(' ', _hand);
        }
    }

}
