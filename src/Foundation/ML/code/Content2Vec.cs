using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using Word2vec.Tools;

namespace Hackathon.Boilerplate.Foundation.ML
{
    public static class Content2Vec
    {
        private static readonly string fileName = "GoogleNews.bin";
        static Vocabulary VocabularyModel { get; }

        static Content2Vec()
        {
            if (VocabularyModel == null)
            {
                var path=  HostingEnvironment.ApplicationPhysicalPath + "App_Data\\"+fileName;
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException(nameof(fileName));

                if (!File.Exists(path))
                    throw new FileNotFoundException("Bin file is not found", path);

                VocabularyModel = new Word2VecBinaryReader().Read(path);
            }
        }

        public static double[] Vectorization(string content)
        {
            var words = TextHelper.GetWords(content);
            var vector = new double[VocabularyModel.VectorDimensionsCount];

            int inVocabularyCount = 0;
            List<Tuple<string, double>> dic = new List<Tuple<string, double>>();

            foreach (string word in words)
            {
                try
                {
                    float[] wordVector = VocabularyModel.GetRepresentationFor(word).NumericVector;
                    double metric = VocabularyModel.GetRepresentationFor(word).MetricLength;
                    dic.Add(new Tuple<string, double>(word, metric));

                    for (int i = 0; i < VocabularyModel.VectorDimensionsCount; i++)
                    {
                        vector[i] += (double)wordVector[i];
                    }
                    inVocabularyCount++;
                }
                catch
                {
                    // ignore if not in vocabulary
                }
            }

            // average of the vectors 
            for (int i = 0; i < VocabularyModel.VectorDimensionsCount; i++)
            {
                vector[i] /= inVocabularyCount;
            }

            return vector;
        }

        public static IEnumerable<ContentObject> Nearest(ContentObject primary, List<ContentObject> other)
        {
            List<ContentObject> calculatedVectors = new List<ContentObject>();
            foreach (var o in other)
            {
                o.Distance = CosineDistance(primary.TextVector, o.TextVector);
                calculatedVectors.Add(o);
            }

            return calculatedVectors.OrderBy(x => x.Distance).Take(5);
        }

        static float CosineDistance(float[] x, float[] y)
        {
            float num1 = 0;
            float d1 = 0;
            float d2 = 0;
            for (int index = 0; index < x.Length; ++index)
            {
                num1 += x[index] * y[index];
                d1 += x[index] * x[index];
                d2 += y[index] * y[index];
            }
            float num2 = (float)Math.Sqrt(d1) * (float)Math.Sqrt(d2);
            if (num1 != 0)
                return 1 - num1 / num2;
            return 1;
        }
    }
}