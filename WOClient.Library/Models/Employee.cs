namespace WOClient.Library.Models
{
    public class Employee: Person
    {
        public Employee(int personId, int managerId, string firstName, string lastName, string email) : base(personId, managerId, firstName, lastName, email)
        {

        }
    }
}
