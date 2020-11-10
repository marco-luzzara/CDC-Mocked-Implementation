using CDCImplementation.CDCLogic.Strategies.DiffWhere;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CDCImplementation.Test.CDCLogic.Strategies.DiffWhere
{
    [TestClass]
    public class DiffWhereStateTest
    {
        [TestMethod]
        public void Serialization_EmptyDictionary()
        {
            DiffWhereState state = new DiffWhereState(new Dictionary<string, string>());
        }
    }
}
