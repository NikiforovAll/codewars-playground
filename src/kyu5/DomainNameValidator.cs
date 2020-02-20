using System.Linq;
using System;
namespace CodeWars.Kyu5.DomainNameValidator
{
    public class DomainNameValidator
    {
        private const char DomainSeparator = '.';
        private const char MinusCharacter = '-';
        public bool validate(string domain)
        {
            var levelNames = domain.Split(DomainSeparator);
            Predicate<string>[] domainLevelValidators = new Predicate<string>[] {
                (string d) => d.Length <= 253,
            };
            Predicate<string[]>[] levelNameListValidators = new Predicate<string[]>[] {
                (string[] lns) => lns.Length <= 127 && lns.Length > 1,
                (string[] lns) => !Int32.TryParse(lns.Last(), out _)
            };
            Predicate<string>[] levelNameValidators = new Predicate<string>[] {
                (string ln) => ln.Length > 0 && ln.Length < 64,
                (string ln) => !ln.StartsWith(MinusCharacter) && !ln.EndsWith(MinusCharacter),
                (string ln) => ln.All(c =>
                    (Char.IsLetterOrDigit(c) && (c < 128))|| c == MinusCharacter)
            };
            return domainLevelValidators.All(p => p.Invoke(domain))
                && levelNameListValidators.All(p => p.Invoke(levelNames))
                && levelNames.All(ln => levelNameValidators.All(p => p.Invoke(ln)));
        }
    }
}
