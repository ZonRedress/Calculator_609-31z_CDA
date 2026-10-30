using Logic;
using System.Globalization;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private readonly Calc _calc = new Calc();

        public MainPage()
        {
            InitializeComponent();
        }

        // Обработчик цифр (0-9)
        private void ButtonDigits_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            _calc.EnterDigit(button.Text);
            Display.Text = _calc.CurrentInput;
        }

        // Обработчик операций (+, -, *, /)
        private void ButtonOperators_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string op = button.Text;

            if (op == "×") op = "*";
            if (op == "÷") op = "/";

            _calc.SetOperation(op);
            Display.Text = _calc.CurrentInput;
        }

        // Обработчик равно (=)
        private void ButtonEqual_Clicked(object sender, EventArgs e)
        {
            Display.Text = _calc.Compute();
        }

        // Обработчик сброса (C)
        private void ButtonClear_Clicked(object sender, EventArgs e)
        {
            _calc.Clear();
            Display.Text = _calc.CurrentInput;
        }

        // Обработчик точки (.)
        private void ButtonDecimal_Clicked(object sender, EventArgs e)
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            _calc.EnterDecimal(separator);
            Display.Text = _calc.CurrentInput;
        }

        // Обработчик смены знака (+/-)
        private void ButtonToggleSign_Clicked(object sender, EventArgs e)
        {
            _calc.ToggleSign();
            Display.Text = _calc.CurrentInput;
        }
    }
}