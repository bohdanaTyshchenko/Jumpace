/* Score.cs  
 * Final Project    
 * Bohdana Tyshchenko (8311417)
 * 12/09/2019
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTyshchenkoFinalProject
{
    //Class to add High Score to the file
    public static class Score
    {
        public static int score=0;

        public static void AddScoreToFile()
        {
            int s1 = 0;
            try
            {
                //If file doesn't exist, it creates it and add 0
                if (!File.Exists("HighScore.txt"))
                {
                    FileStream fs = File.Create("HighScore.txt");
                    fs.Close();
                    StreamWriter streamWriter1 = new StreamWriter("HighScore.txt");
                    streamWriter1.Write("0");
                    streamWriter1.Close();
                }

                StreamReader streamReader = new StreamReader("HighScore.txt");
                int s=0;

                while (!streamReader.EndOfStream)
                {
                    s = Convert.ToInt32(streamReader.ReadLine());
                }

                streamReader.Close();

                //If score in file is lower, than current score
                //Current Score is added to the file
                StreamWriter streamWriter = new StreamWriter("HighScore.txt");
             
                if (score > s)
                {
                    streamWriter.WriteLine(score.ToString());
                }
                else if (score < s)
                {
                    streamWriter.WriteLine(s.ToString());
                }
                else if(score==s)
                {
                    streamWriter.WriteLine(s.ToString());

                }
                streamWriter.Close();

            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Scores file was not found");
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Scores file was not found");
            }
            catch (IOException)
            {
                throw new Exception("An issue with file occured");
            }
            catch (Exception)
            {
                throw new Exception("Exception occured");
            }

        }
    }
}
