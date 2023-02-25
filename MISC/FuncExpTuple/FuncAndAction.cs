using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace FuncExpTuple
{
    public class FuncAndAction
    {


        public static void Run()
        {
            string palabra = "aaasssdddffghhhjjk";
            //Console.WriteLine(PrimerNoRepetidoClassic(palabra));
            //Console.WriteLine(GetPrimerNoRepetidoFunc(palabra));
            //Ejecutar(x => x.First(), "Hola");
            //Ejecutar(x => x.Last(), "Hola");
            //Ejecutar(x => 'x', "Hola");
            Ejecutar( palabra);
            //Ejecutar(GetPrimerNoRepetidoFunc, palabra);
            //DoSomething();
            //DoSomething2("Hola xD");


        }

 


        public static void Ejecutar(string palabra) 
        {

            Console.WriteLine(PrimerNoRepetidoClassic(palabra));

            char PrimerNoRepetidoClassic(string palabra)
            {
                int sumar(int x, int y) 
                {

                    return x + y;
                }

                int i, j;
                bool isRepeted = false;
                char[] chars = palabra.ToCharArray();
                for (i = 0; i < chars.Length; i++)
                {
                    isRepeted = false;
                    for (j = 0; j < chars.Length; j++)
                    {
                        if ((i != j) && (chars[i] == chars[j]))
                        {
                            isRepeted = true;
                            break;
                        }
                    }
                    if (isRepeted == false)
                    {
                        return chars[i];
                    }
                }
                return ' ';
            }

        
        }


        public static Func<string, char> GetPrimerNoRepetidoFunc
            = s => s.ToArray()
                   .GroupBy(x => x)
                   .Where(x => x.Count() == 1)
                   .Select(x => x.Key)
                   .FirstOrDefault();


        
        
        public static Action DoSomething = () => Console.WriteLine("Se hizo algo");
               
        public static Action<string> DoSomething2 = (s) => Console.WriteLine($"Se hizo algo {s}");




        public Predicate<string> esJuanPredicate = s => s == "Juan";

        public static Func<string, bool> esJuan = s =>
        {

            if (s == "Juan")
            {
                return true;
            }
            else
            {

                return false;
            }
        };



    }
}

