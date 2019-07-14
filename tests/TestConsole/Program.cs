using SpeedWatch;
using System;
using System.Collections.Generic;
using System.Linq;

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

            Console.WriteLine("====COLLECTION TESTS ADD VALUE TYPES ====");
            RunCollectionAddTests(Enumerable.Range(1, 100000).ToList());
            Console.WriteLine("====COLLECTION TESTS ADD REFERENCE TYPES ====");
            RunCollectionAddTests(Enumerable.Range(1, 1000).Select(o => new TestObject{ TestObjects = new List<TestObject>()}).ToList());

            Console.WriteLine("====COLLECTION TESTS REMOVE VALUE TYPES ====");
            RunCollectionRemoveTests(Enumerable.Range(1, 100000).ToList());
            Console.WriteLine("====COLLECTION TESTS REMOVE REFERENCE TYPES ====");
            RunCollectionRemoveTests(Enumerable.Range(1, 1000).Select(o => new TestObject { TestObjects = new List<TestObject>() }).ToList());

            Console.ReadKey();
        }

        #region JSON TESTS
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
        #endregion

        #region Collections
        private static void RunCollectionAddTests<T>(List<T> inputList)
        {
            var listTest = new SpeedWatchTest("ListTest - Add",
                () => 
                    RunCollectionAction(inputList, 10, () => new List<T>(), (list, i) => list.Add(i)),
                10, $"Add {inputList.Count} {typeof(T).Name} types to a list");
            var dictionaryTest = new SpeedWatchTest("DictionaryTest - Add",
                () => 
                    RunCollectionAction(inputList, 10, () => new Dictionary<T, T>(), (dic, i) => dic.Add(i, i)),
                10, $"Add {inputList.Count} {typeof(T).Name} types to a dictionary");
            var hashSetTest = new SpeedWatchTest("HashSetTest - Add",
                () => 
                    RunCollectionAction(inputList, 10, () => new HashSet<T>(), (hash, i) => hash.Add(i)),
                10, $"Add {inputList.Count} {typeof(T).Name} types to a hashset");

            Console.WriteLine($@"Collections Add Tests:
{listTest.GetSummary()} 
{dictionaryTest.GetSummary()} 
{hashSetTest.GetSummary()}");
        }

        private static void RunCollectionRemoveTests<T>(List<T> inputList)
        {
            var targetList = inputList.Take(inputList.Count / 2).ToList();

            var listTest = new SpeedWatchTest("ListTest - Remove",
                () => 
                    RunCollectionAction(targetList, 10, () => new List<T>(inputList), (list, i) => list.Remove(i)),
                10, $"Remove half {typeof(T).Name} types from a list");
            var dictionaryTest = new SpeedWatchTest("DictionaryTest - Remove",
                () => RunCollectionAction(targetList, 10, () => new Dictionary<T, T>(inputList.ToDictionary(i => i, i=> i)), (dic, i) => dic.Remove(i)),
                10, $"Remove half {typeof(T).Name} types from a dictionary");
            var hashSetTest = new SpeedWatchTest("HashSetTest - Remove",
                () => RunCollectionAction(targetList, 10, () => new HashSet<T>(inputList), (hash, i) => hash.Remove(i)),
                10, $"Remove half {typeof(T).Name} types from a hashset");

            Console.WriteLine($@"Collections Remove Tests:
{listTest.GetSummary()} 
{dictionaryTest.GetSummary()} 
{hashSetTest.GetSummary()}");
        }
        
        internal static void RunCollectionAction<T, I>(List<I> input, int noOfRuns, Func<T> collectionInitializerFunc, Action<T, I> action)
        {
            for (var i = 0; i < noOfRuns; i++)
            {
                var collection = collectionInitializerFunc();
                input.ForEach(inp => action(collection, inp));
            }
        }
        #endregion
    }
}
