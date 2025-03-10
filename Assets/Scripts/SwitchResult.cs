using System;
using System.Collections;

public class SwitchResult
{
	public SwitchResult()
	{
		this.ThereIs = false;
	}

	public bool ThereIs;

	public bool WithMinus;

	public ArrayList PostStrings = new ArrayList();

	public int PostCharIndex;
}
