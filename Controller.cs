using GaitLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisEmotionalState
{
    class Controller
    {
        private MainForm view;
        private Model model;

        public Controller(MainForm view)
        {
            this.view = view;
            this.model = new Model();
        }

        public void OpenGaitFile(String filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Gait gait = (Gait)formatter.Deserialize(fs);
                model.SetRecognizableGait(gait);
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

        public void LoadData(String path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            String[] gaitFiles = Directory.GetFiles(path);
            String curFile = "";
            FileStream fs = null;
            try
            {
                foreach (String fileGait in gaitFiles)
                {
                    curFile = fileGait;
                    fs = new FileStream(fileGait, FileMode.Open);
                    Gait gait = (Gait)formatter.Deserialize(fs);
                    model.AddGaitToDataset(gait);
                }
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
    }
}
