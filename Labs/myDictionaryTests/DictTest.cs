using myDictionary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace myDictionaryTests
{
    [TestClass]
    public class DictTests
    {
        private Dict<int, string> _dictionary;

        [TestInitialize]
        public void TestInitialize()
        {
            _dictionary = new();
        }

        [TestMethod]
        public void AddItem_WhenAddOneItem_ThenContainsOneItem()
        {
            //arrange
            Item<int, string> item1 = new(4, "France");

            //act
            _dictionary.Add(item1);

            //assert
            Assert.AreEqual(1, _dictionary.Count);
            Assert.AreEqual(1, _dictionary.Count());
        }

        [TestMethod]
        public void AddItem_WhenAddOneItem_ThenItemInDictionary()
        {
            //arrange
            Item<int, string> item1 = new(4, "France");

            //act
            _dictionary.Add(item1);

            //assert
            Assert.AreEqual(item1.Value, _dictionary[4]);
        }

        [TestMethod]
        public void SearchItemInDictionary()
        {
            // arrange
            Item<int, string> item2 = new(3, "Great Britain");
            string searched;

            // act
            _dictionary.Add(item2);

            searched = _dictionary.Search(item2.Key);

            //assert
            Assert.AreEqual(item2.Value, searched);
        }
    }
}

