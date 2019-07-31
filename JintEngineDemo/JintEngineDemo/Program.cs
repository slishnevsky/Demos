using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JintEngine
{
    class Program
    {
        private static readonly string[] roles = new string[] { "LOC_IND", "MANAGERL3" };
        private static readonly Hashtable credentials = new Hashtable { { "country", "CANADA" }, { "age", 8 } };

        static void Main(string[] args)
        {
            var impersonator = "role=='Administrator' OR (role=='LOC_IND' AND country=='CANADA')";
            var includeUsers = "country=='CANADA' AND (role=='MANAGERL1' OR role=='MANAGERL2' OR role=='MANAGERL3' OR role=='LOC_IND') AND age>67";
            var excludeUsers = "role=='MANAGERL1' OR role=='MANAGERL2'";

            try
            {
                var canImpersonate = Evaluate(impersonator);
                var canBeImpersonated = Evaluate(includeUsers);
                canBeImpersonated &= !Evaluate(excludeUsers);

                Console.WriteLine(canImpersonate ? "Impersonator:\tSUCCESS" : "Impersonator:\tFAILED");
                Console.WriteLine(canBeImpersonated ? "Impersonatee:\tSUCCESS" : "Impersonatee:\tFAILED");

                var result = canImpersonate && canBeImpersonated;

                Console.WriteLine(result ? "Impersonation:\tSUCCESS" : "Impersonation:\tFAILED\n");

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

        }

        private static bool Evaluate(string rules)
        {
            // Here we are building Javascript code that will be evaluated from .NET

            // DATA: To hold the roles we need to define an array in JS and populate it with roles
            var data = "var roles=[];";
            foreach (var role in roles)
                data += string.Format("roles.push('{0}');", role);

            // DATA: Adding credentials variables with values
            foreach (DictionaryEntry entry in credentials)
                data += (entry.Value is string) ? string.Format("var {0}='{1}';", entry.Key, entry.Value) : string.Format("var {0}={1};", entry.Key, entry.Value);

            // RULE: We use Regex to find and replace multiple roles conditions with JS code
            rules = Regex.Replace(rules, "role==('[^']*')", "roles.indexOf($1)>=0");

            // RULE: Replacing pseudo ANDs and ORs with JS equivalent
            rules = rules.Replace(" AND ", "&&").Replace(" OR ", "||");

            // Define foo function that returns the result
            // Glue together data and config condition
            var code = string.Format("{0};function foo(){{return {1}}};", data, rules);

            // Executing
            var result = new Jint.Engine().Execute(code).Invoke("foo").AsBoolean();

            return result;
        }
    }
}



