namespace Chapter7
{
    class Classes
    {

    }

    class OneMan
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public OneMan(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public static implicit operator AnotherMan(OneMan person)
        {
            return new AnotherMan(person.FirstName + " " + person.LastName);
        }

    }

    class AnotherMan
    {

        public string FullName { get; set; }


        public AnotherMan(string fullname)
        {
            FullName = fullname;
        }

        //Aunque es más intuitivo (al menos para mí) poner la conversión en el tipo origen, funciona exactamente igual si la 
        //pongo en el tipo destino, y además el código es exactamente igual
        //Pero ojo, esto convierte de Man a AnotherMan y no viceversa
        //public static implicit operator AnotherMan(OneMan person)
        //{
        //    return new AnotherMan(person.FirstName + " " + person.LastName);
        //}

    }

    //Una clase que representa números enteros
    class Entero
    {
        public int Valor { get; set; }

        public virtual int[] Divisores()
        {
            int[] divisores = new int[2] { -1, 1 };
            return divisores;
        }


        public Entero(int valor)
        {
            Valor = valor;
        }

    }

    class EnteroPar : Entero
    {

        //Suponemos que es un entero mayor o igual que dos
        //y que retorno sólo los divisores hasta el 2
        public EnteroPar(int valor) : base(valor)
        {
            //Acá no necesito nada
        }

        public override int[] Divisores()
        {
            int[] divisores = new int[4];
            int[] divisoresBase = base.Divisores();

            divisores[0] = divisoresBase[0];
            divisores[1] = divisoresBase[1];
            divisores[2] = 2;
            divisores[3] = -2;
            return divisores;
        }


        public static explicit operator EnteroPrimo(EnteroPar par)
        {
            //Para no correr riesgos retorno un nuevo objeto
            //Estos es una mutación forzada de EnteroPar a EnteroPrimo
            return new EnteroPrimo(par.Valor);
        }


    }

    class EnteroPrimo : Entero
    {
        public EnteroPrimo(int valor) : base(valor)
        {
            //No hace falta código
        }


        public override int[] Divisores()
        {
            int[] divisores = new int[4];
            int[] divisoresBase = base.Divisores();

            divisores[0] = divisoresBase[0];
            divisores[1] = divisoresBase[1];
            divisores[2] = Valor;
            divisores[3] = -1 * Valor;
            return divisores;
        }

        public static explicit operator EnteroPar(EnteroPrimo primo)
        {
            //Para no correr riesgos retorno un nuevo objeto
            //Estos es una mutación forzada de EnteroPrimo a EnteroPar
            return new EnteroPar(primo.Valor);
        }
    }


}


