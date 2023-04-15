using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using ClassLibSpline;
using static SplineWPFLab.ViewData;

namespace SplineWPFLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewData viewData { get; set; }
        public List<String> RawDataList { get; }
        public string IntegralValue { get => viewData.spline.Integral.ToString() ?? ""; }
        public List<String> SplineDataList { get; }

        public static RoutedCommand ExecuteFromControlsCommand = new RoutedCommand("ExecuteFromControls", typeof(MainWindow));
        public static RoutedCommand ExecuteFromFileCommand = new RoutedCommand("ExecuteFromControls", typeof(MainWindow));
        public BindingExpression exp;
        public MainWindow()
        {

            this.DataContext = this;
            SplineDataList = new();
            viewData = new();

            InitializeComponent();

            this.CommandBindings.Add(new CommandBinding(ExecuteFromControlsCommand, execute_controls_by_Click, can_execute_from_CC_Handler));
            this.CommandBindings.Add(new CommandBinding(ExecuteFromFileCommand, execute_from_File_Handler, can_execute_from_File_C_Handler));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, Save_Click, can_save_C_Handler));
        }
        private void execute_controls_by_Click(object sender, RoutedEventArgs ex)
        {

        }
        private void can_execute_from_CC_Handler(object sender, CanExecuteRoutedEventArgs ex)
        {

        }
        private void can_execute_from_File_C_Handler(object sender, CanExecuteRoutedEventArgs ex)
        {

        }
        private void execute_file_click(object sender, RoutedEventArgs ex)
        {

        }
        private void execute_from_File_Handler(object sender, RoutedEventArgs ex)
        {

        }
        private void SetBindings()
        {

        }
        private void Save_Click(object sender, RoutedEventArgs ex)
        {

        }
        private void can_save_C_Handler(object sender, CanExecuteRoutedEventArgs ex)
        {

        }
        private void open_n_exec_click(object sender, RoutedEventArgs ex)
        {

        }
        private void INT_Borders_value_Init(object sender, EventArgs ex)
        {

        }
        private void Number_of_Nodes_value_Init(object sender, EventArgs ex)
        {

        }
        private void Number_of_SplineNodes_value_Init(object sender, EventArgs ex)
        {

        }
        private void rbUni_Init(object sender, EventArgs ex)
        {

        }
        private void cbFunc_Init(object sender, EventArgs ex)
        {

        }
        private void leftDer_value_Init(object sender, EventArgs ex)
        {
            var binding = new Binding("ViewData.LeftDer");
            binding.Mode = BindingMode.OneWayToSource;
            binding.Converter = new RegexConverter(@"[0-9]+(\.[0-9]+)?", 1);
            leftDer_val.SetBinding(TextBox.TextProperty, binding);
        }
        private void rightDer_value_Init(object sender, EventArgs ex)
        {
            var binding = new Binding("ViewData.RightDer");
            binding.Mode = BindingMode.OneWayToSource;
            binding.Converter = new RegexConverter(@"[0-9]+(\.[0-9]+)?", 1);
            rightDer_val.SetBinding(TextBox.TextProperty, binding);
        }
        private void execute_controls_Init(object sender, EventArgs ex)
        {
            execControls.Command = ExecuteFromControlsCommand;
        }

        private void Menu_Execution_controls_Init(object sender, EventArgs ex)
        {
            MenuExecFromControls.Command = ExecuteFromControlsCommand;
        }

        private void execute_file_Init(object sender, EventArgs ex)
        {
            execFile.Command = ExecuteFromFileCommand;
        }

        private void Menu_Execution_File_Init(object sender, EventArgs ex)
        {
            MenuExecFromFile.Command = ExecuteFromFileCommand;
        }

        private void save_Init(object sender, EventArgs ex)
        {
            save.Command = ApplicationCommands.Save;
        }

        private void Menu_save_Init(object sender, EventArgs ex)
        {
            MenuSave.Command = ApplicationCommands.Save;
        }
    }
}
