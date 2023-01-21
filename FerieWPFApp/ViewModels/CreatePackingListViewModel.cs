using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FerieWPFApp.Models;
using Prism.Mvvm;

namespace FerieWPFApp.ViewModels
{
    public class CreatePackingListViewModel : BindableBase
    {
        private PackingList newPackingList;
        private string title;

        public CreatePackingListViewModel(string title, PackingList packingList)
        {
            Title = title;
            NewPackingList = packingList;
        }
        
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public PackingList NewPackingList
        {
            get { return newPackingList; }
            set { SetProperty(ref newPackingList, value); }
        }

    }

}
