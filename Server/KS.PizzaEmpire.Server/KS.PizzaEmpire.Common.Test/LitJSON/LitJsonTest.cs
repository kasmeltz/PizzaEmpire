using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LitJson;

namespace KS.PizzaEmpire.Common.Test.LitsJSON
{
    [TestClass]
    public class LitJsonTest
    {
        [TestMethod]
        public void TestDate()
        {
            DateTime[] now = new DateTime[1];
            now[0] = DateTime.UtcNow;
            string json = JsonMapper.ToJson(now);
            DateTime[] stillNow = JsonMapper.ToObject<DateTime[]>(json);
        }
    }
}
