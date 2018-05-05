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

namespace SignLanguageTrainer
{
    public partial class CaptureGesturesForm : Form
    {
        public string Path { get; set; }

        KinectSensor sensor = null;
        MultiSourceFrameReader frameReader = null;
        //IList<Body> _bodies;

        /// <summary>
        /// Необработанный массив данных полученный через глубинный сенсор.
        /// </summary>
        private ushort[] rawDepthPixelData = null;
        /// <summary>
        /// Необработанный массив данных полученный через инфракрасный сенсор.
        /// </summary>
        private ushort[] rawIRPixelData = null;

        private CameraSpacePoint[] csp = null;

        private float depthThreshold;

        public CaptureGesturesForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!string.IsNullOrEmpty(this.Path) && Directory.Exists(this.Path))
            {

                // Проверка подключено ли устройство
                if (KinectSensor.GetDefault() != null)
                {
                    this.depthThreshold = Settings.GetGestureDepthThresold();
                    this.sensor = KinectSensor.GetDefault();

                    // Проверка устройства готовность
                    if (this.sensor != null)
                    {
                        if ((this.sensor.KinectCapabilities & KinectCapabilities.Vision) == KinectCapabilities.Vision)
                        {
                            this.sensor.Open();

                            // Открываем multi-source reader фреймов для сенсоров
                            this.frameReader = this.sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);

                            // Получаем описание сенсоров
                            // Kinect version 2:
                            // An RGB color camera – 640×480 in version 1, 1920×1080 in version 2
                            // A depth sensor – 320×240 in v1, 512×424 in v2
                            // An infrared sensor – 512×424 in v2
                            FrameDescription colorFrameDescription = this.sensor.ColorFrameSource.FrameDescription;
                            FrameDescription depthFrameDescription = this.sensor.DepthFrameSource.FrameDescription;
                            FrameDescription irFrameDescription = this.sensor.InfraredFrameSource.FrameDescription;

                            // Устанавливаем размерность глубинного и инфракрасного массива чистых данных.
                            // ushort = 2 bytes per pixel
                            this.rawDepthPixelData = new ushort[depthFrameDescription.Width * depthFrameDescription.Height * 1];
                            this.rawIRPixelData = new ushort[irFrameDescription.Width * irFrameDescription.Height * 1];
                            this.csp = new CameraSpacePoint[colorFrameDescription.Width * colorFrameDescription.Height * 1];
                            // Указываем callback для полученных кадров из framereader'а
                            this.frameReader.MultiSourceFrameArrived += frameReader_MultiSourceFrameArrived;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Не указана папка для сохранения жестов", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        DepthFrameReference depthFrameReference = frame.DepthFrameReference;
                        ShowDepthImage(depthFrameReference);
                        InfraredFrameReference irFrameReference = frame.InfraredFrameReference;
                        ShowIRImage(irFrameReference);

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
                        Rectangle rec = new Rectangle(img.Width / 2 + img.Width / 8 + 2, img.Height / 2 - img.Height / 4 + 2, img.Width / 8 + img.Width / 8 / 8 - 4, img.Height / 4 - 4);
                        Bitmap target = new Bitmap(rec.Width, rec.Height);

                        using (Graphics g = Graphics.FromImage(target))
                        {
                            g.DrawImage(img, new Rectangle(0, 0, target.Width, target.Height),
                                             rec,
                                             GraphicsUnit.Pixel);
                        }

                        var gesture = target;
                        this.pictureBoxGesture.Image = gesture;
                    }
                    catch (Exception)
                    {
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

        /// <summary>
        /// Метод отрисовки глубинной камеры из глубинного фрейма.
        /// </summary>
        /// <param name="frameReference">Ссылка на глубинный фрейм.</param>
        private void ShowDepthImage(DepthFrameReference frameReference)
        {
            // Попытка получить текущий фрейм с сенсора
            DepthFrame frame = frameReference.AcquireFrame();

            if (frame != null)
            {
                FrameDescription description = null;
                Bitmap outputImage = null;
                using (frame)
                {
                    // Теперь получаем описание фрейма и создаем изображение для depth picture box
                    description = frame.FrameDescription;
                    outputImage = new Bitmap(description.Width, description.Height, PixelFormat.Format32bppArgb);

                    // Далее создаем указатель на данные картинки и указываем будующий размер
                    BitmapData imageData = outputImage.LockBits(new Rectangle(0, 0, outputImage.Width, outputImage.Height),
                        ImageLockMode.WriteOnly, outputImage.PixelFormat);
                    IntPtr imageDataPtr = imageData.Scan0;
                    int size = imageData.Stride * outputImage.Height;

                    // Теперь копируем изображение в соответствующий массив. Смещаем каждый RGB пиксел к byte размеру
                    frame.CopyFrameDataToArray(this.rawDepthPixelData);
                    byte[] rawData = new byte[description.Width * description.Height * 4];
                    int i = 0;
                    foreach (ushort point in this.rawDepthPixelData)
                    {
                        rawData[i++] = (byte)(point >> 6);
                        rawData[i++] = (byte)(point >> 4);
                        rawData[i++] = (byte)(point >> 2);
                        rawData[i++] = 255;
                    }

                    // Наконец создаем картинку из буфера
                    System.Runtime.InteropServices.Marshal.Copy(rawData, 0, imageDataPtr, size);
                    outputImage.UnlockBits(imageData);
                }

                this.pictureBoxDeepCamera.Image = drawGestureStaticRectabgle(outputImage);
            }
        }

        /// <summary>
        /// Метод отрисовки инфракрасной камеры из инфракрасного фрейма.
        /// </summary>
        /// <param name="frameReference">Ссылка на инфракрасный фрейм.</param>
        private void ShowIRImage(InfraredFrameReference frameReference)
        {
            // Попытка получить текущий фрейм с сенсора
            InfraredFrame frame = frameReference.AcquireFrame();

            if (frame != null)
            {
                FrameDescription description = null;
                using (frame)
                {
                    // Теперь получаем описание фрейма и создаем изображение для infrare picture box
                    description = frame.FrameDescription;
                    Bitmap outputImage = new Bitmap(description.Width, description.Height, PixelFormat.Format32bppArgb);

                    // Далее создаем указатель на данные картинки и указываем будующий размер
                    System.Drawing.Imaging.BitmapData imageData = outputImage.LockBits(new Rectangle(0, 0, outputImage.Width, outputImage.Height),
                        ImageLockMode.WriteOnly, outputImage.PixelFormat);
                    IntPtr imageDataPtr = imageData.Scan0;
                    int size = imageData.Stride * outputImage.Height;

                    // Теперь копируем изображение в соответствующий массив. Смещаем каждый RGB пиксел к byte размеру, получая grayscale изображение
                    frame.CopyFrameDataToArray(this.rawIRPixelData);
                    byte[] rawData = new byte[description.Width * description.Height * 4];

                    for (int i = 0; i < this.rawIRPixelData.Length; i++)
                    {
                        byte intensity = (byte)(this.rawIRPixelData[i] >> 8);

                        rawData[i * 4] = intensity;
                        rawData[i * 4 + 1] = intensity;
                        rawData[i * 4 + 2] = intensity;
                        rawData[i * 4 + 3] = 255;
                    }

                    // Наконец создаем картинку из буфера
                    System.Runtime.InteropServices.Marshal.Copy(rawData, 0, imageDataPtr, size);
                    outputImage.UnlockBits(imageData);

                    // Test: Немного яркости для выхода IRSensor.
                    this.pictureBoxInfraredCamera.Image = drawGestureStaticRectabgle(AdjustBrightness(outputImage, 8.0f)); // 8.0f для работы от полуметра, как заявлено в документации Kinect v2
                }
            }
        }

        /// <summary>
        /// Get gesture from joint position (with depth threshold)
        /// </summary>
        /// <param name="jointPosition">Позиция правой руки</param>
        /// <param name="threshold">Порог захвата жеста, не больше 2 байт</param>
        /// <param name="colorRectangle">Прямоугольник для цветной камеры</param>
        /// <param name="depthAndInrareRectangle">Прямоугольник для глубинной и инфракрасной камеры</param>
        /// <returns></returns>
        private RectangleF GetGestureFromJointPosition(CameraSpacePoint jointPosition, out Image gesture)
        {
            int xMin = 0, xMax = 0, yMin = 0, yMax = 0;

            // color
            this.sensor.CoordinateMapper.MapColorFrameToCameraSpace(this.rawDepthPixelData, this.csp);
            var colorSpacePoint = this.sensor.CoordinateMapper.MapCameraPointToColorSpace(jointPosition);
            float depthPosition = this.csp[(1920 * Convert.ToUInt16(colorSpacePoint.Y)) + Convert.ToUInt16(colorSpacePoint.X)].Z;

            xMin = xMax = Convert.ToUInt16(colorSpacePoint.X);
            yMin = yMax = Convert.ToUInt16(colorSpacePoint.Y);

            float depthThreshold = depthPosition + this.depthThreshold;
            for (int i = 0; i < this.csp.Length; ++i)
            {
                if (this.csp[i].Z > depthThreshold)
                {
                    this.csp[i].Z = float.NegativeInfinity;
                }
            }

            GetBoundForGesture(depthPosition, ref xMin, ref xMax, ref yMin, ref yMax,
                Convert.ToUInt16(colorSpacePoint.X), Convert.ToUInt16(colorSpacePoint.Y));

            ColorSpacePoint colorPointXMinYMin = sensor.CoordinateMapper.MapCameraPointToColorSpace(this.csp[1920 * yMin + xMin]);
            ColorSpacePoint colorPointXMaxYMax = sensor.CoordinateMapper.MapCameraPointToColorSpace(this.csp[1920 * yMax + xMax]);

            var res = new RectangleF(colorPointXMinYMin.X, colorPointXMinYMin.Y, colorPointXMaxYMax.X - colorPointXMinYMin.X, colorPointXMaxYMax.Y - colorPointXMinYMin.Y);

            if (this.pictureBoxCameraColor.Image != null)
            {
                Bitmap src = this.pictureBoxCameraColor.Image as Bitmap;
                Bitmap target = new Bitmap((int)res.Width, (int)res.Height);

                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                     res,
                                     GraphicsUnit.Pixel);
                }

                gesture = target;
            }
            else
            {
                gesture = null;
            }


            return res;
        }

        private void GetBoundForGesture(float depthPosition,
            ref int xMin, ref int xMax, ref int yMin, ref int yMax,
            int x, int y)
        {
            //int maxIterations = 256 << 16;

            // обход

            // let's say go bottom

            var currentDepth = float.NegativeInfinity;
            do
            {
                currentDepth = this.csp[1920 * ++y + x].Z;
            } while (currentDepth != float.NegativeInfinity);

            --y;

            // (x,y) - the bottom of gesture

            var bottomOfGesture = new Point(x, y);

            //var i = 0;

            do
            {
                //clockwise moving

                if (((this.csp[1920 * (y + 1) + x - 1].Z) != float.NegativeInfinity) && ((this.csp[1920 * (y + 2) + x].Z) == float.NegativeInfinity))
                {
                    --x;
                    ++y;
                }
                else if (((this.csp[1920 * y + x - 1].Z) != float.NegativeInfinity) && ((this.csp[1920 * (y + 1) + x - 1].Z) == float.NegativeInfinity))
                {
                    --x;
                }
                else if (((this.csp[1920 * (y - 1) + x - 1].Z) != float.NegativeInfinity) && ((this.csp[1920 * y + x - 2].Z) == float.NegativeInfinity))
                {
                    --x;
                    --y;
                }
                else if (((this.csp[1920 * (y - 1) + x].Z) != float.NegativeInfinity) && ((this.csp[1920 * (y - 1) + x - 1].Z) == float.NegativeInfinity))
                {
                    --y;
                }
                else if (((this.csp[1920 * (y - 1) + x + 1].Z) != float.NegativeInfinity) && ((this.csp[1920 * (y - 2) + x].Z) == float.NegativeInfinity))
                {
                    --y;
                    ++x;
                }
                else if (((this.csp[1920 * y + x + 1].Z) != float.NegativeInfinity) && ((this.csp[1920 * (y - 1) + x + 1].Z) == float.NegativeInfinity))
                {
                    ++x;
                }
                else if (((this.csp[1920 * (y + 1) + x + 1].Z) != float.NegativeInfinity) && ((this.csp[1920 * y + x + 2].Z) == float.NegativeInfinity))
                {
                    ++x;
                    ++y;
                }
                else if (((this.csp[1920 * (y + 1) + x].Z) != float.NegativeInfinity) && ((this.csp[1920 * (y + 1) + x + 1].Z) == float.NegativeInfinity))
                {
                    ++y;
                }
                else
                {
                    //something shitty has happened
                }

                if (x < xMin) { xMin = x; }
                if (x > xMax) { xMax = x; }
                if (y < yMin) { yMin = y; }
                if (y > yMax) { yMax = y; }

            } while ((x != bottomOfGesture.X || y != bottomOfGesture.Y)/* && ++i < maxIterations*/);
        }

        // Adjust the image's brightness.
        private Bitmap AdjustBrightness(Image image, float brightness)
        {
            // Make the ColorMatrix.
            float b = brightness;
            ColorMatrix cm = new ColorMatrix(new float[][]
                {
            new float[] {b, 0, 0, 0, 0},
            new float[] {0, b, 0, 0, 0},
            new float[] {0, 0, b, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1},
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while applying
            // the new ColorMatrix.
            Point[] points =
            {
        new Point(0, 0),
        new Point(image.Width, 0),
        new Point(0, image.Height),
    };
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect,
                    GraphicsUnit.Pixel, attributes);
            }

            return bm;
        }

        private void btnAddGesture_Click(object sender, EventArgs e)
        {
            if (pictureBoxGesture.Image != null)
            {
                var sp = new SoundPlayer(Properties.Resources.shoot);
                sp.Play();

                var filename = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-ffff") + ".jpg";
                var path = System.IO.Path.Combine(this.Path, filename);
                new Bitmap(this.pictureBoxGesture.Image.Clone() as Image).Save(path, ImageFormat.Jpeg);
            }
        }

        private Image drawGestureStaticRectabgle(Image img)
        {
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(img.Width / 2 + img.Width / 8, img.Height / 2 - img.Height / 4, img.Width / 8 + img.Width / 8 / 8, img.Height / 4));
            }

            return img;
        }
    }
}
