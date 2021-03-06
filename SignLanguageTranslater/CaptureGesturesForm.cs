﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Threading;
using System.Diagnostics;

namespace SignLanguageTranslater
{
    public partial class CaptureGesturesForm : Form
    {
        public string GraphPath { get; set; }
        public string LabelsPath { get; set; }

        public int SkipFrames { get; set; } = 0;

        public int ProbabilityQueueSize { get; set; } = 6;

        private string tmpImageFileName = "tmp_frame.jpg";

        private Dictionary<string, string> translatedFolders = Settings.GetTranslatedFolders();

        private TensorflowRecognizer imgRecognizer = null;

        private Image sharedImage = null;

        KinectSensor sensor = null;
        MultiSourceFrameReader frameReader = null;

        //IList<Body> _bodies;

        /// <summary>
        /// Необработанный массив данных полученный через глубинный сенсор.
        /// </summary>
        //private ushort[] rawDepthPixelData = null;
        /// <summary>
        /// Необработанный массив данных полученный через инфракрасный сенсор.
        /// </summary>
        //private ushort[] rawIRPixelData = null;

        //private CameraSpacePoint[] csp = null;


        public CaptureGesturesForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!string.IsNullOrEmpty(this.GraphPath) && File.Exists(this.GraphPath))
            {
                if (!string.IsNullOrEmpty(this.LabelsPath) && File.Exists(this.LabelsPath))
                {

                    // Проверка подключено ли устройство
                    if (KinectSensor.GetDefault() != null)
                    {
                        this.sensor = KinectSensor.GetDefault();

                        // Проверка устройства готовность
                        if (this.sensor != null)
                        {
                            if ((this.sensor.KinectCapabilities & KinectCapabilities.Vision) == KinectCapabilities.Vision)
                            {
                                this.sensor.Open();

                                // Открываем multi-source reader фреймов для сенсоров
                                this.frameReader = this.sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color /*| FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body | FrameSourceTypes.BodyIndex*/);

                                // Получаем описание сенсоров
                                // Kinect version 2:
                                // An RGB color camera – 640×480 in version 1, 1920×1080 in version 2
                                // A depth sensor – 320×240 in v1, 512×424 in v2
                                // An infrared sensor – 512×424 in v2
                                //FrameDescription colorFrameDescription = this.sensor.ColorFrameSource.FrameDescription;
                                //FrameDescription depthFrameDescription = this.sensor.DepthFrameSource.FrameDescription;
                                //FrameDescription irFrameDescription = this.sensor.InfraredFrameSource.FrameDescription;

                                // Устанавливаем размерность глубинного и инфракрасного массива чистых данных.
                                // ushort = 2 bytes per pixel
                                //this.rawDepthPixelData = new ushort[depthFrameDescription.Width * depthFrameDescription.Height * 1];
                                //this.rawIRPixelData = new ushort[irFrameDescription.Width * irFrameDescription.Height * 1];
                                //this.csp = new CameraSpacePoint[colorFrameDescription.Width * colorFrameDescription.Height * 1];
                                // Указываем callback для полученных кадров из framereader'а

                                this.imgRecognizer = new TensorflowRecognizer(this.GraphPath, this.LabelsPath, this.HandleSymbol, tmpImageFileName, SaveSharedImage,
                                    this.ProbabilityQueueSize, this.SkipFrames);

                                this.imgRecognizer.Start();

                                this.frameReader.MultiSourceFrameArrived += frameReader_MultiSourceFrameArrived;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не указан файл tensorflow символов", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.BeginInvoke(new MethodInvoker(this.Close));
                }
            }
            else
            {
                MessageBox.Show("Не указан файл tensorflow графа", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(this.Close));
            }
        }

        //private void InitEgmuTF()
        //{
        //    this.imgRecognizer = new TensorflowRecognizer(this.GraphPath, this.LabelsPath, this.HandleSymbol, tmpImageFileName, SaveSharedImage,
        //    this.ProbabilityQueueSize, this.SkipFrames);

        //    this.imgRecognizer.Start();
        //}

        private void HandleSymbol(string symbol, float probability)
        {
            this.Invoke(new Action(() =>
            {
                //this.imgRecognizer.Stop();
                Debug.WriteLine(String.Format("Object is {0} with {1}% probability.", symbol, probability));
                //InitEgmuTF();

                if (probability >= Settings.GetImageRecognizedThresold())
                {
                    Font f = new Font("Arial", 24, FontStyle.Bold);
                    var img = DrawText(this.translatedFolders[symbol], f, Color.Black, Color.White);
                    this.pictureBoxTranslatedGesture.Image = img;
                }
            }));
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (this.imgRecognizer != null)
            {
                this.imgRecognizer.Stop();
            }

            if (this.frameReader != null)
            {
                frameReader.Dispose();
            }

            if (this.sensor != null)
            {
                sensor.Close();
            }

            if (File.Exists(this.tmpImageFileName))
            {
                File.Delete(this.tmpImageFileName);
            }
        }

        void frameReader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            try
            {
                MultiSourceFrame frame = e.FrameReference.AcquireFrame();

                if (frame != null)
                {
                    try
                    {
                        ColorFrameReference colorFrameReference = frame.ColorFrameReference;
                        useRGBAImage(colorFrameReference);

                        // Накладные вычисления позиции жеста, будем статично отрисовывать прямоугольник для жеста =(
                        //// Body
                        //using (var bodyFrame = frame.BodyFrameReference.AcquireFrame())
                        //{
                        //    if (bodyFrame != null)
                        //    {
                        //        _bodies = new Body[bodyFrame.BodyFrameSource.BodyCount];

                        //        bodyFrame.GetAndRefreshBodyData(_bodies);

                        //        foreach (var body in _bodies)
                        //        {
                        //            if (body.IsTracked)
                        //            {
                        //                var joint = body.Joints[JointType.HandRight];

                        //                if (joint.TrackingState == TrackingState.Tracked)
                        //                {
                        //                    // 3D space point
                        //                    CameraSpacePoint jointPosition = joint.Position;
                        //                    Image gesture;
                        //                    RectangleF colorRectabgle = GetGestureFromJointPosition(jointPosition, out gesture);

                        //                    // color
                        //                    if (pictureBoxCameraColor.Image != null)
                        //                    {
                        //                        var gf = Graphics.FromImage(pictureBoxCameraColor.Image);
                        //                        gf.DrawRectangle(new Pen(Color.Red, 2),
                        //                            colorRectabgle.Location.X, colorRectabgle.Location.Y, colorRectabgle.Width, colorRectabgle.Height);
                        //                    }

                        //                    this.pictureBoxGesture.Image = gesture;
                        //                    this.btnAddGesture.Enabled = true;
                        //                    this.btnAddGesture.Focus();
                        //                } else
                        //                {
                        //                    this.btnAddGesture.Enabled = false;
                        //                    this.pictureBoxGesture.Image = null;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                        // правой руки нет в камере - отменяем слежку (отмена, вдруг поднесут изображение, в рамках курсовой пусть будет так)

                        //var bodyFrames = frame.BodyFrameReference.AcquireFrame();
                        //if (bodyFrames != null)
                        //{
                        //    var bodies = new Body[bodyFrames.BodyFrameSource.BodyCount];
                        //    bodyFrames.GetAndRefreshBodyData(bodies);
                        //    var bodyFrame = bodies.FirstOrDefault();
                        //    if (bodyFrame == null || !bodyFrame.IsTracked || bodyFrame.Joints[JointType.HandRight].TrackingState != TrackingState.Tracked)
                        //    {
                        //        this.probabilityFrameCounter = 0;
                        //        this.probabilities.Clear();
                        //    }
                        //    else

                        // ...

                        // В зависимости от освещения, Kinect V2 Color Stream переклюяается в 15 fps и 30 fps.
                        //int fps = (frame.ColorFrameReference.RelativeTime- previousFrameTimeCapture).Seconds;
                        //previousFrameTimeCapture = frame.ColorFrameReference.RelativeTime;

                        //будем расчитывать на max - 30 fps.


                        //    var frameID = this.probabilityFrameCounter++;
                        //    Stopwatch watch = Stopwatch.StartNew();

                            // dont use tensorflowahrp gpu or cpu, instead we gonna use emgu.tf cpu with ~500 ms per frame



                            //using (var g = new TFGraph())
                            //{
                            //    g.Import(new TFBuffer(this.modelFile));

                            //    using (var session = new TFSession(g))
                            //    {
                            //        var g_input = g["input"][0];
                            //        var g_output = g["final_result"][0];
                            //        var runner = session.GetRunner();

                            //        //var bitmap = ImageUtil.ResizeImage(img, 224, 224);
                            //        //
                            //        var bitmap = new Bitmap(@"C:\Users\inter\OneDrive\Documents\Visual Studio 2015\Projects\SignLanguageTranslater\SignLanguageTrainer\bin\Debug\TrainImages\a\2018-05-06_04-48-09-3319.jpg");
                            //        var tensor = ImageUtil.CreateTensorFromImageFile(bitmap);

                            //        runner.AddInput(g_input, tensor).Fetch(g_output);
                            //        var output = runner.Run();

                            //        var bestIdx = 0;
                            //        float best = 0;
                            //        var result = output[0];
                            //        var rshape = result.Shape;
                            //        var probabilities = ((float[][])result.GetValue(jagged: true))[0];
                            //        watch.Stop();
                            //        Console.WriteLine($"time took = {watch.Elapsed.Milliseconds}");

                            //        for (int r = 0; r < probabilities.Length; r++)
                            //        {
                            //            if (probabilities[r] > best)
                            //            {
                            //                bestIdx = r;
                            //                best = probabilities[r];
                            //            }
                            //        }

                            //        foreach (var t in output)
                            //        {
                            //            t.Dispose();
                            //        }

                            //        tensor.Dispose();

                            //        bitmap.Dispose();
                                 

                            //        Debug.WriteLine("Tensorflow thinks this is: " + this.labelsFile[bestIdx] + " Prob : " + best * 100);
                            //    }
                            //}
                        }

                        //if (frameID % 150 == 0)
                        //{
                        //    var img = this.pictureBoxCameraColor.Image.Clone() as Image;
                          //  StartRecognitionThread(img, frameID,
                            //    new Action<Dictionary<string, float>>(CollectImageRecognizes));
                        //}
                    //}
                    catch (Exception ex)
                    {
                        var s = ex;
                        
                        // Nothing...
                    }
                }
            }
            catch (Exception)
            {
                // Nothing...
            }
        }

