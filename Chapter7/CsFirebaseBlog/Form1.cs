using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace CsFirebaseBlog
{
    public partial class Form1 : Form
    {
        FirestoreDb db;
        public Form1()
        {
            InitializeComponent();
        }

        //초기화
        private void Form1_Load(object sender, EventArgs e)
        {  
            string path = AppDomain.CurrentDomain.BaseDirectory + @"cs-firebase-blog-firebase-adminsdk-18cwe-b5971a4787.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("cs-firebase-blog");
        }


        //회원가입 버튼 이벤트
        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string pass = textBox2.Text;

            if (id == "" || pass == "") //공백이 입력될 경우
            {
                MessageBox.Show("아이디 또는 비밀번호에 공백이 있습니다.");
                return;
            }
            JoinManagement(id, pass);
           
        }

        //로그인 버튼 이벤트
        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string pass = textBox2.Text;

            if (id == "" || pass == "") //공백이 입력될 경우
            {
                MessageBox.Show("아이디 또는 비밀번호에 공백이 있습니다.");
                return;
            }

            LoginManagement(id, pass);
        }

        //회원가입 관리
        private async void JoinManagement(string id, string pass)
        {
            bool idCheck = await FindId(id);
            if(idCheck) {} //id가 이미 있으므로 회원가입 X
            else if(!idCheck) //id가 없으므로 회원가입 O
            {
                Join(id, pass);
                MessageBox.Show("회원가입이 완료되었습니다.");
            }
        }

        //로그인 관리
        private async void LoginManagement(string id, string pass)
        {
            bool idCheck = await FindId(id, pass);
            if(idCheck) //id, pass 일치
            {
                MessageBox.Show("로그인 되었습니다.");
            } else if(!idCheck) //id, pass 일치하지 않음
            {
                MessageBox.Show("로그인에 실패하였습니다.");
            }
        }

        //아이디 찾는 메서드
        async Task<bool> FindId(string id)
        {
            Query qref = db.Collection("Join").WhereEqualTo("Id", id);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    return true;
                }
            }
            return false;
        }

        //아이디, 비번 찾는 메서드
        async Task<bool> FindId(string id, string pass)
        {
            Query qref = db.Collection("Join").WhereEqualTo("Id", id).WhereEqualTo("Pass", pass);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    return true;
                }
            }
            return false;
        }

        //가입하는 메서드
        void Join(string id, string pass)
        {
            DocumentReference DOC = db.Collection("Join").Document();
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"Id", id },
                {"Pass", pass },
            };
            DOC.SetAsync(data1);
        }

    }
}

