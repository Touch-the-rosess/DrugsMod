using System;

public class MapBlock
{
    public MapBlock()
    {
        this.isLoaded = false;
        this.notSaved = false;
    }

    public void Init()
    {
        this.isLoaded = true;
        this.notSaved = true;
        this.data = new byte[1024];
    }
    
	public byte[] data;

	public bool notSaved;

	public bool isLoaded;
}
