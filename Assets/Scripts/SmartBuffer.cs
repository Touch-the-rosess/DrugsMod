using System;

// Token: 0x02000036 RID: 54
public class SmartBuffer
{
	// Token: 0x060001AE RID: 430 RVA: 0x0001842B File Offset: 0x0001662B
	public SmartBuffer(int maxLen)
	{
		this.buffer = new byte[maxLen];
		this.len = 0;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00018448 File Offset: 0x00016648
	public int copyFromArray(byte[] copyFrom, int offset = 0)
	{
		if (copyFrom.Length - offset > this.buffer.Length)
		{
			throw new Exception("SmartBuffer cant copy - too long");
		}
		for (int i = offset; i < copyFrom.Length; i++)
		{
			this.buffer[i - offset] = copyFrom[i];
		}
		this.len = copyFrom.Length - offset;
		return this.len;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0001849B File Offset: 0x0001669B
	public int getLength()
	{
		return this.len;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000184A3 File Offset: 0x000166A3
	public void clear()
	{
		this.len = 0;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x000184AC File Offset: 0x000166AC
	public byte get(int i)
	{
		if (i > this.len)
		{
			throw new Exception("SmartBuffer index out of range");
		}
		return this.buffer[i];
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x000184CA File Offset: 0x000166CA
	public void push(byte b)
	{
		if (this.len >= this.buffer.Length)
		{
			throw new Exception("SmartBuffer cant push - buffer is full");
		}
		this.buffer[this.len] = b;
		this.len++;
	}

	// Token: 0x040003C0 RID: 960
	private byte[] buffer;

	// Token: 0x040003C1 RID: 961
	private int len;
}
