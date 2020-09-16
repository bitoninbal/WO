using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Library.Models;
using WOClient.Library.Models.Reports;
using WOClient.Resources.Commands;
using WOCommon.Enums;

namespace WOClient.Components.Reports
{
    public class ReportsViewModel: BaseViewModel, IReportsViewModel
    {
        public ReportsViewModel()
        {
            ShowReportCommand = new RelayCommand(ShowReport);
            OpenTasksReport   = new NumberOfOpenTasksReport();
            AllTasksReport    = new AllTasksReport();
        }

        #region Field
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private IPerson _selectedEmployee;
        private ReportsEnum _selectedReport;
        private List<object> _reportCollections;
        #endregion

        #region Commands
        public ICommand ShowReportCommand { get; }
        #endregion

        #region Properties
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                if (_fromDate == value) return;

                _fromDate = value;

                NotifyPropertyChanged(nameof(FromDate));
            }
        }
        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                if (_toDate == value) return;

                _toDate = value;

                NotifyPropertyChanged(nameof(ToDate));
            }
        }
        public IPerson SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (_selectedEmployee == value) return;

                _selectedEmployee = value;

                NotifyPropertyChanged(nameof(SelectedEmployee));
            }
        }
        public List<object> ReportCollections
        {
            get => _reportCollections;
            set
            {
                if (_reportCollections == value) return;

                _reportCollections = value;

                NotifyPropertyChanged(nameof(ReportCollections));
            }
        }
        public ReportsEnum SelectedReport
        {
            get => _selectedReport;
            set
            {
                if (_selectedReport == value) return;

                _selectedReport   = value;
                ReportCollections = null;

                NotifyPropertyChanged(nameof(SelectedReport));
            }
        }
        public NumberOfOpenTasksReport OpenTasksReport { get; set; }
        public AllTasksReport AllTasksReport { get; set; }
        #endregion

        #region Public Methods
        public void Reset()
        {
            FromDate          = DateTime.Now;
            ToDate            = DateTime.Now;
            SelectedEmployee  = null;
            ReportCollections = null;
            SelectedReport    = ReportsEnum.AllEmployeeTasks;
            OpenTasksReport   = new NumberOfOpenTasksReport();
            AllTasksReport    = new AllTasksReport();
        }
        #endregion

        #region Private Methods
        private void ShowReport()
        {
            if (FromDate > ToDate)
            {
                MessageBox.Show("The from date cannot be greater than to date.");

                return;
            }

            var manager = IMainWindowViewModel.User as Manager;

            switch (SelectedReport)
            {
                case ReportsEnum.NumberOfOpenTasks:
                    ReportCollections = OpenTasksReport.GenerateReport(manager.TrackingTasks.ToList(), FromDate, ToDate);

                    break;
                case ReportsEnum.AllTasks:
                    var filter1 = manager.TrackingTasks.ToList()
                                                       .Concat(manager.MyTasks.ToList())
                                                       .Where((task) => task.CreatedDate.Date >= FromDate.Date && task.CreatedDate.Date <= ToDate.Date)
                                                       .OrderBy((task) => task.CreatedDate)
                                                       .ToList();

                    ReportCollections = AllTasksReport.GenerateReport(filter1, FromDate, ToDate);

                    break;
                case ReportsEnum.AllEmployeeTasks:
                    var filter2 = SelectedEmployee?.MyTasks.ToList()
                                                           .Concat(manager.MyTasks.ToList())
                                                           .Where((task) => task.CreatedDate.Date >= FromDate.Date && task.CreatedDate.Date <= ToDate.Date)
                                                           .OrderBy((task) => task.CreatedDate)
                                                           .ToList();

                    if (filter2 != null) ReportCollections = AllTasksReport.GenerateReport(filter2, FromDate, ToDate);

                    break;
            }
        }
        #endregion
    }
}
