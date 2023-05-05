using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BeautySalon.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PlusFigure.xaml
    /// </summary>
    public partial class PlusFigure : UserControl
    {
        public PlusFigure()
        {
            InitializeComponent();
        }

        public Brush FillColor
        {
            get { return (Brush)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Brush), typeof(WatermarkTextBox), new PropertyMetadata(SystemColors.InactiveSelectionHighlightBrush));
    }
}
