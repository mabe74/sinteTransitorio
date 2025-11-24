using System.Text;
using System.Collections.Generic;
using NAudio.Wave;


namespace sintetizador2
{
    public enum ModelosSintes
    {
        MonoPoly = 1,
        ARP2600 = 2,
        Model_D = 3,
        Solina = 4,
        Oddissey = 5,
        MS_5 = 6,
        MS_101 = 7,
        Generico
    }




    internal class sintetizador2
    {

        //Atributos de instancia (cada sinte tienelos suyos)
        private bool _encendido;

        private ModelosSintes _modelo;
        private bool _tieneTeclas;
        private int _numeroDeTeclas;
        private bool _tienePantalla;
        private int _tensionDeTrabajo;
        private string _tipoDeTensionDeTrabajo;
        private int _nivelBateria; //Nivel de bateria del sintetizador (0-100)
        private string _estadoBateriaMensaje;
        private string _mensajeBateria;
        private int _osciladores; //Numero de osciladores
        private int _polifonia; //Numero de notas que puede tocar al mismo tiempo
        private int _opcion; //Elije el sinte

        //================================================================================================================

        //Atributos estáticos (comunes a todos los sintes)
        private static int _cantidadDeSintes = 0;
        private static string _fabricante = "Behringer";
        private static string versionFirmeware = "version 1.0.0";



        //Crea la lista de sintetizadores
        private static List<sintetizador2> listaDeSintes = new List<sintetizador2>();




        //=================================================================================================================

        //Constructor
        public sintetizador2(ModelosSintes modelo, bool tieneTeclas, int numeroDeTeclas, bool tienePantalla, int tensionDeTrabajo,
                             string tipoDeTensionDeTrabajo, int osciladores, int polifonia)

        {
            this._encendido = false; //Valor por defecto

            this._modelo = modelo;
            this._tieneTeclas = tieneTeclas;
            this._numeroDeTeclas = numeroDeTeclas;
            this._tienePantalla = tienePantalla;
            this._tensionDeTrabajo = tensionDeTrabajo;
            this._tipoDeTensionDeTrabajo = tipoDeTensionDeTrabajo;
            this._nivelBateria = 100; //Valor por defecto
            this._estadoBateriaMensaje = "";
            this._mensajeBateria = "";
            this._osciladores = osciladores;
            this._polifonia = polifonia;
            _cantidadDeSintes++;// ver para constructor estatico
        }

        //===================================================================================================================

        //Sobrecarga de constructores



        //Sobrecarga 1: modelo y osciladores
        public sintetizador2(ModelosSintes modelo, int osciladores)

               : this(modelo, false, 0, false, 12, "DC", osciladores, 4)
        {
            // Valores por defecto: sin teclas, sin pantalla, 12V DC, 4 notas de polifonía
        }

        //Sobrecarga 2: modelo solamente
        public sintetizador2(ModelosSintes modelo)
               : this(modelo, false, 0, false, 12, "DC", 2, 4)
        {
            // Modelo básico, sin teclas ni pantalla
        }

        //Sobrecarga 3: sin parámetros (usa valores por defecto)
        public sintetizador2()
               : this(ModelosSintes.Generico, false, 0, false, 12, "DC", 2, 4)
        {
            // Si no se especifica, se crea un MonoPoly básico
        }

        //Sobrecarga 4: modelos y teclas: si / no
        public sintetizador2(ModelosSintes modelo, bool tieneTeclas)

            : this(ModelosSintes.Solina, false, 0, false, 12, "DC", 2, 4)
        {
        }

        //===========================================================================================================================

        // Propiedades estáticas
        public static int CantidadDeSintes => _cantidadDeSintes;
        public static string Fabricante => _fabricante;

        //===========================================================================================================================

        public static string Saludar()
        {
            StringBuilder saludar = new StringBuilder();

            saludar.AppendLine("\nBienvenido!\n");


            return saludar.ToString();
        }

        //===========================================================================================================================



        //Carga las máquinas en la lista
        public static void CargarMaquinas()
        {
            listaDeSintes.Clear();

            listaDeSintes.Add(new sintetizador2(ModelosSintes.MonoPoly, true, 37, true, 12, "DC", 2, 4));
            listaDeSintes.Add(new sintetizador2(ModelosSintes.ARP2600, false, 0, false, 15, "AC", 3, 6));
            listaDeSintes.Add(new sintetizador2(ModelosSintes.Model_D, true, 32, true, 9, "DC", 3, 4));
            listaDeSintes.Add(new sintetizador2(ModelosSintes.Solina, true, 49, true, 12, "DC", 2, 8));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Máquinas cargadas correctamente");
        }


