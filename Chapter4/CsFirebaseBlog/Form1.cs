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

        private void button1_Click(object sender, EventArgs e)
        {
            GetAllData_Of_A_Document();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Get_ALL_Documents_From_A_Collection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Get_Multiple_Documents_From_A_Collection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {  
            string path = AppDomain.CurrentDomain.BaseDirectory + @"cs-firebase-blog-firebase-adminsdk-18cwe-b5971a4787.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("cs-firebase-blog");
        }

        async void GetAllData_Of_A_Document()
        {
            DocumentReference docref = db.Collection("MyTest").Document("TestA");
            DocumentSnapshot snap = await docref.GetSnapshotAsync();

            if (snap.Exists)
            {
                Dictionary<string, object> city = snap.ToDictionary();
                foreach (var item in city)
                {
                    richTextBox1.Text += string.Format("{0}: {1}\n", item.Key, item.Value);

                }
            }
        }

        async void Get_ALL_Documents_From_A_Collection()
        {
            Query qref = db.Collection("MyTest");
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                FirebaseProperty fp = docsnap.ConvertTo<FirebaseProperty>();

                if (docsnap.Exists)
                {
                    richTextBox1.Text += "[Doc Name : " + docsnap.Id + "]\n";
                    richTextBox1.Text += fp.Name + "\n";
                    richTextBox1.Text += fp.PhoneNo + "\n\n";

                }
            }
        }

        //조건 검색
        async void Get_Multiple_Documents_From_A_Collection()
        {
            Query qref = db.Collection("MyTest").WhereEqualTo("Name", "Kim jinwon");
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                FirebaseProperty fp = docsnap.ConvertTo<FirebaseProperty>();

                if (docsnap.Exists)
                {
                    richTextBox1.Text += "[Doc Name: " + docsnap.Id + "]\n";
                    richTextBox1.Text += fp.Name + "\n";
                    richTextBox1.Text += fp.PhoneNo + "\n\n";

                }
            }
        }
    }
}

