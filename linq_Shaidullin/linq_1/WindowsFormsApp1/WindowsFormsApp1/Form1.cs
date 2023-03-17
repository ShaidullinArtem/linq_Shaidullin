using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<Person> people = new List<Person>();

            using (StreamReader sr = new StreamReader("People.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] words = line.Split(' ');

                    string lastName = words[0];
                    string firstName = words[1];
                    string middleName = words[2];
                    int age = int.Parse(words[3]);
                    double weight = double.Parse(words[4]);

                    Person person = new Person(lastName, firstName, middleName, age, weight);
                    people.Add(person);
                }
            }

            var youngPerson = from person in people
                              where person.Age < 40
                              select person;

            foreach (Person person in youngPerson)
                listBox1.Items.Add($"{person.LastName} {person.FirstName} {person.MiddleName}, Возраст -  {person.Age} лет, Вес - {person.Weight} кг.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Department> department = new List<Department>()
            {
                new Department { Name = "Отдел закупок", Reg = "Германия" },
                new Department { Name = "Отдел продаж", Reg = "Испания" },
                new Department { Name = "Отдел маркетинга", Reg = "Испания" }
            };

            List<Employ> employes = new List<Employ>()
            {
                new Employ {Name = "Иванов", Department = "Отдел закупок"},
                new Employ {Name = "Петров", Department = "Отдел закупок"},
                new Employ {Name = "Сидоров", Department = "Отдел продаж"},
                new Employ {Name = "Лямин", Department = "Отдел продаж"},
                new Employ {Name = "Сидоренко", Department = "Отдел маркетинга"},
                new Employ {Name = "Кривоносов", Department = "Отдел продаж"}
            };

            var resultOne = from emp in employes
                            join dep in department on emp.Department equals dep.Name
                            group emp by dep.Reg into g
                            select new { Reg = g.Key, Department = g.ToList() };
            foreach (var group in resultOne)
            {
                listBox2.Items.Add($"Регион: {group.Reg}");
                foreach (var dep in group.Department)
                {
                    listBox2.Items.Add(dep.Name);
                }
            }

            var resultTwo = from emp in employes
                            join dep in department on emp.Department equals dep.Name
                            where dep.Reg.StartsWith("И")
                            select emp;
            foreach (var emp in resultTwo)
            {
                listBox3.Items.Add($"{emp.Name} ({emp.Department})");
            }
        }
    }
}
