using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private PictureBox originalPictureBox;
        private PictureBox downscaledPictureBox;
        private TextBox factorTextBox;
        private Button selectImageButton;
        private Button downscaleButton;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            originalPictureBox = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(400, 300),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            downscaledPictureBox = new PictureBox
            {
                Location = new Point(420, 10),
                Size = new Size(400, 300),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            factorTextBox = new TextBox
            {
                Location = new Point(10, 320),
                Size = new Size(100, 20)
            };

            selectImageButton = new Button
            {
                Location = new Point(120, 320),
                Size = new Size(120, 25),
                Text = "Select Image"
            };

            downscaleButton = new Button
            {
                Location = new Point(250, 320),
                Size = new Size(120, 25),
                Text = "Downscale"
            };

            selectImageButton.Click += SelectImageButton_Click;
            downscaleButton.Click += DownscaleButton_Click;

            Controls.Add(originalPictureBox);
            Controls.Add(downscaledPictureBox);
            Controls.Add(factorTextBox);
            Controls.Add(selectImageButton);
            Controls.Add(downscaleButton);
        }

        private void SelectImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    originalPictureBox.Image = new Bitmap(openFileDialog.FileName);
                }
            }
        }

        private void DownscaleButton_Click(object sender, EventArgs e)
        {
            if (originalPictureBox.Image != null)
            {
                double factor;
                if (double.TryParse(factorTextBox.Text, out factor) && factor > 0 && factor <= 100)
                {
                    Task.Run(() => DownscaleImage(factor));
                }
                else
                {
                    MessageBox.Show("Please enter a valid downscaling factor (1-100).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DownscaleImage(double factor)
        {
            Bitmap originalImage = new Bitmap(originalPictureBox.Image);

            int newWidth = (int)(originalImage.Width * factor / 100);
            int newHeight = (int)(originalImage.Height * factor / 100);

            Bitmap downscaledImage = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(downscaledImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            downscaledPictureBox.Invoke(new Action(() =>
            {
                downscaledPictureBox.Image = downscaledImage;
            }));
        }
    }
}