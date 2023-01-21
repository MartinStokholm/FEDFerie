using FerieWPFApp.Models;
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
        CurrentItems = packingList.Items;
    }
    
    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public PackingList CurrentPackingList
    {
        get => currentPackingList;
        set => SetProperty(ref currentPackingList, value);
    }

    public ObservableCollection<Item> CurrentItems
    {
        get => items = items ?? new ObservableCollection<Item>();
        set => SetProperty(ref items, value);
    }
}