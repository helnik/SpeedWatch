using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace TestConsole
{
    internal static class JsonActions
    {
        internal static TestObject TestObject = new TestObject
            { TestObjects = new List<TestObject> { new TestObject(), new TestObject() } };

        internal static string TestObjectSerialized = SerializeWithNewtonSoft();

        internal static string SerializeWithDataContractJsonSerializer()
        {
            var dcjs = new DataContractJsonSerializer(typeof(TestObject));
            var ms = new MemoryStream();
            dcjs.WriteObject(ms, TestObject);
            ms.Position = 0;
            var sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }

        internal static TestObject DeSerializeWithDataContractJsonSerializer()
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(TestObjectSerialized)))
            {
                var deserializer = new DataContractJsonSerializer(typeof(TestObject));
                var testObject = (TestObject)deserializer.ReadObject(ms);
                return testObject;
            }
        }

        internal static string SerializeWithJavaScriptJsonSerializer()
        {
            var js = new JavaScriptSerializer();
            return js.Serialize(TestObject);
        }

        internal static TestObject DeSerializeWithJavaScriptJsonSerializer()
        {
            var js = new JavaScriptSerializer();
            return js.Deserialize<TestObject>(TestObjectSerialized);
        }

        internal static string SerializeWithNewtonSoft()
        {
            return JsonConvert.SerializeObject(TestObject);
        }

        internal static TestObject DeSerializeWithNewtonSoft()
        {
            return JsonConvert.DeserializeObject<TestObject>(TestObjectSerialized);
        }
    }
}
