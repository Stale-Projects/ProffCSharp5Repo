namespace Chapter5
{
    public class Account
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public Account(string name, decimal balance)
        {
            this.Name = name;
            this.Balance = balance;
        }
    }
}
