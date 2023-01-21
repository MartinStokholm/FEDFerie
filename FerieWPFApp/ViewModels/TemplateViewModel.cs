using FerieWPFApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace FerieWPFApp.ViewModels;

public class TemplateViewModel : BindableBase
{
    private string title = "";
    private PackingList currentTemplate;
    private ObservableCollection<Item> items;

    private string newItemName = "";
    private int newItemQuantity = 0;

    public TemplateViewModel(string title, PackingList template)
    {
        Title = title;
        CurrentTemplate = template;
        CurrentItems = template.Items;
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public PackingList CurrentTemplate
    {
        get => currentTemplate;
        set => SetProperty(ref currentTemplate, value);
    }

    public ObservableCollection<Item> CurrentItems
    {
        get => items;
        set => SetProperty(ref items, value);
    }

    public string NewItemName
    {
        get => newItemName;
        set => SetProperty(ref newItemName, value);
    }

    public int NewItemQuantity
    {
        get => newItemQuantity;
        set => SetProperty(ref newItemQuantity, value);
    }

    private DelegateCommand addItemCommand;
    public DelegateCommand AddItemCommand =>
        addItemCommand = addItemCommand ?? new DelegateCommand(ExecuteAddItemCommand);
    void ExecuteAddItemCommand()
    {
        CurrentItems.Add(new Item(NewItemName, NewItemQuantity));
        CurrentTemplate.Items = CurrentItems;

    }
}