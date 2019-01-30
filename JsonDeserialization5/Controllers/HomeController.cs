using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace JsonDeserialization5.Controllers
{
    public class HomeController : Controller
    {

        public class KnownTypesBinder : ISerializationBinder
        {

            public List<String> GetAllTypeNames()
            {
                var expectedTypes = new List<string>();
               
                List<Assembly> expectedAssemblies = new List<Assembly>();
              //  Assembly assembly = Assembly.Load("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

               // expectedAssemblies.Add(assembly);
               Assembly assembly = Assembly.Load("JsonDeserialization5");
                expectedAssemblies.Add(assembly);
                foreach (var assembly1 in expectedAssemblies)
                {
                    foreach (Type type in assembly1.GetTypes())
                    {
                        expectedTypes.Add(type.FullName);

                    }
                }

                return expectedTypes;




            }
            public void BindToName(Type serializedType, out string assemblyName, out string typeName)
            {
                
                assemblyName = serializedType.AssemblyQualifiedName;
                typeName = serializedType.FullName;
            }
            public Type BindToType(string assemblyName, string typeName)
            {
                List<string> expectedTypeNames = GetAllTypeNames();

                if (!expectedTypeNames.Contains(typeName))
                {

                throw new Exception("invalid type");

                }
                Type type = Type.GetType(typeName);

                return type;
               // throw new NotImplementedException();
            }
        }
        public ActionResult SecureDeserialization()
        {
            string test = "{'$type':'JsonDeserialization5.Models.TestModel,JsonDeserialization5','Name':'rishabh' }";
            string payload = "{'$type':'System.Windows.Data.ObjectDataProvider, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35','MethodName':'Start','MethodParameters':{'$type':'System.Collections.ArrayList, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','$values':['cmd','/ccalc']},'ObjectInstance':{'$type':'System.Diagnostics.Process, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'}}";
            //If the object being deserialized is changed from test to payload , an error will be thrown 
            var a = JsonConvert.DeserializeObject(test, new JsonSerializerSettings
            {
                SerializationBinder = new KnownTypesBinder( ),
                TypeNameHandling = TypeNameHandling.Auto
            });

            return View();
        }

        public ActionResult InsecureDeserialization()
        {
            string payload = "{'$type':'System.Windows.Data.ObjectDataProvider, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35','MethodName':'Start','MethodParameters':{'$type':'System.Collections.ArrayList, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','$values':['cmd','/ccalc']},'ObjectInstance':{'$type':'System.Diagnostics.Process, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'}}";
        
            var a = JsonConvert.DeserializeObject(payload, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

            return View();
        }

      
    }
}