using System;
using System.IO;

namespace FBHC
{
    public class ProblemConfig
    {
        public readonly String Name;
        public readonly int Year;
        public readonly int Round;

        public ProblemConfig(String name, int year, int round)
        {
            Name = name;
            Year = year;
            Round = round;
        }

        public String FileName
        {
            get
            {
                return Name.Replace(" ", "");
            }
        }

        private String BaseDirectory
        {
            get
            {
                return Path.Combine("Files", Year.ToString(), Round.ToString(), FileName);
            }
        }

        public String SampleInputFileName
        {
            get
            {
                return BaseDirectory + "/" + FileName + "SampleInput.txt";
            }
        }

        public String SampleOutputFileName
        {
            get
            {
                return BaseDirectory + "/" + FileName + "SampleOutput.txt";
            }
        }

        public String InputFileName
        {
            get
            {
                return BaseDirectory + "/" + FileName + "Input.txt";
            }
        }
    }
}
