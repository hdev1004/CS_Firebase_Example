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
            Delete_An_Entire_Document();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Delete_A_Field_Within_A_Document();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Delete_An_Element_Inside_A_List();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Delete_An_Element_Inside_An_Array();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Delete_All_Documents_In_A_Collection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {  
            string path = AppDomain.CurrentDomain.BaseDirectory + @"cs-firebase-blog-firebase-adminsdk-18cwe-b5971a4787.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("cs-firebase-blog");
        }

        void Delete_An_Entire_Document()
        {
            DocumentReference docref = db.Collection("Testing2").Document("Doc1");
            docref.DeleteAsync();
            MessageBox.Show("Done");

        }

        void Delete_A_Field_Within_A_Document()
        {
            DocumentReference docref = db.Collection("Testing2").Document("Doc2");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"age", FieldValue.Delete }
            };
            docref.UpdateAsync(data);
            MessageBox.Show("Done");
        }

        void Delete_An_Element_Inside_A_List()
        {
            DocumentReference docref = db.Collection("Add_ListOfObjects").Document("myDoc");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"MyList.PhoneNumber", FieldValue.Delete }
            }; ;
            docref.UpdateAsync(data);
            MessageBox.Show("Done");
        }

        void Delete_An_Element_Inside_An_Array()
        {
            DocumentReference docref = db.Collection("Add_Aray").Document("myDoc");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"My Array", FieldValue.ArrayRemove(456, true) }
            };
            docref.UpdateAsync(data);
            MessageBox.Show("Done");
        }

        async void Delete_All_Documents_In_A_Collection()
        {
            CollectionReference collref = db.Collection("Testing2");
            QuerySnapshot snap = await collref.GetSnapshotAsync();

            foreach (DocumentSnapshot doc in snap.Documents)
            {
                await doc.Reference.DeleteAsync();
            }

        }
    }
}

