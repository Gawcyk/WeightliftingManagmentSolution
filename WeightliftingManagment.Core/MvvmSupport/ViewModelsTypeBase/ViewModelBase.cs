
using Prism.Navigation;

using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase
{
    public abstract class ViewModelBase : ErrorsBindableObject, IDestructible
    {
        protected ViewModelBase()
        {
        }

        public virtual void Destroy()
        {
        }
    }
}
