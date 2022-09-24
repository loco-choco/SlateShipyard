using System.Collections.Generic;
using System.Xml;
using static Sample;

namespace XmlToMenagerieParser
{
    public class DocxfyCompounddefData
    {
        public string name;
        public string @namespace;
        public string compoundname;
        public DoxygenXMLKind kind;
        public DoxygenXMLProt protection;
        public string baseRef;

        public string briefdescription;
        public string detaileddescription;
        public DocxfyLocationData location = new DocxfyLocationData();

        public DocxfySectionsDefData sectionData = new DocxfySectionsDefData();

        public DocxfyCompounddefData(XmlElement element)
        {
            XmlAttribute kindAttr = element.GetAttributeNode("kind");
            XmlAttribute protAttr = element.GetAttributeNode("prot");
            kind = GetDoxygenXMLKind(kindAttr.InnerXml);
            protection = GetDoxygenXMLProtection(protAttr.InnerXml);

            var coumpNameNode = element.SelectSingleNode("compoundname");
            if (coumpNameNode != null)
            {
                compoundname = coumpNameNode.InnerText.Replace("::", ".");
            }
            var baseRefNode = element.SelectSingleNode("basecompoundref");
            if (baseRefNode != null)
            {
                baseRef = baseRefNode.InnerText;
            }

            var briefdescriptionNode = element.SelectSingleNode("briefdescription");
            if (briefdescriptionNode != null)
            {
                briefdescription = briefdescriptionNode.InnerText;
            }

            var detaileddescriptionNode = element.SelectSingleNode("detaileddescription");
            if (coumpNameNode != null)
            {
                detaileddescription = detaileddescriptionNode.InnerText;
            }

            var locationNode = element.SelectSingleNode("location");
            if (locationNode != null)
            {
                location = new DocxfyLocationData((XmlElement)locationNode);
            }

            var membersList = element.SelectNodes("sectiondef");
            if (membersList != null)
            {
                sectionData = new DocxfySectionsDefData(membersList);
            }

            int lastNamespace = compoundname.LastIndexOf('.');
            name = compoundname.Substring(lastNamespace + 1);
            @namespace = compoundname.Substring(0, lastNamespace);
        }
    }
    public class DocxfySectionsDefData
    {
        public Dictionary<DoxygenXMLKind, DocxfyMemberdefDataPerProt> MembersByType = new Dictionary<DoxygenXMLKind, DocxfyMemberdefDataPerProt>();

        public class DocxfyMemberdefDataPerProt 
        {
            public Dictionary<DoxygenXMLProt, List<DocxfyMemberdefData>> MembersByProt = new Dictionary<DoxygenXMLProt, List<DocxfyMemberdefData>>();
            public Dictionary<DoxygenXMLProt, List<DocxfyMemberdefData>> StaticMembersByProt = new Dictionary<DoxygenXMLProt, List<DocxfyMemberdefData>>();
        }


