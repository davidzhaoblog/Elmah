using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class ViewModelBaseWithResultAndUIElement<TSearchCriteria, TSearchResultEntityCollection, TSearchResultEntityItem, TViewModelData, TItemVM, TItemVMSearchCriteria, TNavigationVMEntityContainer, TSQLiteRepository, TSQLiteItemType, TIIdentifier>
        : ViewModelBaseWithResultAndUIElement<TSearchCriteria, TSearchResultEntityCollection, TSearchResultEntityItem, TViewModelData, TSQLiteRepository, TSQLiteItemType, TIIdentifier>
        where TSearchCriteria : class, new()
        where TSearchResultEntityCollection : List<TSearchResultEntityItem>, new()
        where TSearchResultEntityItem : Framework.Models.PropertyChangedNotifier, TIIdentifier, new()
        where TViewModelData : Framework.ViewModels.ViewModelBase<TSearchCriteria, TSearchResultEntityCollection>, new()
        where TItemVMSearchCriteria : class, TIIdentifier, Framework.Models.IClone<TIIdentifier>, new()
        where TItemVM : Framework.Xaml.ViewModelItemBase<TItemVMSearchCriteria, TIIdentifier, TSearchResultEntityItem>
        where TNavigationVMEntityContainer : Framework.Xaml.NavigationVMEntityContainer<TItemVM, TItemVMSearchCriteria, TIIdentifier, TSearchResultEntityItem>, new()
        where TSQLiteRepository : Framework.Xaml.SQLite.SQLiteTableRepositoryBase<TSQLiteItemType, TSearchCriteria, TSearchResultEntityItem, TIIdentifier>
        where TSQLiteItemType : TSearchResultEntityItem, new()
    {
        public virtual TNavigationVMEntityContainer NavigationContainer
        {
            get
            {
                return null;
            }
        }

        public virtual TItemVM ItemVM
        {
            get
            {
                return null;
            }
        }

        public ViewModelBaseWithResultAndUIElement()
        {
            ItemVM.ItemAdded += ItemVM_ItemAdded;
            ItemVM.ItemDeleted += ItemVM_ItemDeleted;
            ItemVM.ItemUpdated += ItemVM_ItemUpdated;
        }

        protected virtual void ItemVM_ItemAdded(object sender, EventArgs e)
        {
            ItemVM_ItemAddedOrUpdated();
        }

        protected virtual void ItemVM_ItemDeleted(object sender, EventArgs e)
        {
            if (BindToGroupedResults)
            {
                if (GroupedResults != null)
                {
                    foreach (var groupedResult in GroupedResults)
                    {
                        if (groupedResult.Any(GetPredicateToGetAnExistingItem(ItemVM.Item)))
                        {
                            groupedResult.Remove(Result.First(GetPredicateToGetAnExistingItem(ItemVM.Item)));
                        }
                    }
                }
            }
            else
                Result.Remove(Result.First(GetPredicateToGetAnExistingItem(ItemVM.Item)));
        }

        protected virtual void ItemVM_ItemUpdated(object sender, EventArgs e)
        {
            ItemVM_ItemAddedOrUpdated();
        }

        protected virtual void ItemVM_ItemAddedOrUpdated()
        {
            try
            {
                if (BindToGroupedResults)
                {
                    var updatedList = new List<TSearchResultEntityItem> { ItemVM.Item };
                    BindResult(updatedList, false);
                }
                else
                {
                    var item = Result.First(GetPredicateToGetAnExistingItem(ItemVM.Item));
                    if (item != null)
                        CopyToItemInList(item, ItemVM.Item);
                    else
                        Result.Add(ItemVM.Item);
                }
            }
            catch //(Exception ex)
            {
            }
        }
    }
}

