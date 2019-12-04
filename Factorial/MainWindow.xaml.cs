using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Factorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Numbers { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Numbers = new List<string>();
        }

        private void calculateFactorial_click(object sender, RoutedEventArgs e)
        {
            double[] numbers = Regex.Matches(textBox_Numbers.Text, @"(\d+(?:\.\d+)?)")
                .OfType<Match>()
                .Select(m => double.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture))
                .ToArray();
            if(numbers.Count() < 10 || numbers.Count() > 10)
            {
                MessageBox.Show("Вы ввели неверное количество чисел");
            }
            else
            {
                for (int i = 0; i < numbers.Count(); i++)
                {
                    Numbers.Add(numbers[i].ToString());
                }
                label_factorialResult.Content = "Сумма факториалов чисел = " + ProcessFactorial(Numbers);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }           
        }

        static string ProcessFactorial(List<string> args)
        {
            var context = new FactorialAssemblyLoadContext();
            var assemblyPath = @"C:\Users\home\source\repos\FactorialCalculate\FactorialCalculate\bin\Debug\netcoreapp3.0\FactorialCalculate.dll";
            Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
            var type = assembly.GetType("FactorialCalculate.Program");
            var greetMethod = type.GetMethod("Factorial");
            var instance = Activator.CreateInstance(type);
            var result = ((string)greetMethod.Invoke(instance, new object[] { args })).ToString();
            return result;
        }
    }
}





            
        

        