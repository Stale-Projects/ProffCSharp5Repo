namespace EnumerarArchivos
{
    class Mamifero
    {
        public int Peso { get; set; }
        public int Edad { get; set; }

        public string Especie { get; set; }

        public override bool Equals(object obj)
        {
            //Si obj no es un Mamifero lo siguiente da null
            //Cubre también el caso en que obj sea null
            var otroMamifero = obj as Mamifero;

            return otroMamifero == null ?
                false :
                Equals(Peso, otroMamifero.Peso) &&
                Equals(Edad, otroMamifero.Edad) &&
                Equals(Especie.ToLower(), otroMamifero.Especie.ToLower());


        }

        public override int GetHashCode()
        {
            //Utilizando un método con primos (simplón)
            int hash = 113;
            hash = hash * (13 + Peso);
            hash = hash * (13 + Edad);
            hash = hash * (13 + Especie != null ? Especie.GetHashCode() : 0);

            return hash;
        }
    }
}
