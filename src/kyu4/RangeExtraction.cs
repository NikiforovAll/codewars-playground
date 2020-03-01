namespace CodeWars.Kyu4.Snail
{
    using System.Text;
    public class RangeExtraction
    {
        public static string Extract(int[] args)
        {
            int start = 0, end = 1;
            var sb = new StringBuilder();
            int Append(int s, int e, bool isAll = false)
            {
                e = isAll ? e : e - 1;
                if (e - s > 1)
                {
                    sb.Append($"{args[s]}-{args[e]},");
                    return e + 1;
                }
                else
                {
                    for (int i = start; i <= e; i++)
                    {
                        sb.Append(args[i] + ",");
                    }
                }
                return end;
            }
            while (end < args.Length)
            {
                if (args[end] - args[end - 1] == 1)
                {
                    end++;
                    continue;
                }
                else
                {
                    start = Append(start, end);
                    end = start + 1;
                }
            }
            if (start < args.Length) {
                Append(start, --end, true);
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
