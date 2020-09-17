using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WOCommon.Enums;
using System.Linq;
using System.Security;
using System;

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
            var myEmployee = GetEmplyee(employeeId);

            if (myEmployee is null) return;

            var employeeTask = myEmployee.MyTasks.SingleOrDefault(task => task.TaskId == taskId);

            if (employeeTask is null) return;

            employeeTask.Comments.Add(comment);
        }
        public async Task AssignedEmployeeAsync(IPerson employee)
        {
            employee.ManagerId = PersonId;

            foreach (var task in employee.MyTasks) TrackingTasks.Add(task);

            CheckIfAllTrackingTasksArchived();
            CheckIfAnyTrackingTasksArchived();
            MyEmployees.Add(employee);

            await UpdateEmployeeDirectManagerAsync(employee.PersonId, PersonId);
        }
        public void AssignedTaskToEmployee(MyTask task, int personId)
        {
            var currentEmployee = MyEmployees.SingleOrDefault(item => item.PersonId == task.AssignedEmployee);

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
        public void Downgrade(IPerson managerToDowngrade)
        {
            managerToDowngrade.Permission = PermissionsEnum.Employee;

            var employee = new Employee(PermissionsEnum.Employee,
                                        managerToDowngrade.PersonId,
                                        managerToDowngrade.ManagerId,
                                        managerToDowngrade.FirstName,
                                        managerToDowngrade.LastName,
                                        managerToDowngrade.Email);

            MyEmployees.Remove(managerToDowngrade);
            MyEmployees.Add(employee);
        }
        public IPerson GetEmplyee(int id)
        {
            return MyEmployees.SingleOrDefault(employee => employee.PersonId == id);
        }
        public async Task RemoveEmployeeAsync(int employeeId)
        {
            var employee = MyEmployees.Single(myEmployee => myEmployee.PersonId == employeeId);

            foreach (var task in TrackingTasks.ToList())
            {
                task.RemoveAllCommentsByEmployeeId(employeeId);

                if (task.AssignedEmployee != employeeId) continue;

                await RemoveTaskAsync(task);
            }

            MyEmployees.Remove(employee);

            await Api.DeleteEmployeeAsync(employeeId);
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
        private void AssignedTaskToEmployee(MyTask task)
        {
            foreach (var myEmployee in MyEmployees)
            {
                if (myEmployee.PersonId != task.AssignedEmployee) continue;

                var clonedTask = (MyTask)task.Clone();

                myEmployee.MyTasks.Add(clonedTask);
                myEmployee.CheckIfAllMyTasksArchived();
                myEmployee.CheckIfAnyMyTasksArchived();

                break;
            }
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
        private async Task UpdateEmployeeDirectManagerAsync(int employeeId, int newManagerId)
        {
            await Api.UpdateUserDbFiledAsync(employeeId, newManagerId, "DirectManager");
            await Api.SendUpdateEventAsync(employeeId);
            await Api.SendUpdateEventAsync(newManagerId);
        }
        #endregion
    }
}
