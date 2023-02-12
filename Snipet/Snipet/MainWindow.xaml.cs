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

namespace Snipet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch(cbSnipetType.SelectedIndex)
            {
                // Delegate Command
                case 1:
                    {
                        CreateDelegateCommand();
                       System.Windows.Clipboard.SetText(txtCode.Text);
                    }
                    break;
                    // Property
                case 0:
                default:
                    {
                        CreateProperty();
                        System.Windows.Clipboard.SetText(txtCode.Text);
                    }
                    break;
            }
            //editText.Text = "";
        }

        void CreateDelegateCommand()
        {
            string base_name = txtBaseName.Text;
            string base_alpha = "_sampleProperty";
            string base_beta = "SampleProperty";
            string type_str = txtType.Text;
            if (string.IsNullOrWhiteSpace(type_str))
                type_str = "string";
            if (!string.IsNullOrWhiteSpace(base_name))
            {
                string[] words = base_name.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        base_alpha = "_" + words[i][0].ToString().ToLower() + words[i].Substring(1);
                        base_beta = words[i][0].ToString().ToUpper() + words[i].Substring(1);
                    }
                    else
                    {
                        base_alpha += words[i][0].ToString().ToUpper() + words[i].Substring(1);
                        base_beta += words[i][0].ToString().ToUpper() + words[i].Substring(1);
                    }
                }
            }
            string base_delta = "";
            base_delta = base_beta + "Func";
            if (!base_alpha.Contains("Cmd"))
            {
                base_alpha += "Cmd";
                base_beta += "Cmd";
            }
            string oko = @"
        private DelegateCommand? " + base_alpha + @"=null;
        public DelegateCommand " + base_beta + @" =>
            " + base_alpha + @" ??= new DelegateCommand(" + base_delta + @");
        void " + base_delta + @"()
        {
            // throw new NotImplementException();
        }
";
            txtCode.Text = oko;
        }
        void CreateProperty()
        {
            string base_name = txtBaseName.Text;
            string base_alpha = "_sampleProperty";
            string base_beta = "SampleProperty";
            string type_str = txtType.Text;
            if (string.IsNullOrWhiteSpace(type_str))
                type_str = "string";
            if (!string.IsNullOrWhiteSpace(base_name))
            {
                string[] words = base_name.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        base_alpha = "_" + words[i][0].ToString().ToLower() + words[i].Substring(1);
                        base_beta = words[i][0].ToString().ToUpper() + words[i].Substring(1);
                    }
                    else
                    {
                        base_alpha += words[i][0].ToString().ToUpper() + words[i].Substring(1);
                        base_beta += words[i][0].ToString().ToUpper() + words[i].Substring(1);
                    }
                }
            }

            string oko = "";
            
            if (type_str == "string")
            {

                oko = @"
        private " + type_str + "? " + base_alpha + @";
        public " + type_str + " " + base_beta + @"
        {
            get => " + base_alpha + @"??="""";
            set => SetProperty(ref " + base_alpha + @",value);
        }
";
            }
            else
            {

                oko = @"
        private " + type_str + "? " + base_alpha + @";
        public " + type_str + " " + base_beta + @"
        {
            get => return " + base_alpha + @";
            set => SetProperty(ref " + base_alpha + @",value);
        }
";
            }

            txtCode.Text = oko;
        }


        void CreatePropertyV1()
        {
            string base_name = txtBaseName.Text;
            string base_alpha = "_sampleProperty";
            string base_beta = "SampleProperty";
            string type_str = txtType.Text;
            if (string.IsNullOrWhiteSpace(type_str))
                type_str = "string";
            if (!string.IsNullOrWhiteSpace(base_name))
            {
                string[] words = base_name.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        base_alpha = "_" + words[i][0].ToString().ToLower() + words[i].Substring(1);
                        base_beta = words[i][0].ToString().ToUpper() + words[i].Substring(1);
                    }
                    else
                    {
                        base_alpha += words[i][0].ToString().ToUpper() + words[i].Substring(1);
                        base_beta += words[i][0].ToString().ToUpper() + words[i].Substring(1);
                    }
                }
            }
            string oko = @"
        private " + type_str + " " + base_alpha + @";
        public " + type_str + " " + base_beta + @"
        {
            get 
            { 
                return " + base_alpha + @";
            }
            set
            {
                SetProperty(ref " + base_alpha + @",value);
            }
        }
";
            txtCode.Text = oko;
        }
    }
}
