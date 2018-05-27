using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalysisEmotionalState
{
    public partial class MainForm : Form
    {
        private Controller controller;

        public MainForm()
        {
            InitializeComponent();
            this.controller = new Controller(this);
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void loadGait_Click(object sender, EventArgs e)
        {
            OpenFileDialog gaitFile = new OpenFileDialog();
            gaitFile.Title = "Gait file";
            gaitFile.Filter = "gait file (*.g)|*.g";
            if (gaitFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String filename = gaitFile.FileName;
                    controller.OpenGaitFile(filename);
                    MessageBox.Show("Загружен файл: " + filename);
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void recognizeEmotion_Click(object sender, EventArgs e)
        {
            try
            {
                int result = controller.recognizeEmotion();
                if (result == 1)
                {
                    MessageBox.Show("Возбужденное состояние");
                }
                else
                {
                    MessageBox.Show("Нормальное состояние");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadData_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sample = new FolderBrowserDialog();
            if (sample.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String pathData = sample.SelectedPath;
                    controller.LoadData(pathData);
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void train_Click(object sender, EventArgs e)
        {
            double learningRate = double.Parse(this.LearningRate.Value.ToString());
            int countEpoch = int.Parse(this.CountEpoch.Value.ToString());
            controller.Train(learningRate, countEpoch);
        }

        private void SaveNetwork_Click(object sender, EventArgs e)
        {

        }

        private void LoadNetwork_Click(object sender, EventArgs e)
        {

        }
    }
}
