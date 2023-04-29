using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BeautySalon.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для WatermarkTextBox.xaml
    /// </summary>
    public partial class WatermarkTextBox : UserControl
    {
        public WatermarkTextBox()
        {
            InitializeComponent();
        }

        /// <summary> Свойство для текста подсказки </summary>
        public Brush DefaultTextBrush
        {
            get { return (Brush)GetValue(DefaultTextBrushProperty); }
            set { SetValue(DefaultTextBrushProperty, value);}
        }
        public static readonly DependencyProperty DefaultTextBrushProperty =
            DependencyProperty.Register("DefaultTextBrush",typeof(Brush), typeof(WatermarkTextBox), new PropertyMetadata(SystemColors.InactiveSelectionHighlightBrush));

        /// <summary> Свойство для текста подсказки</summary>
        public string WaterMarkText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }
        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register(nameof(WaterMarkText), typeof(string), typeof(WatermarkTextBox), new PropertyMetadata("Пусто"));

        /// <summary> Свойство для ввода текста </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(null));

        private void TBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text)) TBlock.Visibility = Visibility.Visible;
            else TBlock.Visibility = Visibility.Hidden;
        }
    }
}
