using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using FerieWPFApp.Models;
using Newtonsoft.Json;

namespace FerieWPFApp.Data
{
    internal class Repository
    {
        //Inspiration taken from the Agent 5 assignment solution
        internal static ObservableCollection<PackingList> ReadFile(string filePath)
        {
            ObservableCollection<PackingList> packingListTemplates = new ObservableCollection<PackingList>();
            if (filePath.EndsWith(".json"))
            {
                var json = File.ReadAllText(filePath);
                packingListTemplates = JsonConvert.DeserializeObject<ObservableCollection<PackingList>>(json);
            }
            else if (filePath.EndsWith(".xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PackingList>));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    packingListTemplates = (ObservableCollection<PackingList>)serializer.Deserialize(fs);
                }
            }
            return packingListTemplates;
        }
        internal static void SaveFile(string filePath, ObservableCollection<PackingList> packingListTemplates)
        {
            if (filePath.EndsWith(".json"))
            {
                var jsonTemplates = JsonConvert.SerializeObject(packingListTemplates, Formatting.Indented);
                File.WriteAllText(filePath, jsonTemplates);
            }
            else if (filePath.EndsWith(".xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PackingList>));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fs, packingListTemplates);
                }
            }
            
            
        }
    }
}
