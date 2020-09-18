using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public interface IPerson: INotifyPropertyChanged
    {
        #region Properties
        bool IsAllMyTasksArchived { get; set; }
        bool IsMyTasksArchivedExists { get; set; }
        int ManagerId { get; set; }
        int PersonId { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        PermissionsEnum Permission { get; set; }
        ObservableCollection<MyTask> MyTasks { get; }
        #endregion

        #region Methods
        void CheckIfAllMyTasksArchived();
        void CheckIfAnyMyTasksArchived();
        void LockTask(MyTask task);
        void MoveMyTaskFromArchive(MyTask task);
        void MoveTaskToArchive(MyTask task);
        void Reset();
        Task UpdateAsync();
        #endregion
    }
}