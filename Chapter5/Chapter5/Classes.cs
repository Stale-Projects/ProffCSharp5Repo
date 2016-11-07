using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter5
{
    class IntegerList
    {
        public void Add(int integer)
        {
            throw new NotImplementedException();
        }
         public int this[int index]
        {
            get { throw new NotImplementedException(); }
        }
    }

    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

    }

    class BookList
    {
        public void Add(Book book)
        {
            throw new NotImplementedException();
        }

        public Book this[int index]
        {
            get { throw new NotImplementedException(); }
        }
    }


    class ObjectList
    {
        public void Add(object objectItem)
        {
            throw new NotImplementedException();
        }

        public object this[int index]
        {
            get { throw new NotImplementedException(); }
        }
    }

    class GenericList<T>
    {
        public void Add(T element)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get { throw new NotImplementedException(); }
        }
    }


    


}
