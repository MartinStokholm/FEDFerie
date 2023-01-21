using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using FerieWPFApp.Models;

namespace FerieWPFApp.Data
{
    internal class Repository
    {
        //Inspiration taken from the Agent 5 assignment solution

        internal static ObservableCollection<PackingList> ReadFile(string fileName)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PackingList>));
            TextReader reader = new StreamReader(fileName);
            // Deserialize all the debtors.
            var packingLists = (ObservableCollection<PackingList>)serializer.Deserialize(reader);
            reader.Close();
            return packingLists;
        }
        internal static void SaveFile(string fileName, ObservableCollection<PackingList> packingLists)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PackingList>));
            TextWriter writer = new StreamWriter(fileName);
            // Serialize all the debtors.
            serializer.Serialize(writer, packingLists);
            writer.Close();
        }
    }
}
