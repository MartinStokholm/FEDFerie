using Prism.Commands;
using System;
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

        private ObservableCollection<PackingList> packingListTemplates;
        private PackingList currentPackingListTemplate;

        // for file handling
        private string filePath = "";
        private string fileName = "No file loaded";
        private string createPackingListName = "";

        // Delegates for commands
        DelegateCommand createPackingListCommand;
        DelegateCommand createTemplateCommand;
        DelegateCommand openPackingListCommand;
        DelegateCommand openTemplateCommand;
        
        DelegateCommand _NewFileCommand;
        DelegateCommand _SaveCommand;
        DelegateCommand _SaveAsCommand;
        DelegateCommand _OpenFileCommand;
        
        public DelegateCommand CreatePackingListCommand =>
            createPackingListCommand ??= new DelegateCommand(ExecuteCreatePackingListCommand);

        public DelegateCommand CreateTemplateCommand =>
            createTemplateCommand ??= new DelegateCommand(ExecuteCreateTemplateCommand);

        public DelegateCommand OpenPackingListCommand =>
            openPackingListCommand ??= new DelegateCommand(ExecuteOpenPackingListCommand);
        public DelegateCommand OpenTemplateCommand =>
            openTemplateCommand ??= new DelegateCommand(ExecuteOpenTemplateCommand);

        public DelegateCommand NewFileCommand {
            get { return _NewFileCommand = new DelegateCommand(ExecuteNewFileCommand); }
        }
        
        public DelegateCommand OpenFileCommand {
            get { return _OpenFileCommand = new DelegateCommand(ExecuteOpenFileCommand); }
        }
        
        public DelegateCommand SaveAsCommand => _SaveAsCommand = _SaveAsCommand ?? new DelegateCommand(ExecuteSaveAsCommand);

        public DelegateCommand SaveCommand =>
            _SaveCommand = _SaveCommand ?? new DelegateCommand(ExecuteSaveFileCommand, CanExecuteSaveFileCommand)
                .ObservesProperty(() => PackingLists.Count);

        public MainWindowViewModel()
        {
            PackingLists = new ObservableCollection<PackingList>();
            PackingListTemplates = new ObservableCollection<PackingList>();

            var festivalItems = new ObservableCollection<Item>()
            {
                new ("Tent", 1),
                new ("Sleeping bag", 1),
                new ("Sleeping mat", 1),
                new ("Clothes", 1),
                new ("Toiletries", 1),
                new ("Food", 1),
                new ("Water", 1),
                new ("Torch", 1),
                new ("First aid kit", 1),
            };
            PackingListTemplates.Add(new PackingList("Festival", festivalItems));

            var SkiferieItems = new ObservableCollection<Item>()
            {
                new ("Skis", 1),
                new ("Ski boots", 1),
                new ("Ski poles", 1),
                new ("Ski helmet", 1),
                new ("Ski goggles", 1),
                new ("Ski gloves", 1),
                new ("Ski socks", 1),
                new ("Ski pants", 1)
            };

            PackingListTemplates.Add(new PackingList("Skiferie", SkiferieItems));

            PackingListTemplates.Add(new PackingList("Storbyferie"));
        }

        public ObservableCollection<PackingList> PackingLists
        {
            get => packingLists;
            set => SetProperty(ref packingLists, value);
        }
        
        public PackingList CurrentPackingList
        {
            get => currentPackingList;
            set => SetProperty(ref currentPackingList, value);
        }

        public ObservableCollection<PackingList> PackingListTemplates
        {
            get => packingListTemplates;
            set => SetProperty(ref packingListTemplates, value);
        }

        public PackingList CurrentPackingListTemplate
        {
            get => currentPackingListTemplate;
            set => SetProperty(ref currentPackingListTemplate, value);
        }

        void ExecuteCreatePackingListCommand()
        {
            // add new packing list as a copy of the current selected template
            var newPackingList = new PackingList
            {
                Name = CurrentPackingListTemplate.Name,
                Items = CurrentPackingListTemplate.Items
            };
            
            PackingLists.Add(newPackingList);
        }

        void ExecuteCreateTemplateCommand()
        {
            var newTemplate = new PackingList("template" + PackingListTemplates.Count);
            PackingListTemplates.Add(newTemplate);
        }

        void ExecuteOpenPackingListCommand()
        {
            
            var vm = new PackingListViewModel("Packing list", CurrentPackingList);

            var dlg = new PackingListView
            {
                DataContext = vm
            };
            dlg.ShowDialog();
        }
        void ExecuteOpenTemplateCommand()
        {

            var vm = new TemplateViewModel("Template", CurrentPackingListTemplate);

            var dlg = new TemplateView
            {
                DataContext = vm
            };
            dlg.ShowDialog();
        }

        public string FileName
        {
            get => fileName;
            set
            {
                SetProperty(ref fileName, value);
                RaisePropertyChanged("Title");
            }
        }

        public string Title => FileName + " - " + AppTitle;

        // Commands for file handling
        private void ExecuteNewFileCommand()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                PackingLists.Clear();
                FileName = "";
            }
        }
        
        private void ExecuteOpenFileCommand()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "json|*.json|All Files|*.*",
                DefaultExt = "json"
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
                    PackingLists.Clear();
                    PackingListTemplates = Repository.ReadFile(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExecuteSaveAsCommand()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "json|*.json|All Files|*.*",
                DefaultExt = "json"
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
        
        private void ExecuteSaveFileCommand()
        {
            SaveFile();
        }

        private bool CanExecuteSaveFileCommand()
        {
            return FileName != "" && PackingListTemplates.Count > 0;
        }

        private void SaveFile()
        {
            try
            {
                Repository.SaveFile(filePath, PackingListTemplates);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
