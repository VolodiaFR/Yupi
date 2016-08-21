﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Yupi.Util
{
	public class Cryptography
	{
		public static string GetUniqueKey (int length)
		{
			string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			byte[] data = new byte[length];

			using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider ()) {
				crypto.GetNonZeroBytes (data);
			}

			StringBuilder result = new StringBuilder (length);
			foreach (byte b in data) {
				result.Append (chars [b % (chars.Length)]);
			}
			return result.ToString ();
		}
	}
}
