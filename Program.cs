using System.ComponentModel;

namespace sintetizador2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int opcion;

            //Carga la lista de sintetizadores
            sintetizador2.CargarMaquinas();
            ISintetizador sinteAdaptado1 = new MonoPolyAdapter();
            ISintetizador sinteAdaptado2 = new ARP2600Adapter();
            ISintetizador sinteAdaptado3 = new Model_DAdapter();
            ISintetizador sinteAdaptado4 = new SolinaAdapter();
            ISintetizador sinteAdaptado5 = new OddisseyAdapter();
            ISintetizador sinteAdaptado6 = new MS_5Adapter();
            ISintetizador sinteAdaptado7 = new MS_101Adapter();
            ISintetizador sinteAdaptado8 = new GenericoAdapter();

            List<ISintetizador> sintes = new List<ISintetizador>()
            {
                new MonoPolyAdapter(),
                new ARP2600Adapter(),
                new Model_DAdapter(),
                new SolinaAdapter(),
                new OddisseyAdapter(),
                new MS_5Adapter(),
                new MS_101Adapter(),
                new GenericoAdapter()
            };

            if (opcion >= 1 && opcion <= sintes.Count)
            {
                ISintetizador sinte = sintes[opcion - 1];

                Console.WriteLine(sinte.EncenderSinte(true));
                Console.WriteLine(sinte.ChequearBateriaMensaje());
                Task.Run(() => sinte.GenerarOndaSenoAnimada());
            }



            do
            {

                /*
                //Crea las instancias de los objetos por medio del constructor base
                sintetizador2 miSinte1 = new sintetizador2(ModelosSintes.MonoPoly, true, 32, false, 12, "DC", 2, 1);
                sintetizador2 miSinte2 = new sintetizador2(ModelosSintes.ARP2600, false, 16, false, 12, "DC", 4, 1);
                sintetizador2 miSinte3 = new sintetizador2(ModelosSintes.Model_D, false, 16, false, 12, "DC", 4, 1);
                sintetizador2 miSinte4 = new sintetizador2(ModelosSintes.Solina, false, 16, false, 12, "DC", 4, 1);
                sintetizador2 miSinte5 = new sintetizador2(ModelosSintes.Oddissey, false, 16, false, 12, "DC", 2, 1);
                sintetizador2 miSinte6 = new sintetizador2(ModelosSintes.MS_5, false, 16, false, 12, "DC", 2, 1);
                sintetizador2 miSinte7 = new sintetizador2(ModelosSintes.MS_101, false, 16, false, 12, "DC", 2, 1);
                */
               
                //Crea las instancias de los objetos por medio de sobrecargas de constructores
                sintetizador2 sinte1 = new sintetizador2(ModelosSintes.MonoPoly, true, 37, true, 15, "DC", 3, 6);//Crea un sinte con todos los parámetros
                sintetizador2 sinte2 = new sintetizador2(ModelosSintes.ARP2600, 2);//Crea un sinte a partir del modelo y la cantidad de osciladores
                sintetizador2 sinte3 = new sintetizador2(ModelosSintes.Model_D);//Crea un sinte a partir del modelo
                sintetizador2 sinte4 = new sintetizador2(ModelosSintes.Solina, false);//Crea un sintetizador a partir del modelo y la cantidad de teclas
                sintetizador2 sinte5 = new sintetizador2(ModelosSintes.Oddissey, 1);
                sintetizador2 sinte6 = new sintetizador2();//Crea un sinte genérico sin parámetros
                sintetizador2 sinte7 = new sintetizador2();//Crea un sinte genérico sin parámetros
                sintetizador2 sinte8 = new sintetizador2();//Crea un sinte genérico sin parámetros



                Console.WriteLine($"Fabricante: {sintetizador2.Fabricante}");
                Console.WriteLine($"Cantidad de sintetizadores creados: {sintetizador2.CantidadDeSintes}");

                string saludo = sintetizador2.Saludar();
                Console.WriteLine(saludo);

                Console.WriteLine("*** MENÚ ***");
                Console.WriteLine("Elija una opción:");

                Console.WriteLine("1.- MonoPoly");
                Console.WriteLine("2.- ARP2600");
                Console.WriteLine("3.- Model D");
                Console.WriteLine("4.- ARP Solina");
                Console.WriteLine("5.- Oddissey");
                Console.WriteLine("6.- MS 5");
                Console.WriteLine("7.- MS 101");
                Console.WriteLine("8.- Genérico");
                Console.WriteLine("9.- Salir");



                //opcion = int.Parse(Console.ReadLine());


                //Corrobora que no se ingrese un caracter erróneo
                if (!int.TryParse(Console.ReadLine(), out opcion)){

                    Console.WriteLine("Opción inválida, intente nuevamente...");
                    continue;

                }

                //Obtiene la lista cargada
                //sintetizador2 lista = sintetizador2.ObtenerLista();
                var lista = sintetizador2.ObtenerLista();


                //Enciende y apaga las máquinas
                string infoEncendido1 = sinte1.EncenderSinte(true); 
                string infoEncendido2 = sinte2.EncenderSinte(true); 
                string infoEncendido3 = sinte3.EncenderSinte(true); 
                string infoEncendido4 = sinte4.EncenderSinte(true); 
                string infoEncendido5 = sinte5.EncenderSinte(true);
                string infoEncendido6 = sinte6.EncenderSinte(true); 
                string infoEncendido7 = sinte7.EncenderSinte(true); 
                string infoEncendido8 = sinte8.EncenderSinte(true);

                if (opcion >= 1 && opcion <= lista.Count)
                {
                    sintetizador2 sinteSeleccionado = lista[opcion - 1];

                    Console.WriteLine(sinteSeleccionado.EncenderSinte(true));
                    Console.WriteLine(sinteSeleccionado.ChequearBateriaMensaje());
                    sinteSeleccionado.MoverPuntos();
                    sinteSeleccionado.NivelBateria = 75;
                    Console.WriteLine($"Carga batería: {sinteSeleccionado.NivelBateria}%");
                    Console.WriteLine(sinteSeleccionado.MostrarEstadoBateriaPorcentaje);
                    //sinte1.GenerarOndaSeno();
                    //sinte1.GenerarOndaSenoAnimada();
                    
                    //Ejecutar la onda en un Task (no bloquea el menú)
                    Task.Run(() => sinteSeleccionado.GenerarOndaSenoAnimada());
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();



            } while (opcion != 9);
            { Console.WriteLine("Bye bye....");}
        }
    }
}


/*
 
 Sobrecargas
 // Crea un sinte con todos los parámetros
var sinte1 = new sintetizador2(ModelosSintes.ARP2600, true, 37, true, 15, "DC", 3, 6);

// Crea un sinte indicando solo el modelo y cantidad de osciladores
var sinte2 = new sintetizador2(ModelosSintes.Model_D, 2);

// Crea un sinte indicando solo el modelo
var sinte3 = new sintetizador2(ModelosSintes.MS_101);

// Crea un sinte genérico sin parámetros
var sinte4 = new sintetizador2();

 
 
 //miSinte1.GenerarOndaSeno();
 //miSinte1.GenerarOndaSenoAnimada();

 
 
 
 
 
 
 
 
 
 
 */