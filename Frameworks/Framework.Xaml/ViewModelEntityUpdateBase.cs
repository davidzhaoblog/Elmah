using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class ViewModelEntityUpdateBase<TMasterEntity, TCriteriaOfMasterEntity, TIdentifier>
        : Framework.Xaml.ViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity, TIdentifier>
        where TMasterEntity : class, new()
        where TCriteriaOfMasterEntity : class, new()
    {
        public ViewModelEntityUpdateBase()
            : base()
        {
            this.UpdateCommand = new Command<TMasterEntity>(Update, CanUpdate);
        }

        public ICommand UpdateCommand { get; protected set; }
        protected virtual async void Update(TMasterEntity o)
        {
            await DoUpdate(o);
        }

        protected abstract Task DoUpdate(TMasterEntity o);

        protected virtual bool CanUpdate(TMasterEntity o)
        {
            return true;
        }
    }
}

