using System.Threading.Tasks;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public class Employee: Person
    {
        public Employee(PermissionsEnum permission,
                        int personId,
                        int managerId,
                        string firstName,
                        string lastName,
                        string email) :
            base(permission,
                personId,
                managerId,
                firstName,
                lastName,
                email)
        {
            Task.Run(InitAsync);
        }

        #region Public Methods
        public override void LockTask(MyTask task)
        {
            LockTaskAction(task);
        }
        public override void MoveTaskToArchive(MyTask task)
        {
            task.IsArchive = true;

            CheckIfAllMyTasksArchived();
            CheckIfAnyMyTasksArchived();
        }
        #endregion

        #region Protected Methods
        protected override async Task InitAsync()
        {
            await InitMyTasksAsync();
        }
        #endregion
    }
}
