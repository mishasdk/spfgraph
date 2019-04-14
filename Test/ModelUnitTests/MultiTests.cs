using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;


namespace Testing {

    [TestClass()]
    public class MultiTests {
        string testSourceFolderPath = "../../TestSource/";

        [TestMethod()]
        public void ReadGraphAndBuildViz() {
            try {
                string path = testSourceFolderPath + "/correctList_01.txt";
                var g = GraphReader.ReadGraphFromFile(path);
                var gb = new GraphBuilder(g);
                var a = gb.CeateBidirectionalGraphToViz();
                Assert.IsTrue(true);
            } catch {
                Assert.Fail();
            }

        }

    }
}
