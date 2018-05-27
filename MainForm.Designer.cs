namespace AnalysisEmotionalState
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadGait = new System.Windows.Forms.Button();
            this.RecognizeEmotion = new System.Windows.Forms.Button();
            this.recognitionUnit = new System.Windows.Forms.GroupBox();
            this.trainingUnit = new System.Windows.Forms.GroupBox();
            this.Train = new System.Windows.Forms.Button();
            this.LoadDataset = new System.Windows.Forms.Button();
            this.countEpochLabel = new System.Windows.Forms.Label();
            this.CountEpoch = new System.Windows.Forms.NumericUpDown();
            this.learningRateLabel = new System.Windows.Forms.Label();
            this.LearningRate = new System.Windows.Forms.NumericUpDown();
            this.SaveNetwork = new System.Windows.Forms.Button();
            this.LoadNetwork = new System.Windows.Forms.Button();
            this.recognitionUnit.SuspendLayout();
            this.trainingUnit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CountEpoch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LearningRate)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadGait
            // 
            this.LoadGait.Location = new System.Drawing.Point(32, 19);
            this.LoadGait.Name = "LoadGait";
            this.LoadGait.Size = new System.Drawing.Size(157, 31);
            this.LoadGait.TabIndex = 0;
            this.LoadGait.Text = "Загрузить файл походки";
            this.LoadGait.UseVisualStyleBackColor = true;
            this.LoadGait.Click += new System.EventHandler(this.loadGait_Click);
            // 
            // RecognizeEmotion
            // 
            this.RecognizeEmotion.Location = new System.Drawing.Point(32, 91);
            this.RecognizeEmotion.Name = "RecognizeEmotion";
            this.RecognizeEmotion.Size = new System.Drawing.Size(157, 31);
            this.RecognizeEmotion.TabIndex = 1;
            this.RecognizeEmotion.Text = "Распознать эмоцию";
            this.RecognizeEmotion.UseVisualStyleBackColor = true;
            this.RecognizeEmotion.Click += new System.EventHandler(this.recognizeEmotion_Click);
            // 
            // recognitionUnit
            // 
            this.recognitionUnit.Controls.Add(this.LoadGait);
            this.recognitionUnit.Controls.Add(this.RecognizeEmotion);
            this.recognitionUnit.Location = new System.Drawing.Point(12, 12);
            this.recognitionUnit.Name = "recognitionUnit";
            this.recognitionUnit.Size = new System.Drawing.Size(231, 140);
            this.recognitionUnit.TabIndex = 2;
            this.recognitionUnit.TabStop = false;
            this.recognitionUnit.Text = "Распознавание";
            // 
            // trainingUnit
            // 
            this.trainingUnit.Controls.Add(this.LoadNetwork);
            this.trainingUnit.Controls.Add(this.SaveNetwork);
            this.trainingUnit.Controls.Add(this.Train);
            this.trainingUnit.Controls.Add(this.LoadDataset);
            this.trainingUnit.Controls.Add(this.countEpochLabel);
            this.trainingUnit.Controls.Add(this.CountEpoch);
            this.trainingUnit.Controls.Add(this.learningRateLabel);
            this.trainingUnit.Controls.Add(this.LearningRate);
            this.trainingUnit.Location = new System.Drawing.Point(298, 12);
            this.trainingUnit.Name = "trainingUnit";
            this.trainingUnit.Size = new System.Drawing.Size(309, 140);
            this.trainingUnit.TabIndex = 3;
            this.trainingUnit.TabStop = false;
            this.trainingUnit.Text = "Обучение";
            // 
            // Train
            // 
            this.Train.Location = new System.Drawing.Point(194, 58);
            this.Train.Name = "Train";
            this.Train.Size = new System.Drawing.Size(109, 24);
            this.Train.TabIndex = 5;
            this.Train.Text = "Обучить";
            this.Train.UseVisualStyleBackColor = true;
            this.Train.Click += new System.EventHandler(this.train_Click);
            // 
            // LoadDataset
            // 
            this.LoadDataset.Location = new System.Drawing.Point(194, 26);
            this.LoadDataset.Name = "LoadDataset";
            this.LoadDataset.Size = new System.Drawing.Size(109, 24);
            this.LoadDataset.TabIndex = 4;
            this.LoadDataset.Text = "Загрузить данные";
            this.LoadDataset.UseVisualStyleBackColor = true;
            this.LoadDataset.Click += new System.EventHandler(this.loadData_Click);
            // 
            // countEpochLabel
            // 
            this.countEpochLabel.AutoSize = true;
            this.countEpochLabel.Location = new System.Drawing.Point(6, 62);
            this.countEpochLabel.Name = "countEpochLabel";
            this.countEpochLabel.Size = new System.Drawing.Size(92, 13);
            this.countEpochLabel.TabIndex = 3;
            this.countEpochLabel.Text = "Количество эпох";
            // 
            // CountEpoch
            // 
            this.CountEpoch.Location = new System.Drawing.Point(102, 62);
            this.CountEpoch.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.CountEpoch.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CountEpoch.Name = "CountEpoch";
            this.CountEpoch.Size = new System.Drawing.Size(57, 20);
            this.CountEpoch.TabIndex = 2;
            this.CountEpoch.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // learningRateLabel
            // 
            this.learningRateLabel.AutoSize = true;
            this.learningRateLabel.Location = new System.Drawing.Point(6, 28);
            this.learningRateLabel.Name = "learningRateLabel";
            this.learningRateLabel.Size = new System.Drawing.Size(104, 13);
            this.learningRateLabel.TabIndex = 1;
            this.learningRateLabel.Text = "Скорость обучения";
            // 
            // LearningRate
            // 
            this.LearningRate.DecimalPlaces = 2;
            this.LearningRate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.LearningRate.Location = new System.Drawing.Point(116, 26);
            this.LearningRate.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LearningRate.Name = "LearningRate";
            this.LearningRate.Size = new System.Drawing.Size(43, 20);
            this.LearningRate.TabIndex = 0;
            this.LearningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // SaveNetwork
            // 
            this.SaveNetwork.Location = new System.Drawing.Point(22, 98);
            this.SaveNetwork.Name = "SaveNetwork";
            this.SaveNetwork.Size = new System.Drawing.Size(109, 24);
            this.SaveNetwork.TabIndex = 6;
            this.SaveNetwork.Text = "Сохранить сеть";
            this.SaveNetwork.UseVisualStyleBackColor = true;
            this.SaveNetwork.Click += new System.EventHandler(this.SaveNetwork_Click);
            // 
            // LoadNetwork
            // 
            this.LoadNetwork.Location = new System.Drawing.Point(194, 98);
            this.LoadNetwork.Name = "LoadNetwork";
            this.LoadNetwork.Size = new System.Drawing.Size(109, 24);
            this.LoadNetwork.TabIndex = 7;
            this.LoadNetwork.Text = "Загрузить сеть";
            this.LoadNetwork.UseVisualStyleBackColor = true;
            this.LoadNetwork.Click += new System.EventHandler(this.LoadNetwork_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 271);
            this.Controls.Add(this.trainingUnit);
            this.Controls.Add(this.recognitionUnit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnalysisEmotionalState";
            this.Load += new System.EventHandler(this.Main_Load);
            this.recognitionUnit.ResumeLayout(false);
            this.trainingUnit.ResumeLayout(false);
            this.trainingUnit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CountEpoch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LearningRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadGait;
        private System.Windows.Forms.Button RecognizeEmotion;
        private System.Windows.Forms.GroupBox recognitionUnit;
        private System.Windows.Forms.GroupBox trainingUnit;
        private System.Windows.Forms.Label learningRateLabel;
        private System.Windows.Forms.NumericUpDown LearningRate;
        private System.Windows.Forms.Label countEpochLabel;
        private System.Windows.Forms.NumericUpDown CountEpoch;
        private System.Windows.Forms.Button Train;
        private System.Windows.Forms.Button LoadDataset;
        private System.Windows.Forms.Button SaveNetwork;
        private System.Windows.Forms.Button LoadNetwork;
    }
}

