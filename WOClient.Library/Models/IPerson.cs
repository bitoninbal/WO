using System.Collections.ObjectModel;
using System.ComponentModel;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public interface IPerson: INotifyPropertyChanged
    {
        int ManagerId { get; set; }
        int PersonId { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        PermissionsEnum Permission { get; set; }
        ObservableCollection<MyTask> MyTasks { get; }
    }
}