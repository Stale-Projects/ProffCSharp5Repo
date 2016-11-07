namespace Chapter8
{
    class Employee
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private decimal _salary;

        public decimal Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        public Employee(string name, decimal salary)
        {
            _name = name;
            _salary = salary;
        }

        //La Comparacion es por salario
        public static int CompareTo(Employee OneGuy, Employee OtherGuy)
        {
            if (OneGuy.Salary > OtherGuy.Salary)
            {
                return -1;
            }
            else if (OneGuy.Salary == OtherGuy.Salary)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

    }
}
