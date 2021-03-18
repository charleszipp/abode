using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Abode;

namespace TestAbode
{
    [TestClass]
    public class Test
    {
        IDigitalTwinsClient mockClient;
        readonly string testDtdl = "{\r\n  \"@id\": \"dtmi:abode:TestModel;1\",\r\n  \"@type\": \"Interface\",\r\n  \"displayName\": \"TestModel\",\r\n  \"contents\": [\r\n    {\r\n      \"@type\": \"Property\",\r\n      \"name\": \"data\",\r\n      \"schema\": \"string\"\r\n    }\r\n  ],\r\n  \"@context\": \"dtmi:dtdl:context;2\"\r\n}";

        [TestMethod]
        public async Task ModelDoesNotExist()
        {
            mockClient = new MockDigitalTwinsClient();
            Model testModel = new(mockClient);
            await testModel.UploadModel(testDtdl);
            MockModelData mockModelData = new(testDtdl);
            bool result = await mockClient.CheckIfModelExist(mockModelData.Id);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ModelDoesExist()
        {
            mockClient = new MockDigitalTwinsClient();
            Model testModel = new(mockClient);
            await testModel.UploadModel(testDtdl);
            // Upload again the same model
            string response = await testModel.UploadModel(testDtdl);
            Assert.AreEqual("Model cannot be uploaded as it already exists.", response);
        }
    }
}
