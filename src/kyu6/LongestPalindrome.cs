namespace CodeWars.Kyu6.LongestPalindrome
{
    /// <summary>
    /// https://www.codewars.com/kata/longest-palindrome/
    /// </summary>
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Kata
    {
        public static int GetLongestPalindrome(string str)
        {
            int length = str?.Length ?? 0;
            // i - starting index of palindrom substring, j - length of palindrom for i row
            bool[,] palindromes = new bool[length, length];
            (int startIndex, int length) result = (0, length != 0 ? 1 : 0);
            // length of palindrome 1
            for (int i = 0; i < length; i++)
            {
                palindromes[i, 0] = true;
                result.startIndex = i;
            }
            //length of palindrome 2
            for (int i = 0; i < length - 1; i++)
            {
                if (str[i] == str[i + 1])
                {
                    palindromes[i, 1] = true;
                    result.startIndex = i;
                    result.length = 2;
                }
            }
            //length of palindrome 3
            for (int j = 2; j < length; j++)
            {
                for (int i = 0; i < length - 1; i++)
                {
                    if (i + j > length - 1)
                    {
                        break;
                    }
                    if (palindromes[i + 1, j - 2] && str[i] == str[i + j])
                    {
                        palindromes[i, j] = true;
                        result.startIndex = i;
                        result.length = j + 1;
                    }
                }
            }
            return result.length;
        }
    }
}