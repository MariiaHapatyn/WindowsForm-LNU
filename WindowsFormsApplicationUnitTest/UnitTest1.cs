using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication;
using System.Drawing;
using System.Globalization;

namespace UnitTest1 {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1 () {
        }
        [TestMethod]
        public void ArgbColorTest () {
            Quad figure = new Quad();
            figure.FigureColor = Color.Gray;
            int expected = Int32.Parse( "#FF808080".Replace( "#", "" ), NumberStyles.HexNumber );

            int actual = figure.ColorArgb;

            Assert.AreEqual( expected, actual );
        }
    }
}
