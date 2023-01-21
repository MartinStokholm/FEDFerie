using Prism.Mvvm;

namespace FerieWPFApp.Models;

public class Item : BindableBase
{
    private string name = "";
    private int quantity = 0;
    private bool isPacked = false;

    public Item() { }

    public Item(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
        IsPacked = isPacked;
    }

    public Item? Clone()
    {
        return this.MemberwiseClone() as Item;
    }
    
    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }

    public int Quantity
    {
        get => quantity;
        set => SetProperty(ref quantity, value);
    }

    public bool IsPacked
    {
        get => isPacked;
        set => SetProperty(ref isPacked, value);
    }

}