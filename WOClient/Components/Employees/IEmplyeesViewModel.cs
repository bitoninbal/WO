using System.Collections.ObjectModel;
using WOClient.Components.Base;
using WOClient.Models;

namespace WOClient.Components.Employees
{
    public interface IEmplyeesViewModel: IBaseViewModel
    {
        #region Properties
        ObservableCollection<IPerson> Employees { get; set; } 
        #endregion
    }
}
