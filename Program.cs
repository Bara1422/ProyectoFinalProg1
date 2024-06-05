using static ProyectoProg1.Program;

namespace ProyectoProg1
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

        public struct Materia
        {
            public int indice;
            public string nombreMateria;
            public bool estaActiva;
        }

        public struct Inscripcion
        {
            public int indice;
            public int indice_alumno;
            public int indice_materia;
            public string estado;
            public double nota;
            public string fecha;
        }

        public static string alumnosPath = "alumnos.txt";
        public static string materiasPath = "materias.txt";
        public static string inscripcionPath = "inscripcion.txt";

        // ------------------------------------------------------------ UTILIDADES -----------------------------------------------------
        // ------------------------------------------------------------ UTILIDADES -----------------------------------------------------
        // ------------------------------------------------------------ UTILIDADES -----------------------------------------------------
        // ------------------------------------------------------------ UTILIDADES -----------------------------------------------------

        // VALIDAR CHAR
        static char validarCharSON(string mensaje)
        {
            char opcionChar;
            do
            {
                Console.WriteLine(mensaje);
            } while (!char.TryParse(Console.ReadLine(), out opcionChar) || (opcionChar != 's' && opcionChar != 'n'));

            return opcionChar;
        }

        // VALIDAR NUMERO

        static int validarNumero(string mensaje)
        {
            int numero;
            do
            {
                Console.WriteLine(mensaje);
            } while (!int.TryParse(Console.ReadLine(), out numero) && numero >= 0);
            return numero;
        }


        /// <summary>
        /// hola esto es un coment
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        static int pedirDia(string mensaje)
        {
            int dia;
            string diaIngresado;
            do
            {
                Console.WriteLine($"Por favor ingrese el dia {mensaje} (01-31), asegurese de que el dia tenga 2 dígitos");
                diaIngresado = Console.ReadLine();
            } while (!(diaIngresado.Length == 2 && int.TryParse(diaIngresado, out dia) && dia >= 1 && dia <= 31));
            return dia;
        }

        // PEDIR MES
        static int pedirMes(string mensaje)
        {
            int mes;
            string mesIngresado;
            do
            {
                Console.WriteLine($"Por favor ingrese el mes {mensaje} (01-12), asegurece de que el mes tenga 2 dígitos");
                mesIngresado = Console.ReadLine();
            } while (!(mesIngresado.Length == 2 && int.TryParse(mesIngresado, out mes) && mes >= 1 && mes <= 12));
            return mes;
        }

        // PEDIR ANIO
        static int pedirAnio(string mensaje)
        {
            int anio;
            do
            {
                Console.WriteLine($"Por favor ingrese el año {mensaje} completo");
            } while (!(int.TryParse(Console.ReadLine(), out anio) && anio >= 1900 && anio <= 2100));
            return anio;
        }

        static bool esBiciesto(int anio)
        {
            return (anio % 4 == 0 && (anio % 100 != 0 || anio % 400 == 0));
        }

        static string fechaValida(string mensaje)
        {
            int dia, mes, anio;
            dia = pedirDia(mensaje);
            mes = pedirMes(mensaje);
            anio = pedirAnio(mensaje);
            string fechaValida = "";
            bool esFechaValida = true;

            if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
            {
                if (dia > 31)
                {
                    esFechaValida = false;
                }
            }
            else if (mes == 2)
            {
                if (esBiciesto(anio))
                {
                    if (dia > 29)
                    {
                        esFechaValida = false;
                    }
                }
                else
                {
                    if (dia > 28)
                    {
                        esFechaValida = false;
                    }
                }
            }
            if (!esFechaValida)
            {
                Console.WriteLine("La fecha ingresada no es válida");
            }
            else
            {
                fechaValida = $"{dia}/{mes}/{anio}";
            }
            return fechaValida;
        }

        // PEDIR STRING NO NULL

        static string stringValido(string mensaje)
        {
            string cadena;
            do
            {
                Console.WriteLine(mensaje);
                cadena = Console.ReadLine();
            } while (cadena.Length < 1 || cadena.Contains(','));

            return cadena;
        }

        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------

        // DAR DE ALTA UN ALUMNOOOOO
        static void AltaAlumno()
        {
            List<Alumno> listita = new List<Alumno>();
            List<Alumno> listaVacia = new List<Alumno>();
            int indiceDeAlumno;

            if (TraerAlumnosDeArchivo(alumnosPath) != null && TraerAlumnosDeArchivo(alumnosPath).Count() > 0)
            {
                listita = TraerAlumnosDeArchivo(alumnosPath);
                indiceDeAlumno = listita.Count;
            }
            else
            {
                indiceDeAlumno = 0;
            }

            Alumno alumno = new Alumno();
            int dni = validarNumero("Ingrese DNI");

            if (listita.Exists(alumno => alumno.dni == dni))
            {
                for (int i = 0; i < listita.Count; i++)
                {
                    if (listita[i].dni == dni)
                    {
                        if (listita[i].estaActivo == false)
                        {
                            Console.WriteLine("El alumno con el dni ingresado se encuentra en la base de datos pero está desactivado");
                            char opcionElegida = validarCharSON("Desea activarlo? s/n");
                            if (opcionElegida == 's')
                            {
                                Alumno al = listita[i];
                                al.estaActivo = true;
                                listita[i] = al;
                                Console.Clear();
                                Console.WriteLine("Alumno activado correctamente");
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
                alumno.indice = ++indiceDeAlumno;
                alumno.nombre = stringValido("Ingrese nombre del alumno");
                alumno.apellido = stringValido("Ingrese apellido del alumno");
                alumno.dni = dni;
                string fechaIngresada = "";
                do
                {
                    fechaIngresada = fechaValida("de nacimiento");

                } while (fechaIngresada == "");

                alumno.fechaNacimiento = fechaIngresada;
                alumno.domicilio = stringValido("Ingrese domicilio");
                alumno.estaActivo = true;
                listaVacia.Add(alumno);
                Console.Clear();
                Console.WriteLine("Alumno ingresado correctamente");
                Console.WriteLine();
                EscribirAlumnoEnArchivo(listaVacia, true);
            }
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

            if (!File.Exists(archivo))
            {
                using (StreamWriter sw = File.CreateText(archivo)) ;
            }

            using (StreamReader sr = new StreamReader(archivo))
            {
                string? linea = sr.ReadLine();
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
        public static void BajaAlumno(List<Alumno> listaAlumnos)
        {
            int dniAlumno = validarNumero("Ingrese el dni del alumno a dar de baja");

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
            int dniIngresado = validarNumero("Ingrese el dni del alumno que quiere modifiar");
            if (listaAlumnos.Exists(alumno => alumno.dni == dniIngresado))
            {
                for (int i = 0; i < listaAlumnos.Count; i++)
                {
                    if (listaAlumnos[i].dni == dniIngresado)
                    {
                        Alumno alumnoModificado = listaAlumnos[i];
                        alumnoModificado.nombre = stringValido("Ingrese nombre del alumno");
                        alumnoModificado.apellido = stringValido("Ingrese apellido del alumno");
                        int dni = validarNumero("Ingrese DNI");
                        // TODO ingresa dni de otro alumno
                        // for(int j = 0;i < listaAlumnos.Count; i++)
                        // {
                        //     if (listaAlumnos[j].dni)
                        // }
                        alumnoModificado.dni = dni;
                        alumnoModificado.fechaNacimiento = fechaValida("de nacimiento");
                        alumnoModificado.domicilio = stringValido("Ingrese domicilio");
                        listaAlumnos[i] = alumnoModificado;
                        Console.Clear();
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
                Console.WriteLine();
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
                Console.WriteLine("*          0 - Volver               *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                string linea = "INDICE".PadRight(10) + "NOMBRE".PadRight(20) + "APELLIDO".PadRight(15) + "DNI".PadRight(10) + "F.NAC".PadRight(15) + "DOMICILIO".PadRight(15) + "ACTIVO";
                List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);

                if (opcion == "1") //Alta alumno
                {
                    AltaAlumno();
                }
                else if (opcion == "2") // Baja alumno
                {
                    BajaAlumno(listaAlumnos);
                }
                else if (opcion == "3") // Modificacion alumno
                {
                    ModificarAlumno(listaAlumnos);
                }
                else if (opcion == "4") // Alumnos activos
                {
                    if (listaAlumnos.Count > 0)
                    {
                        Console.WriteLine("------------- ALUMNOS ACTIVOS --------------");
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
                    else
                    {
                        Console.WriteLine("No hay alumnos ingresados");
                    }
                }
                else if (opcion == "5") // Alumnos inactivos
                {
                    if (listaAlumnos.Count > 0)
                    {

                        Console.WriteLine("------------- ALUMNOS INACTIVOS ------------");
                        //List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
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
                    else
                    {
                        Console.WriteLine("No hay alumnos ingresados");
                    }
                }
            } while (opcion != "0");
        }

        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------



        // ALTA MATERIAAA
        public static void AltaMateria()
        {
            List<Materia> listaMaterias = new List<Materia>();
            List<Materia> listaMateriasVacia = new List<Materia>();
            int indiceMateria;

            if (TraerMateriasDeArchivo(materiasPath) != null && TraerMateriasDeArchivo(materiasPath).Count() > 0)
            {
                listaMaterias = TraerMateriasDeArchivo(materiasPath);
                indiceMateria = listaMaterias.Count;
            }
            else
            {
                indiceMateria = 0;
            }

            string nombreMateria = stringValido("Ingrese nombre de la materia");
            if (listaMaterias.Exists(materia => materia.nombreMateria.ToLower() == nombreMateria.ToLower()))
            {
                for (int i = 0; i < listaMaterias.Count; i++)
                {
                    if (listaMaterias[i].nombreMateria == nombreMateria)
                    {
                        if (listaMaterias[i].estaActiva == false)
                        {
                            Console.WriteLine("La materia se encuentra en la base de datos pero está desactivada");
                            char opcionActivar = validarCharSON("Desea activarla? s/n");
                            if (opcionActivar == 's')
                            {
                                Materia materiaAModificar = new Materia();
                                materiaAModificar = listaMaterias[i];
                                materiaAModificar.estaActiva = true;
                                listaMaterias[i] = materiaAModificar;
                                Console.Clear();
                                Console.WriteLine("Materia activada correctamente");
                            }
                            EscribirMateriaEnArchivo(listaMaterias, false);
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: La materia {nombreMateria} ya existe");
                        }
                    }
                }
            }
            else
            {
                Materia materia = new Materia();
                materia.indice = ++indiceMateria;
                materia.nombreMateria = nombreMateria;
                materia.estaActiva = true;
                listaMateriasVacia.Add(materia);
                Console.Clear();
                Console.WriteLine($"Materia ingresada correctamente con el indice {materia.indice}");
                EscribirMateriaEnArchivo(listaMateriasVacia, true);
            }
        }

        // ESCRIBIR MATERIA EN ARCHIVOO
        public static void EscribirMateriaEnArchivo(List<Materia> listaMaterias, bool concat)
        {
            using (StreamWriter sw = new StreamWriter(materiasPath, concat))
            {
                foreach (Materia materia in listaMaterias)
                {
                    sw.WriteLine($"{materia.indice},{materia.nombreMateria},{materia.estaActiva}");
                }
            }
        }

        // TRAER MATERIA DE ARCHIVOOO
        public static List<Materia> TraerMateriasDeArchivo(string archivo)
        {
            List<Materia> listaMaterias = new List<Materia>();

            if (!File.Exists(archivo))
            {
                using (StreamWriter sw = File.CreateText(archivo)) ;
            }

            using (StreamReader sr = new StreamReader(archivo))
            {
                string? linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] materiaCSV = linea.Split(',');
                    Materia materiaStruct = new Materia();
                    materiaStruct.indice = int.Parse(materiaCSV[0]);
                    materiaStruct.nombreMateria = materiaCSV[1];
                    materiaStruct.estaActiva = bool.Parse(materiaCSV[2]);
                    listaMaterias.Add(materiaStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaMaterias;
        }

        // BAJA MATERIAAAA

        public static void BajaMateria(List<Materia> listaMaterias)
        {

            int indiceMateria = validarNumero("Ingrese el indice de la materia que quiere dar de baja");
            if (listaMaterias.Exists(materia => materia.indice == indiceMateria))
            {
                for (int i = 0; i < listaMaterias.Count; i++)
                {
                    if (listaMaterias[i].indice == indiceMateria)
                    {
                        Materia materia = listaMaterias[i];
                        materia.estaActiva = false;
                        Console.WriteLine("La materia fue dada de baja correctamente");
                        listaMaterias[i] = materia;
                    }
                }
                EscribirMateriaEnArchivo(listaMaterias, false);
            }
            else
            {
                Console.WriteLine("No existe una materia con ese indice");
            }
        }

        // MODIFICAR MATERIAAAAA
        public static void ModificarMateria(List<Materia> listaMaterias)
        {
            int opcionAModificar = validarNumero("Ingrese el indice de la materia a modifiar");

            if (listaMaterias.Exists(materia => materia.indice == opcionAModificar))
            {
                for (int i = 0; i < listaMaterias.Count; i++)
                {
                    if (listaMaterias[i].indice == opcionAModificar)
                    {
                        Materia materia = new Materia();
                        materia = listaMaterias[i];
                        string nuevoNombreMateria = stringValido("Ingrese el nombre de la materia");
                        if (listaMaterias.Exists(materia => materia.nombreMateria == nuevoNombreMateria))
                        {
                            Console.WriteLine("El nombre de la materia ya existe");
                            return;
                        }
                        else
                        {
                            materia.nombreMateria = nuevoNombreMateria;
                            Console.WriteLine("Materia modificada correctamente");
                            listaMaterias[i] = materia;
                        }

                    }
                }
                EscribirMateriaEnArchivo(listaMaterias, false);
            }
            else
            {
                Console.WriteLine("No se encontro una materia con ese indice");
            }
        }

        // MENU MATERIASSS
        static void MenuMaterias()
        {
            string? opcion;
            do
            {
                Console.WriteLine();
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*            MENU MATERIAS          *");
                Console.WriteLine("*-----------------------------------*");
                Console.WriteLine("*        Ingrese una opcion         *");
                Console.WriteLine("*          1 - Alta materia         *");
                Console.WriteLine("*          2 - Baja materia         *");
                Console.WriteLine("*          3 - Modificacion materia *");
                Console.WriteLine("*          4 - Mostrar materias     *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*          0 - Volver               *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                string linea = "INDICE".PadRight(10) + "NOMBRE".PadRight(20) + "ACTIVA";
                List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);

                if (opcion == "1")
                {

                    AltaMateria();
                }
                else if (opcion == "2")
                {
                    BajaMateria(listaMaterias);
                }
                else if (opcion == "3")
                {
                    ModificarMateria(listaMaterias);
                }
                else if (opcion == "4")
                {
                    if (listaMaterias.Count() != 0)
                    {

                        Console.WriteLine(linea);
                        foreach (Materia materia in listaMaterias)
                        {
                            Console.WriteLine($"{materia.indice.ToString().PadRight(10)}{materia.nombreMateria.PadRight(20)}{materia.estaActiva}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay materias ingresadas");
                    }
                }
            } while (opcion != "0");
        }


        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------

        static void InscribirAlumno(List<Inscripcion> listaInscripcion, List<Materia> listaMaterias)
        {
            int dniIngresado = validarNumero("Ingrese el dni del alumno a inscribir");
            List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
            // List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);
            // List<Inscripcion> listaInscripcion = TraerInscripcionDeArchivo(inscripcionPath);
            List<Inscripcion> nuevaInscripcion = new List<Inscripcion>();
            Alumno alumno = new Alumno();
            Materia materia = new Materia();
            bool existeAlumno = false;
            bool existeMateria = false;
            int indiceInscripcion = 0;

            if (TraerInscripcionDeArchivo(inscripcionPath) != null && TraerInscripcionDeArchivo(inscripcionPath).Count() > 0)
            {
                listaInscripcion = TraerInscripcionDeArchivo(inscripcionPath);
                indiceInscripcion = listaInscripcion.Count;
            }
            else
            {
                indiceInscripcion = 0;
            }

            if (listaAlumnos.Exists(alumno => alumno.dni == dniIngresado))
            {
                for (int i = 0; i < listaAlumnos.Count; i++)
                {
                    if (listaAlumnos[i].dni == dniIngresado)
                    {
                        existeAlumno = true;
                        alumno = listaAlumnos[i];
                    }
                }
            }
            else
            {
                Console.WriteLine($"No existe un alumno con el dni {dniIngresado}");
            }

            if (existeAlumno)
            {

                int indiceMateria = validarNumero("Ingrese indice de la materia a inscribir al alumno");
                // o ya inscripto?


                if (listaMaterias.Exists(materia => materia.indice == indiceMateria))
                {
                    for (int i = 0; i < listaMaterias.Count; i++)
                    {
                        if (listaMaterias[i].indice == indiceMateria)
                        {
                            if (listaMaterias[i].estaActiva)
                            {
                                existeMateria = true;
                                materia = listaMaterias[i];
                            }
                            else
                            {
                                existeMateria = false;
                                Console.WriteLine("La materia no está activa");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"No existe una materia con el indice {indiceMateria}");
                }
            }

            if (existeMateria)
            {
                int i = 0;
                bool alumnoInscripto = listaInscripcion.Exists(inscripcion => inscripcion.indice_alumno == alumno.indice && inscripcion.indice_materia == materia.indice);
                if (alumnoInscripto)
                {
                    Console.WriteLine("El alumno ya esta inscripto a esta materia, vaya a la seccion de modificacion");
                    return;
                }

                Inscripcion inscripcion = new Inscripcion();
                inscripcion.indice = ++indiceInscripcion;
                inscripcion.indice_alumno = alumno.indice;
                inscripcion.indice_materia = materia.indice;
                char cursoChar = validarCharSON("El alumno cursó la materia? s/n");
                inscripcion.estado = "Anotado";
                if (cursoChar == 's')
                {
                    char rindioChar = validarCharSON("El alumno rindió el final? s/n");
                    if (rindioChar == 's')
                    {
                        double notaIngresada;
                        do
                        {
                            Console.WriteLine("Ingrese la nota del alumno entre 1 y 10");
                        } while (!double.TryParse(Console.ReadLine(), out notaIngresada) || (notaIngresada < 1 && notaIngresada > 10));

                        inscripcion.nota = notaIngresada;
                        if (notaIngresada >= 6 && notaIngresada <= 10)
                        {
                            inscripcion.estado = "Aprobado";
                        }
                        else
                        {
                            inscripcion.estado = "Desaprobado";
                        }
                        inscripcion.fecha = fechaValida("del parcial");
                    }
                    else
                    {
                        inscripcion.estado = "Cursado";
                    }
                }
                nuevaInscripcion.Add(inscripcion);
                Console.Clear();
                Console.WriteLine("Alumno inscripto correctamente");
                EscribirInscripcionEnArchivo(nuevaInscripcion, true);

            }

        }

        static void EscribirInscripcionEnArchivo(List<Inscripcion> listaInscripcion, bool concat)
        {
            using (StreamWriter sw = new StreamWriter(inscripcionPath, concat))
            {
                foreach (Inscripcion inscripcion in listaInscripcion)
                {
                    sw.WriteLine($"{inscripcion.indice},{inscripcion.indice_alumno},{inscripcion.indice_materia},{inscripcion.estado},{inscripcion.nota},{inscripcion.fecha}");
                }
            }
        }

        static List<Inscripcion> TraerInscripcionDeArchivo(string archivo)
        {
            List<Inscripcion> listaInscripcion = new List<Inscripcion>();

            if (!File.Exists(archivo))
            {
                using (StreamWriter sw = File.CreateText(archivo)) ;
            }

            using (StreamReader sr = new StreamReader(archivo))
            {
                string? linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] inscripcionCSV = linea.Split(',');
                    Inscripcion inscripcionStruct = new Inscripcion();
                    inscripcionStruct.indice = int.Parse(inscripcionCSV[0]);
                    inscripcionStruct.indice_alumno = int.Parse(inscripcionCSV[1]);
                    inscripcionStruct.indice_materia = int.Parse(inscripcionCSV[2]);
                    inscripcionStruct.estado = inscripcionCSV[3];
                    inscripcionStruct.nota = int.Parse(inscripcionCSV[4]);
                    inscripcionStruct.fecha = inscripcionCSV[5];
                    listaInscripcion.Add(inscripcionStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaInscripcion;
        }

        static void EstadoInscripcionAlumno()
        {
            List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);
            List<Inscripcion> listaInscripcion = TraerInscripcionDeArchivo(inscripcionPath);
            int indiceAlumno = validarNumero("Ingrese indice del alumno para ver su estado");
            Inscripcion alumnoYaInscripto = new Inscripcion();
            Console.Clear();
            if (listaInscripcion.Exists(inscr => inscr.indice_alumno == indiceAlumno))
            {
                string linea = "INDICE".PadRight(10) + "ALUMNO".PadRight(10) + "MATERIA".PadRight(20) + "ESTADO".PadRight(15) + "NOTA".PadRight(10) + "FECHA";
                Console.WriteLine(linea);
                string nombreMateria;
                foreach (Inscripcion inscripcion in listaInscripcion)
                {
                    if (inscripcion.indice_alumno == indiceAlumno)
                    {
                        nombreMateria = "";
                        int i = 0;
                        bool encontre = false;
                        while (i < listaMaterias.Count && !encontre)
                        {
                            if (inscripcion.indice_materia == listaMaterias[i].indice)
                            {
                                encontre = true;
                                nombreMateria = listaMaterias[i].nombreMateria;
                            }
                            i++;
                        }
                        string notaCorregida = inscripcion.nota == 0 ? "-" : inscripcion.nota.ToString();
                        Console.WriteLine($"{inscripcion.indice.ToString().PadRight(10)}{inscripcion.indice_alumno.ToString().PadRight(10)}{nombreMateria.PadRight(20)}{inscripcion.estado.PadRight(15)}{notaCorregida.PadRight(10)}{inscripcion.fecha}");
                    }
                }
                Console.WriteLine();
                /*
                char opcionChar = validarCharSON("Desea modificar el alumno? s/n");
                if (opcionChar == 's')
                {
                    int numeroIngresado;
                    do
                    {
                        Console.WriteLine("Ingrese el indice de la  materia que desea modificar");
                    } while (!int.TryParse(Console.ReadLine(), out numeroIngresado) || numeroIngresado < 0 && numeroIngresado > listaMaterias.Count);

                }*/
            }
            else
            {
                Console.WriteLine("No se encontro ningun alumno inscripto con ese indice");
            }
        }

        static void ModificarInscripcion(List<Inscripcion> listaInscripcion, string linea)
        {
            int indiceAlumno = validarNumero("Ingrese el indice del alumno a modificar");
            List<Inscripcion> nuevaLista = new List<Inscripcion>();

            if (listaInscripcion.Exists(inscr => inscr.indice_alumno == indiceAlumno))
            {
                Console.Clear();
                for (int i = 0; i < listaInscripcion.Count; i++)
                {
                    if (listaInscripcion[i].indice_alumno == indiceAlumno)
                    {
                        // recorrer todas las materias que esta inscripto y preguntar cual quiere modificar
                        nuevaLista.Add(listaInscripcion[i]);
                    }
                }
                Console.WriteLine(linea);
                foreach (Inscripcion inscripcion in nuevaLista)
                {
                    string notaCorregida = inscripcion.nota == 0 ? "-" : inscripcion.nota.ToString();
                    Console.WriteLine($"{inscripcion.indice.ToString().PadRight(10)}{inscripcion.indice_alumno.ToString().PadRight(10)}{inscripcion.indice_materia.ToString().PadRight(20)}{inscripcion.estado.PadRight(15)}{notaCorregida.PadRight(10)}{inscripcion.fecha}");
                }
                Console.WriteLine();
                int indiceMateria = validarNumero("Ingrese el indice de la materia a modificar");

                if (listaInscripcion.Exists(materia => materia.indice_materia == indiceMateria))
                {
                    for (int i = 0; i < nuevaLista.Count; i++)
                    {
                        if (listaInscripcion[i].indice_materia == indiceMateria)
                        {
                            Inscripcion inscripcionMod = listaInscripcion[i];
                            char cursoChar = validarCharSON("El alumno cursó la materia? s/n");
                            inscripcionMod.estado = "Anotado";
                            inscripcionMod.nota = 0;
                            inscripcionMod.fecha = "";
                            if (cursoChar == 's')
                            {
                                char rindioChar = validarCharSON("El alumno rindió el final? s/n");
                                if (rindioChar == 's')
                                {
                                    double notaIngresada;
                                    do
                                    {
                                        Console.WriteLine("Ingrese la nota del alumno entre 1 y 10");
                                    } while (!double.TryParse(Console.ReadLine(), out notaIngresada) || (notaIngresada < 1 && notaIngresada > 10));

                                    inscripcionMod.nota = notaIngresada;
                                    if (notaIngresada >= 6 && notaIngresada <= 10)
                                    {
                                        inscripcionMod.estado = "Aprobado";
                                    }
                                    else
                                    {
                                        inscripcionMod.estado = "Desaprobado";
                                    }
                                    inscripcionMod.fecha = fechaValida("del parcial");
                                }
                                else
                                {
                                    inscripcionMod.estado = "Cursado";
                                }
                            }
                            listaInscripcion[i] = inscripcionMod;
                            Console.Clear();
                            Console.WriteLine("Alumno inscripto con exito");
                            EscribirInscripcionEnArchivo(listaInscripcion, false);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No existe la materia con el indice ingresado");
                }

            }
            else
            {
                Console.WriteLine("No se encontro ningun alumno inscripto con ese indice");
            }
        }

        static void MenuInscripciones()
        {
            string? opcion;
            do
            {
                Console.WriteLine();
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*         Menu Inscripciones        *");
                Console.WriteLine("*-----------------------------------*");
                Console.WriteLine("*        Ingrese una opcion         *");
                Console.WriteLine("*          1 - Inscribir alumno     *");
                Console.WriteLine("*          2 - Estado alunno        *");
                Console.WriteLine("*          3 - Modificar estado     *");
                Console.WriteLine("*          4 - Mostar todo          *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*          0 - Volver               *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                List<Inscripcion> listaInscripcion = TraerInscripcionDeArchivo(inscripcionPath);
                List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);
                string linea = "INDICE".PadRight(10) + "ALUMNO".PadRight(10) + "MATERIA".PadRight(20) + "ESTADO".PadRight(15) + "NOTA".PadRight(10) + "FECHA";
                if (opcion == "1")
                {
                    InscribirAlumno(listaInscripcion, listaMaterias);
                }
                else if (opcion == "2")
                {
                    EstadoInscripcionAlumno();

                }
                else if (opcion == "3")
                {
                    ModificarInscripcion(listaInscripcion, linea);
                }
                else if (opcion == "4")
                {
                    Console.WriteLine(linea);
                    string nombreMateria;
                    foreach (Inscripcion inscripcion in listaInscripcion)
                    {
                        nombreMateria = "";
                        int i = 0;
                        bool encontre = false;
                        while (i < listaMaterias.Count && !encontre)
                        {
                            if (inscripcion.indice_materia == listaMaterias[i].indice)
                            {
                                encontre = true;
                                nombreMateria = listaMaterias[i].nombreMateria;
                            }
                            i++;
                        }

                        string notaCorregida = inscripcion.nota == 0 ? "-" : inscripcion.nota.ToString();
                        Console.WriteLine($"{inscripcion.indice.ToString().PadRight(10)}{inscripcion.indice_alumno.ToString().PadRight(10)}{nombreMateria.PadRight(20)}{inscripcion.estado.PadRight(15)}{notaCorregida.PadRight(10)}{inscripcion.fecha}");
                    }
                }
            } while (opcion != "0");
        }


        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------

        // MAINNNNN
        static void Main(string[] args)
        {
            string? opcion;
            do
            {
                Console.WriteLine();
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
                    MenuMaterias();
                }
                else if (opcion == "3")
                {
                    MenuInscripciones();
                }
            } while (opcion != "0");

        }
    }
}

// Que tiene que hacer la seccion inscripcion?
// tiene que hacer un crud o solo ingresar y leer
// tengo que ingresar la fecha 1x1? o formato dd/mm/yyyy
// nota al estar anotado me aparece en 0
// si modifica, al modificarlo como se a cual materia apunta? doble check
// linea 330 midificar alumno si ingresa otro dni ya existente...

// linea 575 nombre de la materia ya existe use return
// linea 876 recorrer materias inscriptas y filtrar LISTO
// linea 928 al modificar use una lista nuevam tengo que modificar la anterior LISTO