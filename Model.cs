using Accord.Neuro;
using Accord.Neuro.Learning;
using GaitLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisEmotionalState
{
    class Model
    {
        private const int COUNT_JOINTS = 4200;
        private ActivationNetwork network;
        private BackPropagationLearning teacher;
        private Gait recognizableGait;
        private List<Gait> dataset;

        private int countLearn;

        public Model()
        {
            this.dataset = new List<Gait>();
            this.network = new ActivationNetwork(new SigmoidFunction(), COUNT_JOINTS, 1);
            this.teacher = new BackPropagationLearning(network);
        }

        public int CountLearn()
        {
            return this.countLearn;
        }

        public void SetRecognizableGait(Gait gait)
        {
            this.recognizableGait = gait;
        }

        public void SetNetwork(ActivationNetwork network)
        {
            this.network = network;
            this.teacher = new BackPropagationLearning(network);
        }

        public ActivationNetwork GetNetwork()
        {
            return this.network;
        }

        public int RecognizeEmotion()
        {
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

        public void AddDataset(List<Gait> gaits)
        {
            this.dataset = gaits;
        }

        public void ClearDataset()
        {
            this.dataset.Clear();
        }

        public void Train(double learningRate, int countEpoch)
        {
            countLearn = 0;
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
                    output[i][0] = 0.8;
                }
                else
                {
                    output[i][0] = 0.2;
                }
            }

            this.teacher = new BackPropagationLearning(network);
            teacher.LearningRate = learningRate;

            //обучаем
            double[] errors = new double[countEpoch];
            FileStream fstream = null;
            StreamWriter sw = null;
            try
            {
                for (int i = 0; i < countEpoch; i++)
                {
                    errors[i] = teacher.RunEpoch(input, output) / dataset.Count;
                    ++countLearn;
                }

                String path = "error_";
                path += System.DateTime.Now.ToString() + ".txt";
                path = path.Replace(" ", "_");
                path = path.Replace(":", ".");
                fstream = new FileStream(path, FileMode.Create);
                sw = new StreamWriter(fstream);
                for (int i = 0; i < errors.Length; i++)
                {
                    sw.WriteLine("epoch: " + (i + 1) + ", error: " + errors[i]);
                }

            }
            finally
            {
                if (sw != null && fstream != null)
                {
                    sw.Close();
                    fstream.Close();
                }
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
