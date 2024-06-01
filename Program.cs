using static ProyectoFinal.Program;
using System.Net;

namespace ProyectoFinal
{
    internal class Program
    {
        public struct Alumno
        {
            public int indice;
            public string nombre;
            public string apellido;
            public int dni;
            public string fechaNacimiento;
            public string domicilio;
            public bool estaActivo;
        }

        public static string alumnosPath = "alumnos.txt";

        // DAR DE ALTA UN ALUMNOOOOO
        static void AltaAlumno(ref Alumno nuevoAlumno)
        {
            List<Alumno> listita = new List<Alumno>();
            List<Alumno> listaVacia = new List<Alumno>();
            int listAlum;

            if (TraerAlumnosDeArchivo(alumnosPath) != null && TraerAlumnosDeArchivo(alumnosPath).Count() > 0)
            {
                listita = TraerAlumnosDeArchivo(alumnosPath);
                listAlum = listita.Count;
            }
            else
            {
                listAlum = 0;
            }

            Alumno alumno = new Alumno();
            int dni;
            do
            {
                Console.WriteLine("Ingrese DNI");
            } while (!int.TryParse(Console.ReadLine(), out dni));

            if (listita.Exists(alumno => alumno.dni == dni))
            {
                for (int i = 0; i < listita.Count; i++)
                {
                    if (listita[i].dni == dni)
                    {
                        if (listita[i].estaActivo == false)
                        {
                            Console.WriteLine("El alumno con el dni ingresado se encuentra en la base de datos pero está desactivado");
                            char opcionElegida;
                            do
                            {
                                Console.WriteLine("Desea activarlo? s/n");
                            } while (!char.TryParse(Console.ReadLine(), out opcionElegida) || (opcionElegida != 's' && opcionElegida != 'n'));
                            if (opcionElegida == 's')
                            {
                                Alumno al = listita[i];
                                al.estaActivo = true;
                                listita[i] = al;
                            }
                            EscribirAlumnoEnArchivo(listita, false);
                        }
                        else
                        {
                            Console.WriteLine("Error: el dni ya esta ingresado");
                        }
                    }
                }
            }
            else
            {
                alumno.indice = ++listAlum;
                Console.WriteLine("Ingrese nombre del alumno");
                alumno.nombre = Console.ReadLine();
                Console.WriteLine("Ingrese apellido del alumno");
                alumno.apellido = Console.ReadLine();
                alumno.dni = dni;
                Console.WriteLine("Ingrese fecha de nacimiento en el formato dd/mm/yy");
                alumno.fechaNacimiento = Console.ReadLine();
                Console.WriteLine("Ingrese domicilio");
                alumno.domicilio = Console.ReadLine();
                alumno.estaActivo = true;
                nuevoAlumno = alumno;
                listaVacia.Add(nuevoAlumno);
                Console.WriteLine("Alumno ingresado correctamente");
                Console.WriteLine();
            }
            EscribirAlumnoEnArchivo(listaVacia, true);
        }

        // ESCRIBIR ALUMNO EN EL ARCHIVOOOO
        public static void EscribirAlumnoEnArchivo(List<Alumno> listaAlumnos, bool concatenar)
        {
            using (StreamWriter sw = new StreamWriter(alumnosPath, concatenar))
            {
                foreach (Alumno alumno in listaAlumnos)
                {
                    sw.WriteLine($"{alumno.indice},{alumno.nombre},{alumno.apellido},{alumno.dni},{alumno.fechaNacimiento},{alumno.domicilio},{alumno.estaActivo}");
                }
            }
        }

