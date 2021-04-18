using BusinessLogic;
using System;

namespace Initiator
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidateSEDOL obj = new ValidateSEDOL();

            //Input string was not 7-characters long
            Console.WriteLine(obj.IsValid("").ValidationDetails);
            Console.WriteLine(obj.IsValid("12").ValidationDetails);
            Console.WriteLine(obj.IsValid("123456789").ValidationDetails);

            //Checksum digit does not agree with the rest of the input
            Console.WriteLine(obj.IsValid("1234567").ValidationDetails);

            //Valid non user define SEDOL
            Console.WriteLine(obj.IsValid("0709954").ValidationDetails);
            Console.WriteLine(obj.IsValid("B0YBKJ7").ValidationDetails);

            //Checksum digit does not agree with the rest of the input
            Console.WriteLine(obj.IsValid("9123451").ValidationDetails);
            Console.WriteLine(obj.IsValid("9ABCDE8").ValidationDetails);

            //SEDOL contains invalid characters
            Console.WriteLine(obj.IsValid("9123_51").ValidationDetails);
            Console.WriteLine(obj.IsValid("VA.CDE8").ValidationDetails);


            //Valid user defined SEDOL
            Console.WriteLine(obj.IsValid("9123458").ValidationDetails);
            Console.WriteLine(obj.IsValid("9ABCDE1").ValidationDetails);
            Console.ReadLine();
        }
    }
}
