using System.IO;

namespace BludataTest.Services
{
    public class Logger
    {
        public void RegisterLogInFile(string log)
        {
            string directory = Directory.GetCurrentDirectory();
            string fileName = "logs.txt";
            string fullPath = directory + "/" + fileName;
            using (StreamWriter writer = new StreamWriter(fullPath, append: true))
            {
                writer.WriteLine(log);
            }
        }
    }
}