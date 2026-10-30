using Logic;

namespace LogicTests
{
    [TestClass]
    public sealed class Test1
    {
        private Calc _calc;

        [TestInitialize]
        public void Setup()
        {
            _calc = new Calc();
        }

        [TestMethod]
        public void БазовыеОперации_Сложение()
        {
            _calc.EnterDigit("2"); _calc.SetOperation("+"); _calc.EnterDigit("3");
            Assert.AreEqual("5", _calc.Compute());
        }

        [TestMethod]
        public void БазовыеОперации_Вычитание()
        {
            _calc.EnterDigit("1"); _calc.EnterDigit("0"); _calc.SetOperation("-"); _calc.EnterDigit("4");
            Assert.AreEqual("6", _calc.Compute());
        }

        [TestMethod]
        public void БазовыеОперации_Умножение()
        {
            _calc.EnterDigit("6"); _calc.SetOperation("*"); _calc.EnterDigit("7");
            Assert.AreEqual("42", _calc.Compute());
        }

        [TestMethod]
        public void БазовыеОперации_Деление()
        {
            _calc.EnterDigit("1"); _calc.EnterDigit("0"); _calc.SetOperation("/"); _calc.EnterDigit("2");
            Assert.AreEqual("5", _calc.Compute());
        }

        [TestMethod]
        public void Последовательные_ПродолжениеСРезультатом()
        {
            _calc.EnterDigit("2"); _calc.SetOperation("+"); _calc.EnterDigit("3"); _calc.Compute();
            
            _calc.SetOperation("+"); _calc.EnterDigit("4");
            
            Assert.AreEqual("9", _calc.Compute());
        }

        [TestMethod]
        public void Последовательные_НовоеЧислоПослеРавно()
        {
            _calc.EnterDigit("2"); _calc.SetOperation("+"); _calc.EnterDigit("2"); _calc.Compute();
            
            _calc.EnterDigit("7"); _calc.SetOperation("+"); _calc.EnterDigit("3");
            
            Assert.AreEqual("10", _calc.Compute());
        }

        [TestMethod]
        public void Повторное_МногократноеНажатиеРавно()
        {
            _calc.EnterDigit("5"); _calc.SetOperation("+"); _calc.EnterDigit("3");
            
            Assert.AreEqual("8", _calc.Compute());
            Assert.AreEqual("11", _calc.Compute());
            Assert.AreEqual("14", _calc.Compute());
        }

        [TestMethod]
        public void Ошибки_ДелениеНаНоль()
        {
            _calc.EnterDigit("5"); _calc.SetOperation("/"); _calc.EnterDigit("0");
            var result = _calc.Compute();

            Assert.IsTrue(_calc.IsError);
            StringAssert.Contains(result, "Ошибка");
        }

        [TestMethod]
        public void Ошибки_ПовторноеНажатиеРавноПослеОшибки()
        {
            _calc.EnterDigit("5"); _calc.SetOperation("/"); _calc.EnterDigit("0"); _calc.Compute();
            
            var result = _calc.Compute();
            Assert.IsTrue(_calc.IsError);
        }

        [TestMethod]
        public void Сброс_ОчищаетВсеДанные()
        {
            _calc.EnterDigit("5"); _calc.SetOperation("/"); _calc.EnterDigit("0"); _calc.Compute();
            
            _calc.Clear();

            Assert.AreEqual("0", _calc.CurrentInput);
            Assert.IsFalse(_calc.IsError);
            
            _calc.EnterDigit("2"); _calc.SetOperation("+"); _calc.EnterDigit("2");
            Assert.AreEqual("4", _calc.Compute());
        }
    }
}