        public static List<sintetizador2> ObtenerLista()
        {

            return listaDeSintes;

        }





        //===========================================================================================================================







        //Sintetizadores
        public string EncenderSinte(bool encendido)
        {
            this._encendido = encendido;

            if (_encendido)
            {

                StringBuilder info1 = new StringBuilder();

                info1.AppendLine($"{_modelo}: ON");


                //info1.AppendLine($"Modelo: {_modelo}");
                info1.AppendLine($"Pantalla: {_tienePantalla}");
                info1.AppendLine($"Tensión: {_tensionDeTrabajo} volts");
                info1.AppendLine($"Tipo de tensión: {_tipoDeTensionDeTrabajo}");
                info1.AppendLine($"Cantidad de osciladores: {_osciladores}");
                info1.AppendLine($"Polifonia: {_polifonia}");

                if (_tieneTeclas)
                {

                    info1.AppendLine($"Número de teclas: {_numeroDeTeclas}");

                }
                else
                {

                    info1.AppendLine($"No posee teclas");

                }


                return info1.ToString();


            }
            else
            {
                StringBuilder info1 = new StringBuilder();
                info1.AppendLine("Sintetizador 1: OFF");
                return info1.ToString();
            }

        }

        //=========================================================================================================================

        public string ChequearBateriaMensaje()
        {
            StringBuilder chequearBateria = new StringBuilder();
            chequearBateria.Append("\nChequeando batería");
            return chequearBateria.ToString();

        }

        //=========================================================================================================================

        public void MoverPuntos()
        {


            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500); // medio segundo de espera (delay en milisegundos)
                Console.Write("."); // imprime un punto sin salto de línea
            }


            //Console.WriteLine(); // para terminar la línea
        }

        //=========================================================================================================================
        //A partir de esta línea
        public class SineWaveProvider : WaveProvider32
        {
            private float frequency;
            private float amplitude = 0.25f;
            private float phase;

            public SineWaveProvider(float frequency = 440f)
            {
                this.frequency = frequency;
            }

            public override int Read(float[] buffer, int offset, int count)
            {
                for (int n = 0; n < count; n++)
                {
                    buffer[n + offset] = amplitude * (float)Math.Sin(phase);
                    phase += (float)(2 * Math.PI * frequency / WaveFormat.SampleRate);
                    if (phase > 2 * Math.PI) ;
                    //phase -= 2 * Math.PI;
                }
                return count;
            }

            public void SetFrequency(float freq)
            {
                frequency = freq;
            }
        }

        public void GenerarOndaSenoAnimada()
        {
            if (_encendido)
            {
                int ancho = 80;
                int alto = 20;
                double frecuencia = 2 * Math.PI / 40;
                int desplazamiento = 0;

                // AUDIO SENO REAL
                var seno = new SineWaveProvider(440f);
                WaveOutEvent waveOut = new WaveOutEvent();
                waveOut.Init(seno);
                waveOut.Play();

                while (true)
                {
                    Thread.Sleep(200);
                    Console.Clear();

                    for (int y = 0; y < alto; y++)
                    {
                        for (int x = 0; x < ancho; x++)
                        {
                            double valor = Math.Sin((x + desplazamiento) * frecuencia);
                            int posY = (int)((valor + 1) * (alto - 1) / 2);

                            if (alto - y - 1 == posY)
                                Console.Write("*");
                            else
                                Console.Write(" ");
                        }
                        Console.WriteLine();
                    }

                    desplazamiento++;



                }
            }
        }








        //Hasta esta línea
        //=============================================================================================================================
        public void GenerarOndaTriangular()
        {






        }

        //=============================================================================================================================

        //Autoreferenciada
        public int NivelBateria
        {
            get
            {
                // Si está fuera de rango, autoregula el valor
                if (_nivelBateria < 0) _nivelBateria = 0;
                else if (_nivelBateria > 100) _nivelBateria = 100;
                return _nivelBateria;
            }
            set
            {
                _nivelBateria = value;
            }
        }



        //Propiedad autocalculada
        public string MostrarEstadoBateriaPorcentaje
        {
            get
            {
                if (_nivelBateria == 100)
                    return "Estado de batería completo";
                else if (_nivelBateria >= 66)
                    return "Estado batería: OK +";
                else if (_nivelBateria >= 33)
                    return "Estado batería: OK -";
                else
                    return "Recargar batería";
            }
        }
    }
}






        
      