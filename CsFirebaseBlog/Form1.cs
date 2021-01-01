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


        private void button2_Click(object sender, EventArgs e)
        {
            Add_Document_with_CustomID();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add_Array();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Add_ListOfObjects();
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

        void Add_Document_with_CustomID()
        {
            DocumentReference DOC = db.Collection("Add_Document_Width_CustomID").Document("myDoc");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"FirestName", "Kim" },
                {"LastName","Jinwon" },
                {"PhoneNumber", "010-1234-5789" }
            };
            DOC.SetAsync(data1);
            MessageBox.Show("data added successfully");
        }

        void Add_Array()
        {
            DocumentReference DOC = db.Collection("Add_Aray").Document("myDoc");
            Dictionary<string, object> data1 = new Dictionary<string, object>();

            ArrayList array = new ArrayList();
            array.Add(123);
            array.Add("name");
            array.Add(true);

            data1.Add("My Array", array);
            DOC.SetAsync(data1);

            MessageBox.Show("data added successfully");
        }

        void Add_ListOfObjects()
        {
            DocumentReference DOC = db.Collection("Add_ListOfObjects").Document("myDoc");
            Dictionary<string, object> MainData = new Dictionary<string, object>();
            Dictionary<string, object> List1 = new Dictionary<string, object>()
            {
                {"FirestName", "Kim" },
                {"LastName","Jinwon" },
                {"PhoneNumber", "010-1234-5678" }
            };

            MainData.Add("MyList", List1);

            DOC.SetAsync(MainData);
            MessageBox.Show("data added successfully");
        }

    }
}

