using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.Build.Utilities;
using UnityEngine.Experimental;
using UnityEditor.Build;
using UnityEngine;
using System.Security.Cryptography;

namespace test
{
    public class testCls : MonoBehaviour
    {
        public GameObject go;
        // Use this for initialization
        void Start()
        {

            //             byte[] bytes;
            //             var md4 = MD4.Create();
            //             bytes = Encoding.ASCII.GetBytes("ec71ca93f02bd4d4194d252bc9679127");
            //             md4.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
            //             bytes = BitConverter.GetBytes(0);
            //             md4.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
            //             bytes = BitConverter.GetBytes(objectID.localIdentifierInFile);
            //             md4.TransformFinalBlock(bytes, 0, bytes.Length);
            //             var hash = BitConverter.ToInt64(md4.Hash, 0);

        }

        
    }
}

