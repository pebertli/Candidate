using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Core
{

    /// <summary>
    /// Super important: consider a distance where the value 1 to 2 is the same than 1 to 5
    /// </summary>
    public enum DISTANCE_FUNCTION
    {
        EUCLIDIAN = 0,
        MANHATTAN = 1
    };

    public class KNNPrediction
    {
        public int NumFeatures { get; set; }                
        public int NumClasses { get; set; }
        public float[][] TrainData { get; set; }
        public float MaxDistance { get; set; }

        private int TrainDataSize;

        public KNNPrediction(int numFeatures, int numClasses, float[][] trainData, float min, float max)
        {
            NumFeatures = numFeatures;
            NumClasses = numClasses;
            TrainData = trainData;

            TrainDataSize = TrainData.Length;

            MaxDistance = (max - min) * (max - min);
        }

        //static void Main(string[] args)
        //{
        //    Console.WriteLine("\nBegin k-NN classification Candidate\n");
            
        //    trainData = LoadData();  // get the normalized data
           
        //    double[] unknown = new double[] { 3, -1, -1, -1, -1 }; //size of array: questions / value: assertive
        //    Console.WriteLine("Classifying item with predictor values: ");

        //    for (int i = 0; i < unknown.Length; i++)
        //    {
        //        Console.Write(unknown[i] + " ");
        //    }

        //    int k = 1;
        //    Console.WriteLine("With k = "+k);
        //    int predicted = Classify(unknown, trainData, numClasses, k);

        //    Console.WriteLine("End k-NN demo \n");
        //    Console.ReadLine();
        //} // Main

        private IndexAndDistance[] Classify(float[] unknown, int k)
        {
            // compute and store distances from unknown to all train data             
            IndexAndDistance[] info = new IndexAndDistance[TrainDataSize];
            //store distance of test sample to each training sample
            for (int i = 0; i < TrainDataSize; ++i)
            {
                IndexAndDistance curr = new IndexAndDistance();
                float dist = Distance(unknown, TrainData[i], DISTANCE_FUNCTION.EUCLIDIAN);
                curr.idx = i;
                curr.dist = dist;
                info[i] = curr;
            }

            return info;
        }

        public float[] ClassifyRanked(float[] unknown, int k)
        {
            IndexAndDistance[] info = Classify(unknown, k);
            Array.Sort(info);  // sort by distance
            float[] ranking = LessDistanceInEachClass(info);

            return ranking;
        }

        public int ClassifySingle(float[] unknown, int k)
        {
            IndexAndDistance[] info = Classify(unknown, k);
            Array.Sort(info);  // sort by distance
            int result = Vote(info, k);  // k nearest classes            

            return result;

        } 


        float[] LessDistanceInEachClass(IndexAndDistance[] info)
        {
            float[] ret = new float[NumClasses];
            for (int j = 0; j < NumClasses; j++) // each class
            {
                float less = float.MaxValue;
                for (int i = 0; i < info.Length; i++) //each pair training-test
                {
                    
                    if(Math.Round(TrainData[info[i].idx][NumFeatures]) == j)//the pair training-test is from the class
                    {
                        if(info[i].dist<less)
                        {
                            less = info[i].dist;
                        }
                    }                    
                }
                ret[j] = less;
            }
            

            return ret;
        }


        int Vote(IndexAndDistance[] info, int k)
        {
            int[] votes = new int[NumClasses];  // one cell per class
            for (int i = 0; i < k; ++i)  // just first k nearest
            {
                int idx = info[i].idx;  // which item
                int c = (int)TrainData[idx][NumFeatures];  // class in last cell
                ++votes[c];
            }       

            int mostVotes = 0;
            int classWithMostVotes = 0;
            for (int j = 0; j < NumClasses; ++j)
            {
                if (votes[j] > mostVotes)
                {
                    mostVotes = votes[j];
                    classWithMostVotes = j;
                }
            }

            return classWithMostVotes;
        }

        float Distance(float[] unknown, float[] data, DISTANCE_FUNCTION df)
        {

            if (df == DISTANCE_FUNCTION.EUCLIDIAN)
            {
                double sum = 0.0;
                for (int i = 0; i < unknown.Length; ++i)
                {
                    float weight = 1;
                    //if (i == 0)
                    //    weight = 10;
                    //else 
                    if ( unknown[i] == -1f) // remove distance contribution                                            
                        sum += weight * MaxDistance;
                    else
                        sum += weight * (unknown[i] - data[i]) * (unknown[i] - data[i]);           
                    
                }
                return (float)Math.Sqrt(sum);
            }
            else if (df == DISTANCE_FUNCTION.MANHATTAN)
                    {
                double sum = 0.0;
                for (int i = 0; i < unknown.Length; ++i)
                {
                    if(i == 0)
                        sum += Math.Abs(unknown[i] - data[i]);
                }
            }

            return 0;
        }

        //static double[][] LoadData()
        //{
        //    double[][] data = new double[3][];//[candidate][question]
        //    //5 features values plus class index
        //    data[0] = new double[] { 2.0, 4.0, 5, 1, 2, 0 };
        //    data[1] = new double[] { 5.0, 5.0, 1, 1, 2, 1 };
        //    data[2] = new double[] { 3.0, 5.0, 2, 1, 1, 2 };         

        //    return data;
        //}

    } // Program

    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        public int idx;  // index of a training item
        public float dist;  // distance from train item to unknown

        // need to sort these to find k closest
        public int CompareTo(IndexAndDistance other)
        {
            if (this.dist < other.dist) return -1;
            else if (this.dist > other.dist) return +1;
            else return 0;
        }
    }
}
