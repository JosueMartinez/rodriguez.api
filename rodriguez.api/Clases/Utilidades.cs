using System;
using System.Globalization;
using System.Threading;

namespace rodriguez.api.Clases
{
    public static class Utilidades
    {
        public static String capitalize(String text)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            text = textInfo.ToLower(text);  //evitando que no se consideren acronimos
            return textInfo.ToTitleCase(text);
        }
    }
}