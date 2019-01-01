using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        long noA = 0;
        long noB = 0;
        long noC = 0;
        long mondaiCnt = 0; //出題数カウント
        long mondaiMax = 0;　//最大出題数
        long seikaiCnt = 0; //正解数
        long type = 0; //1:足し算　2:引き算
        string typeStr; //演算記号表示用
        
        public Form1()
        {
            InitializeComponent();
        }

        private void InBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //数値およびバックスペースのみ受け付ける
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                if (! (e.KeyChar == '-' && InBox.TextLength == 0))
                {
                    e.Handled = true; //いま入力されたものを無視する
                }
            }
            if ((e.KeyChar == (char)Keys.Enter) && InBox.Text != "" && InBox.Text != "-")
            {
                Console.WriteLine(InBox.Text);
                //OutBox.AppendText(InBox.Text + "\r\n");
                Kotaeawase();
                InBox.Text = "";
                if (mondaiCnt < mondaiMax)
                {
                    MondaiSakusei();
                }
                else
                {
                    Seiseki();
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InBox.Enabled = false; //解答欄を入力不可にする
        }

        //問題作成する

        private void MondaiSakusei()
        {
            noA = rand.Next(1, 10);
            noB = rand.Next(1, 10);

            MondaiLbl.Text = noA + typeStr + noB + " = ";
            if (type == 1)
            {
                noC = noA + noB;
            }
            else
            {
                noC = noA - noB;
            }
            mondaiCnt++;
        }

        //答え合わせする
        private void Kotaeawase()
        {
            if (noC == Int64.Parse(InBox.Text))
            {
                OutBox.AppendText(" ○ ");
                seikaiCnt++;
            }
            else
            {
                OutBox.AppendText(" × ");
            }
            OutBox.AppendText(MondaiLbl.Text + InBox.Text + "\r\n");
        }

        //成績の表示
        private void Seiseki()
        {
            InBox.Enabled = false;
            MondaiLbl.Text = ""; //問題の表示をクリア
            OutBox.AppendText(" 正解数は " + seikaiCnt + " です ");

        }
        private void StartBtn_Click(object sender, EventArgs e)
        {
            //出題数を変更する
           if (RB10.Checked)
            {
                mondaiMax = 10;
            }
           if (RB20.Checked)
            {
                mondaiMax = 20;
            }
           if (RB30.Checked)
            {
                mondaiMax = 30;
            }

           //計算タイプを変更する
           if (RBtasu.Checked)
            {
                type = 1;
                typeStr = " + ";
            }
           if (RBhiku.Checked)
            {
                type = 2;
                typeStr = " - ";
            }

            mondaiCnt = 0;
            seikaiCnt = 0; //正解数のカウント用
            MondaiSakusei();
            InBox.Enabled = true; //入力可にする
            InBox.Focus(); //解答欄へフォーカスを移す
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
