using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OfficesDesktop
{
    public partial class Form1 : Form
    {
        private DataGridView dgv;
        private TextBox txtOfficeName;
        private DateTimePicker dtpDate;
        private ComboBox cmbCategory;
        private NumericUpDown nudAmount;
        private TextBox txtNote;
        private Button btnAdd;

        private List<OfficeCost> officeCosts = new List<OfficeCost>();
        private string filePath = "office_costs_2024.csv";

        public Form1()
        {
            InitializeComponent();
            InitializeFormElements();
            LoadCsv();
            RefreshGrid();
        }

        private void InitializeFormElements()
        {
            this.Text = "Office Cost Manager";
            this.Width = 900;
            this.Height = 600;

            dgv = new DataGridView
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 860,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgv);

            Label lblOffice = new Label { Text = "Office Name:", Location = new System.Drawing.Point(10, 320) };
            txtOfficeName = new TextBox { Location = new System.Drawing.Point(120, 320), Width = 200 };

            Label lblDate = new Label { Text = "Date:", Location = new System.Drawing.Point(10, 360) };
            dtpDate = new DateTimePicker { Location = new System.Drawing.Point(120, 360), Width = 200 };

            Label lblCategory = new Label { Text = "Category:", Location = new System.Drawing.Point(10, 400) };
            cmbCategory = new ComboBox { Location = new System.Drawing.Point(120, 400), Width = 200 };
            cmbCategory.Items.AddRange(new string[] { "Maintenance", "Repairs", "Insurance", "Cleaning", "Utilities", "Other" });
            cmbCategory.SelectedIndex = 0;

            Label lblAmount = new Label { Text = "Amount (EUR):", Location = new System.Drawing.Point(10, 440) };
            nudAmount = new NumericUpDown { Location = new System.Drawing.Point(120, 440), Width = 200, Maximum = 1000000, DecimalPlaces = 2 };

            Label lblNote = new Label { Text = "Note:", Location = new System.Drawing.Point(10, 480) };
            txtNote = new TextBox { Location = new System.Drawing.Point(120, 480), Width = 200 };

            btnAdd = new Button { Text = "Add", Location = new System.Drawing.Point(350, 440), Width = 100 };
            btnAdd.Click += BtnAdd_Click;

            this.Controls.Add(lblOffice);
            this.Controls.Add(txtOfficeName);
            this.Controls.Add(lblDate);
            this.Controls.Add(dtpDate);
            this.Controls.Add(lblCategory);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(lblAmount);
            this.Controls.Add(nudAmount);
            this.Controls.Add(lblNote);
            this.Controls.Add(txtNote);
            this.Controls.Add(btnAdd);
        }

        private void LoadCsv()
        {
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(';');
                officeCosts.Add(new OfficeCost
                {
                    Id = int.Parse(parts[0]),
                    OfficeName = parts[1],
                    Date = DateTime.Parse(parts[2]),
                    Category = parts[3],
                    Amount = decimal.Parse(parts[4]),
                    Note = parts[5]
                });
            }
        }

        private void SaveCsv()
        {
            var lines = new List<string> { "id;officename;datum;kategoria;osszeg;megjegyzes" };
            lines.AddRange(officeCosts.Select(x => $"{x.Id};{x.OfficeName};{x.Date:yyyy-MM-dd};{x.Category};{x.Amount};{x.Note}"));
            File.WriteAllLines(filePath, lines);
        }

        private void RefreshGrid()
        {
            dgv.DataSource = null;
            dgv.DataSource = officeCosts;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int nextId = officeCosts.Any() ? officeCosts.Max(x => x.Id) + 1 : 1;
            var newCost = new OfficeCost
            {
                Id = nextId,
                OfficeName = txtOfficeName.Text,
                Date = dtpDate.Value,
                Category = cmbCategory.SelectedItem.ToString(),
                Amount = nudAmount.Value,
                Note = txtNote.Text
            };

            officeCosts.Add(newCost);
            SaveCsv();
            RefreshGrid();

            txtOfficeName.Clear();
            nudAmount.Value = 0;
            txtNote.Clear();
            cmbCategory.SelectedIndex = 0;
            dtpDate.Value = DateTime.Now;
        }
    }

    public class OfficeCost
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}