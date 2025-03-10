using System;
using UnityEngine;
using UnityEngine.UI;

public class MiniSkillManager : MonoBehaviour
{
    private void Start()
    {
        MiniSkillManager.THIS = this;
    }

    public void AddIcon(int progress, string code)
    {
        if (MiniSkillScript.minis.ContainsKey(code))
        {
            MiniSkillScript.minis[code].SetModel(progress, code);
            MiniSkillScript.minis[code].InitGfx();
            return;
        }
        Image image = UnityEngine.Object.Instantiate<Image>(this.miniPrefab);
        image.transform.SetParent(base.gameObject.transform, false);
        MiniSkillScript.minis.Add(code, image.GetComponent<MiniSkillScript>());
        MiniSkillScript.minis[code].SetModel(progress, code);
    }

    private void Update()
    {
    }

    public static MiniSkillManager THIS;

	public Image miniPrefab;
}
