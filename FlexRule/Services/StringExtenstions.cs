using System;
using System.Collections.Generic;
using System.Text;

namespace FlexRule.Services
{
    public static class StringExtenstions
    {
        public static string ReverseWords(this String text)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<char> charList = new List<char>();
            for (int index = 0; index < text.Length; index++)
            {
                if (text[index] == ' ')
                {
                    for (int i = charList.Count - 1; i >= 0; i--)
                        stringBuilder.Append(charList[i]);

                    stringBuilder.Append(' ');

                    charList = new List<char>();
                }
                else
                    charList.Add(text[index]);
            }

            for (int i = charList.Count - 1; i >= 0; i--)
                stringBuilder.Append(charList[i]);

            return stringBuilder.ToString();
        }
    }
}
