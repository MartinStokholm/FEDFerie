using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace FerieWPFApp.Models;

public class PackingList : BindableBase
{
    private string name = "";
    private ObservableCollection<Item>? items;

    public PackingList()
    {
    }

    public PackingList(string name)
    {
        Name = name;
        Items = new ObservableCollection<Item>();
    }
   
    public PackingList(string name, ObservableCollection<Item> items)
    {
        Name = name;
        Items = items; 
    }
    public PackingList? Clone()
    {
       return this.MemberwiseClone() as PackingList;
    }

    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }

    public ObservableCollection<Item> Items
    {
        get => items;
        set => SetProperty(ref items, value);
    }

   
}