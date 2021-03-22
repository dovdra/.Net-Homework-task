using System;
using System.Collections.Generic;
using System.IO;
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

namespace Task
{
    /// <summary>
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        string wanted_path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        public TaskPage()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string numbersLine = NumbersInput.Text;
            var numbers = numbersLine.Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
            //sort numbers
            bubbleSort(numbers);

            //write sorted numbers to file
            string fileName = "sortedNumbers_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt") + ".txt";
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(wanted_path, fileName)))
            {
                 outputFile.WriteLine(string.Join(", ", numbers));
            }
        }

        private void button_Click2(object sender, RoutedEventArgs e)
        {
            //get directory name
            var directory = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
            //order files by created date
            var myFile = directory.GetFiles()
             .OrderByDescending(f => f.LastWriteTime)
             .First();
            //read from last saved file
            string contents = File.ReadAllText(myFile.FullName);
            //show last saved file content
            MessageBox.Show(contents);
        }
        //bubble sort algorithm
        void bubbleSort(List<int> numbers)
        {
            int temp;
            for(int i = 0; i <= numbers.Count - 2; i++)
            {
                for(int j = 0; j <= numbers.Count - 2; j++)
                {
                    if(numbers[j] > numbers[j + 1])
                    {
                        temp = numbers[j + 1];
                        numbers[j + 1] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
        }
    }
     
}
