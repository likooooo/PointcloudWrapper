using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PointcloudWrapper
{
    /// <summary>
    /// 这个类不允许做数据处理，只允许数据的存储，如果要做处理，需要用继承
    /// </summary>
    public class PointcloudF
    {
        float[] x, y, z;
        public float[] X
        {
            get
            {
                return x;
            }
        }
        public float[] Y
        {
            get
            {
                return y;
            }
        }

        public float[] Z
        {
            get
            {
                return z;
            }
        }
    

        /// <summary>
        /// 创建一个基本点云
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public PointcloudF(float[] x, float[] y, float[] z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public List<(float x, float y, float z)> GetPoints()
        {
            List<(float x, float y, float z)> points;
            points = new List<(float x, float y, float z)>();
            int length = x.Length;
            for (int i = 0; i < length; i++)
            {
                points.Add((x[i], y[i], z[i]));
            }
            return points;
        }


        public static PointcloudF Clone(List<(float x, float y, float z)> points)
        {
            PointcloudF ps = new PointcloudF(
                points.Select(s => s.x).ToArray(),
                 points.Select(s => s.y).ToArray(),
                  points.Select(s => s.z).ToArray());
            return ps;
        }
        /// <summary>
        /// 复制一个点云对象
        /// </summary>
        /// <returns></returns>
        public PointcloudF Clone()
             => (new PointcloudF(this.x, this.y, this.z));


        public virtual void Save(string fileName)
        {
            int loopCount = x.Length;
            using (StreamWriter sw = new StreamWriter(File.Create(fileName)))
            {
                for (int i = 0; i < loopCount; i++)
                {
                    sw.WriteLine(x[i] + " " + y[i] + " " + z[i]);
                }
            }
        }
    }

}
