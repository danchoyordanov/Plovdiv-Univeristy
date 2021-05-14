using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Text = "Calculator";
            lblAppTitle.Text = "Calculator".ToUpper();
        }

        // class members
        int maxSize = 0;
        double total1 = 0;
        double total2 = 0;

        bool equalBtnClicked = false;
        bool periodBtnClicked = false;
        bool addBtnClicked = false;
        bool subtractBtnClicked = false;
        bool multiplyBtnClicked = false;
        bool divideBtnClicked = false;

        // class methods
        private void ConfirmExit()
        {
            if (MessageBox.Show("Do you want to exit the application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Minimize()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void PrintNumber(Button btnName)
        {
            if (maxSize < 19)
            {
                if (lblCompute.Text == "0" || equalBtnClicked == true)
                {
                    lblCompute.Text = btnName.Text;
                }
                else if (lblCompute.Text == "0.")
                {
                    lblCompute.Text += btnName.Text;
                }
                else
                {
                    lblCompute.Text += btnName.Text;
                }
                equalBtnClicked = false;
                ++maxSize;
            }
        }

        private void ClearScreen()
        {
            lblCompute.Text = "0";
            maxSize = 0;
            ResetButtons();
        }

        private void ResetButtons()
        {
            equalBtnClicked = false;
            periodBtnClicked = false;
            addBtnClicked = false;
            subtractBtnClicked = false;
            multiplyBtnClicked = false;
            divideBtnClicked = false;
        }

        private void Operator()
        {
            total1 += double.Parse(lblCompute.Text);
            ClearScreen();
            ResetButtons();
        }

        private void Period()
        {
            if (maxSize < 19)
            {
                if (lblCompute.Text == "0" || equalBtnClicked == true)
                {
                    lblCompute.Text = "0.";
                    equalBtnClicked = false;
                }
                else
                {
                    if (periodBtnClicked == false)
                    {
                        lblCompute.Text += ".";
                    }
                    periodBtnClicked = true;
                    ++maxSize;
                }
            }
        }

        private void Zero()
        {
            if (lblCompute.Text == "0" || equalBtnClicked == true)
            {
                lblCompute.Text = btnZero.Text;
                maxSize = 0;
            }
            else
            {
                lblCompute.Text = lblCompute.Text + btnZero.Text;
                ++maxSize;
            }
        }

        private void Add()
        {
            Operator();
            addBtnClicked = true;
        }

        private void Subtract()
        {
            Operator();
            subtractBtnClicked = true;
        }

        private void Multiply()
        {
            Operator();
            multiplyBtnClicked = true;
        }

        private void Divide()
        {
            Operator();
            divideBtnClicked = true;
        }

        private void Equals()
        {
            if (!((addBtnClicked == false) && (subtractBtnClicked == false) && (multiplyBtnClicked == false) && (divideBtnClicked == false)))
            {
                if (addBtnClicked == true)
                {
                    total2 = total1 + double.Parse(lblCompute.Text);
                }
                else if (subtractBtnClicked == true)
                {
                    total2 = total1 - double.Parse(lblCompute.Text);
                }
                else if (multiplyBtnClicked == true)
                {
                    total2 = total1 * double.Parse(lblCompute.Text);
                }
                else if (divideBtnClicked == true)
                {
                    total2 = total1 / double.Parse(lblCompute.Text);
                }
                lblCompute.Text = total2.ToString();
                total1 = 0;
            }
            ResetButtons();
            equalBtnClicked = true;
            maxSize = 0;
        }

        #region class events
        private void btnZero_Click(object sender, EventArgs e)
        {
            Zero();
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            PrintNumber(btnOne);
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            PrintNumber(btnTwo);
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            PrintNumber(btnThree);
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            PrintNumber(btnFour);
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            PrintNumber(btnFive);
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            PrintNumber(btnSix);
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            PrintNumber(btnSeven);
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            PrintNumber(btnEight);
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            PrintNumber(btnNine);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            Period();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            Equals();
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            Subtract();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            Divide();
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            Multiply();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ConfirmExit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            Minimize();
        }
        #endregion
    }
}