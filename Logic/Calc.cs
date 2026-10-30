using System.Globalization;

namespace Logic
{
    public class Calc
    {
        public string CurrentInput { get; private set; } = "0";
        public bool IsError { get; private set; } = false;
        public double AccumulatedValue = 0;

        public string PendingOperation = null;
        public string LastOperation = null;
        public double LastOperand = 0;
        
        public bool IsResultDisplayed = false;

        public void EnterDigit(string digit)
        {
            if (IsError) return;

            if (IsResultDisplayed)
            {
                CurrentInput = digit;
                IsResultDisplayed = false;
                AccumulatedValue = 0;
                PendingOperation = null;
                LastOperation = null;
            }
            else if (CurrentInput == "0" || CurrentInput == "-0")
            {
                CurrentInput = digit;
            }
            else
            {
                CurrentInput += digit;
            }
        }

        public void EnterDecimal(string separator)
        {
            if (IsError) return;

            if (IsResultDisplayed)
            {
                CurrentInput = "0" + separator;
                IsResultDisplayed = false;
                AccumulatedValue = 0;
                PendingOperation = null;
                LastOperation = null;
            }
            else if (!CurrentInput.Contains(separator))
            {
                CurrentInput += separator;
            }
        }

        public void SetOperation(string op)
        {
            if (IsError) return;

            IsResultDisplayed = false;

            if (PendingOperation != null)
            {
                Compute();
            }

            if (!double.TryParse(CurrentInput, NumberStyles.Any, CultureInfo.CurrentCulture, out double currentNum))
                currentNum = 0;

            AccumulatedValue = currentNum;
            PendingOperation = op;
            CurrentInput = "0";
        }

        public string Compute()
        {
            if (IsError) return CurrentInput;

            if (!double.TryParse(CurrentInput, NumberStyles.Any, CultureInfo.CurrentCulture, out double currentNumber))
                currentNumber = 0;

            if (PendingOperation == null && LastOperation != null)
            {
                currentNumber = LastOperand;
                AccumulatedValue = Calculate(AccumulatedValue, currentNumber, LastOperation);
            }
            else if (PendingOperation != null)
            {
                LastOperation = PendingOperation;
                LastOperand = currentNumber;
                AccumulatedValue = Calculate(AccumulatedValue, currentNumber, PendingOperation);
                PendingOperation = null;
            }
            else
            {
                return CurrentInput;
            }

            if (double.IsInfinity(AccumulatedValue) || double.IsNaN(AccumulatedValue))
            {
                IsError = true;
                CurrentInput = "Ошибка";
                IsResultDisplayed = false;
                return CurrentInput;
            }

            CurrentInput = AccumulatedValue.ToString(CultureInfo.CurrentCulture);
            
            IsResultDisplayed = true; 
            
            return CurrentInput;
        }

        private double Calculate(double a, double b, string op)
        {
            return op switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => b == 0 ? double.NaN : a / b,
                _ => b
            };
        }

        public void Clear()
        {
            CurrentInput = "0";
            AccumulatedValue = 0;
            PendingOperation = null;
            LastOperation = null;
            LastOperand = 0;
            IsError = false;
            IsResultDisplayed = false;
        }

        public void ToggleSign()
        {
            if (IsError || CurrentInput == "0") return;
            
            if (CurrentInput.StartsWith("-"))
                CurrentInput = CurrentInput.Substring(1);
            else
                CurrentInput = "-" + CurrentInput;
        }
    }
}