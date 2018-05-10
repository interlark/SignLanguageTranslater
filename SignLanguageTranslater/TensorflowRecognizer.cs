using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Emgu.TF;
//using Emgu.TF.Models;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using TensorFlow;

namespace SignLanguageTranslater
{
    public class TensorflowRecognizer
    {
        private string graphPath;
        private string labelsPath;
        private int queueSize;
        private int skipFrames;
        private Action<string, float> callback;
        private Func<bool> saveImage;
        private string currentImage;
        private CancellationTokenSource cts;
        private float[] probabilities = null;
        private int frameCounter = 0;
        private int currentQueueFrame = 0;
        private TFGraph graph = new TFGraph();

        public TensorflowRecognizer(string graphPath, string labelsPath, Action<string, float> callback, string currentImage, Func<bool> saveImage, int queueSize = 1, int skipFrames = 1)
        {
            this.graphPath = graphPath;
            this.labelsPath = labelsPath;
            this.queueSize = queueSize;
            this.skipFrames = skipFrames;
            this.callback = callback;
            this.currentImage = currentImage;
            this.saveImage = saveImage;
            this.graph.Import(new TFBuffer(File.ReadAllBytes(this.graphPath)));
        }

        private void HandleFramesRealTime()
        {
            while (!this.cts.IsCancellationRequested)
            {
                ++frameCounter;

                while (!saveImage())
                {
                    Thread.Sleep(100);
                }

                if ((this.skipFrames == 0 || frameCounter % this.skipFrames == 0) && File.Exists(this.currentImage))
                {
                    //Inception inceptionGraph = null;
                    //Tensor imageTensor = null;
                    float[] currentProbabilities = null;

                    //try
                    //{

                    using (var session = new TFSession(graph))
                    {
                        var g_input = graph["input"][0];
                        var g_output = graph["final_result"][0];
                        var runner = session.GetRunner();

                        var bitmap = new Bitmap(this.currentImage);
                        var tensor = ImageUtil.CreateTensorFromImageFile(bitmap);
                        bitmap.Dispose();

                        runner.AddInput(g_input, tensor).Fetch(g_output);
                        var output = runner.Run();

                        var result = output[0];
                        var rshape = result.Shape;
                        currentProbabilities = ((float[][])result.GetValue(jagged: true))[0];

                        //    inceptionGraph = new Inception(null, new string[] { this.graphPath, this.labelsPath }, null, "input", "final_result");
                        //    imageTensor = ImageIO.ReadTensorFromImageFile(this.currentImage, 224, 224, 128.0f, 1.0f/128.0f);
                        //    currentProbabilities = inceptionGraph.Recognize(imageTensor);
                        //}
                        //catch(Exception)
                        //{
                        //    --frameCounter;
                        //    continue;
                        //}

                        if (this.probabilities == null)
                        {
                            this.probabilities = currentProbabilities;
                        }
                        else
                        {
                            this.probabilities = this.probabilities.Zip(currentProbabilities, (a, b) => (a + b)).ToArray();
                        }

                        if (++this.currentQueueFrame >= this.queueSize)
                        {
                            RecognizeSymbolFromQueue(/*inceptionGraph*/);
                        }

                    }
                }
            }
        }

        public void Start()
        {
            this.cts = new CancellationTokenSource();
            Task.Factory.StartNew(HandleFramesRealTime);
        }

        public void Stop()
        {
            this.cts.Cancel();
        }

        private void RecognizeSymbolFromQueue(/*Inception graph*/)
        {
            if (this.cts.Token.IsCancellationRequested)
            {
                return;
            }

            if (this.probabilities != null)
            {
                for (int i = 0; i < this.probabilities.Length; ++i)
                {
                    this.probabilities[i] /= this.currentQueueFrame;
                }

                String[] labels = File.ReadAllLines(this.labelsPath);
                float maxVal = 0;
                int maxIdx = 0;
                for (int i = 0; i < this.probabilities.Length; i++)
                {
                    if (this.probabilities[i] > maxVal)
                    {
                        maxVal = this.probabilities[i];
                        maxIdx = i;
                    }
                }

                this.callback(labels[maxIdx], maxVal * 100);
            }

            this.probabilities = null;
            this.currentQueueFrame = 0;
            this.frameCounter = 0;
        }

    }
}
