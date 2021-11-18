﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RondleidingRobot.Models
{
    abstract class FileHelper
    {
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


        public static string getFileString(string fileName)
        {
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"RondleidingRobot");
            return System.IO.Path.Combine(folderPath, fileName);
        }
    }
}
