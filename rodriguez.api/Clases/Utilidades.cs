using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

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