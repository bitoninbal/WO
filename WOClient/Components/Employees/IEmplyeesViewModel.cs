using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Library.Models;

namespace WOClient.Components.Employees
{
    public interface IEmplyeesViewModel: IBaseViewModel
    {
        #region Properties
        IPerson Employee { get; set; }
        #endregion

        #region Methods
        Task DeleteEmployeeAsync();
        Task OpenNewEmployeeAsync();
        #endregion
    }
}
