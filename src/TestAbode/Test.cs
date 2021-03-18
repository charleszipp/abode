using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Abode;

namespace TestAbode
{
    [TestClass]
    public class Test
    {
        IAbode mockAbode;
        readonly string testDtdl = "{\r\n  \"@id\": \"dtmi:abode:TestModel;1\",\r\n  \"@type\": \"Interface\",\r\n  \"displayName\": \"TestModel\",\r\n  \"contents\": [\r\n    {\r\n      \"@type\": \"Property\",\r\n      \"name\": \"data\",\r\n      \"schema\": \"string\"\r\n    }\r\n  ],\r\n  \"@context\": \"dtmi:dtdl:context;2\"\r\n}";

        [TestMethod]
        public async Task ModelCanBeUploadedOnce()
        {
            mockAbode = new MockAbode();
            Model testModel = new(mockAbode);
            await testModel.UploadModel(testDtdl);

            Assert.AreEqual("Model already exists.", await testModel.UploadModel(testDtdl));
        }
    }
}
