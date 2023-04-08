﻿using System.Text;

namespace MindITCommonModulesAPI.Util
{
    public class ReferenceIDGenerate
    {
        public string GenerateRefId()
        {
            
                int length = 3;

                // creating a StringBuilder object()
                StringBuilder str_build = new StringBuilder();
                Random random = new Random();
                string num = random.Next(100, 999).ToString();
                char letter;

                for (int i = 0; i < length; i++)
                {
                    double flt = random.NextDouble();
                    int shift = Convert.ToInt32(Math.Floor(25 * flt));
                    letter = Convert.ToChar(shift + 65);
                    str_build.Append(letter);
                }
                return(str_build.ToString()+"-"+num);
            }
        }


    
}
