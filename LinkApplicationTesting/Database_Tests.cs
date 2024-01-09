
using LinkApplication;
using Moq;

namespace LinkApplicationTesting
{
    public class Database_Tests
    {

        Database_Connecter _connecter;

        [SetUp]
        public void Setup()
        {
            _connecter = new Database_Connecter();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}