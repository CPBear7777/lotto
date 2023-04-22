using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotteryTicket
{
    internal class WinPrize
    {
        public static string WinWhich;
        List<int> SamNum = new List<int>();
        public static int prize = 0;
        public static void PrizeList(int WiningNum,bool SpeNum)//兌獎，對照獎項與金額
        {
            string Awards = "";

            if (WiningNum == 0)
            {
                Awards = "沒得";
                prize = 0;
            }
            else if (WiningNum == 1)
            {
                if(SpeNum == false)
                {
                    Awards = "沒得";
                    prize = 0;
                }
                else
                {
                    Awards = "普";
                    prize = 100;
                }
            } else if (WiningNum == 3)
            {
                if (SpeNum == false)
                {
                    Awards = "玖";
                    prize = 100;
                }
                else
                {
                    Awards = "柒";
                    prize = 400;
                }
                
            } else if (WiningNum == 2)
            {
                if (SpeNum == false)
                {
                    Awards = "沒得";
                    prize = 0;
                }
                else
                {
                    Awards = "捌";
                    prize = 200;
                }
            }
            else if(WiningNum == 4)
            {
                if(SpeNum == false)
                {
                    Awards = "陸";
                    prize = 800;
                }
                else
                {
                    Awards = "伍";
                    prize = 4000;
                }
            }
            else if(WiningNum == 5)
            {
                if (SpeNum == false)
                {
                    Awards = "肆";
                    prize = 20000;
                }
                else
                {
                    Awards = "參";
                    prize = 150000;
                }
            }
            else if (WiningNum == 6)
            {
                if(SpeNum == false)
                {
                    Awards = "貳";
                    prize = 24719101;
                }
                else
                {
                    Awards = "頭";
                    prize = 200000000;
                }
            }
            Form1 form1 = new Form1();
            form1.ThePeriodPrize += prize;

            WinWhich = String.Format("{0}獎！\n獎金{1:N}元", Awards, prize);
        }

    }
}
