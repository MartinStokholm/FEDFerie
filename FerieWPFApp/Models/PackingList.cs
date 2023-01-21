
using System.Collections.Generic;
using Prism.Mvvm;

namespace FerieWPFApp.Models;

public class PackingList : BindableBase
{
    private string name = "";
    private IEnumerable<Item> items;

    public PackingList()
    {
        Items = items; }

    public PackingList(string name)
    {
        Name = name;
        Items = items;
    }
   
    public PackingList(string name, IEnumerable<Item> items)
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

    public IEnumerable<Item> Items
    {
        get { return items; }
        set { SetProperty(ref items, value); }
    }

   
}