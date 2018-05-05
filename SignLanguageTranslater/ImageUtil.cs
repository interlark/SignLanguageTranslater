using System.Drawing;
using System.IO;
using TensorFlow;

namespace SignLanguageTranslater
{
    public static class ImageUtil
    {
        // Convert the image in filename to a Tensor suitable as input to the Inception model.
        public static TFTensor CreateTensorFromImageFile(Image img, TFDataType destinationDataType = TFDataType.Float)
        {
            ImageConverter _imageConverter = new ImageConverter();
            var contents = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));

            // DecodeJpeg uses a scalar String-valued tensor as input.
            var tensor = TFTensor.CreateString(contents);

            TFOutput input, output;

            // Construct a graph to normalize the image
            using (var graph = ConstructGraphToNormalizeImage(out input, out output, destinationDataType))
            {
                // Execute that graph to normalize this one image
                using (var session = new TFSession(graph))
                {
                    var normalized = session.Run(
                        inputs: new[] { input },
                        inputValues: new[] { tensor },
                        outputs: new[] { output });

                    return normalized[0];
                }
            }
        }

        // The inception model takes as input the image described by a Tensor in a very
        // specific normalized format (a particular image size, shape of the input tensor,
        // normalized pixel values etc.).
        //
        // This function constructs a graph of TensorFlow operations which takes as
        // input a JPEG-encoded string and returns a tensor suitable as input to the
        // inception model.
        private static TFGraph ConstructGraphToNormalizeImage(out TFOutput input, out TFOutput output, TFDataType destinationDataType = TFDataType.Float)
        {
            const int W = 299;
            const int H = 299;
            const float Mean = 170.0f; ;
            const float Scale = 1;
            var graph = new TFGraph();
            input = graph.Placeholder(TFDataType.String);
            output = graph.Cast(graph.Div(
                x: graph.Sub(
                    x: graph.ResizeBilinear(
                        images: graph.ExpandDims(
                            input: graph.Cast(
                                graph.DecodeJpeg(contents: input, channels: 3), DstT: TFDataType.Float),
                            dim: graph.Const(0, "make_batch")),
                        size: graph.Const(new int[] { W, H }, "size")),
                    y: graph.Const(Mean, "mean")),
                y: graph.Const(Scale, "scale")), destinationDataType);

            return graph;
        }
    }
}
