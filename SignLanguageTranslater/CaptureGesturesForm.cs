using System;
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

namespace SignLanguageTranslater
{
    public partial class CaptureGesturesForm : Form
    {
        public string GraphPath { get; set; }
        public string LabelsPath { get; set; }
        public int SkipFrames { get; set; } = 0;

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
                                this.frameReader = this.sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color/* | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body*/);

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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (frameReader != null)
            {
                frameReader.Dispose();
            }

            if (sensor != null)
            {
                sensor.Close();
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

                        Bitmap img = this.pictureBoxCameraColor.Image as Bitmap;
                        if (img != null)
                        {
                            Rectangle rec = new Rectangle(img.Width / 2 + img.Width / 8 + 2, img.Height / 2 - img.Height / 4 + 2, img.Width / 4 - 4, img.Height / 4 - 4);
                            Bitmap target = new Bitmap(rec.Width, rec.Height);

                            using (Graphics g = Graphics.FromImage(target))
                            {
                                g.DrawImage(img, new Rectangle(0, 0, target.Width, target.Height),
                                                 rec,
                                                 GraphicsUnit.Pixel);
                            }

                            var gesture = target;

                            //todo: recognition

                        }
                    }
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
                this.pictureBoxCameraColor.Image = drawGestureStaticRectabgle(outputImage);
            }
            else
            {
                // Nothing...
            }
        }

        private Image drawGestureStaticRectabgle(Image img)
        {
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(img.Width / 2 + img.Width / 8, img.Height / 2 - img.Height/4, img.Width / 4, img.Height / 4));
            }

            return img;
        }
    }
}
