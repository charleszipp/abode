using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TwinApp;

namespace TwinAppTest
{
    [TestClass]
    public class TwinAppTest
    {
        ITwin mockTwin;
        readonly string testDtdl = "{\r\n  \"@id\": \"dtmi:example:TestModel;1\",\r\n  \"@type\": \"Interface\",\r\n  \"displayName\": \"TestModel\",\r\n  \"contents\": [\r\n    {\r\n      \"@type\": \"Property\",\r\n      \"name\": \"data\",\r\n      \"schema\": \"string\"\r\n    }\r\n  ],\r\n  \"@context\": \"dtmi:dtdl:context;2\"\r\n}";

        [TestMethod]
        public async Task ModelCanBeUploadedOnce()
        {
            mockTwin = new MockTwin();
            Model testModel = new(mockTwin);
            await testModel.UploadModel(testDtdl);

            Assert.AreEqual("Model already exists.", await testModel.UploadModel(testDtdl));
        }
    }
}
