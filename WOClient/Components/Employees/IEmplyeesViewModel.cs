using WOClient.Components.Base;
using WOClient.Components.NewEmployee;
using WOClient.Library.Models;

namespace WOClient.Components.Employees
{
    public interface IEmplyeesViewModel: IBaseViewModel
    {
        #region Properties
        IPerson Employee { get; set; }
        INewEmployeeViewModel NewEmployeeVm { get; }
        #endregion
    }
}
