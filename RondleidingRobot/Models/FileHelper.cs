using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RondleidingRobot.Models
{
    //De filehelper klasse helpt met het openen van een txt file en om gemakkelijk alle bestanden uit 1 map te halen.
    abstract class FileHelper
    {
        //de read movement functie diend er voor om alle bewegingen uit een textbestand te halen
        public static List<string> ReadMovement()
        {
            string beweging;
            List<string> movementList = new List<string>();

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(folderPath, "bewegingen robot.txt");

            StreamReader reader = File.OpenText(filePath);

            string row;

            row = reader.ReadLine();
            while (row != null)
            {
                beweging = Convert.ToString(row);
                movementList.Add(beweging);
                row = reader.ReadLine();
            }
            reader.Close();
            return movementList;
        }

        //de get filestring functie zorgt dat we alle bestanden uit de map RondleidingRobot kunnen halen, in deze map staan dus alle text en geluidsbestanden.
        public static string getFileString(string fileName)
        {
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"RondleidingRobot");
            return System.IO.Path.Combine(folderPath, fileName);
        }
    }
}
