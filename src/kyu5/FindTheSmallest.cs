namespace CodeWars.Kyu5.FindTheSmallest
{

    /// <summary>
    /// https://www.codewars.com/kata/find-the-smallest
    /// </summary>
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ToSmallest
    {
        public static long[] Smallest(long n)
        {
            var result = new long[] { n, 0, 0 };
            var digits = n.ToString()
                .ToCharArray()
                .Select(c => (byte)int.Parse(c.ToString()))
                .ToList<byte>();

            for (var i = 0; i < digits.Count - 1; i++)
            {
                var solutionIndex1 = FindMinOnInterval(digits, i);
                var solutionIndex2 = FindMaxShiftOnSequence(digits, i);

                var move1 = (
                    from: solutionIndex1,
                    to: i,
                    isValidMove: solutionIndex1 != i
                );
                var move2 = (
                    from: i,
                    to: solutionIndex2,
                    isValidMove: solutionIndex2 != i
                );
                var finalMove = (from: 0, to: 0, isValidMove: false);
                List<byte> finalSequence = null;
                if (move1.isValidMove || move2.isValidMove)
                {
                    var sequence1 = Transform(digits, move1.from, move1.to);
                    var sequence2 = Transform(digits, move2.from, move2.to);
                    var k = 0;
                    var comp = true;
                    for (; k < sequence1.Count && move1.isValidMove && move2.isValidMove; k++)
                    {
                        if (sequence1[k] < sequence2[k])
                        {
                            comp = true;
                            break;
                        }else if(sequence1[k] > sequence2[k]){
                            comp = false;
                            break;
                        }
                        if(k == sequence1.Count -1){
                            comp = move1.from < move2.from;
                        }
                    }
                    
                    finalMove =  comp? move1 : move2;
                    finalSequence = comp ? sequence1 : sequence2;

                }

                if (finalMove.isValidMove)
                {
                    result[0] = ToLong(finalSequence);
                    result[1] = finalMove.from;
                    result[2] = finalMove.to;
                    break;
                }
            }
            return result;
        }

        private static int FindMinOnInterval(List<byte> source, int startIndex)
        {
            var min = source[startIndex];
            var index = startIndex;
            for (var i = startIndex + 1; i < source.Count; i++)
            {
                if (min == 0)
                    break;
                min = source[i] < min ? source[index = i] : min;
            }
            return index;
        }

        private static int FindMaxShiftOnSequence(List<byte> source, int startIndex)
        {
            byte baseElement = source[startIndex];
            int index = startIndex;
            for (; index + 1 != source.Count && source[index + 1] < baseElement; index++)
            { }
            return index;
        }
        private static List<byte> Transform(List<byte> source, int from, int to)
        {
            var digits = source.Select(d=>d).ToList();
            byte tmp = digits[from];
            digits.RemoveAt(from);
            if(to < from)
            {
                digits.Insert(to, tmp);
            }else{
                digits.Insert(to, tmp);
            }
            return digits;
        }
        private static long ToLong(List<byte> digits)
        {
            long acc = 0;
            var digitsNoZeros = digits.SkipWhile(d => d == 0).ToList();
            long j = (long)Math.Pow(10, digitsNoZeros.Count - 1);
            for (int i = 0; i < digitsNoZeros.Count; i++, j /= (long)10)
            {
                acc += (long)digitsNoZeros[i] * j;
            }
            return acc;
        }
    }
}