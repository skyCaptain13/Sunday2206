using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace ConsoleApplication1
{
    class Program
    {

        public class Employee : INotifyPropertyChanged
        {
            private int _ID;
            private string _name;
            private double _salary;

            public int ID { get; set; }
            public string Name { get; set; }
            public Double Salary
            {
                get { return _salary; }
                set
                {
                    if (_salary != value)
                    {
                        _salary = value;
                        OnSalaryChanged("Salary");
                    }

                }
            }

            protected void OnSalaryChanged(string propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }

        static void Main(string[] args)
        {
            Employee emp1 = new Employee() { ID = 1, Name = "Mehdi", Salary = 1200 };
            Employee emp2 = new Employee() { ID = 2, Name = "Elena", Salary = 1500 };
            Employee emp3 = new Employee() { ID = 3, Name = "Hola", Salary = 1800 };

            emp1.PropertyChanged += new PropertyChangedEventHandler(emp1_salaryChanged);
            emp2.PropertyChanged += new PropertyChangedEventHandler(emp1_salaryChanged);
            emp3.PropertyChanged += new PropertyChangedEventHandler(emp1_salaryChanged);

            emp1.Salary = 1900;
            Console.ReadLine();
            emp2.Salary = 1300;
            Console.ReadLine();
            emp3.Salary = 1300;
            Console.ReadLine();
        }

        static void emp1_salaryChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName + " changed to: " + ((Employee)sender).Salary.ToString());
        }
    }
}
