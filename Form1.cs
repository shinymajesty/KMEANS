/*
 * Author: Fabian Schopf
 * Program: KmeansColorClustering (For image color reduction)
 * Subject: MELA-4AHWII
*/
using System.Diagnostics;

namespace KmeansColorClustering
{
    public partial class Form1 : Form
    {

        private readonly List<Size> Resolutions = [
            new(1280, 720),
            new(1920, 1080),
            new(2560, 1440),
            ];

        private Image originalImage = new Bitmap(1, 1); // Just to avoid null reference exception
        private Image resultImage = new Bitmap(1, 1);
        private Image diffImage = new Bitmap(1, 1);

        public Form1()
        {
            InitializeComponent();
            SetupResolutionsBox();
        }

        private void SetupResolutionsBox()
        {
            comboBoxResolution.Items.AddRange(Resolutions.Select(r => $"{r.Width}x{r.Height}").ToArray());
            comboBoxResolution.SelectedIndex = 1;
        }

        private void ComboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            var display = Screen.FromControl(this).WorkingArea;
            var resolution = Resolutions[comboBoxResolution.SelectedIndex];
            if (display.Width + 50 < resolution.Width || display.Height + 50 < resolution.Height)
                resolution = Resolutions[comboBoxResolution.SelectedIndex - 1];


            this.Size = resolution;
            this.Top = 0;
            this.Left = 0;
        }

        private void ChkFullScreen_CheckStateChanged(object sender, EventArgs e)
        {
            this.WindowState = chkFullScreen.Checked ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (lblTitle.Font.Size > lblTitle.Height - 11) lblTitle.Font = new(lblTitle.Font.FontFamily, lblTitle.Font.Size - 1);
            else if (lblTitle.Font.Size < lblTitle.Height - 4 && lblTitle.Font.Size < 46) lblTitle.Font = new(lblTitle.Font.FontFamily, lblTitle.Font.Size + 1);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            // Creadit to Cristoph Wilflingseder for the outstanding idea of using OFD to load media (code from me tho ... duh!)
            using OpenFileDialog ofd = new()
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.tif",
                Title = "Select an Image",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    originalImage = Image.FromFile(ofd.FileName);
                    pictureBoxOriginal.Image = originalImage;
                    pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
                    label4.Text = $"Image loaded from: {ofd.FileName}";
                    btnGO.Enabled = true;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (ex is OutOfMemoryException)
                        msg = "There was an error loading this image. It may be corrupted or not supported.";

                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnGO_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new();
            sw.Start();

            int k = (int)numInClusters.Value;
            int iterations = (int)numInIterations.Value;
            int runs = (int)numInRuns.Value;
            bool generateDiff = chkGenerateDiff.Checked;

            resultImage = KMeans.Cluster(originalImage, k, iterations, runs, progress => { progressBar1.Value = progress; });
            if (generateDiff)
            {
                diffImage = KMeans.GenerateDifferenceImage(originalImage, resultImage);
            }


            progressBar1.Value = 0;
            pictureBoxOutput.Image = resultImage;
            btnSaveDiff.Enabled = generateDiff;
            btnSave.Enabled = true;

            label5.Text = $"Time elapsed: {sw.ElapsedMilliseconds}ms";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveImage(resultImage);
        }

        private void BtnSaveDiff_Click(object sender, EventArgs e)
        {
            SaveImage(diffImage);
        }
        private static void SaveImage(Image image)
        {
            using SaveFileDialog dlg = new()
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.tif",
                Title = "Save Image",
                CheckPathExists = true,
                CheckWriteAccess = true
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image.Save(dlg.FileName);
                    MessageBox.Show("Image Saved Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
