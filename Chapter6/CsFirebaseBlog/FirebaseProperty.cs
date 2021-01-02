using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace CsFirebaseBlog
{
    [FirestoreData]
    public class FirebaseProperty
    {
        [FirestoreProperty]
        //필드 이름
        public string Name { get; set; }

        [FirestoreProperty]
        //필드 이름
        public string PhoneNo { get; set; }
    }
}
