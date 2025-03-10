using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebImage : MonoBehaviour
{
    public void SetSizeAndUrl(int w, int h, string url)
    {
        this.w = w;
        this.h = h;
        this.url = url;
        this.loading = true;
        if (this.inited)
        {
            this.UpdateSizeAndUrl();
        }
    }

    public int GetHeight()
    {
        return this.h;
    }

    private void UpdateSizeAndUrl()
    {
        if (this.url.StartsWith("inner:"))
        {
            string[] array = this.url.Split(new char[]
            {
                ':'
            });
            string a = array[1];
            int num = int.Parse(array[2]);
            Sprite[] sprites = InventoryItem.sprites;
            if (!(a == "CLAN"))
            {
                if (!(a == "PACK"))
                {
                    if (!(a == "INV"))
                    {
                        if (!(a == "SKILL"))
                        {
                            if (!(a == "PROG"))
                            {
                                if (a == "SKIN")
                                {
                                    sprites = RobotScript.sprites;
                                }
                            }
                            else
                            {
                                sprites = ProgAction.sprites;
                            }
                        }
                        else
                        {
                            sprites = SkillButtonScript.sprites;
                        }
                    }
                    else
                    {
                        sprites = InventoryItem.sprites;
                    }
                }
                else
                {
                    sprites = PackSpriteScript.sprites;
                }
            }
            else
            {
                sprites = ClanSpriteScript.sprites;
            }
            Sprite sprite = sprites[num];
            this.image.sprite = sprite;
            this.image.SetNativeSize();
            Vector2 sizeDelta = this.image.rectTransform.sizeDelta;
            if (this.w > 0)
            {
                sizeDelta.x = (float)this.w * sizeDelta.x;
                sizeDelta.y = (float)this.w * sizeDelta.y;
            }
            else if (this.w == -1)
            {
                sizeDelta.x = 0.5f * sizeDelta.x;
                sizeDelta.y = 0.5f * sizeDelta.y;
            }
            this.image.rectTransform.sizeDelta = sizeDelta;
            this.loading = false;
            this.image.color = new Color(1f, 1f, 1f, 1f);
            return;
        }
        if (this.url != "")
        {
            this.loading = true;
            this.UpdateSizeAndUrlNoCheck();
            return;
        }
        UnityEngine.Debug.Log("no url to load");
    }

    private IEnumerator LoadImage()
    {
        /*www = new WWW(this.url);
        try
        {
            yield return www;
            this.loading = false;
            this.image.color = new Color(1f, 1f, 1f, 1f);
            Texture2D texture2D = M3Decompressor.M3Decompress(www.bytes);
            if (texture2D != null)
            {
                Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f));
                sprite.texture.filterMode = FilterMode.Trilinear;
                this.image.sprite = sprite;
                WebImage.ImgCache.Add(this.url, texture2D);
            }
        }
        finally
        {
            if (www != null)
            {
                ((IDisposable)www).Dispose();
            }
        }
        www = null;
        yield break;
        yield break;*/
        using (WWW www = new WWW(url))
        {
            yield return www;
            loading = false;
            image.color = new Color(1f, 1f, 1f, 1f);
            Texture2D texture2D = M3Decompressor.M3Decompress(www.bytes);
            if (texture2D != null)
            {
                Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0f, 0f));
                sprite.texture.filterMode = FilterMode.Trilinear;
                image.sprite = sprite;
                ImgCache.Add(url, texture2D);
            }
        }
    }

    private void UpdateSizeAndUrlNoCheck()
    {
        if (!this.off)
        {
            this.image.sprite = this.Loader;
            Vector2 sizeDelta = this.image.rectTransform.sizeDelta;
            sizeDelta.x = (float)this.w;
            sizeDelta.y = (float)this.h;
            this.image.rectTransform.sizeDelta = sizeDelta;
            if (WebImage.ImgCache.ContainsKey(this.url))
            {
                Texture2D texture2D = WebImage.ImgCache[this.url];
                this.loading = false;
                this.image.color = new Color(1f, 1f, 1f, 1f);
                Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f));
                sprite.texture.filterMode = FilterMode.Trilinear;
                this.image.sprite = sprite;
                return;
            }
            base.StartCoroutine(this.LoadImage());
        }
    }

    private void Start()
    {
        this.inited = true;
        this.image = base.GetComponent<Image>();
        if (!this.off)
        {
            this.UpdateSizeAndUrl();
        }
    }

    private void Update()
    {
        if (!this.off && this.loading)
        {
            this.image.color = new Color(1f, 1f, 1f, 0.1f + 0.1f * Mathf.Sin(5f * Time.unscaledTime));
        }
    }
        
	public Sprite Loader;

	private bool inited;

	public static bool cacheInited = false;

	public static Dictionary<string, Texture2D> ImgCache = new Dictionary<string, Texture2D>();

	public string url = "";

	public int w;

	public int h;

	private bool loading = true;

	public bool off;

	private Image image;
}

