using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.HelloWorld
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // HelloPersonTest();


            Int32 x;
            int y;

            x = 10;

            decimal price = 10.99m;

            double speed = 10.5;
            float weigth = 70.07f;
            char sign = 'a';
            char first = "Marcin"[0];

            DateTime today = DateTime.Now;

            bool isOn = true;

            int[] numbers = new int[6];

            int[] happyNumbers = new int[] { 3, 5, 8 };

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void HelloPersonTest()
        {
            Console.WriteLine("Podaj imię");
            string firstName = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko");
            string lastName = Console.ReadLine();

            // zła praktyka
            // string message = "Witaj " + firstName + " " + lastName;

            // string message = string.Format("Witaj {0} {1}", firstName, lastName);

            // C# 6.0
            string message = $"{firstName} {lastName}";

            Console.WriteLine(message);
        }
    }
}
