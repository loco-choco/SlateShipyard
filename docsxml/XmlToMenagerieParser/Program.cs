using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XmlToMenagerieParser;

public class Sample
{
    public static void Main()
    {
        try
        {
            //Create the XmlDocument.
            string indexFilePath;
            Console.Write("index.xml folder path: ");
            indexFilePath = Console.ReadLine();
            const string indexFileName = "index.xml";


            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(indexFilePath, indexFileName));

            List<DocxfyCompounddefData> classesAndStructsData = new List<DocxfyCompounddefData>();

            //Display the document element.
            XmlNode root = doc.SelectSingleNode("doxygenindex");
            if (root != null)
            {
                //Display the contents of the child nodes.
                if (root.HasChildNodes)
                {
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        XmlElement element = (XmlElement)root.ChildNodes[i];

                        string name = element.FirstChild.InnerText;

                        XmlAttribute refidAttr = element.GetAttributeNode("refid");
                        XmlAttribute kindAttr = element.GetAttributeNode("kind");
                        string refid = refidAttr.InnerXml;
                        string kind = kindAttr.InnerXml;

                        var kindType = GetDoxygenXMLKind(kind);

                        if (kindType == DoxygenXMLKind.CLASS || kindType == DoxygenXMLKind.STRUCT)
                        {
                            classesAndStructsData.Add(ReadRefidXml(indexFilePath, refid));
                        }
                    }
                }
            }

            string apiTemplateFilePath;
            const string apiTemplatefileName = "apiTemplate.md";
            Console.Write("{0} folder path: ", apiTemplatefileName);
            apiTemplateFilePath = Console.ReadLine();

            string mdFileTemplate = File.ReadAllText(Path.Combine(apiTemplateFilePath, apiTemplatefileName));

            foreach( var member in classesAndStructsData) 
            {
                CreateMDFromCompoundData(member, mdFileTemplate, apiTemplateFilePath);
            }
            Console.Write("Done!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0} - {1}", ex.Message, ex.StackTrace);
        }
        Console.ReadLine();
    }

    public static void CreateMDFromCompoundData(DocxfyCompounddefData data, string templateFile, string outputFolderPath)
    {
        var lexerOptions = new LexerOptions() { Mode = ScriptMode.Default, Lang = ScriptLang.Default};
        var template = Template.Parse(templateFile, lexerOptions: lexerOptions);

        var DocxfyCompounddefDataScriptObject = new ScriptObject();
        DocxfyCompounddefDataScriptObject.Import(data);
        DocxfyCompounddefDataScriptObject.Import("get_members", new Func<string,string,string, DocxfyMemberdefData[]>((kind,prot,is_static) => data.sectionData.GetMembers(kind, prot, is_static)));

        var context = new TemplateContext();
        context.PushGlobal(DocxfyCompounddefDataScriptObject);

        var result = template.Render(context);

        if (template.HasErrors)
        {
            foreach (var error in template.Messages)
            {
                Console.WriteLine(error);
            }
            return;
        }

        File.WriteAllText(Path.Combine(outputFolderPath, data.name + ".md"), result);
    }
    public static string GetMembers(string kind)
    {
        var doxKind = GetDoxygenXMLKind(kind);
        return doxKind.ToString();
    }

    public static DocxfyCompounddefData ReadRefidXml(string indexFolderPath, string refid)
    {
        const string xmlExtension = ".xml";
        XmlDocument doc = new XmlDocument();
        doc.Load(Path.Combine(indexFolderPath, refid + xmlExtension));

        XmlNode root = doc.ChildNodes[1].ChildNodes[0];
        return new DocxfyCompounddefData((XmlElement)root);
    }
    public static DoxygenXMLKind GetDoxygenXMLKind(string kind)
    {
        switch (kind)
        {
            case "class":
                return DoxygenXMLKind.CLASS;
            case "struct":
                return DoxygenXMLKind.STRUCT;
            case "namespace":
                return DoxygenXMLKind.NAMESPACE;
            case "file":
                return DoxygenXMLKind.FILE;
            case "dir":
                return DoxygenXMLKind.DIR;
            case "function":
                return DoxygenXMLKind.FUNCTION;
            case "variable":
                return DoxygenXMLKind.VARIABLE;
            default:
                return DoxygenXMLKind.Unknown;
        }
    }

    public static DoxygenXMLProt GetDoxygenXMLProtection(string prot)
    {
        switch (prot)
        {
            case "private":
                return DoxygenXMLProt.PRIVATE;
            case "public":
                return DoxygenXMLProt.PUBLIC;
            case "protected":
                return DoxygenXMLProt.PROTECTED;
            case "package":
                return DoxygenXMLProt.INTERNAL;
            default:
                return DoxygenXMLProt.Unknown;
        }
    }
}

public enum DoxygenXMLKind
{
    CLASS,
    STRUCT,
    NAMESPACE,
    FILE,
    DIR,
    FUNCTION,
    VARIABLE,
    EVENT,
    Unknown
}


public enum DoxygenXMLProt
{
    PRIVATE,
    PUBLIC,
    PROTECTED,
    INTERNAL,
    Unknown
}