using System;
using UnityEngine;

public class ImageCache : MonoBehaviour
{
    private void Start()
    {
        ImageCache.THIS = this;
        WebImage.ImgCache.Add("http://minesgame.ru/img/p_st.png", this.p_st.texture);
        WebImage.ImgCache.Add("http://minesgame.ru/img/p_sn.png", this.p_sn.texture);
        WebImage.ImgCache.Add("http://minesgame.ru/img/p_cr.png", this.p_cr.texture);
        WebImage.ImgCache.Add("http://minesgame.ru/img/p_tr.png", this.p_tr.texture);
        WebImage.ImgCache.Add("http://minesgame.ru/img/p_ss.png", this.p_ss.texture);
        WebImage.ImgCache.Add("http://minesgame.ru/img/p_os.png", this.p_os.texture);
    }

    private void Update()
    {
    }    

	public Sprite p_st;

	public Sprite p_sn;

	public Sprite p_cr;

	public Sprite p_tr;

	public Sprite p_ss;

	public Sprite p_os;

	public static ImageCache THIS;
}
