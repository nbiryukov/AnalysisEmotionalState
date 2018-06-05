using Accord.Neuro;
using GaitLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AnalysisEmotionalState
{
    class Controller
    {
        private Model model;
        private bool recognizableGaitLoad;

        public Controller()
        {
            this.model = new Model();
            this.recognizableGaitLoad = false;
        }

        public void OpenGaitFile(String filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Gait gait = (Gait)formatter.Deserialize(fs);
                model.SetRecognizableGait(gait);
                this.recognizableGaitLoad = true;
            }
            catch (SerializationException e)
            {
                throw new SerializationException("Не удалось открыть данный файл");
            }
            finally
            {
                fs.Close();
            }
        }

        public void OpenNetworkFile(String filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            try
            {
                ActivationNetwork network = (ActivationNetwork)Network.Load(fs);
                model.SetNetwork(network);
            }
            catch (SerializationException e)
            {
                throw new SerializationException("Не удалось открыть данный файл");
            }
            finally
            {
                fs.Close();
            }
        }

        public void SaveNN(String path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                model.GetNetwork().Save(fs);
            }
        }

        public void LoadData(String path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            String[] gaitFiles = Directory.GetFiles(path);
            String curFile = "";
            FileStream fs = null;
            try
            {
                List<Gait> dataset = new List<Gait>();
                Gait gait = null;
                foreach (String fileGait in gaitFiles)
                {
                    curFile = fileGait;
                    fs = new FileStream(fileGait, FileMode.Open);
                    gait = (Gait)formatter.Deserialize(fs);
                    dataset.Add(gait);
                }
                model.ClearDataset();
                model.AddDataset(dataset);
            }
            catch (SerializationException e)
            {
                throw new SerializationException("Не удалось открыть файл " + curFile + ". Проверьте файлы в выбранной директории");
            }
            finally
            {
                fs.Close();
            }
        }

        public int recognizeEmotion()
        {
            return model.RecognizeEmotion();
        }

        public void Train(double learningRate, int countEpoch)
        {
            model.Train(learningRate, countEpoch);
        }

        public int CountLearnEpoch()
        {
            return model.CountLearn();
        }

        public bool RecognizableGaitLoad()
        {
            return this.recognizableGaitLoad;
        }

        public void CreateNetwork()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Layer[]));
            Layer[] layers;
            FileStream fs = null;
            try
            {
                if (!File.Exists("config.xml"))
                    throw new Exception("Отсутствует конфигурационный файл");

                fs = new FileStream("config.xml", FileMode.Open);
                layers = (Layer[])serializer.Deserialize(fs);
                fs.Close();
                for (int i = 0; i < layers.Length; i++)
                {
                    if (layers[i].Neurons == 0)
                        throw new Exception("Конфигурационный файл имеет неправильный формат");
                }
                if (layers.Length > 5)
                    throw new Exception("Превышен лимит на количество скрытых слоев. Вы можете создать не более 5 скрытых слоев");

                IActivationFunction func = new SigmoidFunction();
                ActivationNetwork network = new ActivationNetwork(func, 4200, 1);
                switch (layers.Length)
                {
                    case 1:
                        network = new ActivationNetwork(func, 4200, layers[0].Neurons, 1);
                        break;
                    case 2:
                        network = new ActivationNetwork(func, 4200, layers[0].Neurons, layers[1].Neurons, 1);
                        break;
                    case 3:
                        network = new ActivationNetwork(func, 4200, layers[0].Neurons, layers[1].Neurons, layers[2].Neurons, 1);
                        break;
                    case 4:
                        network = new ActivationNetwork(func, 4200, layers[0].Neurons, layers[1].Neurons, layers[2].Neurons, layers[3].Neurons, 1);
                        break;
                    case 5:
                        network = new ActivationNetwork(func, 4200, layers[0].Neurons, layers[1].Neurons, layers[2].Neurons, layers[3].Neurons, layers[4].Neurons, 1);
                        break;
                }

                model.SetNetwork(network);

            }
            catch (InvalidOperationException e)
            {
                throw new Exception("Конфигурационный файл имеет неправильный формат");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
