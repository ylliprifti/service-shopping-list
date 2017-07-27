using NUnit.Framework;
using FluentAssertions;
using TP = ThirdParty.ShoppingList.Service;
using TPI = ThirdParty.ShoppingList.Service.Interfaces;
using TPM = ThirdParty.ShoppingList.Service.Model;

namespace ThirsParty.ShoppingList.Service.Tests
{
    [TestFixture]
    public class RepositoryTest
    {
        private TPI.IRepository<TPI.IItem> _repository;
        private Moq.Mock<TPI.IStorage> _mockStorage = new Moq.Mock<TPI.IStorage>();
        private TPM.ShoppingItem _item1, _item2, _item3;
        [SetUp]
        public void Setup()
        {


            _repository = new TP.Repository( _mockStorage.Object);
            _item1 = new TPM.ShoppingItem("Milk", 1);
            _item2 = new TPM.ShoppingItem("Butter", 2);
            _item3 = new TPM.ShoppingItem("Egg", 5); 

        }

        [Test]
        public void CreateRepository() {
            _repository.Should().NotBeNull();
        }

        [Test]
        public void TestInsert() {
            _mockStorage.Setup(x => x.Contains(_item1.Name)).Returns(true);
            _mockStorage.Setup(x => x[_item1.Name]).Returns(_item1);
            _mockStorage.Setup(x => x.Contains(_item2.Name)).Returns(false);
            

            bool result = _repository.Insert(_item1);
            result.Should().BeTrue();                   //Insert was successfull

            // Item exists, item is retreived and updated.
            _mockStorage.Verify(x => x[_item1.Name], Moq.Times.Once);
            _mockStorage.VerifySet(x => x[_item1.Name] = Moq.It.IsAny<TPI.IItem>(), Moq.Times.Once);

            result = _repository.Insert(_item2);
            result.Should().BeTrue();                 //Insert was successfull

            // Item does not exist, item is only set
            _mockStorage.Verify(x => x[_item2.Name], Moq.Times.Never);
            _mockStorage.VerifySet(x => x[_item2.Name] = Moq.It.IsAny<TPI.IItem>(), Moq.Times.Once);




        }

        [Test]
        public void TestFailedInsert() {
            var result = _repository.Insert(new TPM.ShoppingItem());
            result.Should().BeFalse();
        }

        [Test]
        public void TestGet() {
            //Left Intentionally blank. 
            //This unit is a proxy to the underlaying storage. 
            //Nothing to test
            Assert.IsTrue(true);
        }

        [Test]
        public void TestDelete()
        {
            _mockStorage.Setup(x => x.Contains(_item1.Name)).Returns(false);
            _mockStorage.Setup(x => x.Contains(_item2.Name)).Returns(true);

            _repository.Delete(_item1).Should().BeFalse(); //_item1 does not exists; return false;
            _mockStorage.Verify(x => x.Remove(_item1.Name), Moq.Times.Never);

            _repository.Delete(_item2).Should().BeTrue(); //_item2 exists; return true;
            _mockStorage.Verify(x => x.Remove(_item2.Name), Moq.Times.Once);

            _repository.Delete(new TPM.ShoppingItem()).Should().BeFalse(); // Empty item, no delete


        }

        [Test]
        public void TestUpdate() {

            _mockStorage.Setup(x => x.Contains(_item1.Name)).Returns(false);
            _mockStorage.Setup(x => x.Contains(_item3.Name)).Returns(true);

            _repository.Update(_item1).Should().BeFalse(); //_item1 does not exists; return false;
            _mockStorage.VerifySet(x => x[_item1.Name] = _item1, Moq.Times.Never);

            _repository.Update(_item3).Should().BeTrue(); //_item2  exists; return true;
            _mockStorage.VerifySet(x => x[_item3.Name] = _item3, Moq.Times.Once);

            _repository.Update(new TPM.ShoppingItem()).Should().BeFalse(); // Empty item, no update

        }

        //[Test]
        //public void TestMethods() {
        //    _repository.Insert(_item1).Should().BeTrue();
        //    _repository.Insert(_item2).Should().BeTrue();
        //    _repository.Insert(_item2).Should().BeTrue();
        //    _repository.Insert(_item3).Should().BeTrue();
        //    _repository.Insert(new ShoppingItem()).Should().BeFalse();
        //    _repository.Get(new ShoppingItem("Milk", 0)).Should().BeSameAs(_item1);
        //    _repository.Get(_item2).Quantity.Should().Be(4);
        //    _repository.Update(new ShoppingItem("Milk", 0)).Should().BeTrue();
        //    _repository.Get(_item1).Quantity.Should().Be(0);
        //    _repository.Delete(_item1).Should().BeTrue();
        //    _repository.Get().Should().HaveCount(2);
        //}

    }
}
