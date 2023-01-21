using Prism.Commands;
using System;
using System.Collections.Generic;
using FerieWPFApp.Models;
using FerieWPFApp.Views;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.IO;
using FerieWPFApp.Data;
using System.Windows;
using Microsoft.Win32;

namespace FerieWPFApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly string AppTitle = "FerieWPFApp";
        private ObservableCollection<PackingList> packingLists;
        private PackingList currentPackingList;
        private string filePath = "";
        private string fileName = "No file loaded";
        
        public MainWindowViewModel()
        {
            PackingLists = new ObservableCollection<PackingList>();
            var pants = new Item("pants", 2);
            var shoes = new Item("shoes", 1);

            var items = new List<Item>();
            items.Add(pants);
            items.Add(shoes);

            PackingLists.Add(new PackingList("Festival", items));
            PackingLists.Add(new PackingList("Summer on the Beach", items));
            PackingLists.Add(new PackingList("Camping", items));

            CurrentPackingList = PackingLists[0];
        }
        public PackingList CurrentPackingList
        {
            get { return currentPackingList; }
            set { SetProperty(ref currentPackingList, value); }
        }

        public ObservableCollection<PackingList> PackingLists 
        {
            get { return packingLists; }
            set { SetProperty(ref packingLists, value); }
        }
        public string FileName
        {
            get { return fileName; }
            set
            {
                SetProperty(ref fileName, value);
                RaisePropertyChanged("Title");
            }
        }

        public string Title
        {
            get { return FileName + " - " + AppTitle; }
        }
        
        private DelegateCommand createPackingListCommand;
        public DelegateCommand CreatePackingListCommand=>
            createPackingListCommand ?? (createPackingListCommand = new DelegateCommand(ExecuteCreatePackingListCommand));
        void ExecuteCreatePackingListCommand()
        {
            var newPackingList = new PackingList();
            var createPackingListViewModel = new CreatePackingListViewModel("Create new Packing list", newPackingList);

            var dlg = new CreatePackingListView
            {
                DataContext = createPackingListViewModel
            };
            if (dlg.ShowDialog() == true)
            {
                PackingLists.Add(newPackingList);
                CurrentPackingList = newPackingList;
            }
        }

        private DelegateCommand openPackingListCommand;
        public DelegateCommand OpenPackingListCommand =>
            openPackingListCommand ?? (openPackingListCommand = new DelegateCommand(ExecuteOpenPackingListCommand));
        
        void ExecuteOpenPackingListCommand()
        {
            var tempPackingList = CurrentPackingList.Clone();
            var vm = new ViewPackingListViewModel("Packing list", tempPackingList);

            var dlg = new ViewPackingListView
            {
                DataContext = vm
            };
            if (dlg.ShowDialog() == true)
            {
                CurrentPackingList.Items = tempPackingList.Items;
                
            }
        }

        DelegateCommand _NewFileCommand;
        public DelegateCommand NewFileCommand
        {
            get { return _NewFileCommand = new DelegateCommand(NewFileCommand_Execute); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                PackingLists.Clear();
                FileName = "";

            }
        }

        DelegateCommand _OpenFileCommand;
        public DelegateCommand OpenFileCommand
        {
            get { return _OpenFileCommand = new DelegateCommand(OpenFileCommand_Execute); }
        }

        private void OpenFileCommand_Execute()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Debtor documents|*.dbt|All Files|*.*",
                DefaultExt = "dbt"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(Application.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                FileName = Path.GetFileName(filePath);
                try
                {
                    PackingLists = Repository.ReadFile(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        DelegateCommand _SaveAsCommand;
        public DelegateCommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new DelegateCommand(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Debtor documents|*.dbt|All Files|*.*",
                DefaultExt = "dbt"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(Application.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                FileName = Path.GetFileName(filePath);
                SaveFile();
            }
        }

        DelegateCommand _SaveCommand;
        public DelegateCommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new DelegateCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)
                  .ObservesProperty(() => PackingLists.Count));
            }
        }

        private void SaveFileCommand_Execute()
        {
            SaveFile();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return FileName != "" && PackingLists.Count > 0;
        }

        private void SaveFile()
        {
            try
            {
                Repository.SaveFile(filePath, PackingLists);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
