using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpBasicsDemo
{
    public static class StringToDouble
    {
        public static void Invoke()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

            Console.WriteLine("Thread.CurrentThread.CurrentCulture = new CultureInfo(\"ru-RU\")\n");

            // if input string format is different than a format of current culture
            // input string is considered invalid, unless input string format culture is provided as additional parameter

            string input = "1 234 567,89"; // Russian Culture format
            // string input = "1234567.89"; // Invariant Culture format

            double result; // preset before conversion to see if it changes
            bool success = false;

            try
            {
                // never fails
                // if conversion is impossible - returns false and default double (0.00)

                Console.WriteLine("double.TryParse(\"{0}\")\n", input);
                success = double.TryParse(input, out result);
                Console.WriteLine(success ? "SUCCESS\n" : "FAILED\n");

                Console.WriteLine("double.TryParse(\"{0}\", NumberStyles.Number, CultureInfo.InvariantCulture, out result)\n", input);
                success = double.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
                Console.WriteLine(success ? "SUCCESS\n" : "FAILED\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: {0}\n", ex.Message);
            }

            try
            {
                // if input is null, returns default double (0.00)
                // if input is invalid - fails (Input string was not in a correct format exception)

                Console.WriteLine("Convert.ToDouble(\"{0}\")\n", input);
                result = Convert.ToDouble(input);
                Console.WriteLine("SUCCESS\n");

                Console.WriteLine("Convert.ToDouble(\"{0}\", CultureInfo.InvariantCulture)\n", input);
                result = Convert.ToDouble(input, CultureInfo.InvariantCulture);
                Console.WriteLine("SUCCESS\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: {0}\n", ex.Message);
            }

            try
            {
                // if input is null - fails (Value cannot be null)
                // if input is invalid - fails (Input string was not in a correct format exception)

                Console.WriteLine("double.Parse(\"{0}\")\n", input);
                result = double.Parse(input);
                Console.WriteLine("SUCCESS\n");

                Console.WriteLine("double.Parse(\"{0}\", CultureInfo.InvariantCulture)\n", input);
                result = double.Parse(input, CultureInfo.InvariantCulture);
                Console.WriteLine("SUCCESS\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: {0}\n", ex.Message);
            }
        }
    }
}