        public DocxfySectionsDefData()
        {
        }
        public DocxfyMemberdefData[] GetMembers(string kind, string prot, string is_static) 
        {
            var doxKind = GetDoxygenXMLKind(kind);
            var doxProtection = GetDoxygenXMLProtection(prot);
            if (MembersByType.TryGetValue(doxKind, out var members))
            {
                if(is_static == "yes")
                {
                    if (members.StaticMembersByProt.TryGetValue(doxProtection, out var membersFromProt))
                    {
                        return membersFromProt.ToArray();
                    }
                }
                else if(is_static == "no")
                {
                    if (members.MembersByProt.TryGetValue(doxProtection, out var membersFromProt))
                    {
                        return membersFromProt.ToArray();
                    }
                }
            }
            return new DocxfyMemberdefData[0];
        }
        public DocxfySectionsDefData(XmlNodeList elements)
        {
            foreach (XmlNode element in elements)
            {
                for (int i = 0; i < element.ChildNodes.Count; i++)
                {
                    var member = new DocxfyMemberdefData((XmlElement)element.ChildNodes[i]);

                    if (MembersByType.TryGetValue(member.kind, out var memberKindList))
                    {
                        if(member.staticStatus == "yes") 
                        {
                            if (memberKindList.StaticMembersByProt.TryGetValue(member.prot, out var memberProtList))
                            {
                                memberProtList.Add(member);
                            }
                            else
                            {
                                memberKindList.StaticMembersByProt[member.prot] = new List<DocxfyMemberdefData>() { member };
                            }
                        }
                        else 
                        {
                            if (memberKindList.MembersByProt.TryGetValue(member.prot, out var memberProtList))
                            {
                                memberProtList.Add(member);
                            }
                            else
                            {
                                memberKindList.MembersByProt[member.prot] = new List<DocxfyMemberdefData>() { member };
                            }
                        }
                        
                    }
                    else
                    {
                        MembersByType[member.kind] = new DocxfyMemberdefDataPerProt();
                        if (member.staticStatus == "yes")
                        {
                            MembersByType[member.kind].StaticMembersByProt[member.prot] = new List<DocxfyMemberdefData>() { member };
                        }
                        else
                        {
                            MembersByType[member.kind].MembersByProt[member.prot] = new List<DocxfyMemberdefData>() { member };
                        }
                    }
                }
            }
        }
    }
    public class DocxfyMemberdefData
    {
        public DoxygenXMLKind kind;
        public DoxygenXMLProt prot;
        public string staticStatus;
        public string type;
        public string argsstring;
        public string name;
        public string briefdescription;
        public string detaileddescription;
        public string inbodydescription;
        public DocxfyLocationData location;

        public DocxfyMemberdefData(XmlElement element)
        {
            XmlAttribute kindAttr = element.GetAttributeNode("kind");
            XmlAttribute protAttr = element.GetAttributeNode("prot");
            XmlAttribute staticAttr = element.GetAttributeNode("static");
            kind = GetDoxygenXMLKind(kindAttr.InnerXml);
            prot = GetDoxygenXMLProtection(protAttr.InnerXml);
            staticStatus = staticAttr.InnerXml;

            var typeNode = element.SelectSingleNode("type");
            if (typeNode != null)
            {
                type = typeNode.InnerText;
            }
            var argsstringNode = element.SelectSingleNode("argsstring");
            if (argsstringNode != null)
            {
                argsstring = argsstringNode.InnerText;
            }
            var nameNode = element.SelectSingleNode("name");
            if (nameNode != null)
            {
                name = nameNode.InnerText;
            }
            var briefdescriptionNode = element.SelectSingleNode("briefdescription");
            if (briefdescriptionNode != null)
            {
                briefdescription = briefdescriptionNode.InnerText;
            }

            var detaileddescriptionNode = element.SelectSingleNode("detaileddescription");
            if (detaileddescriptionNode != null)
            {
                detaileddescription = detaileddescriptionNode.InnerText;
            }

            var inbodydescriptionNode = element.SelectSingleNode("inbodydescription");
            if (inbodydescriptionNode != null)
            {
                inbodydescription = inbodydescriptionNode.InnerText;
            }

            var locationNode = element.SelectSingleNode("location");
            if (locationNode != null)
            {
                location = new DocxfyLocationData((XmlElement)locationNode);
            }
        }
    }

    public class DocxfyLocationData
    {
        public string file;
        public string line;
        public string column;
        public DocxfyLocationData()
        {
            file = "";
            line = "";
            column = "";
        }
        public DocxfyLocationData(XmlElement element)
        {
            XmlAttribute fileAttr = element.GetAttributeNode("file");
            XmlAttribute lineAttr = element.GetAttributeNode("line");
            XmlAttribute columnAttr = element.GetAttributeNode("column");
            file = fileAttr.InnerXml;
            line = lineAttr.InnerXml;
            column = columnAttr.InnerXml;
        }
    }
}
