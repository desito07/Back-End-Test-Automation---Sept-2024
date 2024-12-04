namespace ApiTestingBasics
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var token = GlobalConstants.AuthenticateUser("admin@gmail.com", "admin@gmail.com");
        }

        [Test]
        public void Test1()
        {
            
        }
    }
}