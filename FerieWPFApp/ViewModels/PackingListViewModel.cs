using FerieWPFApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace FerieWPFApp.ViewModels;

public class PackingListViewModel : BindableBase
{
    private string title = "";
    private PackingList currentPackingList;
    private ObservableCollection<Item> items;
  
    public PackingListViewModel(string title, PackingList packingList)
    {
        Title = title;
        CurrentPackingList = packingList;
        CurrentItems = (ObservableCollection<Item>)packingList.Items;
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

 


}