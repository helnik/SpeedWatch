using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestConsole
{
    [DataContract]
    public class TestObject
    {
        [DataMember(Name = "name")]
        public string Name { get; set; } = "Test";

        [DataMember(Name = "age")]
        public int Age { get; set; } = 100;

        [DataMember(Name = "testObjects")]
        public List<TestObject> TestObjects { get; set; }
    }
}
