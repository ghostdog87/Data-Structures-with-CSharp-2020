namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (parentheses.Length % 2 == 1 || string.IsNullOrEmpty(parentheses))
            {
                return false;
            }

            var openingBrackets = new Stack<char>();

            foreach (var character in parentheses)
            {
                char expectedCharacter = default;

                switch (character)
                {
                    case ')':
                        expectedCharacter = '(';
                        break;
                    case '}':
                        expectedCharacter = '{';
                        break;
                    case ']':
                        expectedCharacter = '[';
                        break;
                    default:
                        openingBrackets.Push(character);
                        break;
                }

                if(expectedCharacter == default)
                {
                    continue;
                }

                if(openingBrackets.Pop() != expectedCharacter)
                {
                    return false;
                }

            }

            return openingBrackets.Count == 0;
        }
    }
}
