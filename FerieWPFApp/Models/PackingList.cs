
using System.Collections.Generic;
using Prism.Mvvm;

namespace FerieWPFApp.Models;

public class PackingList : BindableBase
{
    private string name = "";
    private List<Item> items = new();

    public PackingList()
    {
       
    }
    
    public PackingList(string name, List<Item> items)
    {
        Name = name;
        Items = items; 
    }
    public PackingList Clone()
    {
        return this.MemberwiseClone() as PackingList;
    }

    public string Name
    {
        get { return name; }
        set { SetProperty(ref name, value); }
    }

    public List<Item> Items
    {
        get { return items; }
        set { SetProperty(ref items, value); }
    }

   
}