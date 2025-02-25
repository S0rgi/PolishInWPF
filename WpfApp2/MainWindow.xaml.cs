using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Proj.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private HashSet<TextBlock> dict = new();
    private bool _isInitialized = false;
    public MainWindow()
    {
        InitializeComponent();
        _isInitialized = true;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        List<string> errors = new List<string>();
        
        // Если есть ошибки, показываем их и прерываем выполнение
        foreach ( var str in dict )
            if ( str.Text != string.Empty )
                errors.Add(str.Text);
        if ( errors.Count > 0 )
        {
            MessageBox.Show(string.Join("\n", errors), "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        double startX = double.Parse(StartX_Input_box.Text);
        double endX = double.Parse(EndX_Input_box.Text);
        double startY = double.Parse(StartY_Input_box.Text);
        double endY = double.Parse(EndY_Input_box.Text);
        string functionName = InfixExpression_Input_Box.Text;
        // Создаём объект с данными
        var graphData = new GraphData
        {
            StartX = startX,
            EndX = endX,
            StartY = startY,
            EndY = endY,
            FunctionName = functionName
        };

        MessageBox.Show($"StartX: {graphData.StartX}, EndX: {graphData.EndX}\n" +
                        $"StartY: {graphData.StartY}, EndY: {graphData.EndY}\n" +
                        $"Function: {graphData.FunctionName}",
                        "Данные графика", MessageBoxButton.OK, MessageBoxImage.Information);

    }
    #region
    private void InfixExpression_Input_Box_TextChanged(object sender, TextChangedEventArgs e)
    {
        if ( !_isInitialized )
            return;
        string functionName = InfixExpression_Input_Box.Text;
        string functionPattern = @"^\s*((?:ln|sin|cos|tan|ctg|sqrt)\(\s*[^()]+\s*\)|\d+(?:\.\d+)?|[x-z])(?:\s*[*/+-]\s*((?:ln|sin|cos|tan|ctg|sqrt)\(\s*[^()]+\s*\)|\d+(?:\.\d+)?|[x-z]))*\s*$";

        if ( !Regex.IsMatch(functionName, functionPattern) )
            Expression_Error_Block.Text = "FunctionName: Некорректный формат математического выражения.";
        else
            Expression_Error_Block.Text = string.Empty;
        dict.Add(Expression_Error_Block);
    }

    private void StartX_Input_box_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateInput(StartX_Input_box, StartX_Error_Block, EndX_Input_box, EndX_Error_Block, "EndX должно быть больше чем StartX");
    }

    private void EndX_Input_box_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateInput(EndX_Input_box, EndX_Error_Block, StartX_Input_box, StartX_Error_Block, "EndX должно быть больше чем StartX");
    }

    private void StartY_Input_box_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateInput(StartY_Input_box, StartY_Error_Block, EndY_Input_box, EndY_Error_Block, "EndY должно быть больше чем StartY");
    }

    private void EndY_Input_box_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateInput(EndY_Input_box, EndY_Error_Block, StartY_Input_box, StartY_Error_Block, "EndY должно быть больше чем StartY");
    }
    #endregion
    private void ValidateInput(TextBox inputBox, TextBlock errorBlock, TextBox otherInputBox, TextBlock otherErrorBlock, string errorMessage)
    {
        if ( !_isInitialized )
            return;

        if ( !double.TryParse(inputBox.Text, out double value) )
        {
            errorBlock.Text = "Введите double";
        }
        else
        {
            errorBlock.Text = string.Empty;

            if ( double.TryParse(otherInputBox.Text, out double otherValue) && otherValue <= value )
            {
                errorBlock.Text = errorMessage;
                otherErrorBlock.Text = errorMessage;
            }
            else if ( otherErrorBlock.Text == errorMessage )
            {
                otherErrorBlock.Text = string.Empty;
            }
        }
        dict.Add(errorBlock);
        dict.Add(otherErrorBlock);
    }
}

