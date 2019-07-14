using SpeedWatch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====SERIALIZER TESTS====");
            RunSerializerTests();
            Console.WriteLine("====DESERIALIZER TESTS====");
            RunDeSerializerTests();
            Console.ReadKey();
        }

        private static void RunSerializerTests()
        {
            var dataContractSerializerTest = new SpeedWatchTest("DataContractSerializer",
                () => JsonActions.SerializeWithDataContractJsonSerializer(),
                10, "DataContractJsonSerializer test");
            var javaScriptSerializerTest = new SpeedWatchTest("JavaScriptSerializer",
                () => JsonActions.SerializeWithJavaScriptJsonSerializer(),
                10, "JavaScriptJsonSerializer test");
            var newtonSoftSerializerTest = new SpeedWatchTest("NewtonsoftSerializer",
                () => JsonActions.SerializeWithNewtonSoft(),
                10, "NewtonsoftSerializer test");

            Console.WriteLine($@"{dataContractSerializerTest.GetSummary()} 
{javaScriptSerializerTest.GetSummary()} 
{newtonSoftSerializerTest.GetSummary()}");
        }

        private static void RunDeSerializerTests()
        {
            var dataContractDeSerializerTest = new SpeedWatchTest("DataContractDeSerializer",
                () => JsonActions.DeSerializeWithDataContractJsonSerializer(),
                10, "DataContractJsonDeSerializer test");
            var javaScriptDeSerializerTest = new SpeedWatchTest("JavaScriptDeSerializer",
                () => JsonActions.DeSerializeWithJavaScriptJsonSerializer(),
                10, "JavaScriptJsonDeSerializer test");
            var newtonSoftDeSerializerTest = new SpeedWatchTest("NewtonsoftDeSerializer",
                () => JsonActions.DeSerializeWithNewtonSoft(),
                10, "NewtonsoftDeSerializer test");

            Console.WriteLine($@"{dataContractDeSerializerTest.GetSummary()} 
{javaScriptDeSerializerTest.GetSummary()} 
{newtonSoftDeSerializerTest.GetSummary()}");
        }
    }
}
