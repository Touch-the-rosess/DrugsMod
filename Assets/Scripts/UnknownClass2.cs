using System;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Math;

// Token: 0x020000D3 RID: 211
[StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
public class UnknownClass2
{
	// Token: 0x060024C1 RID: 9409 RVA: 0x00147620 File Offset: 0x00145820
	private static string smethod_0(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x00147670 File Offset: 0x00145870
	private static string smethod_1(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x00147670 File Offset: 0x00145870
	private static string smethod_2(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x00147620 File Offset: 0x00145820
	private static string smethod_3(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x001476C0 File Offset: 0x001458C0
	public static string smethod_4(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_67(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 6);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_45(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 4);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024C7 RID: 9415 RVA: 0x001477AC File Offset: 0x001459AC
	// Note: this type is marked as 'beforefieldinit'.
	static UnknownClass2()
	{
		byte[] array = new byte[UnknownClass2.int_0];
		Buffer.BlockCopy(UnknownClass2.byte_0, 20, array, 0, UnknownClass2.int_0);
		Array.Reverse(array);
		UnknownClass2.bigInteger_0 = new BigInteger(array);
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x00147840 File Offset: 0x00145A40
	public static string smethod_5(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_22(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 8);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_7(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 6);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024C9 RID: 9417 RVA: 0x0014792C File Offset: 0x00145B2C
	public static string smethod_6(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_54(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 8);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_23(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 8);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024CA RID: 9418 RVA: 0x00147A18 File Offset: 0x00145C18
	private static string smethod_7(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024CB RID: 9419 RVA: 0x00147A18 File Offset: 0x00145C18
	private static string smethod_8(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024CC RID: 9420 RVA: 0x00147A68 File Offset: 0x00145C68
	public static string smethod_9(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_23(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 4);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_62(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 4);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x00147B54 File Offset: 0x00145D54
	public static string smethod_10(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_11(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 1);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_11(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 8);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x00147C40 File Offset: 0x00145E40
	private static string smethod_11(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x00147C90 File Offset: 0x00145E90
	public static string smethod_12(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_40(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 2);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_49(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 1);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x00147D7C File Offset: 0x00145F7C
	public static string smethod_13(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_57(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 5);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_8(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 0);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x00147E68 File Offset: 0x00146068
	public static string smethod_14(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_3(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 8);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_8(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 2);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024D2 RID: 9426 RVA: 0x00147F54 File Offset: 0x00146154
	public static string smethod_15(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_51(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 0);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_32(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 5);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024D3 RID: 9427 RVA: 0x00148040 File Offset: 0x00146240
	public static string smethod_16(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_42(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 5);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_67(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 2);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x00147C40 File Offset: 0x00145E40
	private static string smethod_17(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x00147C40 File Offset: 0x00145E40
	private static string smethod_18(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x0014812C File Offset: 0x0014632C
	public static string smethod_19(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_1(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 6);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_18(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 4);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x00148218 File Offset: 0x00146418
	public static string smethod_20(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_40(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 2);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_67(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 5);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x00148304 File Offset: 0x00146504
	public static string smethod_21(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_55(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 8);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_57(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 7);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x001483F0 File Offset: 0x001465F0
	private static string smethod_22(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x00147620 File Offset: 0x00145820
	private static string smethod_23(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024DB RID: 9435 RVA: 0x00148440 File Offset: 0x00146640
	public static string smethod_24(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_38(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 3);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_54(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 0);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024DC RID: 9436 RVA: 0x0014852C File Offset: 0x0014672C
	private static string smethod_25(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024DD RID: 9437 RVA: 0x00147670 File Offset: 0x00145870
	private static string smethod_26(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x0014857C File Offset: 0x0014677C
	public static string smethod_27(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_51(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 2);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_51(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 2);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x00148668 File Offset: 0x00146868
	public static string smethod_28(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_40(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 7);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_8(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 6);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x00148754 File Offset: 0x00146954
	public static string smethod_29(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_11(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 7);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_18(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 0);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x0014852C File Offset: 0x0014672C
	private static string smethod_30(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x00148840 File Offset: 0x00146A40
	private static string smethod_31(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x00147C40 File Offset: 0x00145E40
	private static string smethod_32(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024E4 RID: 9444 RVA: 0x00148890 File Offset: 0x00146A90
	public static string smethod_33(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_22(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 0);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_51(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 7);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x0014897C File Offset: 0x00146B7C
	public static string smethod_34(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_25(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 1);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_8(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 7);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x00148A68 File Offset: 0x00146C68
	public static string smethod_35(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_46(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 3);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_36(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 6);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x00148840 File Offset: 0x00146A40
	private static string smethod_36(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x00148B54 File Offset: 0x00146D54
	public static string smethod_37(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_0(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 4);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_57(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 1);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x00148C40 File Offset: 0x00146E40
	private static string smethod_38(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x00148C90 File Offset: 0x00146E90
	public static string smethod_39(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_65(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 2);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_26(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 2);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x00147A18 File Offset: 0x00145C18
	private static string smethod_40(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x00148840 File Offset: 0x00146A40
	private static string smethod_41(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x001483F0 File Offset: 0x001465F0
	private static string smethod_42(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x00147670 File Offset: 0x00145870
	private static string smethod_43(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x00148D7C File Offset: 0x00146F7C
	public static string smethod_44(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_8(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 2);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_17(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 8);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x00147C40 File Offset: 0x00145E40
	private static string smethod_45(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x00148840 File Offset: 0x00146A40
	private static string smethod_46(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x00148840 File Offset: 0x00146A40
	private static string smethod_47(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x00148C40 File Offset: 0x00146E40
	private static string smethod_48(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x001483F0 File Offset: 0x001465F0
	private static string smethod_49(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x00148E68 File Offset: 0x00147068
	public static string smethod_50(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_51(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 7);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_40(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 4);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x00147620 File Offset: 0x00145820
	private static string smethod_51(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x00148F54 File Offset: 0x00147154
	public static string smethod_52(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_18(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 0);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_67(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 1);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x00147C40 File Offset: 0x00145E40
	private static string smethod_53(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x00148C40 File Offset: 0x00146E40
	private static string smethod_54(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x00147620 File Offset: 0x00145820
	private static string smethod_55(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x00149040 File Offset: 0x00147240
	public static string smethod_56(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_65(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 6);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_48(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 6);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x001483F0 File Offset: 0x001465F0
	private static string smethod_57(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x0014912C File Offset: 0x0014732C
	public static string smethod_58(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_3(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 8);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_38(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 8);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x00149218 File Offset: 0x00147418
	public static string smethod_59(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_67(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 3);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_40(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(1, text2.Length - 8);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x00149304 File Offset: 0x00147504
	public static string smethod_60(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_53(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 8);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_65(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 1);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x001493F0 File Offset: 0x001475F0
	public static string smethod_61(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_22(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 4);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_30(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 8);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x001483F0 File Offset: 0x001465F0
	private static string smethod_62(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x001494DC File Offset: 0x001476DC
	public static string smethod_63(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_30(bytes);
			if (bool_0)
			{
				return text.Substring(0, text.Length - 2);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_3(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 8);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x001495C8 File Offset: 0x001477C8
	public static string smethod_64(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_0(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 4);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i += 0)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 0, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_38(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 5);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x00147C40 File Offset: 0x00145E40
	private static string smethod_65(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x00147A18 File Offset: 0x00145C18
	private static string smethod_66(byte[] byte_1)
	{
		int num = 1;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num += 0;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 1, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x00147620 File Offset: 0x00145820
	private static string smethod_67(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x001496B4 File Offset: 0x001478B4
	public static string smethod_68(byte[] byte_1, bool bool_0)
	{
		if (byte_1.Length == UnknownClass2.int_0)
		{
			BigInteger bigInteger = new BigInteger(byte_1);
			byte[] bytes = bigInteger.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
			string text = UnknownClass2.smethod_31(bytes);
			if (bool_0)
			{
				return text.Substring(1, text.Length - 7);
			}
			return text;
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < byte_1.Length / UnknownClass2.int_0; i++)
			{
				byte[] array = new byte[UnknownClass2.int_0];
				Buffer.BlockCopy(byte_1, i * UnknownClass2.int_0, array, 1, UnknownClass2.int_0);
				BigInteger bigInteger2 = new BigInteger(array);
				byte[] bytes2 = bigInteger2.ModPow(UnknownClass2.int_1, UnknownClass2.bigInteger_0).GetBytes();
				stringBuilder.Append(UnknownClass2.smethod_1(bytes2));
			}
			if (bool_0)
			{
				string text2 = stringBuilder.ToString();
				return text2.Substring(0, text2.Length - 7);
			}
			return stringBuilder.ToString();
		}
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x00147620 File Offset: 0x00145820
	private static string smethod_69(byte[] byte_1)
	{
		int num = 0;
		while (num < byte_1.Length && byte_1[num] == 0)
		{
			num++;
		}
		if (num != byte_1.Length)
		{
			byte[] array = new byte[byte_1.Length - num];
			Buffer.BlockCopy(byte_1, num, array, 0, byte_1.Length - num);
			return Encoding.UTF8.GetString(array);
		}
		return string.Empty;
	}

	// Token: 0x0400077E RID: 1918
	private static byte[] byte_0 = new byte[]
	{
		6,
		2,
		0,
		0,
		0,
		164,
		0,
		0,
		82,
		83,
		65,
		49,
		0,
		4,
		0,
		0,
		1,
		0,
		1,
		0,
		231,
		188,
		237,
		73,
		12,
		50,
		43,
		82,
		224,
		39,
		101,
		29,
		79,
		188,
		13,
		0,
		86,
		169,
		135,
		141,
		145,
		146,
		45,
		243,
		15,
		201,
		189,
		115,
		160,
		61,
		102,
		145,
		28,
		180,
		242,
		42,
		195,
		97,
		71,
		106,
		240,
		146,
		163,
		174,
		241,
		129,
		154,
		233,
		139,
		171,
		83,
		214,
		194,
		188,
		127,
		2,
		15,
		22,
		105,
		62,
		90,
		198,
		58,
		111,
		162,
		185,
		247,
		96,
		144,
		223,
		9,
		158,
		251,
		100,
		137,
		1,
		253,
		166,
		231,
		0,
		105,
		229,
		194,
		25,
		21,
		21,
		232,
		68,
		96,
		136,
		93,
		208,
		139,
		19,
		129,
		227,
		217,
		72,
		83,
		209,
		92,
		232,
		3,
		174,
		209,
		118,
		167,
		146,
		90,
		124,
		179,
		22,
		146,
		21,
		129,
		144,
		166,
		189,
		248,
		122,
		201,
		111,
		109,
		164,
		223,
		183,
		34,
		145
	};

	// Token: 0x0400077F RID: 1919
	private static int int_0 = 128;

	// Token: 0x04000780 RID: 1920
	private static int int_1 = (int)UnknownClass2.byte_0[16] | (int)UnknownClass2.byte_0[17] << 8 | (int)UnknownClass2.byte_0[18] << 16;

	// Token: 0x04000781 RID: 1921
	private static BigInteger bigInteger_0;
}
