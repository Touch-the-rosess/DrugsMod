using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class M3Decompressor : MonoBehaviour
{
	// Token: 0x060001A5 RID: 421 RVA: 0x00017ECC File Offset: 0x000160CC
	public static Texture2D M3Decompress(byte[] source)
	{
		if (!M3Decompressor.inited)
		{
			M3Decompressor.inited = true;
			M3Decompressor.inBuffer = new SmartBuffer(2500000);
			M3Decompressor.outBuffer = new SmartBuffer(2500000);
		}
		if (source.Length < 15)
		{
			return null;
		}
		int num = Convert.ToInt32(BitConverter.ToUInt16(source, 0));
		int num2 = Convert.ToInt32(BitConverter.ToUInt16(source, 2));
		for (int i = 0; i < 10; i++)
		{
			M3Decompressor.operations[i] = source[4 + i];
		}
		M3Decompressor.inBuffer.copyFromArray(source, 14);
		Texture2D texture2D = new Texture2D(num, num2, TextureFormat.RGBA32, false);
		for (int j = 0; j < 10; j++)
		{
			switch (M3Decompressor.operations[j])
			{
			case 1:
				M3Decompressor.UnDelta();
				M3Decompressor.SwapBuffers();
				break;
			case 2:
				M3Decompressor.UnNgramm();
				M3Decompressor.SwapBuffers();
				break;
			case 3:
				M3Decompressor.UnRLE();
				M3Decompressor.SwapBuffers();
				break;
			case 4:
				for (int k = 0; k < num2; k++)
				{
					for (int l = 0; l < num; l++)
					{
						texture2D.SetPixel(l, num2 - 1 - k, new Color((float)M3Decompressor.inBuffer.get(4 * (k * num + l)) / 255f, (float)M3Decompressor.inBuffer.get(4 * (k * num + l) + 1) / 255f, (float)M3Decompressor.inBuffer.get(4 * (k * num + l) + 2) / 255f, (float)M3Decompressor.inBuffer.get(4 * (k * num + l) + 3) / 255f));
					}
				}
				texture2D.Apply();
				break;
			case 5:
				for (int m = 0; m < num2; m++)
				{
					for (int n = 0; n < num; n++)
					{
						texture2D.SetPixel(n, num2 - 1 - m, new Color((float)M3Decompressor.inBuffer.get(0 + (m * num + n)) / 255f, (float)M3Decompressor.inBuffer.get(num * num2 + (m * num + n)) / 255f, (float)M3Decompressor.inBuffer.get(2 * (num * num2) + (m * num + n)) / 255f, (float)M3Decompressor.inBuffer.get(3 * (num * num2) + (m * num + n)) / 255f));
					}
				}
				texture2D.Apply();
				break;
			}
		}
		return texture2D;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0001813C File Offset: 0x0001633C
	public static void UnNgramm()
	{
		M3Decompressor.outBuffer.clear();
		int i = 0;
		byte b = M3Decompressor.inBuffer.get(i);
		i++;
		for (int j = 0; j < 256; j++)
		{
			M3Decompressor.dictionary[j] = 0;
		}
		byte b2 = M3Decompressor.inBuffer.get(i);
		i++;
		while (i < 1560 && b2 != b)
		{
			M3Decompressor.dictionary[(int)b2] = i;
			while (i < 1560 && b2 != b)
			{
				b2 = M3Decompressor.inBuffer.get(i);
				i++;
			}
			b2 = M3Decompressor.inBuffer.get(i);
			i++;
		}
		if (i >= 1560)
		{
			throw new Exception("M3G NGRAMM CORRUPTED?!");
		}
		int num = 0;
		while (i < M3Decompressor.inBuffer.getLength())
		{
			num++;
			b2 = M3Decompressor.inBuffer.get(i);
			i++;
			if (M3Decompressor.dictionary[(int)b2] > 0)
			{
				int num2 = M3Decompressor.dictionary[(int)b2];
				byte b3 = M3Decompressor.inBuffer.get(num2);
				num2++;
				while (b3 != b)
				{
					M3Decompressor.outBuffer.push(b3);
					b3 = M3Decompressor.inBuffer.get(num2);
					num2++;
				}
			}
			else if (b2 == b)
			{
				b2 = M3Decompressor.inBuffer.get(i);
				i++;
				M3Decompressor.outBuffer.push(b2);
			}
			else
			{
				M3Decompressor.outBuffer.push(b2);
			}
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00018290 File Offset: 0x00016490
	public static void UnRLE()
	{
		byte b = M3Decompressor.inBuffer.get(0);
		byte b2 = M3Decompressor.inBuffer.get(1);
		M3Decompressor.outBuffer.clear();
		int i = 2;
		while (i < M3Decompressor.inBuffer.getLength())
		{
			byte b3 = M3Decompressor.inBuffer.get(i);
			i++;
			if (b3 == b)
			{
				int num = (int)M3Decompressor.inBuffer.get(i);
				i++;
				byte b4 = M3Decompressor.inBuffer.get(i);
				i++;
				for (int j = 0; j < num; j++)
				{
					M3Decompressor.outBuffer.push(b4);
				}
			}
			else if (b3 == b2)
			{
				int num2 = (int)M3Decompressor.inBuffer.get(i);
				i++;
				int num3 = (int)M3Decompressor.inBuffer.get(i);
				i++;
				byte b5 = M3Decompressor.inBuffer.get(i);
				i++;
				for (int k = 0; k < num2 + 256 * num3; k++)
				{
					M3Decompressor.outBuffer.push(b5);
				}
			}
			else
			{
				M3Decompressor.outBuffer.push(b3);
			}
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00018398 File Offset: 0x00016598
	public static void UnDelta()
	{
		M3Decompressor.outBuffer.clear();
		int num = 0;
		for (int i = 0; i < M3Decompressor.inBuffer.getLength(); i++)
		{
			int num2 = num + (int)M3Decompressor.inBuffer.get(i);
			if (num2 > 255)
			{
				num2 -= 256;
			}
			num = num2;
			M3Decompressor.outBuffer.push((byte)num2);
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x000183F2 File Offset: 0x000165F2
	public static void SwapBuffers()
	{
		SmartBuffer smartBuffer = M3Decompressor.inBuffer;
		M3Decompressor.inBuffer = M3Decompressor.outBuffer;
		M3Decompressor.outBuffer = smartBuffer;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00002512 File Offset: 0x00000712
	private void Start()
	{
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00002512 File Offset: 0x00000712
	private void Update()
	{
	}

	// Token: 0x040003BB RID: 955
	public static SmartBuffer inBuffer;

	// Token: 0x040003BC RID: 956
	public static SmartBuffer outBuffer;

	// Token: 0x040003BD RID: 957
	public static byte[] operations = new byte[10];

	// Token: 0x040003BE RID: 958
	public static int[] dictionary = new int[256];

	// Token: 0x040003BF RID: 959
	public static bool inited = false;
}
