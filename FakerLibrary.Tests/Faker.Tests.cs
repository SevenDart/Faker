using System;
using NUnit.Framework;

namespace FakerLibrary.Tests
{
    public class Tests
    {
        private IFaker _faker;
        
        [SetUp]
        public void Setup()
        {
            _faker = new Faker();
        }

        [Test]
        public void CreateInt()
        {
            //Arrange
            Type targetType = typeof(int);
            
            //Act
            var testValue = _faker.Create<int>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
        }

        
        
        [Test]
        public void CreateEmptyClass()
        {
            //Arrange
            Type targetType = typeof(EmptyClass);
            
            //Act
            var testValue = _faker.Create<EmptyClass>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
        }
        
        [Test]
        public void CreateSimpleFieldsClass()
        {
            //Arrange
            Type targetType = typeof(SimpleFieldsClass);
            
            //Act
            var testValue = _faker.Create<SimpleFieldsClass>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
            Assert.NotNull(testValue.X);
            Assert.NotNull(testValue.Y);
            Assert.NotNull(testValue.Z);
        }
        
        [Test]
        public void CreateTwoConstructorsClass()
        {
            //Arrange
            Type targetType = typeof(TwoConstructorsClass);
            
            //Act
            var testValue = _faker.Create<TwoConstructorsClass>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
            Assert.NotNull(testValue.X);
            Assert.NotNull(testValue.Y);
            Assert.NotNull(testValue.Z);
        }
        
        [Test]
        public void CreateClassWithPrivateConstructor()
        {
            //Arrange
            Type targetType = typeof(PrivateConstructorClass);
            
            //Act
            var testValue = _faker.Create<PrivateConstructorClass>();
            
            //Assert
            Assert.Null(testValue);
        }
        
        [Test]
        public void CreateClassWithoutConstructor()
        {
            //Arrange
            Type targetType = typeof(WithoutConstructorClass);
            
            //Act
            var testValue = _faker.Create<WithoutConstructorClass>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
            Assert.NotNull(testValue.X);
            Assert.NotNull(testValue.Y);
            Assert.NotNull(testValue.Z);
        }
        
        [Test]
        public void CreatePropertiesClass()
        {
            //Arrange
            Type targetType = typeof(PropertiesClass);
            
            //Act
            var testValue = _faker.Create<PropertiesClass>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
            Assert.NotNull(testValue.X);
            Assert.NotNull(testValue.Y);
            Assert.Null(testValue.Z);
        }
        
        [Test]
        public void CreateClassWithInnerClass()
        {
            //Arrange
            Type targetType = typeof(ClassWithInnerClass);
            
            //Act
            var testValue = _faker.Create<ClassWithInnerClass>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
            Assert.NotNull(testValue.X);
            Assert.NotNull(testValue.Y);
            Assert.NotNull(testValue.Z);
        }
        
        [Test]
        public void CreateClassWithExceptionConstructor()
        {
            //Arrange
            Type targetType = typeof(ClassWithExceptionConstructor);
            
            //Act
            var testValue = _faker.Create<ClassWithExceptionConstructor>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
            Assert.NotNull(testValue.X);
            Assert.Null(testValue.Y);
        }
        
        [Test]
        public void CreateRecursiveFieldClass()
        {
            //Arrange
            Type targetType = typeof(RecursiveFieldClass);
            
            //Act
            var testValue = _faker.Create<RecursiveFieldClass>();
            
            //Assert
            Assert.IsInstanceOf(targetType, testValue);
        }
    }
}