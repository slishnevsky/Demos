using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasicsDemo
{
    public static class StringToNumber
    {
        public static void Invoke()
        {
            string convertToInt = "12";
            string nullString = null;
            string maxValue = "32222222222222222222222222222222222";
            string formatException = "12.32";

            int parseResult;

            bool canParse;

            // Never throws any exception
            // If conversion can be done, TryParse sets canParse = true  and assignes converted value to parseResult
            // If conversion cannot be done, TryParse sets canParse = false  and assignes 0 to parseResult

            // For int.TryParse

            // It will perfectly convert interger
            Console.WriteLine("canParse = int.TryParse({0}, out parseResult)", convertToInt);
            canParse = int.TryParse(convertToInt, out parseResult);
            Console.WriteLine("canParse: {0}, parseResult: {1}", canParse, parseResult);
            Console.WriteLine("-----------------------------------");

            // It will raise Argument Null Exception
            Console.WriteLine("canParse = int.TryParse(null, out parseResult)");
            canParse = int.TryParse(nullString, out parseResult);
            Console.WriteLine("canParse: {0}, parseResult: {1}", canParse, parseResult);
            Console.WriteLine("-----------------------------------");

            //It willl raise Over Flow Exception
            Console.WriteLine("canParse = int.TryParse({0}, out parseResult)", maxValue);
            canParse = int.TryParse(maxValue, out parseResult);
            Console.WriteLine("canParse: {0}, parseResult: {1}", canParse, parseResult);
            Console.WriteLine("-----------------------------------");

            // It will raise Format Exception
            Console.WriteLine("canParse = int.TryParse({0}, out parseResult)", formatException);
            canParse = int.TryParse(formatException, out parseResult);
            Console.WriteLine("canParse: {0}, parseResult: {1}", canParse, parseResult);
            Console.WriteLine("-----------------------------------");

            // For int.Parse

            // It will perfectly convert interger
            Console.WriteLine("parseResult = int.Parse({0})", convertToInt);
            parseResult = int.Parse(convertToInt);
            Console.WriteLine("parseResult: {0}", parseResult);
            Console.WriteLine("-----------------------------------");

            // It will raise Argument Null Exception
            Console.WriteLine("parseResult = int.Parse(null)");
            try { parseResult = int.Parse(nullString); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine("-----------------------------------");

            //It willl raise Over Flow Exception
            Console.WriteLine("parseResult = int.Parse({0})", maxValue);
            try { parseResult = int.Parse(maxValue); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine("-----------------------------------");

            // It will raise Format Exception
            Console.WriteLine("parseResult = int.Parse({0})", formatException);
            try { parseResult = int.Parse(formatException); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine("-----------------------------------");

            // For Convert.ToInt32

            // It will perfectly convert integer
            Console.WriteLine("parseResult = Convert.ToInt32({0})", convertToInt);
            parseResult = Convert.ToInt32(convertToInt);
            Console.WriteLine("-----------------------------------");

            // It will ouput as 0 if Null string is there
            Console.WriteLine("parseResult = Convert.ToInt32(null)");
            parseResult = Convert.ToInt32(nullString);
            Console.WriteLine("-----------------------------------");

            // It will raise Over Flow Exception
            Console.WriteLine("parseResult = Convert.ToInt32({0})", maxValue);
            try { parseResult = Convert.ToInt32(maxValue); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine("-----------------------------------");

            // It will raise Format Exception
            Console.WriteLine("parseResult = Convert.ToInt32({0})", formatException);
            try { parseResult = Convert.ToInt32(formatException); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine("-----------------------------------");
        }
    }
}