        //public Thread StartRecognitionThread(Image img, uint frameID, Action<Dictionary<string, float>> callback)
        //{
        //    var t = new Thread(() => ThreadRecognition(img, frameID, callback));
        //    t.Priority = ThreadPriority.Highest;
        //    t.IsBackground = true;
        //    t.Start();
        //    return t;
        //}

        //private void ThreadRecognition(Image img, uint frameID, Action<Dictionary<string, float>> callback)
        //{
        //    if (img != null)
        //    {
        //        var probabilities = new Dictionary<string, float>();
        //        var rec = new Rectangle(img.Width / 2 + img.Width / 8 + 2, img.Height / 2 - img.Height / 4 + 2, img.Width / 4 - 4, img.Height / 4 - 4);
        //        var gesture = new Bitmap(rec.Width, rec.Height);

        //        using (var g = Graphics.FromImage(gesture))
        //        {
        //            g.DrawImage(img, new Rectangle(0, 0, gesture.Width, gesture.Height),
        //                                rec,
        //                                GraphicsUnit.Pixel);
        //        }

        //        var tmpGesturePath = Path.Combine("tmp", $"image_thread_{frameID}");

        //        gesture.Save(tmpGesturePath, ImageFormat.Jpeg);

        //        Tensor imageTensor = ImageIO.ReadTensorFromImageFile(tmpGesturePath, 299, 299, 250.0f, 1.0f / 250.0f);

