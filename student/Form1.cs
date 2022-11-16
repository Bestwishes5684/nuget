using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UserStrory.Models;


namespace WinFormsApp3
{
    public partial class Form1 : Form

    {
        int age = 0;

        private Nuget.Nuget<Student> nug;
        private readonly BindingSource bindingSource;
        public Form1()
        {
            InitializeComponent();

            dataGridstudent.AutoGenerateColumns = false;
            nug = new Nuget.Nuget<Student>();
            bindingSource = new BindingSource();
            bindingSource.DataSource =nug.Get();
            dataGridstudent.DataSource = bindingSource;



        }

     
        private void delite_Click(object sender, EventArgs e)
        {
            var id = (Student)dataGridstudent.Rows[dataGridstudent.SelectedRows[0].Index].DataBoundItem;
            if (MessageBox.Show($"Вы действительно хотите удалить {id.FullName}",
                "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                nug.Remove(id);
                bindingSource.ResetBindings(false);
                CalculateScrol();

            }
        }
        private void CalculateScrol()
        {
            all1.Text = $"Всего учащихся {nug.Get().Count.ToString()}";
            var male = nug.Get().Where(x => x.Gender == Gender.Male).Count();
            var female = nug.Get().Count(x => x.Gender == Gender.Female);
            human.Text = $"M: {male} Ж: {female}";


            

            var Kol = 0;
            foreach (var student in nug.Get())
            {
                if ((student.Math + student.Russ + student.inf) > 150)
                {
                    Kol++;
                }
            }
            credited.Text = $"Студенты, набравшие больше 150 баллов: " + Kol;
        }

        private void change_Click(object sender, EventArgs e)
        {
            var id = (Student)dataGridstudent.Rows[dataGridstudent.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new Form2(id);

            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {

                nug.Change(id, infoForm.Student);
                CalculateScrol();

                bindingSource.ResetBindings(false);
            }
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
            delite.Enabled= dataGridstudent.SelectedRows.Count > 0;
            change.Enabled = dataGridstudent.SelectedRows.Count >0;
            
        }

        private void add_Click(object sender, EventArgs e)
        {
            var stdInfoForm = new Form2();
            if (stdInfoForm.ShowDialog(this) == DialogResult.OK)
            {
                stdInfoForm.Student.Id = Guid.NewGuid();
                nug.Get().Add(stdInfoForm.Student);
                bindingSource.ResetBindings(false);
                CalculateScrol();
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнил: Задумин Р.А " +"\n"+
                "группа: Ип-20-3" +"\n"+
                "Команда: 2");
        }

        private void dataGridstudent_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var student = (Student)dataGridstudent.Rows[e.RowIndex].DataBoundItem;
            if (dataGridstudent.Columns[e.ColumnIndex].Name == "total_total")
            {

                e.Value = Math.Round(student.Math + student.Russ + student.inf);
            }

            if (dataGridstudent.Columns[e.ColumnIndex].Name == "Pol")
            {
                var gender = (Gender)e.Value;
                switch (gender)
                {
                    case Gender.Male:
                        e.Value = "Мужской";
                        break;
                    case Gender.Female:
                        e.Value = "Женский";
                        break;
                }
            }


            if (dataGridstudent.Columns[e.ColumnIndex].Name == "Datarod")
            {
                var data = (Student)dataGridstudent.Rows[e.RowIndex].DataBoundItem;
                int i = (bool)(DateTime.Now.Day > data.Datarod.Day && DateTime.Now.Month >= data.Datarod.Month) ? -1 : 0;
                age += DateTime.Now.Year - data.Datarod.Year + i;
                e.Value = age;
                age = 0;
            }
            if (dataGridstudent.Columns[e.ColumnIndex].Name == "FORM")
            {
                var forma = (FormOB)e.Value;
                switch (forma)
                {
                    case (FormOB.fulltime):
                        e.Value = "Очное";
                        break;
                    case (FormOB.correspondence):
                        e.Value = "Заочное";
                        break;
                    case (FormOB.both):
                        e.Value = "Очно-заочно";
                        break;
                }
            }
        }

        private void credited_Click(object sender, EventArgs e)
        {

        }

        private void dataGridstudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }