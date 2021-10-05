namespace FakerLibrary.Tests
{
    class EmptyClass
    {
        
    }

    class SimpleFieldsClass
    {
        public int? X;
        public bool? Y;
        public double? Z;

        public SimpleFieldsClass(int x, bool y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    class TwoConstructorsClass
    {
        public int? X;
        public bool? Y;
        public double? Z;

        public TwoConstructorsClass(int x, bool y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public TwoConstructorsClass(int x, bool y)
        {
            X = x;
            Y = y;
        }
    }

    class PrivateConstructorClass
    {
        public int? X;

        private PrivateConstructorClass(int x)
        {
            X = x;
        }
    }

    class WithoutConstructorClass
    {
        public int? X;
        public bool? Y;
        public double? Z;
    }

    class ClassWithInnerClass
    {
        public int? X;
        public bool? Y;
        public SimpleFieldsClass Z;

        public ClassWithInnerClass(int x, bool y, SimpleFieldsClass z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}