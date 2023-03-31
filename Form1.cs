using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotteryTicket
{
    public partial class Form1 : Form
    {
        //宣告開獎lbl的Array(因為是固定數量，所以用Array)
        Label[] lblLotArray = new Label[6];//開獎隨機順序
        Label[] lblSeqLotArray = new Label[6];//開獎排序
        Label[] lblMyNumArray = new Label[6];//自選號碼

        List<int> _MyNumList = new List<int>();//儲存第一區自選號
        int _MyNumSpe = -1;//儲存第二區自選號


        List<Button> NumButtonList = new List<Button>();//第一區生成的按鈕
        List<Button> SpeNumButtonList = new List<Button>();//第二區生成的按鈕


        //int intSelectNum1 = 0;//第一區選幾個號碼了
        int intSelectNum2 = 0;//第二區選幾個號碼了
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //把開獎欄位lblRdNum放到 Array中
            lblLotArray[0] = lblRdNum1;
            lblLotArray[1] = lblRdNum2;
            lblLotArray[2] = lblRdNum3;
            lblLotArray[3] = lblRdNum4;
            lblLotArray[4] = lblRdNum5;
            lblLotArray[5] = lblRdNum6;

            //開獎排序欄位
            lblSeqLotArray[0] = lblSeqNum1;
            lblSeqLotArray[1] = lblSeqNum2;
            lblSeqLotArray[2] = lblSeqNum3;
            lblSeqLotArray[3] = lblSeqNum4;
            lblSeqLotArray[4] = lblSeqNum5;
            lblSeqLotArray[5] = lblSeqNum6;

            //自選欄位lblMyNum放到Array中
            lblMyNumArray[0] = lblMyNum1;
            lblMyNumArray[1] = lblMyNum2;
            lblMyNumArray[2] = lblMyNum3;
            lblMyNumArray[3] = lblMyNum4;
            lblMyNumArray[4] = lblMyNum5;
            lblMyNumArray[5] = lblMyNum6;

            //創建選號Button
            //第一區
            int Num1 = 1;
            int Locat1X = 3;
            int Locat1Y = 50;
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (Num1 <= 38)
                    {
                        Button  btnMyNum = new Button();//創建一個Button
                        //Button的屬性
                        btnMyNum.Size = new Size(40, 35);
                        btnMyNum.Location = new Point(Locat1X, Locat1Y);
                        btnMyNum.BackColor = Color.White;
                        btnMyNum.ForeColor = Color.Black;
                        btnMyNum.Font = new Font("微軟正黑體", 12);
                        btnMyNum.Text = Num1.ToString("D2");

                        btnMyNum.Click += new EventHandler(Num1ByMyself_Click);//賦予觸發事件
                        NumButtonList.Add(btnMyNum);//放到list
                        groupMyNum.Controls.Add(btnMyNum);//放到Form裡面
                        Locat1X += 45;
                    }
                    Num1++;
                }
                Locat1X = 3;
                Locat1Y += 40;
            }
            //第二區
            int Num2 = 0;
            int Locat2X = 3;
            int Locat2Y = 240;
            for (int j = 0; j < 8; j++)
            {
                Num2++;
                Button MyNumSpe = new Button();//創建一個Button
                //Button的屬性
                MyNumSpe.Size = new Size(40, 35);
                MyNumSpe.Location = new Point(Locat2X, Locat2Y);
                MyNumSpe.BackColor = Color.MistyRose;
                MyNumSpe.ForeColor = Color.Red;
                MyNumSpe.Font = new Font("微軟正黑體", 12);
                MyNumSpe.Text = Num2.ToString("D2");

                //MyNumSpe.Click +=

                MyNumSpe.Click += new EventHandler(Num2ByMyself_Click);//賦予觸發事件
                SpeNumButtonList.Add(MyNumSpe);//放到list
                groupMyNum.Controls.Add(MyNumSpe);//放到Form裡面
                Locat2X += 45;
            }
        }

        //選號
        private void Num1ByMyself_Click(object sender, EventArgs e)//利用Object snder來取得Button的內容，object指形態，sender指Button，把自己傳進去。
        {
            Button BTNSelectedNum = (Button)sender;//(型態) object→Button
            bool is1Selected = false;
            
            
            //選號按鍵變色
            if(BTNSelectedNum.BackColor == Color.White)//第一區選取的
            {
                BTNSelectedNum.BackColor = Color.Green;
                BTNSelectedNum.ForeColor = Color.White;
                is1Selected = true;
            }
            else if (BTNSelectedNum.BackColor == Color.Green)//第一區沒選取的
            {
                BTNSelectedNum.BackColor = Color.White;
                BTNSelectedNum.ForeColor = Color.Black;
                is1Selected = false;
            }

            //第一區
            //號碼放到List中
            if (is1Selected == true)//第一區有選
            {
                if (_MyNumList.Count < 6)//第一區有沒有選超過
                {
                    //btnNum.Add( Convert.ToInt32(BTNSelectedNum.Text));
                    
                    _MyNumList.Add(Convert.ToInt32(BTNSelectedNum.Text));//放到第一區暫存中
                    //btnNum++;
                    is1Selected = false;
                }
                else
                {
                    MessageBox.Show($"第一區已達選號上限");
                    BTNSelectedNum.BackColor = Color.White;
                    BTNSelectedNum.ForeColor = Color.Black;
                    is1Selected = false;
                }    
            }
            //去上課吧88
            else////第一區取消選取
            {
                int deletNum = Convert.ToInt32(BTNSelectedNum.Text);
                //btnNum.Remove(deletNum);//從暫時選號中取消

                _MyNumList.Remove(deletNum);
                //btnNum--;
            }
            //把第一區所選號碼放到lbl中，每一次都會清空在重新放置。
            for(int i = 0; i < lblMyNumArray.Length; i++)
            {
                lblMyNumArray[i].Text = "--";//全部清空
                for(int j = 0; j < _MyNumList.Count; j++)
                {
                    lblMyNumArray[j].Text = _MyNumList[j].ToString();
                }
            }
        }

        private void Num2ByMyself_Click(object sender, EventArgs e)//利用Object snder來取得Button的內容，object指形態，sender指Button，把自己傳進去。
        {
            Button BTNSelectedNum = (Button)sender;//(型態) object→Button
            bool is2Selected = false;
            
            if(BTNSelectedNum.BackColor == Color.MistyRose)//第二區選取的
            {
                BTNSelectedNum.BackColor = Color.Red;
                BTNSelectedNum.ForeColor = Color.MistyRose;
                is2Selected = true;
            }
            else if (BTNSelectedNum.BackColor == Color.Red)//第二區沒選取的
            {
                BTNSelectedNum.BackColor = Color.MistyRose;
                BTNSelectedNum.ForeColor = Color.Red;
                is2Selected = false;
            }

            //第二區
            if (is2Selected == true)//第二區有選
            {
                if (intSelectNum2 < 1)//第二區有沒有選超過
                {
                    lblMyNumSpe.Text = BTNSelectedNum.Text;//放到lbl
                    _MyNumSpe = Convert.ToInt32(BTNSelectedNum.Text);//放到第二區暫存中
                    intSelectNum2++;
                    is2Selected = false;
                }
                else
                {
                    MessageBox.Show($"第二區已達選號上限");
                    BTNSelectedNum.BackColor = Color.MistyRose;
                    BTNSelectedNum.ForeColor = Color.Red;
                    is2Selected = false;
                }
            }
            else////第二區取消選
            {
                lblMyNumSpe.Text = "--";
                intSelectNum2--;
            }
        }


        private void btnMulti_Click(object sender, EventArgs e)//btn包牌
        {
            Console.WriteLine(e.ToString());
            btnClean.PerformClick();
            //第一區號碼-隨機
            int[] MultiNum1 = 第一區隨機選號();
            //第一區-大小排序
            Array.Sort(MultiNum1);//排序
            for(int i = 0; i < 6; i++)
            {
                NumButtonList[MultiNum1[i]-1].PerformClick();
         //       lblMyNumArray[i].Text = NumButtonList[MultiNum1[i]].ToString();
            }

            //第二區號碼
            int MultiSpeNum = 第二區隨機選號();
            SpeNumButtonList[MultiSpeNum - 1].PerformClick();
            lblMyNumSpe.Text = MultiSpeNum.ToString();
        }

        private void btnAddOne_Click(object sender, EventArgs e)
        {

        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            //第一區
            for (int i = 0; i < 6; i++)//自選號第一區欄位清空
            {
                lblMyNumArray[i].Text = "--";
            }
            lblMyNumSpe.Text = "--";//自選號第二區欄位清空

            for (int i = 0; i < _MyNumList.Count; i++)//把第一區按鈕顏色變回來 //這個地方可以優化
            {
                
                NumButtonList[_MyNumList[i]-1].BackColor = Color.White;
                NumButtonList[_MyNumList[i]-1].ForeColor = Color.Black;
                //Console.WriteLine(NumButtonList[_MyNumList[i]-1]);
            }
            _MyNumList.Clear();

            //第二區待解決
           if (_MyNumSpe != -1)
            {
                SpeNumButtonList[_MyNumSpe-1].BackColor = Color.MistyRose;
                SpeNumButtonList[_MyNumSpe-1].ForeColor = Color.Red;
                _MyNumSpe = -1;
                intSelectNum2=0;
            }
            //這是控制第二區的

            //btnNum = 0;
        }

        private void btnLottery_Click(object sender, EventArgs e)
        {
            //第一區號碼-隨機
            int[] LotNum1 = 第一區隨機選號();
            for (int i = 0; i < lblLotArray.Length; i++)//放到開獎欄中
            {
                lblLotArray[i].Text = LotNum1[i].ToString();
            }

            //第一區-大小排序
            Array.Sort(LotNum1);//排序
            for (int i = 0; i < lblSeqLotArray.Length; i++)//放到排序開獎中
            {
                lblSeqLotArray[i].Text = LotNum1[i].ToString();
            }

            //第二區號碼
            int LotSpeNum = 第二區隨機選號();
            lblRdNumSpe.Text = LotSpeNum.ToString();
            lblSeqNumSpe.Text = LotSpeNum.ToString();  
        }

        public int[] 第一區隨機選號()//測試中 
        {
            //開出順序
            Random RandNum = new Random();//第一區六碼
            int[] Num1 = new int[6];
            //抽第一區號碼
            for (int i = 0; i < 6; i++)//要抽滿的次數
            {
                Num1[i] = RandNum.Next(1, 39);//產生下一個亂數
                for (int j = 0; j < i; j++)//逐一檢查 i 是否與前面的任何一個有重複
                {
                    if (Num1[j] == Num1[i])//如果有重複
                    {
                        j = 0;//j歸零，下一次可以重新檢查
                        Num1[i] = RandNum.Next(1, 39);// i 重抽
                    }
                }
            }
            return Num1;
        }
        public int 第二區隨機選號()
        {
            int Num2;

            Random RandNumSpe = new Random();//第二區一碼
            Num2 = RandNumSpe.Next(1, 9);

            return Num2;
        }

        //大小順序
        //第一區
        //Array.Sort(Num1);//排序

        private void btnPrize_Click(object sender, EventArgs e)
        {

        }
    }
}
