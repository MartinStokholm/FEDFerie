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
            var debtors = (ObservableCollection<PackingList>)serializer.Deserialize(reader);
            reader.Close();
            return debtors;
        }
        internal static void SaveFile(string fileName, ObservableCollection<PackingList> debtors)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PackingList>));
            TextWriter writer = new StreamWriter(fileName);
            // Serialize all the debtors.
            serializer.Serialize(writer, debtors);
            writer.Close();
        }
    }
}
