using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlledSubstancesApplication.Unity;

namespace CSATestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //843294
            string mrn = "843294";
            AllscriptsQuery q = new AllscriptsQuery(mrn);
            string pt_name = q.GetPatientFullNameLastNameFirst();
            Console.WriteLine(pt_name);

            Console.Read();
        }
    }
}
