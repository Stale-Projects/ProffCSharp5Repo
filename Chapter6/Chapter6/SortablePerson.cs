using System;

namespace Chapter6
{
    class SortablePerson : IComparable<SortablePerson>
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public int CompareTo(SortablePerson otherPerson)
        {
            if (otherPerson == null)
                return 0;
            int lastNameComparison = this.LastName.CompareTo(otherPerson.LastName);
            if (lastNameComparison == 0)
            {
                return this.FirstName.CompareTo(otherPerson.FirstName);
            }
            else
            {
                return lastNameComparison;
            }
        }

        public override string ToString()
        {
            return _lastName + ", " + _firstName;
        }
    }
}
