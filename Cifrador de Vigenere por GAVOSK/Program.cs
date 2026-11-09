using System;
using System.Text;
using System.Linq;

namespace SistemaGAVOSK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Suite de Criptografía GAVOSK - Versión Integral";

            Console.WriteLine("====================================================");
            Console.WriteLine("      SISTEMA CRIPTOGRÁFICO INTEGRAL GAVOSK");
            Console.WriteLine("====================================================\n");

            Console.WriteLine("1. Encriptar mensaje con clave");
            Console.WriteLine("2. Descifrar mensaje con clave");
            Console.Write("\nSeleccione una opción: ");
            string opcion = Console.ReadLine();

            Console.Write("\nIngrese el mensaje: ");
            string mensaje = Console.ReadLine();

            Console.Write("Ingrese la palabra clave (Llave): ");
            string llave = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(mensaje) || string.IsNullOrWhiteSpace(llave)) return;

            // La magia ocurre aquí: Procesamos internamente, pero el usuario no ve el texto "mutilado"
            string resultado = ProcesarVigenere(mensaje, llave, opcion == "1");

            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine(opcion == "1" ? "RESULTADO ENCRIPTADO:" : "RESULTADO DESCIFRADO:");
            Console.WriteLine(resultado);
            Console.WriteLine(new string('=', 40));

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        static string ProcesarVigenere(string texto, string llave, bool encriptar)
        {
            StringBuilder salida = new StringBuilder();

            // Normalizamos la llave internamente para el cálculo
            string llaveNorm = NormalizarInterno(llave).Replace(" ", "");
            int indiceLlave = 0;

            foreach (char c in texto)
            {
                if (char.IsLetter(c))
                {
                    // Convertimos la letra actual a su base (sin tilde) para el cálculo
                    char letraBase = NormalizarInterno(c.ToString())[0];
                    int m = letraBase - 'A';
                    int k = llaveNorm[indiceLlave % llaveNorm.Length] - 'A';

                    int operacion;
                    if (encriptar)
                        operacion = (m + k) % 26;
                    else
                        operacion = (m - k + 26) % 26;

                    salida.Append((char)(operacion + 'A'));
                    indiceLlave++;
                }
                else
                {
                    salida.Append(c); // Mantenemos espacios, puntos, etc.
                }
            }
            return salida.ToString();
        }

        static string NormalizarInterno(string texto)
        {
            // Solo para cálculos matemáticos internos
            string conTildes = "ÁÉÍÓÚÑ";
            string sinTildes = "AEIOUN";
            StringBuilder sb = new StringBuilder();
            foreach (char c in texto.ToUpper())
            {
                int index = conTildes.IndexOf(c);
                sb.Append(index != -1 ? sinTildes[index] : c);
            }
            return sb.ToString();
        }
    }
}