        // TRAER ALUMNOOOOO DE ARCHIVOOOO
        public static List<Alumno> TraerAlumnosDeArchivo(string archivo)
        {
            List<Alumno> listaAlumnos = new List<Alumno>();

            if (!File.Exists(alumnosPath))
            {
                using (StreamWriter sw = File.CreateText(alumnosPath)) ;
            }

            using (StreamReader sr = new StreamReader(archivo))
            {
                string linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] alumnoCSV = linea.Split(',');
                    Alumno alumnoStruct = new Alumno();
                    alumnoStruct.indice = int.Parse(alumnoCSV[0]);
                    alumnoStruct.nombre = alumnoCSV[1];
                    alumnoStruct.apellido = alumnoCSV[2];
                    alumnoStruct.dni = int.Parse(alumnoCSV[3]);
                    alumnoStruct.fechaNacimiento = alumnoCSV[4];
                    alumnoStruct.domicilio = alumnoCSV[5];
                    alumnoStruct.estaActivo = bool.Parse(alumnoCSV[6]);
                    listaAlumnos.Add(alumnoStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaAlumnos;
        }

        // BAJA ALUMNOOOOO
        public static void BajaAlumno()
        {
            List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
            int dniAlumno;
            do
            {
                Console.WriteLine("Ingrese el dni del alumno a dar de baja");
            } while (!int.TryParse(Console.ReadLine(), out dniAlumno));

            if (listaAlumnos.Any(elemento => elemento.dni == dniAlumno))
            {
                for (int i = 0; i < listaAlumnos.Count; i++)
                {
                    if (listaAlumnos[i].dni == dniAlumno)
                    {
                        Alumno alumno = listaAlumnos[i];
                        alumno.estaActivo = false;
                        Console.WriteLine("El alumno fue dado de baja correctamente");
                        listaAlumnos[i] = alumno;
                    }
                }
                EscribirAlumnoEnArchivo(listaAlumnos, false);
            }
            else
            {
                Console.WriteLine($"El alumno con el dni {dniAlumno} no existe");
            }
        }

        // MODIFICAR ALUMNOOOO
        static void ModificarAlumno(List<Alumno> listaAlumnos)
        {
            int dniIngresado;
            do
            {
                Console.WriteLine("Ingrese el dni del alumno que quiere modificar");
            } while (!int.TryParse(Console.ReadLine(), out dniIngresado));
            if (listaAlumnos.Exists(alumno => alumno.dni == dniIngresado))
            {
                for (int i = 0; i < listaAlumnos.Count; i++)
                {
                    if (listaAlumnos[i].dni == dniIngresado)
                    {
                        Alumno alumnoModificado = listaAlumnos[i];
                        Console.WriteLine("Ingrese nombre del alumno");
                        alumnoModificado.nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese apellido del alumno");
                        alumnoModificado.apellido = Console.ReadLine();
                        int dni;
                        do
                        {
                            Console.WriteLine("Ingrese DNI");
                        } while (!int.TryParse(Console.ReadLine(), out dni));
                        alumnoModificado.dni = dni;
                        Console.WriteLine("Ingrese fecha de nacimiento en el formato dd/mm/yy");
                        alumnoModificado.fechaNacimiento = Console.ReadLine();
                        Console.WriteLine("Ingrese domicilio");
                        alumnoModificado.domicilio = Console.ReadLine();

                        listaAlumnos[i] = alumnoModificado;
                        Console.WriteLine("Alumno modificado correctamente");
                        Console.WriteLine();
                    }
                }
                EscribirAlumnoEnArchivo(listaAlumnos, false);
            }
            else
            {
                Console.WriteLine("No se encontro ningun alumno con ese dni");
            }
        }


        // MENU DE ALUMNOSSSS
        static void MenuAlumnos()
        {
            string opcion;
            do
            {
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*            MENU ALUMNOS           *");
                Console.WriteLine("*-----------------------------------*");
                Console.WriteLine("*        Ingrese una opcion         *");
                Console.WriteLine("*          1 - Alta alumno          *");
                Console.WriteLine("*          2 - Baja alumno          *");
                Console.WriteLine("*          3 - Modificacion alumno  *");
                Console.WriteLine("*          4 - Alumnos activos      *");
                Console.WriteLine("*          5 - Alumnos inactivos    *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*          0 - Salir                *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                string linea = "INDICE".PadRight(10) + "NOMBRE".PadRight(20) + "APELLIDO".PadRight(15) + "DNI".PadRight(10) + "F.NAC".PadRight(15) + "DOMICILIO".PadRight(15) + "ACTIVO";

                if (opcion == "1")
                {
                    Alumno nuevoAlumno = new Alumno();
                    AltaAlumno(ref nuevoAlumno);


                }
                else if (opcion == "2")
                {
                    BajaAlumno();
                }
                else if (opcion == "3")
                {
                    List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
                    ModificarAlumno(listaAlumnos);
                }
                else if (opcion == "4")
                {
                    List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
                    Console.WriteLine(linea);
                    foreach (Alumno alumno in listaAlumnos)
                    {
                        if (alumno.estaActivo == true)
                        {
                            Console.Write($"{alumno.indice.ToString().PadRight(10)}{alumno.nombre.PadRight(20)}{alumno.apellido.PadRight(15)}{alumno.dni.ToString().PadRight(10)}{alumno.fechaNacimiento.PadRight(15)}{alumno.domicilio.PadRight(15)}{alumno.estaActivo} ");
                            Console.WriteLine();
                        }
                    }
                }
                else if (opcion == "5")
                {
                    List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
                    Console.WriteLine(linea);
                    foreach (Alumno alumno in listaAlumnos)
                    {
                        if (alumno.estaActivo == false)
                        {
                            Console.Write($"{alumno.indice.ToString().PadRight(10)}{alumno.nombre.PadRight(20)}{alumno.apellido.PadRight(15)}{alumno.dni.ToString().PadRight(10)}{alumno.fechaNacimiento.PadRight(15)}{alumno.domicilio.PadRight(15)}{alumno.estaActivo} ");
                            Console.WriteLine();
                        }
                    }
                }
            } while (opcion != "0");
        }

        // MAINNNNN
        static void Main(string[] args)
        {
            string opcion;
            do
            {
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*             OPCIONES              *");
                Console.WriteLine("*-----------------------------------*");
                Console.WriteLine("*        Ingrese una opcion         *");
                Console.WriteLine("*          1 - Alumnos              *");
                Console.WriteLine("*          2 - Materias             *");
                Console.WriteLine("*          3 - Inscripcion          *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*          0 - Salir                *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                if (opcion == "1")
                {
                    MenuAlumnos();
                }
                else if (opcion == "2")
                {
                    Console.WriteLine("TODO MATERIAS");
                }
                else if (opcion == "3")
                {
                    Console.WriteLine("TODO INSCRIPCION");
                }
            } while (opcion != "0");

        }
    }
}