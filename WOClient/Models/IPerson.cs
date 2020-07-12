using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WOClient.Models
{
    public interface IPerson: INotifyPropertyChanged
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int ManagerId { get; set; }
        int PersonId { get; set; }
        ObservableCollection<MyTask> MyTasks { get; }
    }
}