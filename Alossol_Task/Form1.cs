namespace Alossol_Task
{
    using System;
    using System.Data;
    using System.Windows.Forms;
    using System.Xml.Linq;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ParseXML(string filePath)
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
            new DataColumn("Part InputSeq"),
            new DataColumn("Part Name"),
            new DataColumn("Order"),
            new DataColumn("Assembly"),
            new DataColumn("Quantity"),
            new DataColumn("MachineTime"),
            new DataColumn("AreaUsed"),
            new DataColumn("Weight Used"),
            });

            var xdoc = XDocument.Load(filePath);
            var programs = xdoc.Descendants("Program");

            foreach (var program in programs)
            {
                var parts = program.Descendants("Part");

                foreach (var part in parts)
                {
                    var row = dt.NewRow();
                    row["Part InputSeq"] = part.Attribute("InputSeq")?.Value;
                    row["Part Name"] = part.Attribute("Name")?.Value;
                    row["Order"] = part.Attribute("Order")?.Value;
                    row["Assembly"] = part.Attribute("Assembly")?.Value;
                    row["Quantity"] = part.Attribute("Quantity")?.Value;
                    row["MachineTime"] = part.Attribute("MachineTime")?.Value;
                    row["AreaUsed"] = part.Attribute("AreaUsed")?.Value;
                    row["Weight Used"] = part.Attribute("WeightUsed")?.Value;
                    dt.Rows.Add(row);
                }
            }
            dt.Columns["Part InputSeq"].SetOrdinal(0);
            dt.Columns["Part Name"].SetOrdinal(1);
            dt.Columns["Order"].SetOrdinal(2);
            dt.Columns["Assembly"].SetOrdinal(3);
            dt.Columns["Quantity"].SetOrdinal(4);
            dt.Columns["MachineTime"].SetOrdinal(5);
            dt.Columns["AreaUsed"].SetOrdinal(6);
            dt.Columns["Weight Used"].SetOrdinal(7);

            dataGridView1.DataSource = dt;

            double totalWeightUsed = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalWeightUsed += double.Parse(row["Weight Used"].ToString());
            }

            MessageBox.Show($"Total Weight Used: {totalWeightUsed}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParseXML("C:\\Users\\Options\\OneDrive\\Desktop\\emraties-task\\Task\\105642_18.fmx");
        }




    }





}