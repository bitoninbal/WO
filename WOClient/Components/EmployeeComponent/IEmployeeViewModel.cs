using WOClient.Components.Base;
using WOClient.Library.Models;

namespace WOClient.Components.EmployeeComponent
{
    public interface IEmployeeViewModel: IBaseViewModel
    {
        #region Methods
        void SetProperties(IPerson employee);
        void Reset();
        #endregion
    }
}
