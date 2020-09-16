using WOClient.Components.Base;
using WOClient.Library.Models;

namespace WOClient.Components.NewEmployee
{
    public interface IEmployeeViewModel : IBaseViewModel
    {
        #region Methods
        void SetProperties(IPerson employee);
        void Reset();
        #endregion
    }
}
