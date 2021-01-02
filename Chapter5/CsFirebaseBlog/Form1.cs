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
            Replace_A_Document_Deleting_All_Previous_Fields();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Update_Specific_Fields_or_Add_New_Fields_Within_A_Document();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Update_List_Elements_or_Nested_Elements();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update_Array_Elements();
        }
        private void Form1_Load(object sender, EventArgs e)
        {  
            string path = AppDomain.CurrentDomain.BaseDirectory + @"cs-firebase-blog-firebase-adminsdk-18cwe-b5971a4787.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("cs-firebase-blog");
        }

        async void Replace_A_Document_Deleting_All_Previous_Fields()
        {
            DocumentReference docref = db.Collection("Testing").Document("docs1");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"name", "Jinwon" },
                {"web", "naver.com" }
            };
            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                await docref.SetAsync(data);
            }
        }

        async void Update_Specific_Fields_or_Add_New_Fields_Within_A_Document()
        {
            DocumentReference docref = db.Collection("Testing").Document("docs1");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"name", "Kim Jinwon" },
                {"web", "daum.com" },
                {"newField", 123 }
            };
            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                await docref.UpdateAsync(data);
            }
        }
        async void Update_List_Elements_or_Nested_Elements()
        {
            DocumentReference docref = db.Collection("Add_ListOfObjects").Document("myDoc");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"MyList.PhoneNumber", "010-0000-0000" }
            };
            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                await docref.UpdateAsync(data);
            }
        }

        async void Update_Array_Elements()
        {
            DocumentReference docref = db.Collection("Add_Aray").Document("myDoc");
            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                await docref.UpdateAsync("My Array", FieldValue.ArrayUnion(123, "abcd", 456));
            }
        }
    }
}

