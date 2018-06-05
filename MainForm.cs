using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalysisEmotionalState
{
    public partial class MainForm : Form
    {
        private Controller controller;
        private Thread trainThread;
        private int lastCountLearn;

        public MainForm()
        {
            InitializeComponent();
            this.controller = new Controller();
            this.Train.Enabled = false;
            this.RecognizeEmotion.Enabled = false;
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
                    this.RecognizeEmotion.Enabled = true;
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void recognizeEmotion_Click(object sender, EventArgs e)
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

        private void loadData_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sample = new FolderBrowserDialog();
            if (sample.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String pathData = sample.SelectedPath;
                    controller.LoadData(pathData);
                    MessageBox.Show("Данные загружены");
                    this.Train.Enabled = true;
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void train_Click(object sender, EventArgs e)
        {
            double learningRate = double.Parse(this.LearningRate.Value.ToString());
            int countEpoch = int.Parse(this.CountEpoch.Value.ToString());
            if (learningRate > 1 || learningRate < 0)
            {
                MessageBox.Show("Скорость обучения должна находиться в промежутке от 0 до 1"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (countEpoch > 100000 || countEpoch < 10)
            {
                MessageBox.Show("Количество эпох обучения должно быть в промежутке от 10 до 100000"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                trainThread = new Thread(new ThreadStart(train));
                trainThread.Start();
                this.RecognizeEmotion.Enabled = false;
                this.LoadGait.Enabled = false;
                this.SaveNetwork.Enabled = false;
                this.LoadNetwork.Enabled = false;
                this.CreateNetwork.Enabled = false;
                this.LoadDataset.Enabled = false;
                this.Train.Enabled = false;
                this.CountEpoch.Enabled = false;
                this.LearningRate.Enabled = false;

                this.lastCountLearn = 0;
                this.ProgressBar.Maximum = int.Parse(this.CountEpoch.Value.ToString());
                this.TimerBar.Start();
                this.TimerButton.Start();
            }

        }

        private void train()
        {
            double learningRate = double.Parse(this.LearningRate.Value.ToString());
            int countEpoch = int.Parse(this.CountEpoch.Value.ToString());
            controller.Train(learningRate, countEpoch);
        }

        private void timerButton(object sender, EventArgs e)
        {
            int countEpoch = int.Parse(this.CountEpoch.Value.ToString());
            if (countEpoch == controller.CountLearnEpoch())
            {
                this.RecognizeEmotion.Enabled = controller.RecognizableGaitLoad();
                this.LoadGait.Enabled = true;
                this.SaveNetwork.Enabled = true;
                this.LoadNetwork.Enabled = true;
                this.CreateNetwork.Enabled = true;
                this.LoadDataset.Enabled = true;
                this.Train.Enabled = true;
                this.CountEpoch.Enabled = true;
                this.LearningRate.Enabled = true;
                this.TimerButton.Stop();
            }
        }

        private void bar(object sender, EventArgs e)
        {
            int countEpoch = int.Parse(this.CountEpoch.Value.ToString());
            int count = controller.CountLearnEpoch();
            if (count > lastCountLearn)
            {
                lastCountLearn = count;
                this.ProgressBar.Value += 1;
            }
            else if (count == countEpoch)
            {
                this.ProgressBar.Value = 0;
                this.TimerBar.Stop();
            }
        }


        private void SaveNetwork_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveNN = new SaveFileDialog();
            saveNN.Filter = "nn file (*.nn)|.nn";
            saveNN.Title = "Save network";
            saveNN.ShowDialog();
            if (saveNN.FileName != "")
            {
                controller.SaveNN(saveNN.FileName);
            }
        }

        private void LoadNetwork_Click(object sender, EventArgs e)
        {
            OpenFileDialog networkFile = new OpenFileDialog();
            networkFile.Title = "Network file";
            networkFile.Filter = "network file (*.nn)|*.nn";
            if (networkFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String filename = networkFile.FileName;
                    controller.OpenNetworkFile(filename);
                    MessageBox.Show("Нейронная сеть загружена");
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            try
            {
                controller.CreateNetwork();
                MessageBox.Show("Нейронная сеть создана");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Main_Closing(object sender, FormClosedEventArgs e)
        {
            if (trainThread != null)
                trainThread.Abort();
        }
    }
}
