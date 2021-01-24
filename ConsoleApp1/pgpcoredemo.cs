using PgpCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public class pgpcoredemo
    {
        public void Sample()
        {
            using (PGP pgp = new PGP())
            {
                //https://www.nuget.org/packages/PgpCore/1.2.0 -- use this nuget
                //https://blog.bitscry.com/2018/07/05/pgp-encryption-and-decryption-in-c/ -- refer this bog
               
                // Generate keys
                pgp.GenerateKey(@"D:\pgp\keys\public.asc", @"D:\pgp\keys\private.asc", "email@email.com", "password");

                //use this
                // Encrypt stream
                using (FileStream inputFileStream = new FileStream(@"D:\pgp\keys\content.txt", FileMode.Open))
                using (Stream outputFileStream = File.Create(@"D:\pgp\keys\encrypted.pgp"))
                using (Stream publicKeyStream = new FileStream(@"D:\pgp\keys\public.asc", FileMode.Open))
                    pgp.EncryptStream(inputFileStream, outputFileStream, publicKeyStream, true, true);

                // Decrypt stream
                using (FileStream inputFileStream = new FileStream(@"D:\pgp\keys\encrypted.pgp", FileMode.Open))
                using (Stream outputFileStream = File.Create(@"D:\pgp\keys\decrypted2.txt"))
                using (Stream privateKeyStream = new FileStream(@"D:\pgp\keys\private.asc", FileMode.Open))
                    pgp.DecryptStream(inputFileStream, outputFileStream, privateKeyStream, "password");
            }
        }
    }
}
