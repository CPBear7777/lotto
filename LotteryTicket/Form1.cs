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
        Label[] lblLotArray = new Label[6];//lbl開獎隨機順序
        Label[] lblSeqLotArray = new Label[6];//lbl開獎排序
        Label[] lblMyNumArray = new Label[6];//lbl自選號碼
        public static int[] LotNum1 = new int[6];//開獎號
        public static int LotSpeNum;

        List<int> _MyNumList = new List<int>();//儲存第一區自選號
        int _MyNumSpe = -1;//儲存第二區自選號

        List<List<int>>ThisPeriodMyNums = new List<List<int>>();//存放本次下注的所有號碼組

        List<Button> NumButtonList = new List<Button>();//第一區生成的按鈕
        List<Button> SpeNumButtonList = new List<Button>();//第二區生成的按鈕

        public static bool isMulti = false;
        bool isNewLot = true;

        public static int _No = 1;//組數
        int _Period = 1;//期數
        String String_Period;
        public int ThePeriodPrize = 0;

        int AllRow = 0;

        string NumString;




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

                MyNumSpe.Click += new EventHandler(Num2ByMyself_Click);//賦予觸發事件
                SpeNumButtonList.Add(MyNumSpe);//放到list
                groupMyNum.Controls.Add(MyNumSpe);//放到Form裡面
                Locat2X += 45;

            }
            //listBox
            listLuckyNum.Items.Add("組別　　第一區號碼                                   第二區號碼");
            listLuckyNum.Items.Add("-----------------------------------------------------");
            listLuckyNum.Items.Add("-00-　　01　38　22　12　13　55　　　08(範例組)");

            //包牌起始組數
            txtMultiNum.Text = Convert.ToString(1);

            //控制期數
            String_Period = String.Format("{0:D4}期", _Period);
            lbl_Period.Text = String_Period;
            lbl_Period2.Text = String_Period;
        }

        //選號
        private void Num1ByMyself_Click(object sender, EventArgs e)//利用Object snder來取得Button的內容，object指形態，sender指Button，把自己傳進去。
        {
            Button BTNSelectedNum = (Button)sender;//(型態) object→Button
            bool is1Selected = false;

            //選號按鍵變色
            if (BTNSelectedNum.BackColor == Color.White)//第一區選取的
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
            if(isMulti == false)//非包牌
            {
                if (is1Selected == true)//第一區有選
                {
                    Console.WriteLine($"第一區{ _MyNumList.Count}");
                    if (_MyNumList.Count < 6)//第一區有沒有選超過
                    {
                        _MyNumList.Add(Convert.ToInt32(BTNSelectedNum.Text));//放到第一區暫存中
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
                else////第一區取消選取
                {
                    int deletNum = Convert.ToInt32(BTNSelectedNum.Text);

                    _MyNumList.Remove(deletNum);
                }
            }
            
            
            //把第一區所選號碼放到lbl中，每一次都會清空在重新放置。
            for (int i = 0; i < lblMyNumArray.Length; i++)
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
            btnClean.PerformClick();//每次自動選號時，btnClean就會執行一次

            //第一區號碼-隨機
            isMulti = true;
            _MyNumList = firstBolck().ToList();

            //第一區-大小排序
            _MyNumList.Sort();//排序
            for(int i = 0; i < 6; i++)
            {
                NumButtonList[_MyNumList[i]-1].PerformClick();//被選號會逐一點選
            }

            //第二區號碼
            _MyNumSpe = SecondBolck();
            SpeNumButtonList[_MyNumSpe - 1].PerformClick();
            lblMyNumSpe.Text = _MyNumSpe.ToString();

            isMulti = false;
        }

        private void btnMulti2_Click(object sender, EventArgs e)
        {
            isMulti = true;//是包牌
            int _Nolimit = Convert.ToInt32(txtMultiNum.Text);//要選幾組

            //顯示包牌號
            string MessageString = ($"　　　　　　　　<<電腦選號>> 　　　　　　{txtMultiNum.Text} 組\n" +
               $"組別　　第一區號碼                                第二區號碼\n" +
               $"===============================\n");

            if (_Nolimit >= 1)
            {
                btnClean.PerformClick();
                List<List<int>> _Rantest = new List<List<int>>();
                List<int> _Rantest2 = new List<int>();

                for (int i = 0; i < _Nolimit; i++)//總共要抽幾組
                {
                    _MyNumList = firstBolck().ToList();//第一區第一筆
                    _MyNumList.Sort();
                    _Rantest.Add(_MyNumList);

                    _MyNumSpe = SecondBolck();//第二區第一筆
                    _Rantest2.Add(_MyNumSpe);
                    for (int j = 0; j < i; j++)
                    {
                        while(_Rantest[j][0] == _Rantest[i][0])//第一區第二筆與第一筆一樣時，重抽
                        {
                            j = 0;
                            _Rantest.RemoveAt(i);
                            _MyNumList = firstBolck().ToList();
                            _MyNumList.Sort();
                            _Rantest.Add(_MyNumList);
                        }
                        while(_Rantest2[i]== _Rantest2[i - 1])//第二區第二筆與第一筆一樣時，重抽
                        {
                            _Rantest2.RemoveAt(i);
                            _MyNumSpe = SecondBolck();
                            _Rantest2.Add(_MyNumSpe);
                        }
                    }
                    //在listBOX顯示本次選號
                    NumString = String.Format("-{0:D2}-　　{1:D2}　{2:D2}　{3:D2}　{4:D2}　{5:D2}　{6:D2}　　　{7:D2}\n", _No, _MyNumList[0], _MyNumList[1], _MyNumList[2], _MyNumList[3], _MyNumList[4], _MyNumList[5], _MyNumSpe);
                    MessageString += $"{NumString}";
                    listLuckyNum.Items.Add(NumString);
                    _No++;

                    //將本期選號放在同一個List內，用於後續兌獎用
                    List<int> Copy_MyNumList = new List<int>();
                    Copy_MyNumList.Add(_MyNumList[0]);
                    Copy_MyNumList.Add(_MyNumList[1]);
                    Copy_MyNumList.Add(_MyNumList[2]);
                    Copy_MyNumList.Add(_MyNumList[3]);
                    Copy_MyNumList.Add(_MyNumList[4]);
                    Copy_MyNumList.Add(_MyNumList[5]);
                    Copy_MyNumList.Add(_MyNumSpe);
                    ThisPeriodMyNums.Add(Copy_MyNumList);
                }
            }
            else
            {
                MessageBox.Show("請輸入1以上的有效組數");
            }
            MessageBox.Show(MessageString, "您的電腦選號", MessageBoxButtons.YesNo);
        }

        private void btnAddOne_Click(object sender, EventArgs e)
        {
            //加入listBox
            if (_MyNumList.Count == 6 && intSelectNum2 == 1)
            {
                //將本期選號放在同一個List內，用於後續兌獎用
                //此步驟是為防止__MyNumList被覆蓋掉的方法
                List<int> Copy_MyNumList = new List<int>();
                Copy_MyNumList.Add(_MyNumList[0]);
                Copy_MyNumList.Add(_MyNumList[1]);
                Copy_MyNumList.Add(_MyNumList[2]);
                Copy_MyNumList.Add(_MyNumList[3]);
                Copy_MyNumList.Add(_MyNumList[4]);
                Copy_MyNumList.Add(_MyNumList[5]);
                Copy_MyNumList.Add(_MyNumSpe);
                ThisPeriodMyNums.Add(Copy_MyNumList);

                NumString = String.Format("-{0:D2}-　　{1:D2}　{2:D2}　{3:D2}　{4:D2}　{5:D2}　{6:D2}　　　{7:D2}\n", _No, _MyNumList[0], _MyNumList[1], _MyNumList[2], _MyNumList[3], _MyNumList[4], _MyNumList[5], _MyNumSpe);
                listLuckyNum.Items.Add(NumString);
                _No++;
                btnClean.PerformClick();
            }
            else
            {
                MessageBox.Show("您尚未完整選號！");
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            isMulti = false;
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
            }
            _MyNumList.Clear();

            //第二區
           if (_MyNumSpe != -1)
            {
                SpeNumButtonList[_MyNumSpe-1].BackColor = Color.MistyRose;
                SpeNumButtonList[_MyNumSpe-1].ForeColor = Color.Red;
                _MyNumSpe = -1;
                intSelectNum2=0;
            }
        }

        private void btnLottery_Click(object sender, EventArgs e)//開獎
        {
            if (listLuckyNum.Items.Count > 3)
            {
                if (isNewLot == true)
                {
                    //第一區號碼-隨機
                    LotNum1 = firstBolck();
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
                    LotSpeNum = SecondBolck();
                    lblRdNumSpe.Text = LotSpeNum.ToString();
                    lblSeqNumSpe.Text = LotSpeNum.ToString();

                    isNewLot = false;
                }
                else
                {
                    MessageBox.Show($"本期尚未兌獎，請先兌獎。");
                }
            }
            else
            {
                MessageBox.Show($"本期尚未投注，請先投注。");
            }
               
        }

        public int[] firstBolck()//第一區自動選號
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
                    while (Num1[j] == Num1[i])//如果有重複
                    {
                        j = 0;//j歸零，下一次可以重新檢查
                        Num1[i] = RandNum.Next(1, 39);// i 重抽
                    }
                }
            }
            //Console.WriteLine($"隨機開了幾個{ Num1.Length}");
            return Num1;
        }
        public int SecondBolck()//第二區自動選號
        {
            int Num2;

            Random RandNumSpe = new Random();//第二區一碼
            Num2 = RandNumSpe.Next(1, 9);

            return Num2;
        }

        private void btnPrize_Click(object sender, EventArgs e)//兌獎
        {
            List<string> prizeHistory = new List<string>();
            int TotalPrize = 0;
            
            isNewLot = true;

            if (lblRdNum1.Text != "--")//有開獎才可以兌獎
            {
                if (listLuckyNum.Items.Count > 3)//有投注才可以執行
                {
                    int WinNum = 0;
                    bool WinSpeNum;
                    int NUM = 0;
                    _No = 1;
                    for (int i = 0; i < ThisPeriodMyNums.Count; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            foreach (int k in LotNum1)
                            {
                                if (ThisPeriodMyNums[i][j] == k)
                                {
                                    WinNum++;
                                }
                            }
                        }

                        if (ThisPeriodMyNums[i][6] == LotSpeNum)
                        {
                            WinSpeNum = true;
                        }
                        else
                        {
                            WinSpeNum = false;
                        }
                        NUM++;
                        //Console.WriteLine($"-{NUM}- 第一區中{WinNum}個號碼，{WinPrize.PrizeList(WinNum, WinSpeNum)}");
                        WinPrize.PrizeList(WinNum, WinSpeNum);//獎項對照
                        prizeHistory.Add(WinPrize.WinWhich);
                        TotalPrize += WinPrize.prize;
                        ThePeriodPrize += WinPrize.prize;
                        WinNum = 0;
                    }
                    MessageBox.Show(String.Format("本期中獎金額{0:N}元", TotalPrize));
                    ThePeriodPrize = 0;


                    //加入歷史資料(dataGridView)
                    string My_ = "";
                    string Lot_ = "";

                    int dataRow = ThisPeriodMyNums.Count;
                    dataHistory.Rows.Add(dataRow);

                    for (int i = 0; i < ThisPeriodMyNums.Count; i++)
                    {
                        for (int j = 0; j < ThisPeriodMyNums[i].Count - 1; j++)
                        {
                            My_ += String.Format("{0:D2} ", ThisPeriodMyNums[i][j]);
                        }
                        My_ += String.Format("  {0:D2}", ThisPeriodMyNums[i][6]);
                        dataHistory[1, (i + AllRow)].Value = My_;
                        dataHistory[3, (i + AllRow)].Value = prizeHistory[i];
                        My_ = "";
                    }

                    for (int i = 0; i < LotNum1.Length; i++)
                    {
                        Lot_ += String.Format("{0:D2} ", LotNum1[i]);
                    }
                    Lot_ += String.Format("  {0:D2}", LotSpeNum);

                    dataHistory[0, AllRow].Value = String_Period;
                    dataHistory[2, AllRow].Value = Lot_;
                    AllRow += dataRow;



                    //計算期數
                    _Period++;
                    String_Period = String.Format("{0:D4}期", _Period);
                    lbl_Period.Text = String_Period;
                    lbl_Period2.Text = String_Period;

                    //累積獎金
                    lblTotalPrize.Text = String.Format("累積得獎金額：{0:N}元", TotalPrize);

                    //重置欄位
                    ThisPeriodMyNums.Clear();//對完獎後所有號碼組清除  
                    listLuckyNum.Items.Clear();//listBox清空
                    listLuckyNum.Items.Add("組別　　第一區號碼                                   第二區號碼");
                    listLuckyNum.Items.Add("-----------------------------------------------------");
                    listLuckyNum.Items.Add("-00-　　01　38　22　12　13　55　　　08(範例組)");
                    for (int i = 0; i < 6; i++)//開獎欄清空
                    {
                        lblLotArray[i].Text = "--";
                        lblSeqLotArray[i].Text = "--";
                    }
                    lblRdNumSpe.Text = "--";
                    lblSeqNumSpe.Text = "--";
                }
                else
                {
                    MessageBox.Show("本期您尚未投注");
                }
            }
            else
            {
                MessageBox.Show("本期尚未開獎");

            }
        }
    }
}
