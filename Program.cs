using System;

namespace Homework_5_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            string locationOfImport = Console.ReadLine();
            string locationOfConvolutionKernel = Console.ReadLine();
            string locationOResult = Console.ReadLine();

            double[,] Import = ReadImageDataFromFile(locationOfImport);
            double[,] ConvolutionKernel = ReadImageDataFromFile(locationOfConvolutionKernel);

            double[,] repeatedTextureImport = new double[Import.GetLength(0)+2,Import.GetLength(1)+2];
            for(int i =0;i < Import.GetLength(0); i++)
            {
                for(int j = 0; j < Import.GetLength(1); j++)
                {
                    repeatedTextureImport[i+1, j+1] = Import[i,j];

                }
            }
            for (int i = 0; i < Import.GetLength(0); i++)
            {
                repeatedTextureImport[Import.GetLength(0) + 1, i+1] = Import[0, i];
                repeatedTextureImport[0, i+1] = Import[4, i];
            }
            for (int i = 0; i < Import.GetLength(1); i++)
            {
                repeatedTextureImport[i+1, Import.GetLength(1) + 1] = Import[i, 0];
                repeatedTextureImport[i+1, 0] = Import[i, 4];
            }
            repeatedTextureImport[0,0] = Import[4,4];
            repeatedTextureImport[0,6] = Import[4,0];
            repeatedTextureImport[6,0] = Import[0,4];
            repeatedTextureImport[6,6] = Import[0,0];

            double [,] outputImageData = new double[Import.GetLength(0), Import.GetLength(1)];
            for(int  i = 0;i< Import.GetLength(0); i++)
            {
                for (int j = 0; j < Import.GetLength(1); j++)
                {
                    outputImageData[i, j] = repeatedTextureImport[i     , j  ] * ConvolutionKernel[1-1 , 1-1   ] +
                                            repeatedTextureImport[i     , j+1] * ConvolutionKernel[1   , 1     ] +
                                            repeatedTextureImport[i     , j+2] * ConvolutionKernel[1   , 1+1   ] +
                                            repeatedTextureImport[i+1   , j  ] * ConvolutionKernel[1   , 1-1   ] +
                                            repeatedTextureImport[i+1   , j+1] * ConvolutionKernel[1   , 1     ] + //centre
                                            repeatedTextureImport[i+1   , j+2] * ConvolutionKernel[1   , 1+1   ] +
                                            repeatedTextureImport[i+2   , j  ] * ConvolutionKernel[1+1 , 1-1   ] +
                                            repeatedTextureImport[i+2   , j+1] * ConvolutionKernel[1+1 , 1     ] +
                                            repeatedTextureImport[i+2   , j+2] * ConvolutionKernel[1+1 , 1+1   ] ;
                }

                WriteImageDataToFile(locationOResult,outputImageData);
            }
        }
        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }
        static void WriteImageDataToFile(string imageDataFilePath,
                                 double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i,
                                                imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }

            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
        }

    }
}
