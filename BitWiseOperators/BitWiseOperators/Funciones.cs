namespace BitWiseOperators
{
    class Funciones
    {
        /// <summary>
        /// Me deveuelve una representación de los ceros y unos que componen un int 
        /// en formato string
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetIntBinaryString(int n)
        {
            //Primero creo un array de 32 chars que representan los 32 bits del int
            char[] b = new char[32];

            int pos = 31;
            int i = 0;

            while (i < 32)
            {
                //En esta línea el 1 puede representarse por 0000 0000 0000 0000 0000 0000 0000 0001
                //Entonces 1<<1 sería por ejemplo:           0000 0000 0000 0000 0000 0000 0000 0010 
                //Voy corriendo el 1 y me fijo con el operaodr & si el número original tenía un 1 ahí
                //Esta es provista por la web: http://www.dotnetperls.com/xor
                //Ver mi propia implementación: GetIntBinaryStringMV
                if ((n & (1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            //Retornar el array de chars convertido a String
            return new string(b);
        }

        public static string GetIntBinaryStringMV(int value)
        {
            char[] charArray = new char[32];
            for (int i = 31; i >= 0; i--)
            {
                charArray[31 - i] = (((1 << i) & value) != 0) ? '1' : '0';

            }

            return new string(charArray);
        }
    }
}
