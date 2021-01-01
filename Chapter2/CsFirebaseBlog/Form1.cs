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
            Add_Document_with_AutoID();
        }


        private void Form1_Load(object sender, EventArgs e)
        {  
            string path = AppDomain.CurrentDomain.BaseDirectory + @"cs-firebase-blog-firebase-adminsdk-18cwe-b5971a4787.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("cs-firebase-blog");
        }


        void Add_Document_with_AutoID()
        {
            CollectionReference coll = db.Collection("Add_Document_Width_AutoID");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"FirestName", "Kim" },
                {"LastName","Jinwon" },
                {"PhoneNumber", "010-1234-5678" }
            };
            coll.AddAsync(data1);
            MessageBox.Show("data added successfully");
        }
    }
}