        //        var inceptionGraph = new Inception(null, new string[] { this.GraphPath, this.LabelsPath }, null, "Mul", "final_result");

        //        float[] probability = inceptionGraph.Recognize(imageTensor);

        //        for (int index = 0; index < probability.Length; ++index)
        //        {
        //            probabilities.Add(inceptionGraph.Labels[index], probability[index]);
        //        }

        //        callback(probabilities);
        //    }
        //}


        /// <summary>
        /// Метод отрисовки обычной камеры из цветного фрейма
        /// </summary>
        /// <param name="frameReference">Ссылка на цветной фрейм.</param>
        private void useRGBAImage(ColorFrameReference frameReference)
        {
            // Попытка получить текущий фрейм с сенсора
            ColorFrame frame = frameReference.AcquireFrame();

            if (frame != null)
            {
                Bitmap outputImage = null;
                BitmapData imageData = null;

                // Теперь получаем описание фрейма и создаем изображение для color picture box
                FrameDescription description = frame.FrameDescription;
                outputImage = new Bitmap(description.Width, description.Height, PixelFormat.Format32bppArgb);

                // Далее создаем указатель на данные картинки и указываем будующий размер
                imageData = outputImage.LockBits(new Rectangle(0, 0, outputImage.Width, outputImage.Height),
                    ImageLockMode.WriteOnly, outputImage.PixelFormat);
                IntPtr imageDataPtr = imageData.Scan0;
                int size = imageData.Stride * outputImage.Height;

                using (frame)
                {
                    // Копируем данные изображения в буфер. Не забываем что имеем дело с BGRA форматом
                    if (frame.RawColorImageFormat == ColorImageFormat.Bgra)
                    {
                        frame.CopyRawFrameDataToIntPtr(imageDataPtr, (uint)size);
                    }
                    else
                    {
                        frame.CopyConvertedFrameDataToIntPtr(imageDataPtr, (uint)size, ColorImageFormat.Bgra);
                    }
                }

                // Наконец создаем картинку из буфера. 
                outputImage.UnlockBits(imageData);

                var img = drawGestureStaticRectabgle(outputImage);

                this.pictureBoxCameraColor.Image = img;

                this.sharedImage = img.Clone() as Image;
            }
            else
            {
                // Nothing...
            }
        }

        private bool SaveSharedImage()
        {
            bool flag = false;
            this.Invoke(new Action(() => {
                if (this.sharedImage != null)
                {
                    Image img = this.sharedImage;
                    Rectangle rec = new Rectangle(img.Width / 2 + img.Width / 8 + 2, img.Height / 2 - img.Height / 4 + 2, img.Width / 8 + img.Width / 8 / 8 - 4, img.Height / 4 - 4);
                    Bitmap target = new Bitmap(rec.Width, rec.Height);

                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.DrawImage(img, new Rectangle(0, 0, target.Width, target.Height),
                                         rec,
                                         GraphicsUnit.Pixel);
                    }

 

                    ImageUtil.ResizeImage(target, 224, 224).Save(this.tmpImageFileName, ImageFormat.Jpeg);
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }));

            return flag;
        }

        private Image drawGestureStaticRectabgle(Image img)
        {
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(img.Width / 2 + img.Width / 8, img.Height / 2 - img.Height / 4, img.Width / 8 + img.Width / 8 / 8, img.Height / 4));
            }

            return img;
        }

        private Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}
