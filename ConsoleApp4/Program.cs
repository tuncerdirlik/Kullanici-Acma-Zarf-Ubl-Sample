using System.Reflection.Metadata;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var standarBusinessDocument = CreateStandardBusinessDocument();
            string xml_standarBusinessDocument = SerializeStandardBusinessDocument(standarBusinessDocument);

            var processUserAccount = CreateProcessUserAccount();

            //string xml_1 = SerializeToXml<ProcessUserAccountType>(processUserAccount);
            //string xml_2 = SerializeProcessUserAccount(processUserAccount);
            //string xml_3 = File.ReadAllText("C:\\Users\\tuncerdirlik\\source\\repos\\ConsoleApp4\\ConsoleApp4\\XMLFile2.xml");

            //var processUserAccount_1 = DeserializeFromXml(xml_1);
            //var processUserAccount_2 = DeserializeFromXml(xml_2);
            //var processUserAccount_3 = DeserializeFromXml(xml_3);
            //string signatureXml = processUserAccount_3.ApplicationArea.Signature.Any.InnerXml;

        }

        static ProcessUserAccountType CreateProcessUserAccount()
        {
            ProcessUserAccountType processUserAccount = new ProcessUserAccountType();
            processUserAccount.ApplicationArea = new ApplicationAreaType();
            processUserAccount.ApplicationArea.Sender = new SenderType
            {
                LogicalID = new IdentifierType1
                {
                    Value = "9999999999"
                }
            };

            processUserAccount.ApplicationArea.Receiver = new ReceiverType[] { };
            processUserAccount.ApplicationArea.CreationDateTime = DateTime.Now.ToString();
            processUserAccount.ApplicationArea.Signature = new SignatureType();

            processUserAccount.DataArea = new ProcessUserAccountDataAreaType();
            processUserAccount.DataArea.Process = new ProcessType { };
            processUserAccount.DataArea.UserAccount = new UserAccountType[]
            {
                new UserAccountType
                {
                    UserID = new IdentifierType2
                    {
                        Value = "9999999999"
                    },
                    PersonName = new PersonNameType
                    {
                        FormattedName = new FormattedNameType
                        {
                            Value = "e-Fatura Deneme A.Ş."
                        },
                        GivenName = new NameType1[]
                        {
                            new NameType1()
                        },
                        MiddleName = new MiddleNameType(),
                        FamilyName = new FamilyNameType[]
                        {
                            new FamilyNameType()
                        }
                    },
                    UserRole = new UserRoleType[]
                    {
                        new UserRoleType
                        {
                            RoleCode = "GB",
                            RoleName = new TextType1
                            {
                                Value = "Gönderici Birim"
                            }
                        }
                    },
                    AuthorizedWorkScope = new WorkScopeType[]
                    {
                        new WorkScopeType
                        {
                            WorkScopeCode = new CodeType1
                            {
                                Value = "urn:mail:defaultpk@gib.gov.tr"
                            },
                            WorkScopeName = "Gönderici Birimi Etiketi"
                        }
                    },
                    AccountConfiguration = new AccountConfigurationType
                    {
                        UserOptionCode = new CodeType1[]
                        {
                            new CodeType1
                            {
                                Value = "2"
                            }
                        }
                    }
                },
                new UserAccountType
                {
                    UserID = new IdentifierType2
                    {
                        Value = "9999999999"
                    },
                    PersonName = new PersonNameType
                    {
                        FormattedName = new FormattedNameType
                        {
                            Value = "e-Fatura Deneme A.Ş."
                        },
                        GivenName = new NameType1[]
                        {
                            new NameType1()
                        },
                        MiddleName = new MiddleNameType(),
                        FamilyName = new FamilyNameType[]
                        {
                            new FamilyNameType()
                        }
                    },
                    UserRole = new UserRoleType[]
                    {
                        new UserRoleType
                        {
                            RoleCode = "PK",
                            RoleName = new TextType1
                            {
                                Value = "Posta Kutusu"
                            }
                        }
                    },
                    AuthorizedWorkScope = new WorkScopeType[]
                    {
                        new WorkScopeType
                        {
                            WorkScopeCode = new CodeType1
                            {
                                Value = "urn:mail:defaultpk@gib.gov.tr"
                            },
                            WorkScopeName = "Posta Kutusu Birimi Etiketi"
                        }
                    },
                    AccountConfiguration = new AccountConfigurationType
                    {
                        UserOptionCode = new CodeType1[]
                        {
                            new CodeType1
                            {
                                Value = "2"
                            }
                        }
                    }
                }
            };

            return processUserAccount;
        }

        static StandardBusinessDocument CreateStandardBusinessDocument()
        {
            StandardBusinessDocument standardBusinessDocument = new StandardBusinessDocument();
            standardBusinessDocument.StandardBusinessDocumentHeader = new StandardBusinessDocumentHeader();
            standardBusinessDocument.StandardBusinessDocumentHeader.HeaderVersion = "1.0";
            standardBusinessDocument.StandardBusinessDocumentHeader.Sender = new Partner[]
            {
                new Partner
                {
                    Identifier = new PartnerIdentification
                    {
                        Value = "usergb"
                    },
                    ContactInformation = new ContactInformation[]
                    {
                        new ContactInformation
                        {
                            Contact = "e-Fatura Deneme A.Ş",
                            ContactTypeIdentifier = "UNVAN"
                        },
                        new ContactInformation
                        {
                            Contact = "9999999999",
                            ContactTypeIdentifier = "VKN_TCKN"
                        }
                    }
                }
            };
            standardBusinessDocument.StandardBusinessDocumentHeader.Receiver = new Partner[]
            {
                new Partner
                {
                    Identifier = new PartnerIdentification
                    {
                        Value = "GIB"
                    },
                    ContactInformation = new ContactInformation[]
                    {
                        new ContactInformation
                        {
                            Contact = "Gelir İdaresi Başkanlığı",
                            ContactTypeIdentifier = "UNVAN"
                        },
                        new ContactInformation
                        {
                            Contact = "3900383669",
                            ContactTypeIdentifier = "VKN_TCKN"
                        }
                    }
                }
            };
            standardBusinessDocument.StandardBusinessDocumentHeader.DocumentIdentification = new DocumentIdentification
            {
                Standard = string.Empty,
                TypeVersion = "1.2",
                InstanceIdentifier = Guid.NewGuid().ToString(),
                Type = "USERENVELOPE",
                CreationDateAndTime = DateTime.Now
            };


            PackageElements packageElements = new PackageElements();
            packageElements.ElementType = "PROCESSUSERACCOUNT";
            packageElements.ElementCount = 1;
            packageElements.ElementList = new PackageElementsElementList();

            XmlDocument docProccessUserAccount = new XmlDocument();
            var processUserAccount = CreateProcessUserAccount();
            docProccessUserAccount.LoadXml(SerializeProcessUserAccount(processUserAccount));

            //packageElements.ElementList.Any = docProccessUserAccount.DocumentElement?.SelectNodes("//*")?.Cast<XmlElement>().ToArray();
            packageElements.ElementList.Any = new XmlElement[] { docProccessUserAccount.DocumentElement };

            Package package = new Package();
            package.Elements = new PackageElements[] { packageElements };

            XmlDocument docPackage = new XmlDocument();
            docPackage.LoadXml(SerializePackage(package));

            standardBusinessDocument.Any = docPackage.DocumentElement;

            return standardBusinessDocument;

        }

        static ProcessUserAccountType DeserializeFromXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProcessUserAccountType));
            using (StringReader reader = new StringReader(xml))
            {
                return (ProcessUserAccountType)serializer.Deserialize(reader);
            }
        }

        static string SerializeToXml<T>(T data)
        {
            //XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            //xmlSerializerNamespaces.Add("oa", "http://www.openapplications.org/oagis/9");
            //xmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            //xmlSerializerNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            //xmlSerializerNamespaces.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, data);
            return stringWriter.ToString();
        }

        static string SerializeProcessUserAccount(ProcessUserAccountType processUserAccount)
        {
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("oa", "http://www.openapplications.org/oagis/9");
            xmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xmlSerializerNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlSerializerNamespaces.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");

            var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            var ms = new MemoryStream();
            var writer = XmlWriter.Create(ms, settings);
            var srl = new XmlSerializer(processUserAccount.GetType());
            srl.Serialize(writer, processUserAccount, xmlSerializerNamespaces);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            var sRead = new StreamReader(ms);
            var readXml = sRead.ReadToEnd();

            return readXml;
        }

        static string SerializePackage(Package package)
        {
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("ef", "http://www.efatura.gov.tr/package-namespace");

            var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            var ms = new MemoryStream();
            var writer = XmlWriter.Create(ms, settings);
            var srl = new XmlSerializer(package.GetType());
            srl.Serialize(writer, package, xmlSerializerNamespaces);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            var sRead = new StreamReader(ms);
            var readXml = sRead.ReadToEnd();

            return readXml;
        }

        static string SerializeStandardBusinessDocument(StandardBusinessDocument standardBusinessDocument)
        {
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            xmlSerializerNamespaces.Add("ef", "http://www.efatura.gov.tr/package-namespace");
            xmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            XmlSerializer serializer = new XmlSerializer(standardBusinessDocument.GetType());
            StringWriter stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, standardBusinessDocument, xmlSerializerNamespaces);
            return stringWriter.ToString();

            //var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            //var ms = new MemoryStream();
            //var writer = XmlWriter.Create(ms, settings);
            //var srl = new XmlSerializer(standardBusinessDocument.GetType());
            //srl.Serialize(writer, standardBusinessDocument, xmlSerializerNamespaces);
            //ms.Flush();
            //ms.Seek(0, SeekOrigin.Begin);
            //var sRead = new StreamReader(ms);
            //var readXml = sRead.ReadToEnd();

            //return readXml;
        }
    }
}