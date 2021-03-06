﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TecCardAPI.Util
{
    public static class Hash
    {
        public static string MakeHash(this string value)
        {
            return MakeHashImpl(value);
        }

        private static string MakeHashImpl(string value)
        {
            using (var hash = SHA512.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                var builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
