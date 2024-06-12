using System;
using System.Windows.Forms;

namespace Baseball123
{
    public partial class Form1 : Form
    {
        private string ResultNumber;

        public Form1()
        {
            InitializeComponent();
        }

        private void MenuNewGame_Click(object sender, EventArgs e)
        {
            int value1 = 0, value2 = 0, value3 = 0;

            //난수 생성을 위한 클래스 선언
            Random random = new Random();

            //서로 다른 3자리 숫자를 만들기 위해 계속 반복 작업 수행
            //무한루트를 방지하기 위해 1000번까지만 반복
            for (int i = 0; i < 1000; i++)
            {
                int rnd = random.Next(1, 10);   //1~9 사이의 단일 정수

                if (value1 == 0)
                    value1 = rnd;
                else if (value1 != 0 && value2 == 0 && value1 != rnd)
                    value2 = rnd;
                else if (value1 != 0 && value2 != 0 && value3 == 0 && value1 != rnd && value2 != rnd)
                    value3 = rnd;

                if (value3 != 0)
                    break;

            }

            //난수 생성으로 만들어진 서로 다른 3자리 숫자를 합침 = 찾아야 할 3자리 숫자
            if (value1 != 0 && value2 != 0 && value3 != 0)
                ResultNumber = value1.ToString() + value2.ToString() + value3.ToString();

            //리스트박스, 실행 횟수 클리어
            listBox.Items.Clear();
            labelCount.Text = "0";

        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            NumberCheck();
        }

        private void NumberCheck()
        {
            if (ResultNumber == null || ResultNumber == "")
            {
                MessageBox.Show("Menu에서 [새로 시작하기]를 해 주세요.");
                return;
            }

            if (textNumber.Text.Length != 3)
            {
                MessageBox.Show("서로 다른 3자리 숫자를 입력하세요.");
                return;
            }

            if (textNumber.Text.Substring(0, 1) == textNumber.Text.Substring(1, 1) ||
                textNumber.Text.Substring(0, 1) == textNumber.Text.Substring(2, 1) ||
                textNumber.Text.Substring(1, 1) == textNumber.Text.Substring(2, 1))
            {
                MessageBox.Show("서로 다른 3자리 숫자를 입력하세요.");
                return;
            }

            //시도 횟수를 1씩 누적시킴
            labelCount.Text = (int.Parse(labelCount.Text) + 1).ToString();

            int Strike = 0, Ball = 0;

            //위치하는 자리수가 일치하면 Strike 판정
            if (ResultNumber.Substring(0, 1) == textNumber.Text.Substring(0, 1))
                Strike += 1;
            if (ResultNumber.Substring(1, 1) == textNumber.Text.Substring(1, 1))
                Strike += 1;
            if (ResultNumber.Substring(2, 1) == textNumber.Text.Substring(2, 1))
                Strike += 1;

            //3자리 숫자에 포함은 되지만 위치하는 자리수가 다른 경우 Ball 판정
            if (ResultNumber.Substring(0, 1) == textNumber.Text.Substring(1, 1) || ResultNumber.Substring(0, 1) == textNumber.Text.Substring(2, 1))
                Ball += 1;
            if (ResultNumber.Substring(1, 1) == textNumber.Text.Substring(0, 1) || ResultNumber.Substring(1, 1) == textNumber.Text.Substring(2, 1))
                Ball += 1;
            if (ResultNumber.Substring(2, 1) == textNumber.Text.Substring(0, 1) || ResultNumber.Substring(2, 1) == textNumber.Text.Substring(1, 1))
                Ball += 1;

            //리스트박스에 판정결과 출력
            string ResultMsg = string.Format("{0} Strike {1} Ball", Strike.ToString(), Ball.ToString());

            //마지막 입력한 내용이 첫째줄에 오도록 Insert() 인덱스 0에 추가함
            //끝줄에 출력되도록 하려면 listBox.Items.Add()로 해주면 됨
            listBox.Items.Insert(0, string.Format("[{0}] {1} - {2}", labelCount.Text, textNumber.Text, ResultMsg));

            if (Strike == 3)
            {
                MessageBox.Show("숫자를 모두 맞추었습니다.");
                //현재 게임을 종료함
                ResultNumber = "";
            }

            textNumber.Text = "";
        }

        private void textNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자를 입력하고 엔터를 쳤을 때도 판정 실행
            if (e.KeyChar == 13)
            {
                NumberCheck();
            }
        }

    }
}
