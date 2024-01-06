using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Learn.Classes
{

    public static class Settings
    {
        public static string AppDataDir { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LearnSettings");

        //public static Themes currentTheme {get; set;}

        #region App Defaults
        public static bool customExitOption { get; set; } = false;

        #endregion

        #region Main Window Defaults
        public static int numBestTermsToShow { get; set; } = 5;
        public static int numWorstTermsToShow { get; set; } = 5;

        #endregion

        #region Import Defaults
        public static string rowSeperatorDefault { get; set; } = ";";
        public static string termSeperatorDefault { get; set; } = ",";

        #endregion

        #region Quick Learn Defaults
        public static int waitIntervalQL { get; set; } = 1;
        public static int termsPerGroupQL { get; set; } = 10;
        public static int MCQMaxAttempts { get; set; } = 2;

        #endregion

        #region Matching Cards Defaults


        #endregion

        public static void SaveSettings()
        {
            try
            {
                string XMLPath = Path.Combine(AppDataDir,"LearnUserSettings.xml");
                if (!Directory.Exists(AppDataDir))
                {
                    Directory.CreateDirectory(AppDataDir);
                }
                if (!File.Exists(XMLPath))
                {
                    var Stream = File.Create(XMLPath);
                    Stream.Close();
                }
                XmlWriter writer = XmlWriter.Create(XMLPath);
                writer.WriteStartDocument();

                writer.WriteStartElement("Learn");
                writer.WriteAttributeString("version", "1.0");

                writer.WriteElementString("customExitOption", customExitOption.ToString());

                writer.WriteElementString("numBestTermsToShow", numBestTermsToShow.ToString());
                writer.WriteElementString("numWorstTermsToShow", numWorstTermsToShow.ToString());

                writer.WriteElementString("rowSeperatorDefault", rowSeperatorDefault.ToString());
                writer.WriteElementString("termSeperatorDefault", termSeperatorDefault.ToString());

                writer.WriteElementString("waitIntervalQL", waitIntervalQL.ToString());
                writer.WriteElementString("termsPerGroupQL", termsPerGroupQL.ToString());
                writer.WriteElementString("hardStudyMode", MCQMaxAttempts.ToString());

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Close();

            }
            catch {

            }
        }

        public static void LoadSettings()
        {
            try
            {
                string XMLPath = Path.Combine(AppDataDir, "LearnUserSettings.xml");

                if (File.Exists(XMLPath))
                {

                    string xmlFile = File.ReadAllText(XMLPath);
                    XElement xml = XElement.Parse(xmlFile);

                    customExitOption = Convert.ToBoolean(xml.Element("customExitOption")?.Value);

                    numBestTermsToShow = Convert.ToInt16(xml.Element("numBestTermsToShow")?.Value);
                    numWorstTermsToShow = Convert.ToInt16(xml.Element("numWorstTermsToShow")?.Value);

                    rowSeperatorDefault = Convert.ToString(xml.Element("rowSeperatorDefault")?.Value);
                    termSeperatorDefault = Convert.ToString(xml.Element("termSeperatorDefault")?.Value);
                    MCQMaxAttempts = Convert.ToInt16(xml.Element("hardStudyMode")?.Value);

                    waitIntervalQL = Convert.ToInt16(xml.Element("waitIntervalQL")?.Value);
                    termsPerGroupQL = Convert.ToInt16(xml.Element("termsPerGroupQL")?.Value);

                }
            }
            catch
            {
                
            }
        }


    }
}
