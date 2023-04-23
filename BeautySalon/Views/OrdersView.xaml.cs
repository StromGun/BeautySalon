using System.Windows.Controls;

namespace BeautySalon.Views
{
    /// <summary>
    /// Логика взаимодействия для OrdersView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();

            scheduleControl.IntervalHeight = 25.0;
            scheduleControl.Interval = new(0, 15, 0);
            scheduleControl.TimeStart = new(8, 0, 0);
            scheduleControl.TimeEnd = new(16, 0, 0);
            scheduleControl.Redraw();
        }
    }
}
