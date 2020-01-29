using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvrakOnayUI.Helper
{
    public class RandomString
    {
        private static string letters = "qwertyuopasdfghjklizxcvbnm123456789QWERTYIOPASDFGHJKLMNBVCXZ";
        private static Random rnd = new Random();
        public static string GetRandomKey() {
            string key=String.Empty;
            for (int i = 0; i < 20; i++)
            {
                key += letters[rnd.Next(letters.Length)];
            }
            return key;
        }
    }
}