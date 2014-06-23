using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Data;
using System.Collections.ObjectModel;

namespace WpfApplication1
{
    /// <summary>
    /// RowValidationErrorTemplateWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class RowValidationErrorTemplateWindow : Window
    {
        public RowValidationErrorTemplateWindow()
        {
            InitializeComponent();
        }
    }

    /// <summary>バインディング ソース（コレクション）</summary>
    public class Courses : ObservableCollection<Course>
    {
        public Courses()
        {
            this.Add(new Course
            {
                Name = "Learning WPF",
                Id = 1001,
                StartDate = new DateTime(2010, 1, 11),
                EndDate = new DateTime(2010, 1, 22)
            });
            this.Add(new Course
            {
                Name = "Learning Silverlight",
                Id = 1002,
                StartDate = new DateTime(2010, 1, 25),
                EndDate = new DateTime(2010, 2, 5)
            });
            this.Add(new Course
            {
                Name = "Learning Expression Blend",
                Id = 1003,
                StartDate = new DateTime(2010, 2, 8),
                EndDate = new DateTime(2010, 2, 19)
            });
            this.Add(new Course
            {
                Name = "Learning LINQ",
                Id = 1004,
                StartDate = new DateTime(2010, 2, 22),
                EndDate = new DateTime(2010, 3, 5)
            });
        }
    }

    /// <summary>バインディング ソース</summary>
    public class Course
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
            }
        }

        private int _number;
        public int Id
        {
            get
            {
                return _number;
            }
            set
            {
                if (_number == value) return;
                _number = value;
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (_startDate == value) return;
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (_endDate == value) return;
                _endDate = value;
            }
        }
    }

    /// <summary>カスタム バリデーション</summary>
    /// <remarks>行毎のバリデーション</remarks>
    public class CourseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            // 行単位のチェック（カレント行を取得）
            Course course = (value as BindingGroup).Items[0] as Course;

            if (course.StartDate > course.EndDate)
            {
                return new ValidationResult
                    (false, "Start Date must be earlier than End Date.");
            }
            else
            {
                // return new ValidationResult(true, null);
                return ValidationResult.ValidResult;
            }
        }
    }
}
