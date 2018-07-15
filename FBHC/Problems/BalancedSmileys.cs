using System;

namespace FBHC.Problems
{
    class BalancedSmileys:Problem
    {
		public override ProblemConfig Config => new ProblemConfig("Balanced Smileys", 2013, 0);

		public override string SolveTestCase(string[] input)
        {
            _isBalanced = new bool?[input[0].Length+1, input[0].Length / 2+2];
            return IsBalanced(input[0]) ? "YES" : "NO";
        }

        private bool?[,] _isBalanced;
        private bool IsBalanced(string input, int d = 0)
        {
            if (_isBalanced[input.Length, d].HasValue)
            {
                return _isBalanced[input.Length, d].Value;
            }

            if (input == string.Empty)
            {
                return d == 0;
            }

            char c = input[0];

            bool balanced = false;
            
            if (d > input.Length)
            {
                balanced = false;
            }
            else if (c == '(')
            {
                balanced= IsBalanced(input.Substring(1), d + 1);
            }
            else if (c == ')')
            {
                balanced = d > 0 && IsBalanced(input.Substring(1), d - 1);
            }
            else if (c == ':')
            {
                if (input.Length > 1)
                {
                    if (input[1] == ')' || input[1] == '(')
                    {
                        if (IsBalanced(input.Substring(2), d))
                        {
                            balanced = true;
                        }
                    }
                }
                if (!balanced)
                {
                    balanced = IsBalanced(input.Substring(1), d);
                }
            }
            else if (char.IsLetter(c) || c == ' ')
            {
                balanced = IsBalanced(input.Substring(1), d);
            }
            
            _isBalanced[input.Length, d] = balanced;

            return balanced;
        }
    }
}
