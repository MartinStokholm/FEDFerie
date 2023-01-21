using FerieWPFApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Transactions;

namespace FerieWPFApp.ViewModels;

public class ViewPackingListViewModel : BindableBase
{
    private string title = "";
    PackingList currentPackingList;
    private ObservableCollection<Item> items;
    Item currentItem;

    public ViewPackingListViewModel(string title, PackingList packingList)
    {
        Title = title;
        CurrentPackingList = packingList;
        CurrentItems = new ObservableCollection<Item>(packingList.Items);
    }
 
    public string Title
    {
        get { return title; }
        set { SetProperty(ref title, value); }
    }

    public PackingList CurrentPackingList
    {
        get { return currentPackingList; }
        set { SetProperty(ref currentPackingList, value); }
    }

    public ObservableCollection<Item> CurrentItems
    {
        get { return items ?? (items = new ObservableCollection<Item>()); }
        set { SetProperty(ref items, value); }
    }

    public ViewPackingListViewModel()
    {
        var shoes = new Item("Shoes", 1 );
        var pants = new Item("Pants", 2 );

        CurrentItems.Add(shoes);
        CurrentItems.Add(pants);
    }
}