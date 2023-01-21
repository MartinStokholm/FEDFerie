
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace FerieWPFApp.Models;

public class PackingList : BindableBase
{
    private string name = "";
    private ObservableCollection<Item> items;

    public PackingList()
    {
        Items = items; }

    public PackingList(string name)
    {
        Name = name;
        Items = items;
    }
   
    public PackingList(string name, ObservableCollection<Item> items)
    {
        Name = name;
        Items = items; 
    }
    public PackingList Clone()
    {
        return new PackingList(this.Name, this.Items);
    }

    public string Name
    {
        get { return name; }
        set { SetProperty(ref name, value); }
    }

    public ObservableCollection<Item> Items
    {
        get { return items; }
        set { SetProperty(ref items, value); }
    }

   
}