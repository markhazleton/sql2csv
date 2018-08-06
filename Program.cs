namespace Sql2Csv
{
    using System;
    using System.IO;
    using System.Reflection;

    class Program
    {
        static void Main()
        {
            var myExportConfig = new ExportConfiguration();
            var path = Assembly.GetExecutingAssembly().CodeBase;
            myExportConfig.ConfigPath = Path.GetDirectoryName(path).Replace("file:\\", string.Empty);
            myExportConfig.GetFromXml();

            Console.WriteLine("          **** ");
            Console.WriteLine("          **** ");
            Console.WriteLine("          **** ");


            Console.WriteLine(string.Format("          **** Configuration Path: {0}", myExportConfig.ConfigPath));
            Console.WriteLine(string.Format("          **** Data Path: {0}", myExportConfig.DataPath));
            Console.WriteLine(string.Format("          **** Script Path: {0}", myExportConfig.ScriptPath));
            Console.WriteLine(string.Format("          **** Configuration List: {0}",
                                            myExportConfig.DatabaseConfigurationListPath));

            Console.WriteLine("          **** ");
            Console.WriteLine("          **** ");
            Console.WriteLine("          **** ");

            var myConfigList = new SiteConfigurationCollection()
            { DataFolderPath = myExportConfig.DatabaseConfigurationListPath };
            myConfigList.GetFromXml();
            Export.ProcessExtractSql(myConfigList, myExportConfig);
        }
    }
}
