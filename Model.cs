using Accord.Neuro;
using Accord.Neuro.Learning;
using GaitLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisEmotionalState
{
    class Model
    {
        private const int COUNT_JOINTS = 4200;
        private ActivationNetwork network;
        private Gait recognizableGait;
        private List<Gait> dataset;

        public Model()
        {
            this.dataset = new List<Gait>();
            this.network = new ActivationNetwork(new SigmoidFunction(), COUNT_JOINTS, 14, 1);
        }

        public void SetRecognizableGait(Gait gait)
        {
            this.recognizableGait = gait;
        }

        public int RecognizeEmotion()
        {
            if (this.recognizableGait == null)
            {
                throw new Exception("Выберите файл походки для распознавания");
            }
            // преобразовать матрицу во входной вектор
            double[] input = FromMatrixToVector(recognizableGait.GetJoints());
            // вычислить выходной вектор сети
            double[] output = network.Compute(input);

            if (output[0] >= 0.5)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void AddGaitToDataset(Gait gait)
        {
            this.dataset.Add(gait);
        }

        public void Train(double learningRate, int countEpoch)
        {
            // подготавливаем учебные данные
            double[][] input = new double[dataset.Count][];
            double[][] output = new double[dataset.Count][];
            Gait g = null;
            for (int i = 0; i < dataset.Count; i++)
            {
                g = dataset[i];
                input[i] = FromMatrixToVector(g.GetJoints());
                output[i] = new double[1];
                if (g.GetType() == 1)
                {
                    output[i][0] = 0.65;
                }
                else
                {
                    output[i][0] = 0.35;
                }
            }

            // создаем учителя
            BackPropagationLearning teacher = new BackPropagationLearning(network);
            teacher.LearningRate = learningRate;

            //обучаем
            FileStream fstream = null;
            StreamWriter sw = null;
            try
            {
                fstream = new FileStream("C:\\Users\\Nikita\\Desktop\\error.txt", FileMode.Create);
                sw = new StreamWriter(fstream);
                for (int i = 0; i < countEpoch; i++)
                {
                    double error = teacher.RunEpoch(input, output) / dataset.Count;
                    sw.WriteLine(error + ", out neuron:" + network.Output[0]);

                }
            }
            finally
            {
                sw.Close();
                fstream.Close();
            }
        }

        private double[] FromMatrixToVector(List<List<double>> joints)
        {
            double[] vector = new double[COUNT_JOINTS];

            int length = 0;
            for (int i = 0; i < joints.Count; i++)
            {
                int count = joints[i].Count;
                for (int j = 0; j < count; j++)
                {
                    vector[length] = joints[i][j];
                    length++;
                }
            }

            return vector;
        }
    }
}
