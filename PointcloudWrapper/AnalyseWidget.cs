using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace PointcloudWrapper.Widget
{
    public class AxisDraw
    {
        public float Max;
        public float Min;
        public int StartBlockIndex;
        public int EndBlockIndex;
        

        //绘制控制部分有关
        int blockCount;
        (float clipMin, float clipMax)[] blockVale;
        //(哪个区间，计数),一个格子对应的数据区间 StartBlockIndex+区间*oneStepLength～StartBlockIndex+(区间+1)*oneStepLength
        Dictionary<int, int> map;
       

        //绘制属性
        Pen pen = new Pen(Color.Black, 1);
        public Color SetColor
        {
            get
            {
                return pen.Color;
            }
            set
            {
                pen.Color = value;
            }
        }


        /// <summary>
        /// 判定值在哪个区间
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        int InWitchBlock(float val)
        {
            for (int i = 0; i <= blockCount; i++)
            {
                if (blockVale[i].clipMin<=val&& val<blockVale[i].clipMax)
                {
                    return i;
                }
            }           
            //把最小值归到第0个区间
            return 0;
        }



        /// <summary>
        /// 通过输入值来生成统计的数据
        /// </summary>
        /// <param name="data"></param>
        void CreateBlock(float[] data)
        {
            blockVale = new (float, float)[blockCount];
            float oneStepLength = (EndBlockIndex - StartBlockIndex) / (float)blockCount;

            //对区间进行初始化初始化
            map = new Dictionary<int, int>();
            for (int i = 0; i < blockCount; i++)
            {
                map.Add(i, 0);
                blockVale[i] = (StartBlockIndex + i * oneStepLength, StartBlockIndex + (i + 1) * oneStepLength);
            }
            //遍历数据，统计出现的区间的个数
            int dataLength = data.Length;

            for (int i = 0; i < dataLength; i++)
            {
                map[InWitchBlock(data[i])]++;
            }
        }

        public AxisDraw(float[] data,int startBlockIndex=0,int endBlockIndex=255)
        {
            Max = data.Max();
            Min = data.Min();
            StartBlockIndex = startBlockIndex;
            EndBlockIndex = endBlockIndex;
            this.blockCount = endBlockIndex - startBlockIndex;
            CreateBlock(data);
        }



        public Bitmap Draw(int width=1280,int height=720)
        {
            Bitmap bmp = new Bitmap(width, height);
            int leftOffset = (int)(width * 0.1f);
            int botOffset = (int)(height * 0.1f);
            int xAxisLength = (int)(width * 0.8f);
            int yAxisLength = (int)(height * 0.8f);
            float xStep = xAxisLength / 255f;
            float yStep = yAxisLength / (float)map.Values.Max();

            //开始绘制
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //将GDI+中原始的坐标原点平移
            g.TranslateTransform(0f, height);
            //变换x，y轴的正方向
            g.ScaleTransform(1f, -1f);

            foreach (KeyValuePair<int,int> p in map)
            {
                Rectangle rectangle = new Rectangle(
                  (int)(leftOffset + p.Key * xStep),
                    botOffset, 
                    (int)xStep, 
                    (int)(yStep * p.Value));
                g.DrawRectangle(pen, rectangle);
            }

            return bmp;
        }
        public void Draw(PictureBox pictureBox, int width = 1280, int height = 720)
        {
            Bitmap bmp = new Bitmap(width, height);
            int leftOffset = (int)(width * 0.1f);
            int botOffset = (int)(height * 0.1f);
            int xAxisLength = (int)(width * 0.8f);
            int yAxisLength = (int)(height * 0.8f);
            float xStep = xAxisLength / 255f;
            float yStep = yAxisLength / (float)map.Values.Max();

            //开始绘制
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //将GDI+中原始的坐标原点平移
            g.TranslateTransform(0f, height);
            //变换x，y轴的正方向
            g.ScaleTransform(1f, -1f);

            foreach (KeyValuePair<int, int> p in map)
            {
                Rectangle rectangle = new Rectangle(
                  (int)(leftOffset + p.Key * xStep),
                    botOffset,
                    (int)xStep,
                    (int)(yStep * p.Value));
                g.DrawRectangle(pen, rectangle);
            }

            pictureBox.Image = bmp;
        }
    }
    public class AxisX: AxisDraw
    {
        public AxisX(float[] data,int dispMin, int dispMax)
            : base(data, dispMin, dispMax)
        { }
    }
    public class AxisY: AxisDraw
    {
        public AxisY(float[] data, int blockCount = 255) : base(data, blockCount)
        { }
    }
    public class AxisZ: AxisDraw
    {
        public AxisZ(float[] data, int blockCount = 255) : base(data, blockCount)
        { }
    }


    //public class AnalyseWidget
    //{
    //    PointcloudF pointcloud;
    //    Control dispWidget;
    //    float xMin, xMax, yMin, yMax, zMin, zMax;
    //    public AnalyseWidget(PointcloudF pointcloud,Control dispWidget)
    //    {
    //        this.pointcloud = pointcloud;
    //        this.dispWidget = dispWidget;
    //        xMin = pointcloud.X.Min();
    //        xMax = pointcloud.X.Max();
    //        xMin = pointcloud.X.Min();
    //        xMax = pointcloud.X.Max();
    //        xMin = pointcloud.X.Min();
    //        xMax = pointcloud.X.Max();
    //    }
        
    //}
}
