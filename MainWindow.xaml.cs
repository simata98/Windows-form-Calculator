using System;
using System.Windows;
using System.Windows.Controls;


namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double savedValue; // txtResult에 있는 값 저장
        private double memory = 0; // 메모리에 저장된 값
        private char op = '\0'; // 현재 계산할 op
        private bool opFlag = false; // 연산자를 누른 후인지 체크하는 flag
        private bool newButton; // 메모리 버튼을 누른 후인지 체크
        private bool percentFlag; // % 처리를 위한 flag

        public MainWindow()
        {
            InitializeComponent();
            // 메모리를 설정하기 전에는 일부 버튼이 활성화 되지 않게 설정
            btnMC.IsEnabled = false;
            btnMR.IsEnabled = false;
            btnMs.IsEnabled = true;
            btnMPlus.IsEnabled = true;
            btnMMinus.IsEnabled = true;
        }

        private void btn_Click(object sender, RoutedEventArgs e) // 숫자 버튼을 클릭했을 때 행동 설정
        {
            Button btn = sender as Button;
            string number = btn.Content.ToString();

            if(txtResult.Text == "0" || newButton)
            {
                txtResult.Text = number;
                opFlag = false;
                newButton = false;
            }
            else
            {
                txtResult.Text += number;
            }

            txtResult.Text = Comma(txtResult.Text); // 3자리마다 콤마가 찍히는 기능
        }


        private void btnOp_Click(object sender, RoutedEventArgs e) // 연산자 버튼을 클릭했을 때 행동 설정
        {
            Button btn = sender as Button;

            savedValue = double.Parse(txtResult.Text);
            txtExp.Text += txtResult.Text + " " + btn.Content.ToString() + " "; // 위에 연산 진행 중임을 띄우는 기능 설정
            op = btn.Content.ToString()[0];
            opFlag = true;
            percentFlag = true;
            newButton = true;
        }

        private void Dot_Click(object sender, RoutedEventArgs e) // 소수점 기능 설정
        {
            if (txtResult.Text.Contains("."))
                return;
            else
                txtResult.Text += ".";
        }

        private string Comma(string number) // 3자리 단위로 콤마 생기게 하는 기능 설정
        {
            int pos = 0;
            double v = double.Parse(number);

            if (number.Contains("."))
            {
                pos = number.Length - number.IndexOf('.');
                if (pos == 1)
                    return number;
                string formatStr = "{0:N" + (pos - 1) + "}";
                number = string.Format(formatStr, v);
            }
            else
            {
                number = string.Format("{0:N0}", v);
            }
            return number;
        }

        private void Equal_Click(object saender, RoutedEventArgs e) // 등호 클릭시 기능 설정
        {
            switch (op) {
                case '+':
                    txtResult.Text = (savedValue + double.Parse(txtResult.Text)).ToString();
                    break;
                case '-':
                    txtResult.Text = (savedValue - double.Parse(txtResult.Text)).ToString();
                    break;
                case 'x':
                    txtResult.Text = (savedValue * double.Parse(txtResult.Text)).ToString();
                    break;
                case '/':
                    txtResult.Text = (savedValue / double.Parse(txtResult.Text)).ToString();
                    break;
            }
            txtResult.Text = Comma(txtResult.Text);
            txtExp.Text = "";
            btnMs.IsEnabled = true;
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e) // 루트 기능
        {
            txtExp.Text = "√(" + txtResult.Text + ") ";
            txtResult.Text = Math.Sqrt(double.Parse(txtResult.Text)).ToString();
            txtResult.Text = Comma(txtResult.Text);
        }

        private void btnSqr_Click(object sender, RoutedEventArgs e) // 제곱 기능
        {
            txtExp.Text = "sqr(" + txtResult.Text + ") ";
            txtResult.Text = Math.Pow(double.Parse(txtResult.Text), 2).ToString();
            txtResult.Text = Comma(txtResult.Text);
        }

        private void btnRecip_Click(object sender, RoutedEventArgs e) // 1/N 기능
        {
            txtExp.Text = "1 / (" + txtResult.Text + ") ";
            txtResult.Text = (1 / double.Parse(txtResult.Text)).ToString();
            txtResult.Text = Comma(txtResult.Text);
        }

        private void btnPlusMinus_Click(object sender, RoutedEventArgs e) // +- 기능
        {
            double v = double.Parse(txtResult.Text);
            txtResult.Text = (-v).ToString();
            txtResult.Text = Comma(txtResult.Text);
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e) // 퍼센트 기능
        {
            if(percentFlag)
            {
                double p = double.Parse(txtResult.Text);
                p = savedValue * p / 100.0;
                txtResult.Text = p.ToString();
                txtExp.Text += txtResult.Text;
                percentFlag = false;
            }
        }
        private void btnMs_Click(object sebder, RoutedEventArgs e) // 메모리에 담는 기능
        {
            memory = double.Parse(txtResult.Text);
            btnMC.IsEnabled = true;
            btnMR.IsEnabled = true;
            btnMPlus.IsEnabled = true;
            btnMMinus.IsEnabled = true;
        }

        private void btnMR_Click(object sender, RoutedEventArgs e) // 메모리의 값 출력기능
        {
            txtResult.Text = memory.ToString();
            newButton = true;
            txtResult.Text = Comma(txtResult.Text);
        }

        private void btnMC_Click(object sender, RoutedEventArgs e) // 메모리 값 초기화 기능
        {
            txtResult.Text = "0";
            memory = 0;
            btnMR.IsEnabled = false;
            btnMC.IsEnabled = false;
            btnMMinus.IsEnabled = false;
            btnMPlus.IsEnabled = false;
        }

        private void btnMPlus_Click(object sender, RoutedEventArgs e) // M+ 기능
        {
            btnMR.IsEnabled = true;
            btnMC.IsEnabled = true;
            btnMMinus.IsEnabled = true;
            btnMPlus.IsEnabled = true;
            memory += double.Parse(txtResult.Text);
        }

        private void btnMMinus_Click(object sender, RoutedEventArgs e) // M- 기능
        {
            btnMR.IsEnabled = true;
            btnMC.IsEnabled = true;
            btnMMinus.IsEnabled = true;
            btnMPlus.IsEnabled = true;
            memory -= double.Parse(txtResult.Text);
        }

        private void btnCE_Click(object sender, RoutedEventArgs e) // CE기능
        {
            txtResult.Text = "0";
        }

        private void btnC_Click(object sender, RoutedEventArgs e) // C 기능
        {
            txtResult.Text = "0";
            txtExp.Text = "";
            savedValue = 0;
            op = '\0';
            opFlag = false;
            percentFlag = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) // 한글자씩 지우는 기능
        {
            txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);
            if (txtResult.Text.Length == 0)
                txtResult.Text = "0";
        }
    }
}
