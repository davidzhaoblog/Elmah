using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Windows.Input;

//using Framework.Xaml.SQLite;

namespace Elmah.PetStore.ViewModels
{
    public partial class PetVM
        : Framework.Xaml.ViewModelBase
    {
        public const string MessageTitle_LoadData = "Load_PetStore_Pet_ListVM";
        public string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Pet;

        protected Elmah.PetStore.Models.Pet m_Item;
        public Elmah.PetStore.Models.Pet Item
        {
            get { return m_Item; }
            set
            {
                Set(t, ref m_Item, value);

                m_Item = value != null ? GetAClone(value) : new Elmah.PetStore.Models.Pet();
                m_Item.PropertyChanged += Item_PropertyChanged;
                RaisePropertyChanged(this.Item,);
            }
        }

        /// <summary>
        /// Initializes a new instance of the IndexVM class.
        /// </summary>
        public PetVM()
            : base()
        {
           
        }

        protected override async Task<Elmah.PetStore.Models.Pet> InsertToServer()
        {
            var client = WebApiClientFactory.CreatePetApiClient();

            var result = client.PutAsync()
            var item = new Elmah.DataSourceEntities.ELMAH_Error();
            item.CopyFrom<Elmah.EntityContracts.IELMAH_Error>(Item);

            var result = await client.UpsertEntityAsync(item);
            return result;
        }
    }
}

