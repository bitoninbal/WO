using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public class Manager: Person
    {
        public Manager(PermissionsEnum permission,
                       int personId,
                       int managerId,
                       string firstName,
                       string lastName,
                       string email):
            base(permission,
                 personId,
                 managerId,
                 firstName,
                 lastName,
                 email)
        {
            _myEmployees = new ObservableCollection<IPerson>();
            _trackingTasks = new ObservableCollection<MyTask>();

            Task.Run(InitAsync);
        }

        #region Fields
        private bool _isAllTrackingTasksArchived;
        private bool _isTrackingTasksArchivedExists;
        private ObservableCollection<IPerson> _myEmployees;
        private ObservableCollection<MyTask> _trackingTasks;
        #endregion

        #region Properties
        public bool IsAllTrackingTasksArchived
        {
            get => _isAllTrackingTasksArchived;
            set
            {
                if (_isAllTrackingTasksArchived == value) return;

                _isAllTrackingTasksArchived = value;

                NotifyPropertyChanged(nameof(IsAllTrackingTasksArchived));
            }
        }
        public bool IsTrackingTasksArchivedExists
        {
            get => _isTrackingTasksArchivedExists;
            set
            {
                if (_isTrackingTasksArchivedExists == value) return;

                _isTrackingTasksArchivedExists = value;

                NotifyPropertyChanged(nameof(IsTrackingTasksArchivedExists));
            }
        }
        public ObservableCollection<IPerson> MyEmployees
        {
            get => _myEmployees;
            set
            {
                if (_myEmployees == value) return;

                _myEmployees = value;

                NotifyPropertyChanged(nameof(MyEmployees));
            }
        }
        public ObservableCollection<MyTask> TrackingTasks
        {
            get => _trackingTasks;
            set
            {
                if (_trackingTasks == value) return;

                _trackingTasks = value;

                NotifyPropertyChanged(nameof(TrackingTasks));
            }
        }
        #endregion

        #region Public Methods
        public void AddCommentToEmployeeTask(int employeeId, int taskId, Comment comment)
        {
            var task = GetTaskOfEmployee(employeeId, taskId);

            if (task is null) return;

            task.Comments.Add(comment);
        }
        public void AssignedTaskToEmployee(MyTask task, int personId)
        {
            var currentEmployee = GetEmplyee(task.AssignedEmployee);

            if (currentEmployee is null) return;

            var currentEmployeeTask = currentEmployee.MyTasks.SingleOrDefault(item => item.TaskId == task.TaskId);
            var newEmployee         = MyEmployees.SingleOrDefault(item => item.PersonId == personId);

            if (newEmployee is null || currentEmployeeTask is null) return;

            currentEmployeeTask.AssignedEmployee = personId;
            task.AssignedEmployee                = personId;

            currentEmployee.MyTasks.Remove(currentEmployeeTask);
            newEmployee.MyTasks.Add(currentEmployeeTask);
        }
        public void CheckIfAllTrackingTasksArchived()
        {
            IsAllTrackingTasksArchived = TrackingTasks.All((task) => task.IsArchive);
        }
        public void CheckIfAnyTrackingTasksArchived()
        {
            IsTrackingTasksArchivedExists = TrackingTasks.Any((task) => task.IsArchive);
        }
        public async Task DowngradeAsync(Manager managerToDowngrade)
        {
            await AssignedAllEmployeesAsync(managerToDowngrade);

            Downgrade(managerToDowngrade);
        }
        public async Task DowngradeAsync(Manager managerToDowngrade, Manager replacerManager)
        {
            await replacerManager.AssignedAllEmployeesAsync(managerToDowngrade);

            Downgrade(managerToDowngrade);
        }
        public IPerson GetEmplyee(int id)
        {
            return MyEmployees.SingleOrDefault(employee => employee.PersonId == id);
        }
        public MyTask GetTaskOfEmployee(int employeeId, int taskId)
        {
            var myEmployee = GetEmplyee(employeeId);

            if (myEmployee is null) return null;

            return myEmployee.MyTasks.SingleOrDefault(myTask => myTask.TaskId == taskId);
        }
        public override void LockTask(MyTask task)
        {
            if (task.AssignedEmployee == PersonId)
            {
                LockTaskAction(task);

                return;
            }

            var myEmployee = GetEmplyee(task.AssignedEmployee);

            if (myEmployee is null) return;

            var employeeTask = myEmployee.MyTasks.SingleOrDefault(myTask => myTask.TaskId == task.TaskId);

            if (employeeTask is null) return;

            employeeTask.IsCompleted = task.IsCompleted;

            if (!task.IsArchive) return;
            if (task.IsCompleted) return;

            myEmployee.MoveMyTaskFromArchive(employeeTask);

            task.IsArchive = false;

            CheckIfAllTrackingTasksArchived();
            CheckIfAnyTrackingTasksArchived();
        }
        public override void MoveTaskToArchive(MyTask task)
        {
            task.IsArchive = true;

            CheckIfAllTrackingTasksArchived();
            CheckIfAnyTrackingTasksArchived();

            if (task.AssignedEmployee == PersonId) return;

            var myEmployee = GetEmplyee(task.AssignedEmployee);

            if (myEmployee is null) return;

            var employeeTask = myEmployee.MyTasks.SingleOrDefault(myTask => myTask.TaskId == task.TaskId);

            if (employeeTask is null) return;

            employeeTask.IsArchive = true;

            myEmployee.CheckIfAllMyTasksArchived();
            myEmployee.CheckIfAnyMyTasksArchived();
        }
        public async Task RemoveTaskAsync(MyTask task)
        {
            TrackingTasks.Remove(task);

            var myEmployee = MyEmployees.Single(employee => employee.PersonId == task.AssignedEmployee);

            myEmployee.MyTasks.Remove(myEmployee.MyTasks.Single(myTask => myTask.TaskId == task.TaskId));

            await Api.DeleteTaskAsync(task.TaskId, task.AssignedEmployee);

            CheckIfAllTrackingTasksArchived();
            CheckIfAnyTrackingTasksArchived();
        }
        public override void Reset()
        {
            base.Reset();

            TrackingTasks.Clear();
            MyEmployees.Clear();
        }
        public async Task<bool> TryAddEmployeeAsync(string firstName,
                                                    string lastName,
                                                    string email,
                                                    SecureString password,
                                                    PermissionsEnum permission)
        {
            var employeeId = await Api.EmployeeRegisterAsync(firstName, lastName, email, password, permission, PersonId);

            if (employeeId == 0) return false;

            switch (permission)
            {
                case PermissionsEnum.Manager:
                    MyEmployees.Add(new Manager(permission, employeeId, PersonId, firstName, lastName, email));

                    break;
                case PermissionsEnum.Employee:
                    MyEmployees.Add(new Employee(permission, employeeId, PersonId, firstName, lastName, email));

                    break;
            }

            return true;
        }
        public async Task<bool> TryAddTaskAsync(int assignedEmployee,
                                                DateTime createDate,
                                                string description,
                                                DateTime finalDate,
                                                PriorityEnum priority,
                                                string subject)
        {
            var taskId = await Api.AddTaskAsync(PersonId,
                                                assignedEmployee,
                                                createDate,
                                                description,
                                                finalDate,
                                                priority,
                                                subject);

            if (taskId == 0) return false;

            var task = new MyTask(true)
            {
                AssignedEmployee = assignedEmployee,
                CreatedDate      = DateTime.Now.Date,
                Description      = description,
                FinalDate        = finalDate,
                Priority         = priority,
                TaskId           = taskId,
                Subject          = subject
            };

            task.SetInitModeFalse();

            TrackingTasks.Add(task);
            AssignedTaskToEmployee(task);
            CheckIfAllTrackingTasksArchived();
            CheckIfAnyTrackingTasksArchived();

            return true;
        }
        public async Task<bool> TryRemoveEmployeeAsync(IPerson employeeToDelete)
        {
            if (IsEmployeeHasOpenTasks(employeeToDelete)) return false;

            await AssignedAllEmployeesAsync(employeeToDelete);

            foreach (var task in TrackingTasks.ToList())
            {
                task.RemoveAllCommentsByEmployeeId(employeeToDelete.PersonId);

                if (task.AssignedEmployee != employeeToDelete.PersonId) continue;

                await RemoveTaskAsync(task);
            }

            MyEmployees.Remove(employeeToDelete);

            await Api.DeleteEmployeeAsync(employeeToDelete.PersonId);

            return true;
        }
        public void Upgrade(IPerson employeeToUpgrade)
        {
            employeeToUpgrade.Permission = PermissionsEnum.Manager;

            var newManager = new Manager(PermissionsEnum.Manager,
                                         employeeToUpgrade.PersonId,
                                         employeeToUpgrade.ManagerId,
                                         employeeToUpgrade.FirstName,
                                         employeeToUpgrade.LastName,
                                         employeeToUpgrade.Email);

            MyEmployees.Remove(employeeToUpgrade);
            MyEmployees.Add(newManager);
        }
        #endregion

        #region Protected Methods
        protected override async Task InitAsync()
        {
            await InitMyTasksAsync();
            await InitMyEmployeesAsync();
            await InitTrackingTasksAsync();
        }
        #endregion

        #region Private Methods
        private async Task AssignedAllEmployeesAsync(IPerson oldManager)
        {
            if (!(oldManager is Manager)) return;

            var manager = oldManager as Manager;

            foreach (var employee in manager.MyEmployees) await AssignedEmployeeAsync(employee);
        }
        private async Task AssignedEmployeeAsync(IPerson employee)
        {
            employee.ManagerId = PersonId;

            foreach (var task in employee.MyTasks.ToList())
            {
                TrackingTasks.Add(task);

                await task.UpdateTaskCreaterIdAsync(PersonId);
            }

            CheckIfAllTrackingTasksArchived();
            CheckIfAnyTrackingTasksArchived();
            MyEmployees.Add(employee);

            await UpdateEmployeeDirectManagerAsync(employee.PersonId, PersonId);
        }
        private void AssignedTaskToEmployee(MyTask task)
        {
            var employee = GetEmplyee(task.AssignedEmployee);

            if (employee is null) return;

            var clonedTask = (MyTask)task.Clone();

            employee.MyTasks.Add(clonedTask);
            employee.CheckIfAllMyTasksArchived();
            employee.CheckIfAnyMyTasksArchived();
        }
        private void Downgrade(Manager managerToDowngrade)
        {
            managerToDowngrade.Permission = PermissionsEnum.Employee;

            var newEmployee = new Employee(PermissionsEnum.Employee,
                                           managerToDowngrade.PersonId,
                                           managerToDowngrade.ManagerId,
                                           managerToDowngrade.FirstName,
                                           managerToDowngrade.LastName,
                                           managerToDowngrade.Email);

            MyEmployees.Remove(managerToDowngrade);
            MyEmployees.Add(newEmployee);
        }
        private async Task InitMyEmployeesAsync()
        {
            var result = await Api.GetEmployeesAsync(PersonId);

            if (result is null) return;

            MyEmployees = result;
        }
        private async Task InitTrackingTasksAsync()
        {
            var result = await Api.GetTrackingTasksAsync(PersonId);

            if (result is null) return;

            TrackingTasks = result;

            IsAllTrackingTasksArchived    = CheckIfAllTasksArchived(TrackingTasks);
            IsTrackingTasksArchivedExists = CheckIfAnyTasksArchived(TrackingTasks);
        }
        private bool IsEmployeeHasOpenTasks(IPerson employee)
        {
            foreach (var task in employee.MyTasks) if (!task.IsCompleted) return true;

            return false;
        }
        private async Task UpdateEmployeeDirectManagerAsync(int employeeId, int newManagerId)
        {
            await Api.UpdateUserDbFiledAsync(employeeId, newManagerId, "DirectManager");
            await Api.SendUpdateEventAsync(employeeId);
            await Api.SendUpdateEventAsync(newManagerId);
        }
        #endregion
    }
}
