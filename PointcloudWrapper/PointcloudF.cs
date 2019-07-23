using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointcloudWrapper
{
    /**
   * 基本点云数据结构
   * **/
    interface IPointcloudF
    {
        float[] X
        {
            get;
        }
        float[] Y
        {
            get;
        }
        float[] Z
        {
            get;
        }

        PointcloudF Clone();
    }


    /// <summary>
    /// 这个类不允许做数据处理，只允许数据的存储，如果要做处理，需要用继承
    /// </summary>
    public class PointcloudF : IPointcloudF
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




        /// <summary>
        /// 复制一个点云对象
        /// </summary>
        /// <returns></returns>
        public PointcloudF Clone()
             => (new PointcloudF(this.x, this.y, this.z));
    }

}
