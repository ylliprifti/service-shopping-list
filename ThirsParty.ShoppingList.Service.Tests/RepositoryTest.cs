using NUnit.Framework;
using FluentAssertions;
using ThirdParty.ShoppingList.Service;
using ThirdParty.ShoppingList.Service.Model;

namespace ThirsParty.ShoppingList.Service.Tests
{
    [TestFixture]
    public class RepositoryTest
    {
        private MemoryRepository _repository;
        private ShoppingItem _item1, _item2, _item3;
        [SetUp]
        public void Setup()
        {
            _repository = new MemoryRepository();
            _item1 = new ShoppingItem("Milk", 1);
            _item2 = new ShoppingItem("Butter", 2);
            _item3 = new ShoppingItem("Egg", 5); 

        }

        [Test]
        public void CreateRepository() {
            _repository.Should().NotBeNull();
        }

        [Test]
        public void TestMethods() {
            _repository.Insert(_item1).Should().BeTrue();
            _repository.Insert(_item2).Should().BeTrue();
            _repository.Insert(_item2).Should().BeTrue();
            _repository.Insert(_item3).Should().BeTrue();
            _repository.Insert(new ShoppingItem()).Should().BeFalse();
            _repository.Get(new ShoppingItem("Milk", 0)).Should().BeSameAs(_item1);
            _repository.Get(_item2).Quantity.Should().Be(4);
            _repository.Update(new ShoppingItem("Milk", 0)).Should().BeTrue();
            _repository.Get(_item1).Quantity.Should().Be(0);
            _repository.Delete(_item1).Should().BeTrue();
            _repository.Get().Should().HaveCount(2);
        }

    }
}
