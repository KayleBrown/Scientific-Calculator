using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scientific_Calculator
{
    public partial class Form1 : Form
    {
        private string currentExpression = "";
        public Form1()
        {
            InitializeComponent();
            TextBox.AutoSize = true;
        }

        private void Numbers_Click(object sender, EventArgs e)
        {
          Button num = (Button)sender;

            if (TextBox.Text == "0" || TextBox.Text == "Error")
            {
                TextBox.Text = num.Text;
                currentExpression = num.Text;
            }
            else
            {
                if (num.Text == "-" && !currentExpression.EndsWith(" "))
                {
                    TextBox.Text += num.Text;
                    currentExpression += "(" + num.Text;
                }
                else
                {
                    TextBox.Text += num.Text;
                    currentExpression += num.Text;
                }
            }
        }

        private void Ops_Click(object sender, EventArgs e)
        {
            Button op = (Button)sender;

            if (!string.IsNullOrEmpty(currentExpression) && !currentExpression.EndsWith(" "))
            {
                currentExpression += " " + op.Text + " ";
                TextBox.Text = currentExpression;
            }
            else if (op.Text == "-" && (currentExpression.EndsWith("(") || currentExpression.EndsWith(" ")))
            {
                currentExpression += op.Text;
                TextBox.Text = currentExpression;
            }
        }


        private void Equals_Click(object sender, EventArgs e)
        {
            try
            {
                currentExpression = TextBox.Text;

                if (currentExpression.Contains("^"))
                {
                    string[] parts = currentExpression.Split('^');
                    if (parts.Length == 2)
                    {
                        double baseValue = Convert.ToDouble(parts[0]);
                        double exponent = Convert.ToDouble(parts[1]);
                        double result = Math.Pow(baseValue, exponent);

                        TextBox.Text = result.ToString();
                        currentExpression = result.ToString();
                        return;
                    }
                    else
                    {
                        TextBox.Text = "Error";
                        currentExpression = "";
                        return;
                    }
                }

                DataTable table = new DataTable();

                if (!string.IsNullOrWhiteSpace(currentExpression))
                {
                    if (currentExpression.EndsWith(" ") || currentExpression.EndsWith("("))
                    {
                        TextBox.Text = "Error";
                        currentExpression = "";
                        return;
                    }

                    var result = table.Compute(currentExpression, "");
                    TextBox.Text = result.ToString();
                    currentExpression = result.ToString();
                }
                else
                {
                    TextBox.Text = "Error";
                    currentExpression = "";
                }
            }
            catch (Exception ex)
            {
                TextBox.Text = "Error";
                currentExpression = "";
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (TextBox.Text.Length > 0)
            {
                TextBox.Text = TextBox.Text.Remove(TextBox.Text.Length - 1, 1);
                currentExpression = TextBox.Text;
            }
        }


        private void Clear_Click(object sender, EventArgs e)
        {
            TextBox.Text = "0";
            currentExpression = "";
        }

        private void CE_Click(object sender, EventArgs e)
        {
            TextBox.Text = "0";
            currentExpression = "";
        }

        private void Neg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox.Text))
            {
                TextBox.Text = "-";
                currentExpression = "-";
            }
            else if (currentExpression.EndsWith(" ") || currentExpression.EndsWith("("))
            {
                TextBox.Text += "-";
                currentExpression += "-";
            }
            else if (currentExpression.EndsWith("-"))
            {
                TextBox.Text = TextBox.Text.TrimEnd('-');
                currentExpression = currentExpression.TrimEnd('-');
            }
            else
            {
                TextBox.Text = "-";
                currentExpression += "-";
            }
        }

        private void Log_Click(object sender, EventArgs e)
        {
            double log = Convert.ToDouble(TextBox.Text);
            log = Math.Log10(log);
            TextBox.Text = log.ToString();
        }

        private void Sqrt_Click(object sender, EventArgs e)
        {
            double sqrt = Convert.ToDouble(TextBox.Text);
            sqrt = Math.Sqrt(sqrt);
            TextBox.Text = sqrt.ToString();
        }

        private void X2_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(TextBox.Text) * Convert.ToDouble(TextBox.Text);
            TextBox.Text = x.ToString();
        }

        private void X3_Click(object sender, EventArgs e)
        {
            double x, y, z, a;

            y = Convert.ToDouble(TextBox.Text);
            z = Convert.ToDouble(TextBox.Text);
            a = Convert.ToDouble(TextBox.Text);

            x = (y * z * a);
            TextBox.Text = x.ToString();
        }

        private void ASin_Click(object sender, EventArgs e)
        {
            double asin = Convert.ToDouble(TextBox.Text);
            asin = Math.Asin(asin);
            TextBox.Text = asin.ToString();
        }

        private void ACos_Click(object sender, EventArgs e)
        {
            double acos = Convert.ToDouble(TextBox.Text);
            acos = Math.Acos(acos);
            TextBox.Text = acos.ToString();
        }

        private void ATan_Click(object sender, EventArgs e)
        {
            double atan = Convert.ToDouble(TextBox.Text);
            atan = Math.Atan(atan);
            TextBox.Text = atan.ToString();
        }

        private void Sin_Click(object sender, EventArgs e)
        {
            double sin = Convert.ToDouble(TextBox.Text);
            sin = Math.Sin(sin);
            TextBox.Text = sin.ToString();
        }

        private void Cos_Click(object sender, EventArgs e)
        {
            double cos = Convert.ToDouble(TextBox.Text);
            cos = Math.Cos(cos);
            TextBox.Text = cos.ToString();
        }

        private void Tan_Click(object sender, EventArgs e)
        {
            double tan = Convert.ToDouble(TextBox.Text);
            tan = Math.Tan(tan);
            TextBox.Text = tan.ToString();
        }

        private void Precent_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(currentExpression) && !currentExpression.EndsWith(" "))
                {
                    currentExpression += " / 100";
                    DataTable table = new DataTable();
                    var result = table.Compute(currentExpression, "");
                    TextBox.Text = result.ToString();
                    currentExpression = result.ToString();
                }
                else
                {
                    TextBox.Text = "Error";
                    currentExpression = "";
                }
            }
            catch (Exception ex)
            {
                TextBox.Text = "Error";
                currentExpression = "";
            }
        }

        private void Pi_Click(object sender, EventArgs e)
        {
            if (TextBox.Text == "0" || TextBox.Text == "Error")
            {
                TextBox.Text = "3.141592653589793238";
                currentExpression = "3.141592653589793238";
            }
            else
            {
                TextBox.Text += "3.141592653589793238";
                currentExpression += "3.141592653589793238";
            }
        }

        private void standeredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 419;
            TextBox.Width = 354;
        }

        private void scientificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 626;
            TextBox.Width = 563;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are You Sure You Want to Exit?", "Scientific Calculator", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (exit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 419;
            TextBox.Width = 354;
        }
    }
}
