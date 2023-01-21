using Prism.Mvvm;

namespace FerieWPFApp.Models;

public class Item : BindableBase
{
    private string name = "";
    private int quantity = 0;

    public Item(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
    
    public string Name
    {
        get { return name; }
        set { SetProperty(ref name, value); }
    }

    public int Quantity
    {
        get { return quantity; }
        set { SetProperty(ref quantity, value); }
    }

}