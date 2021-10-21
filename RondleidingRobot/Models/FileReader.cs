using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RondleidingRobot.Models
{
    abstract class FileReader
    {
        public static List<string> ReadMovement()
        {
            string beweging;
            List<string> movementList = new List<string>();

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = System.IO.Path.Combine(folderPath, "bewegingen robot.txt");

            StreamReader reader = File.OpenText(filePath);

            string row;
            string[] columns;
            char[] separators = { ':' };

            row = reader.ReadLine();
            while (row != null)
            {
                columns = row.Split(separators);
                beweging = Convert.ToString(columns[0]);
                movementList.Add(beweging);
                row = reader.ReadLine();
            }
            reader.Close();
            return movementList;
        }
    }
}
