﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

public class ExportConfiguration
{
    public string ScriptPath { get; set; }
    public string DataPath { get; set; }
    public string ConfigPath { get; set; }
    public string DatabaseConfigurationListPath { get; set; }
    public string TargetDatabaseName { get; set; }

    public ExportConfiguration(string reqScriptPath, string reqDataPath)
    {
        DataPath = reqDataPath;
        ScriptPath = reqScriptPath;
    }
    public ExportConfiguration()
    {
    }
    public string SaveXML()
    {
        var sReturn = string.Empty;
        var myXML = new XmlDocument();

        if ((this != null))
        {
            var oXS = new XmlSerializer(typeof(ExportConfiguration));
            using (var writer = new StringWriter())
            {
                oXS.Serialize(writer, this);
                myXML.LoadXml(writer.ToString());
            }
            myXML.Save(string.Format("{0}\\ExportConfiguration.xml", ConfigPath));
        }
        return sReturn;
    }
    public ExportConfiguration GetFromXML()
    {
        var x = new XmlSerializer(typeof(ExportConfiguration));
        try
        {
            using (var objStreamReader = new StreamReader(string.Format("{0}\\ExportConfiguration.xml", ConfigPath)))
            {
                var myY = (ExportConfiguration)x.Deserialize(objStreamReader);
                ConfigPath = myY.ConfigPath;
                DataPath = myY.DataPath;
                ScriptPath = myY.ScriptPath;
                DatabaseConfigurationListPath = myY.DatabaseConfigurationListPath;
                TargetDatabaseName = myY.TargetDatabaseName;
                objStreamReader.Close();
            }
        }
        catch (Exception e)
        {
            ExportConfiguration ex = new ExportConfiguration();
            var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            ex.ConfigPath = System.IO.Path.GetDirectoryName(path).Replace("file:\\", string.Empty);
            ex.ScriptPath = String.Format("{0}/script/", System.IO.Path.GetDirectoryName(path).Replace("file:\\", string.Empty));
            ex.DataPath  = String.Format("{0}/data/", System.IO.Path.GetDirectoryName(path).Replace("file:\\", string.Empty));
            ex.DatabaseConfigurationListPath = String.Format("{0}/", System.IO.Path.GetDirectoryName(path).Replace("file:\\", string.Empty));
            ex.SaveXML();
            Console.WriteLine("{0} Exception caught.", e);
        }

        return this;
    }
}
