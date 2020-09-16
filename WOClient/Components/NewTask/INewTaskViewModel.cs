using System.Threading.Tasks;
using WOClient.Components.Base;

namespace WOClient.Components.NewTask
{
    public interface INewTaskViewModel: IBaseViewModel
    {
        #region Methods
        Task SendNewTaskAsync();
        void Reset();
        #endregion
    }
}
