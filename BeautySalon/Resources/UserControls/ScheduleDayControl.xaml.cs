using System;
using System.Collections;
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

namespace BeautySalon.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ScheduleDayControl.xaml
    /// </summary>
    public partial class ScheduleDayControl : UserControl
    {
        private DateTime currentDate;
        public DateTime CurrentDate { get => currentDate;
            set
            {
                currentDate = value.Date;
                uiTitle.Content = string.Format("{0} {1:00}.{2}.{3}", CurrentDate.DayOfWeek.ToString(), CurrentDate.Day, CurrentDate.Month, CurrentDate.Year);
                ChangeDate();
            }
        }

        public TimeSpan Interval { get; set; }
        // что жэто?
        public bool FitToSize { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }


        private static double TimeVisualIntervalHeight = 50.0;      // minimum height between displaying time interval. Минимальная высота между временными интервалами 

        private int timeDisplayInterval;
        private double intervalHeight;
        public double IntervalHeight
        {
            get => intervalHeight;
            set
            {
                intervalHeight = value;
                if (TimeVisualIntervalHeight % value == 0) {
                    timeDisplayInterval = (int)(TimeVisualIntervalHeight / value);
                }
                else {
                    timeDisplayInterval = (int)(TimeVisualIntervalHeight / value) + 1;
                }
            }
        }


        public List<ScheduleItem>? Items { get => (List<ScheduleItem>)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

        public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("Items", typeof(List<ScheduleItem>),
            typeof(ScheduleDayControl), new PropertyMetadata(null));

        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public event EventHandler? ScheduleItemClick;

        public ScheduleDayControl()
        {
            InitializeComponent();
            //Items = new();

            // assign event handlers.  Назначение события
            uiCanvas.MouseLeftButtonDown += OnScheduleItemClick;

            // defaults values
            Interval = new(0, 30, 0);
            TimeStart = new(0, 0, 0);
            TimeEnd = new(1, 0, 0, 0);
            CurrentDate = DateTime.Today.Date;
            FitToSize = false;
            IntervalHeight = 35.0;

            Redraw();
        }

        public void Redraw()
        {
            uiGrid.Children.Clear();
            uiGrid.RowDefinitions.Clear();
            uiCanvas.Children.Clear();

            UpdateLayout();

            double canvasHeight = 0.0;

            int rows = 0;
            // calculate number of intervals in day and height of drawing canvas. Расчет кол-ва интервалос в дне и высоту холста
            for (TimeSpan c = TimeEnd; c > TimeStart; c -= Interval)
            {
                rows++;
                canvasHeight += IntervalHeight;
            }

            uiGrid.Children.Add(uiGridTimeline);
            uiGridTimeline.SetValue(Grid.RowSpanProperty, rows);
            uiCanvas.SetValue(Grid.RowSpanProperty, rows);

            //recalculate intervals and override canvas height if FitToSize is enabled
            if (FitToSize)
            {
                canvasHeight = uiScroll.ActualHeight;
                IntervalHeight = canvasHeight / rows;
            }

            //create grid rows and draw time intervals.  Создание строк и временных интервалов
            TimeSpan time = TimeStart;
            for (int i = 0; i < rows; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new(IntervalHeight);
                uiGrid.RowDefinitions.Add(row);

                Border border = new();
                border.BorderBrush = uiGridTimeline.BorderBrush;
                border.BorderThickness = new Thickness(0, 0, 0, 1);
                border.SetValue(Grid.ColumnSpanProperty, 2);
                border.SetValue(Grid.RowProperty, i);
                uiGrid.Children.Add(border);

                if (i == 0 || i % timeDisplayInterval == 0)
                {
                    Label label = new();
                    label.Content = string.Format("{0:00}:{1:00}", time.Hours, time.Minutes);
                    label.SetValue(Grid.ColumnProperty, 0);
                    label.SetValue(Grid.RowProperty, i);
                    label.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
                    label.SetValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                    uiGrid.Children.Add(label);
                }

                time = time.Add(Interval);
            }

            // draw items
            Items?.Sort(ScheduleItem.SortByStart);
            double totalSeconds = TimeEnd.TotalSeconds - TimeStart.TotalSeconds;

            foreach(ScheduleItem item in Items)
            {
                item.GeneratePanel(uiCanvas.ActualWidth, uiCanvas.ActualHeight, TimeStart.TotalSeconds, TimeEnd.TotalSeconds, totalSeconds);
            }

            int startItem = Items.FindIndex(x => x.Start >= CurrentDate.Add(TimeStart));
            if (startItem > -1)
            {
                for ( int i =startItem; i < Items.Count && Items[i].Start < CurrentDate.Add(TimeEnd); i++)
                {
                    uiCanvas.Children.Add(Items[i].Panel);
                }
            }
        }


        public void Add(ScheduleItem item)
        {
            double totalSeconds = TimeEnd.TotalSeconds - TimeStart.TotalSeconds;
            item.GeneratePanel(uiCanvas.ActualWidth, uiCanvas.ActualHeight, TimeStart.TotalSeconds, TimeEnd.TotalSeconds, totalSeconds);
            Items.Add(item);
            if (item.Start >= CurrentDate.Add(TimeStart) && item.Start < CurrentDate.Add(TimeEnd))
            {
                uiCanvas.Children.Add(item.Panel);
            }
        }

        public void Remove(ScheduleItem item)
        {
            Items?.Remove(item);
        }

        public void Remove(Guid id)
        {
            Items?.RemoveAll(x => x.ID == id);
        }

        public void Clear()
        {
            uiCanvas.Children.Clear();
            Items?.Clear();
        }

        public IReadOnlyCollection<ScheduleItem> GetItems()
        {
            return Items.AsReadOnly();
        }


        private void OnScheduleItemClick(object sender, MouseButtonEventArgs e)
        {
            int i = Items.FindIndex(x => x.Start >= CurrentDate.Add(TimeStart));
            for (; i< Items.Count && !Items[i].Panel.IsMouseOver; i++) { }
            if (i < Items.Count && Items[i].Clickable)
                ScheduleItemClick?.Invoke(Items[i], EventArgs.Empty);
            else 
                ScheduleItemClick?.Invoke(null, EventArgs.Empty);
        }

        private void ChangeDate()
        {
            uiCanvas.Children.Clear();

            int startItem = Items.FindIndex(x => x.Start >= CurrentDate.Add(TimeStart));
            if (startItem > -1)
            {
                for (int i = startItem; i < Items.Count && Items[i].Start < CurrentDate.Add(TimeEnd); i++)
                {
                    uiCanvas.Children.Add(Items[i].Panel);
                }
            }
        }

    }


    public class ScheduleItem
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        internal string Time { get => string.Format("{0} - {1}", Start.ToShortTimeString(), End.ToShortTimeString()); }
        internal Border? Panel;

        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Clickable { get; set; }
        public Brush? BorderColor { get; set; }
        public Brush? FillColor { get; set; }

        public static Brush DefaultBorderBrush = Brushes.Black;
        public static Brush DefaultFillColor = Brushes.LightGreen;

        public readonly Guid ID;
        public object? Data;  // can hold a reference to an object if needed.   Может хранить ссылку на объект если требуется

        public ScheduleItem()
        {
            ID = Guid.NewGuid();

            //defaults
            BorderColor = DefaultBorderBrush;
            FillColor = DefaultFillColor;
            Clickable = true;
        }

        internal void GeneratePanel(double width, double height, double secondsStart, double secondsEnds, double secondsTotal)
        {
            Panel = new Border
            {
                BorderThickness = new Thickness(1.0),
                BorderBrush = BorderColor,
                Background = FillColor
            };

            double yPos = (Start.TimeOfDay.TotalSeconds - secondsStart) / secondsTotal * height;

            Panel.Width = width > 16 ? width : 0.0;
            Panel.Height = End < Start ? 0.0 : (End.TimeOfDay.TotalSeconds - secondsStart) / secondsTotal * height - yPos;

            Canvas.SetLeft(Panel, 8);
            Canvas.SetTop(Panel, yPos);

            StackPanel panel = new()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            Label apptTime = new()
            {
                Margin = new Thickness(1.0, 0.0, 0.0, 0.0),
                FontSize = 11,
                Padding = new Thickness(0.0),
                Foreground = Brushes.Black
            };

            apptTime.Content = Time;

            Label apptDesc = new()
            {
                Margin = new Thickness(1.0, 0.0, 0.0, 0.0),
                FontSize = 11,
                Padding = new Thickness(0.0),
                Foreground = Brushes.Black
            };

            apptDesc.Content = Title;

            panel.Children.Add(apptTime);
            panel.Children.Add(apptDesc);
            Panel.Child = panel;
        }

        internal static int SortByStart(ScheduleItem x, ScheduleItem y)
        {
            if (x != null)
            {
                if (y == null)
                {
                    return DateTime.Compare(x.Start, y.Start);
                }
                else return -1; // y is null, put x first
            }
            else
            {
                if (y != null)
                {
                    return 1; // x is null, put y first
                }
                else return 0; // they're both null
            }

        }
    }
}
