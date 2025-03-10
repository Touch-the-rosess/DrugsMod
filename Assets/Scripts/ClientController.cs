using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClientController : MonoBehaviour
{
    private void Start()
    {
        this.stringGen();
        this.robotRenderer = this.mainRenderer.GetComponent<RobotRenderer>();
        this.terrainRenderer = this.mainRenderer.GetComponent<TerrainRendererScript>();
        this.serverTime = this.obvyazkaObject.GetComponent<ServerTime>();
        this.obvyazka = this.obvyazkaObject.GetComponent<Obvyazka>();
        ClientController.THIS = this;
        this.Cursor.SetActive(false);
        this.autoDiggButton.onClick.AddListener(new UnityAction(this.ToggleAutoDigg));
        this.ShowAutoDigg();
        this.NoGUIClickPad.onClick.AddListener(new UnityAction(this.NoGUIClick));
    }

    private void ToggleAutoDigg()
    {
        ClientController.CanGoto = false;
        ServerTime.THIS.SendTypicalMessage(-1, this.str1, 0, 0, "_");
    }

    public void ShowAutoDigg()
    {
        if (!ClientController.autoDigg)
        {
            this.autoDiggButtonText.text = "АВТОКОПА-";
            this.autoDiggButtonText.color = new Color(0.5f, 0.5f, 0.5f);
            this.autoDiggButton.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.7f);
            return;
        }
        this.autoDiggButtonText.text = "АВТОКОПА+";
        this.autoDiggButtonText.color = new Color(1f, 1f, 1f);
        this.autoDiggButton.GetComponent<Image>().color = new Color(0.7f, 0.2f, 0.2f, 0.7f);
    }

    public void AddFX(int x, int y, int fx)
    {
        if (Time.unscaledDeltaTime > 0.5f)
        {
            return;
        }
        switch (fx)
        {
            case 0:
                this.AddAnimation(1, x, y);
                return;
            case 1:
            case 9:
                break;
            case 2:
                this.AddAnimation(2, x, y);
                if (ClientConfig.SOUND_DEATH)
                {
                    this.AddVolumedSound(x, y, 4, 200f, 1f);
                    return;
                }
                break;
            case 3:
                if (ClientConfig.SOUND_BOMBTICK)
                {
                    this.AddVolumedSound(x, y, 3, 49f, 1f);
                    return;
                }
                break;
            case 4:
                if (ClientConfig.SOUND_BOMB)
                {
                    this.AddVolumedSound(x, y, 2, 49f, 1f);
                    return;
                }
                break;
            case 5:
                if (ClientConfig.SOUND_DESTROY)
                {
                    this.AddVolumedSound(x, y, 5, 49f, 1f);
                    return;
                }
                break;
            case 6:
                if (ClientConfig.SOUND_DIZZ)
                {
                    this.AddVolumedSound(x, y, 11, 49f, 1f);
                    return;
                }
                break;
            case 7:
                if (ClientConfig.SOUND_EMI)
                {
                    this.AddVolumedSound(x, y, 6, 49f, 1f);
                    return;
                }
                break;
            case 8:
                if (ClientConfig.SOUND_GEOLOGY)
                {
                    this.AddVolumedSound(x, y, 7, 49f, 1f);
                    return;
                }
                break;
            case 10:
                if (ClientConfig.SOUND_TP_IN)
                {
                    this.AddVolumedSound(x, y, 12, 49f, 1f);
                    return;
                }
                break;
            case 11:
                if (ClientConfig.SOUND_TP_OUT)
                {
                    this.AddVolumedSound(x, y, 13, 49f, 1f);
                    return;
                }
                break;
            case 12:
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.nohpfxPrefab);
                    gameObject.transform.SetParent(this.RenderWrapper.transform, false);
                    gameObject.transform.position = new Vector3((float)x + 0.5f, (float)(-(float)y) - 0.5f, -7f);
                    return;
                }
            case 13:
                {
                    GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.nohpfxSmallPrefab);
                    gameObject2.transform.SetParent(this.RenderWrapper.transform, false);
                    gameObject2.transform.position = new Vector3((float)x + 0.5f, (float)(-(float)y) - 0.5f, -7f);
                    return;
                }
            case 14:
                {
                    GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.nohpfxSmallPrefab);
                    gameObject3.transform.SetParent(this.RenderWrapper.transform, false);
                    gameObject3.transform.position = new Vector3((float)x + 0.5f, (float)(-(float)y), -7f);
                    return;
                }
            case 15:
                {
                    GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.nohpfxSmallPrefab);
                    gameObject4.transform.SetParent(this.RenderWrapper.transform, false);
                    gameObject4.transform.position = new Vector3((float)x + 0.5f, (float)(-(float)y) - 0.7f, -7f);
                    gameObject4.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                    return;
                }
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
                {
                    GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.smokePrefab);
                    gameObject5.transform.SetParent(this.RenderWrapper.transform, false);
                    gameObject5.GetComponent<ParticleSystem>().startColor = this.smokeColors[fx - 16];
                    gameObject5.transform.position = new Vector3((float)x + 0.5f, (float)(-(float)y) - 0.5f, -3f);
                    return;
                }
            case 24:
                {
                    GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.volcanoPrefab);
                    gameObject6.transform.SetParent(this.RenderWrapper.transform, false);
                    gameObject6.transform.position = new Vector3((float)x + 0.5f, (float)(-(float)y) - 0.5f, -3f);
                    if (ClientConfig.SOUND_VOLC)
                    {
                        this.AddVolumedSound(x, y, 14, 49f, 1f);
                        return;
                    }
                    break;
                }
            case 25:
                if (ClientConfig.SOUND_C190)
                {
                    this.AddVolumedSound(x, y, 15, 49f, 0.2f);
                }
                break;
            default:
                return;
        }
    }

    private void AddGunShot(int x, int y, int bid, int color)
    {
        int num = -1;
        GameObject free = this.gunShotPool.GetFree(out num);
        if (num != -1)
        {
            free.transform.SetParent(this.RenderWrapper.transform, false);
            free.GetComponent<GunShotScript>().Setup(x, y, bid, color, num);
        }
    }

    public void AddDirectedFX(int bid, int x, int y, int fx, int dir, int col)
    {
        switch (fx)
        {
            case -1:
            case 7:
                this.AddGunShot(x, y, bid, col);
                return;
            case 0:
                if (bid != this.myBotId || this.isProgrammator)
                {
                    this.AddBz(x, y, dir, bid != this.myBotId || ClientController.ownSounds);
                    return;
                }
                break;
            case 1:
                this.AddBoom(x, y, dir, col);
                return;
            case 2:
                if (dir > 230)
                {
                    dir = 500 + (dir - 230) * 20;
                }
                else if (dir > 200)
                {
                    dir = 200 + (dir - 200) * 10;
                }
                this.AddCrys(x, y, this.crysFromCode[col], dir, bid, 0);
                return;
            case 3:
                if (dir > 230)
                {
                    dir = 500 + (dir - 230) * 20;
                }
                else if (dir > 200)
                {
                    dir = 200 + (dir - 200) * 10;
                }
                this.AddCrys(x, y, this.crysFromCode[col], dir, bid, 150);
                return;
            case 4:
                if (dir > 230)
                {
                    dir = 500 + (dir - 230) * 20;
                }
                else if (dir > 200)
                {
                    dir = 200 + (dir - 200) * 10;
                }
                this.AddCrys2(x, y, this.crysFromCode[col], dir, bid);
                return;
            case 5:
                if (RobotRenderer.THIS.bots.ContainsKey(bid))
                {
                    if (ClientConfig.SOUND_HEAL)
                    {
                        this.AddVolumedSound(x, y, 8, 49f, 1f);
                    }
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.healfxPrefab);
                    gameObject.transform.parent = RobotRenderer.THIS.bots[bid].transform;
                    gameObject.transform.position = RobotRenderer.THIS.bots[bid].transform.position + new Vector3(0f, 0f, -0.5f);
                    return;
                }
                break;
            case 6:
                if (RobotRenderer.THIS.bots.ContainsKey(bid))
                {
                    if (ClientConfig.SOUND_HURT)
                    {
                        this.AddVolumedSound(x, y, 9, 49f, 1f);
                    }
                    RobotRenderer.THIS.bots[bid].GetComponent<RobotScript>().tremor = 0.5f;
                    GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.hurtfxPrefab);
                    gameObject2.GetComponent<ParticleSystem>().startColor = Color.Lerp(new Color(1f, 0f, 0f), new Color(0f, 2f, 1f), (float)col / 100f);
                    gameObject2.GetComponent<ParticleSystem>().startSize = 0.65f + (float)col / 50f;
                    gameObject2.transform.SetParent(RobotRenderer.THIS.bots[bid].transform, false);
                    gameObject2.transform.position = RobotRenderer.THIS.bots[bid].transform.position + new Vector3(0f, 0f, -0.5f);
                }
                break;
            default:
                return;
        }
    }

    private void AddCrys2(int x, int y, string crys, int dx, int dy)
    {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.crys2Prefab);
        gameObject.transform.SetParent(this.RenderWrapper.transform, false);
        gameObject.GetComponent<CrysAutScript>().SetCrys(x, y, crys, dx, dy);
    }

    private void AddCrys(int x, int y, string crys, int num, int bid, int delay)
    {
        int index = -1;
        GameObject free = this.crysPlusPool.GetFree(out index);
        if (index != -1)
        {
            free.transform.SetParent(this.RenderWrapper.transform, false);
            free.GetComponent<CrysPlusScript>().SetCrys(x, y, crys, num, bid, delay, index);
        }
    }

    private void AddBoom(int x, int y, int size, int color)
    {
        int num = -1;
        GameObject free = this.boomPool.GetFree(out num);
        if (num != -1)
        {
            free.transform.SetParent(this.RenderWrapper.transform, false);
            free.GetComponent<BoomScript>().Setup(x, y, size, color, num);
        }
    }

    public void stopAutoMove()
    {
        this.automove = false;
        this.Cursor.SetActive(false);
    }

    private void startAutoMove()
    {
        this.automove = true;
        this.Cursor.SetActive(true);
        this.Cursor.transform.position = new Vector3((float)this.GotoX + 0.5f, (float)(-(float)this.GotoY) - 0.5f, -6f);
    }

    private float euristic(int x, int y)
    {
        return this.ROAD_PAUSE * (float)(Mathf.Abs(x - this.GotoX) + Mathf.Abs(y - this.GotoY)) + 0.01f * Mathf.Sqrt((float)((x - this.GotoX) * (x - this.GotoX) + (y - this.GotoY) * (y - this.GotoY)));
    }

    private bool CheckAStarFor(RoutePoint from, int x, int y)
    {
        int num = ClientController.map.GetCell(x, y);
        if (x != this.GotoX || y != this.GotoY)
        {
            if (this.closedSet.Contains(x + y * ClientController.map.width))
            {
                return false;
            }
            if (Math.Abs(x - this.myBot.gx) + Math.Abs(y - this.myBot.gy) > ClientConfig.mouseR)
            {
                return false;
            }
            if (CellRender.UNBREAKABLE[num])
            {
                return false;
            }
        }
        float num2 = this.BZ_PAUSE;
        if (CellModel.isEmpty[num])
        {
            if (this.isRoad(num))
            {
                num2 = this.ROAD_PAUSE;
            }
            else
            {
                num2 = this.XY_PAUSE;
            }
        }
        else
        {
            num2 = this.BZ_PAUSE * (float)CellRender.BZCOST[num];
            if ((ClientConfig.mouseNoDig || !ClientController.autoDigg) && (x != this.GotoX || y != this.GotoY))
            {
                return false;
            }
        }
        if (num == 37 && PackRenderer.THIS.IsPackOn(x, y) && (x != this.GotoX || y != this.GotoY))
        {
            num2 = 1000f;
        }
        float num3 = 1f / (1f + this.euristic(x, y));
        num2 = num3 * this.XY_PAUSE + (1f - num3) * num2;
        float num4 = from.g + num2;
        if (this.openedSet.Contains(x + y * ClientController.map.width))
        {
            if (num4 < this.points[x + y * ClientController.map.width].g)
            {
                RoutePoint RoutePoint = this.points[x + y * ClientController.map.width];
                RoutePoint.g = num4;
                RoutePoint.f = num4 + this.euristic(x, y);
                RoutePoint.px = from.x;
                RoutePoint.py = from.y;
            }
        }
        else
        {
            RoutePoint value = default(RoutePoint);
            value.x = x;
            value.y = y;
            value.cell = num;
            value.g = num4;
            value.f = num4 + this.euristic(x, y);
            value.px = from.x;
            value.py = from.y;
            this.points[x + y * ClientController.map.width] = value;
            this.openedSet.Add(x + y * ClientController.map.width);
        }
        return false;
    }

    private void UpdateRoute()
    {
        if (!this.automove)
        {
            return;
        }
        if (this.myBot.gx == this.GotoX && this.myBot.gy == this.GotoY)
        {
            this.stopAutoMove();
            return;
        }
        int num = this.myBot.gx - this.GotoX;
        int num2 = this.myBot.gy - this.GotoY;
        if (num * num + num2 * num2 > ClientConfig.mouseR * ClientConfig.mouseR)
        {
            this.stopAutoMove();
            return;
        }
        this.route.Clear();
        this.points.Clear();
        this.openedSet.Clear();
        this.closedSet.Clear();
        RoutePoint RoutePoint = default(RoutePoint);
        RoutePoint.x = this.myBot.gx;
        RoutePoint.y = this.myBot.gy;
        RoutePoint.cell = 32;
        RoutePoint.h = this.euristic(this.myBot.gx, this.myBot.gy);
        RoutePoint.g = 0f;
        RoutePoint.f = RoutePoint.h + RoutePoint.g;
        this.points.Add(RoutePoint.x + RoutePoint.y * ClientController.map.width, RoutePoint);
        this.openedSet.Add(RoutePoint.x + RoutePoint.y * ClientController.map.width);
        int i = 0;
        while (i < ClientConfig.mouseMaxStack)
        {
            i++;
            float num3 = float.PositiveInfinity;
            int num4 = -1;
            foreach (int num5 in this.openedSet)
            {
                RoutePoint RoutePoint2 = this.points[num5];
                if (RoutePoint2.f < num3)
                {
                    num3 = RoutePoint2.f;
                    num4 = num5;
                }
            }
            if (num4 == -1)
            {
                this.stopAutoMove();
                return;
            }
            RoutePoint RoutePoint3 = this.points[num4];
            this.openedSet.Remove(num4);
            this.closedSet.Add(num4);
            this.CheckAStarFor(RoutePoint3, RoutePoint3.x + 1, RoutePoint3.y);
            this.CheckAStarFor(RoutePoint3, RoutePoint3.x - 1, RoutePoint3.y);
            this.CheckAStarFor(RoutePoint3, RoutePoint3.x, RoutePoint3.y + 1);
            this.CheckAStarFor(RoutePoint3, RoutePoint3.x, RoutePoint3.y - 1);
            if (this.openedSet.Contains(this.GotoX + ClientController.map.width * this.GotoY))
            {
                break;
            }
        }
        if (i == ClientConfig.mouseMaxStack)
        {
            this.stopAutoMove();
            return;
        }
        RoutePoint RoutePoint4 = this.points[this.GotoX + ClientController.map.width * this.GotoY];
        i = 0;
        while (i < ClientConfig.mouseMaxLen)
        {
            this.route.Add(RoutePoint4);
            RoutePoint4 = this.points[RoutePoint4.px + ClientController.map.width * RoutePoint4.py];
            if (RoutePoint4.x == this.myBot.gx && RoutePoint4.y == this.myBot.gy)
            {
                break;
            }
        }
        if (i == ClientConfig.mouseMaxLen)
        {
            this.stopAutoMove();
            return;
        }
    }

    public void FromMapGoto(int x, int y)
    {
        if (!ProgrammatorView.active && (!this.isProgrammator || ProgPanel.handMode))
        {
            this.tryGotoX = x;
            this.tryGotoY = y;
            ClientController.CanGoto = true;
            this.TryToGoto(true);
        }
    }

    private void NoGUIClick()
    {
        if (this.myBot != null && !ConnectionManager.disconnected && !ChatManager.THIS.ChatInput.isFocused && !GUIManager.THIS.localChatInput.isFocused && !ProgrammatorView.active && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "InputField") && !AYSWindowManager.THIS.gameObject.activeSelf && (!this.isProgrammator || ProgPanel.handMode) && !TutorialNavigation.THIS.lens.activeSelf)
        {
            Vector2 v = UnityEngine.Input.mousePosition;
            Vector2 vector = Camera.main.ScreenToWorldPoint(v);
            int num = Mathf.FloorToInt(vector.x);
            int num2 = -Mathf.CeilToInt(vector.y);
            this.tryGotoX = num;
            this.tryGotoY = num2;
            base.Invoke("TryToGotoInvokable", 0.1f);
        }
    }

    private void TryToGotoInvokable()
    {
        this.TryToGoto(false);
    }

    private void TryToGoto(bool DisableCheck = false)
    {
        if (this.myBot == null)
        {
            return;
        }
        if (!ClientController.MouseControl)
        {
            return;
        }
        if (!ClientController.CanGoto)
        {
            ClientController.CanGoto = true;
            return;
        }
        bool flag = false;
        if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject != null && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "ChatField" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "LocalChat"))
        {
            flag = true;
        }
        if (!DisableCheck && (MapViewer.THIS.gameObject.activeSelf || OKWindowManager.THIS.gameObject.activeSelf || AYSWindowManager.THIS.gameObject.activeSelf || PopupManager.THIS.GUIWindow.activeSelf || flag))
        {
            ClientController.CanGoto = true;
            return;
        }
        int num = ClientController.map.GetCell(this.tryGotoX, this.tryGotoY);
        if (num != 83)
        {
            if ((CellRender.UNBREAKABLE[num] || num == 37) && !this.SmartPointing(this.tryGotoX + 1, this.tryGotoY) && !this.SmartPointing(this.tryGotoX - 1, this.tryGotoY) && !this.SmartPointing(this.tryGotoX, this.tryGotoY + 1) && !this.SmartPointing(this.tryGotoX, this.tryGotoY - 1) && !this.SmartPointing(this.tryGotoX + 1, this.tryGotoY + 1) && !this.SmartPointing(this.tryGotoX - 1, this.tryGotoY + 1) && !this.SmartPointing(this.tryGotoX + 1, this.tryGotoY - 1))
            {
                this.SmartPointing(this.tryGotoX - 1, this.tryGotoY - 1);
            }
            num = ClientController.map.GetCell(this.tryGotoX, this.tryGotoY);
            if (CellRender.UNBREAKABLE[num])
            {
                int num2 = -1;
                int num3 = -1;
                if (this.tryGotoX > this.myBot.gx)
                {
                    num2 = 1;
                }
                if (this.tryGotoY > this.myBot.gy)
                {
                    num3 = 1;
                }
                if (!this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY) && !this.SmartFreePointing(this.tryGotoX, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY + num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY + num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY))
                {
                    this.SmartFreePointing(this.tryGotoX, this.tryGotoY + num3);
                }
            }
            num = ClientController.map.GetCell(this.tryGotoX, this.tryGotoY);
            if (CellRender.UNBREAKABLE[num])
            {
                return;
            }
        }
        this.GotoX = this.tryGotoX;
        this.GotoY = this.tryGotoY;
        this.startAutoMove();
        this.UpdateRoute();
    }

    private bool SmartFreePointing(int x, int y)
    {
        if (CellModel.isEmpty[ClientController.map.GetCell(x, y)])
        {
            this.tryGotoX = x;
            this.tryGotoY = y;
            return true;
        }
        return false;
    }

    private bool SmartPointing(int x, int y)
    {
        if (ClientController.map.GetCell(x, y) == 37 && PackRenderer.THIS.IsPackOn(x, y))
        {
            this.tryGotoX = x;
            this.tryGotoY = y;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (ClientController.inited)
        {
            TutorialNavigation.THIS.UpdateArrow();
            float num = 0f;
            if ((float)this.myBot.gy > this.DEPTH)
            {
                if (this.slowRedAlpha < 0.01f)
                {
                    this.slowRedAlpha = 1f;
                }
                else
                {
                    num = 0.01f + 0.5f * Mathf.Min(0.01f * ((float)this.myBot.gy - this.DEPTH), 1f);
                }
            }
            this.slowRedAlpha = 0.9f * this.slowRedAlpha + 0.1f * num;
            this.redPad.color = new Color(1f, 0f, 0f, this.slowRedAlpha);
            if (this.myBotId != -1)
            {
                Vector2 vector = new Vector2((float)this.view_x - this.tcx, (float)this.view_y - this.tcy);
                this.aveDCamera = 0.97f * this.aveDCamera + 0.03f * Vector2.ClampMagnitude(vector, 10f);
                this.aveDCamera2 = 0.97f * this.aveDCamera2 + 0.03f * this.aveDCamera;
                float num2 = 0.02f * this.aveDCamera2.magnitude + 3E-06f * vector.sqrMagnitude * vector.sqrMagnitude;
                if (num2 > 0.3f)
                {
                    num2 = 0.3f;
                }
                this.tcx += num2 * vector.x;
                this.tcy += num2 * vector.y;
            }
            this.tremor *= 0.9f;
            this.terrainRenderer.cx = this.tcx;
            this.terrainRenderer.cy = this.tcy;
            if (this.tremor > 0.03f)
            {
                Vector3 position = this.myBot.transform.position;
                position.x += this.tremor * (UnityEngine.Random.value - 0.5f);
                position.y += this.tremor * (UnityEngine.Random.value - 0.5f);
                this.myBot.transform.position = position;
            }
            if (this.myBot != null)
            {
                this.view_x = this.myBot.gx;
                this.view_y = this.myBot.gy;
            }
            if (this.Cursor.activeSelf)
            {
                this.Cursor.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.5f + 0.5f * Mathf.Sin(10f * Time.time));
            }
            if (this.myBot != null && !ConnectionManager.disconnected && !ChatManager.THIS.ChatInput.isFocused && !GUIManager.THIS.localChatInput.isFocused && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "InputField") && !AYSWindowManager.THIS.gameObject.activeSelf && !ProgrammatorView.active)
            {
                if (UnityEngine.Input.GetKeyDown(ClientConfig.AUTOREM_KEY))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str2, 0, 0, "-");
                }
                if (UnityEngine.Input.GetKeyDown(ClientConfig.AGR_KEY))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str3, 0, 0, "-");
                }
                if (UnityEngine.Input.GetKeyDown(ClientConfig.AUTODIG_KEY))
                {
                    this.ToggleAutoDigg();
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha0))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 0, "0");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 1, "1");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 2, "2");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 3, "3");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 4, "4");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 5, "5");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha6))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 6, "6");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha7))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 7, "7");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha8))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 8, "8");
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha9))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 9, "9");
                }
                if (UnityEngine.Input.GetKeyDown(ClientConfig.PROG_KEY))
                {
                    ProgPanel.THIS.OnPlayStop();
                }
                if (UnityEngine.Input.GetKeyDown(ClientConfig.MAP_KEY) && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "InputField"))
                {
                    if (MapViewer.THIS.gameObject.activeSelf)
                    {
                        MapViewer.THIS.OnExit();
                    }
                    else
                    {
                        MapViewer.THIS.Show();
                    }
                }
                if (UnityEngine.Input.GetKeyDown(ClientConfig.INV_KEY) && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "InputField"))
                {
                    ServerTime.THIS.SendTypicalMessage(-1, this.str6, 0, 0, "_");
                }
            }
            if (this.myBot != null && !ConnectionManager.disconnected && !ChatManager.THIS.ChatInput.isFocused && !GUIManager.THIS.localChatInput.isFocused && !ProgrammatorView.active && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "InputField") && !AYSWindowManager.THIS.gameObject.activeSelf && (!this.isProgrammator || ProgPanel.handMode))
            {
                if ((UnityEngine.Input.GetKeyUp(KeyCode.LeftCommand) || UnityEngine.Input.GetKeyUp(KeyCode.RightCommand) || UnityEngine.Input.GetKeyUp(KeyCode.LeftControl) || UnityEngine.Input.GetKeyUp(KeyCode.RightControl)) && ClientController.CtrlToggle)
                {
                    this.isControl = !this.isControl;
                }
                if (UnityEngine.Input.GetKeyDown("up") || UnityEngine.Input.GetKeyDown(ClientConfig.MOVE_UP_KEY))
                {
                    this.stopAutoMove();
                    this.lastDirKey = "up";
                }
                if (UnityEngine.Input.GetKeyDown("down") || UnityEngine.Input.GetKeyDown(ClientConfig.MOVE_DOWN_KEY))
                {
                    this.stopAutoMove();
                    this.lastDirKey = "down";
                }
                if (UnityEngine.Input.GetKeyDown("left") || UnityEngine.Input.GetKeyDown(ClientConfig.MOVE_LEFT_KEY))
                {
                    this.stopAutoMove();
                    this.lastDirKey = "left";
                }
                if (UnityEngine.Input.GetKeyDown("right") || UnityEngine.Input.GetKeyDown(ClientConfig.MOVE_RIGHT_KEY))
                {
                    this.stopAutoMove();
                    this.lastDirKey = "right";
                }
                if (!ClientController.CtrlToggle)
                {
                    this.isControl = (UnityEngine.Input.GetKey(KeyCode.LeftCommand) || UnityEngine.Input.GetKey(KeyCode.RightCommand) || UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl));
                }
                if (!this.notActive && Time.unscaledTime > this.TimeForNextOperation)
                {
                    bool isShift = UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift);
                    if (UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        bool flag = false;
                        if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject != null && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "ChatField" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "LocalChat"))
                        {
                            flag = true;
                        }
                        if (!OKWindowManager.THIS.gameObject.activeSelf && !PopupManager.THIS.GUIWindow.activeSelf && !flag && GUIManager.THIS.inventoryItem != -1)
                        {
                            ServerTime.THIS.SendTypicalMessage(-1, this.str7, 0, 0, "_");
                        }
                    }
                    else if (UnityEngine.Input.GetKey(ClientConfig.GEO_KEY))
                    {
                        this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str8, this.myBot.gx, this.myBot.gy, "_");
                        this.AddTime(this.BZ_PAUSE, false);
                    }
                    else if (UnityEngine.Input.GetKey(ClientConfig.DIGG_KEY))
                    {
                        UnityEngine.Debug.Log("bz " + myBot.gx + ":" + myBot.gy);
                        this.AddBz(this.myBot.gx, this.myBot.gy, this.dir, ClientController.ownSounds);
                        this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str10, this.myBot.gx, this.myBot.gy, this.dir.ToString());
                        this.AddTime(this.BZ_PAUSE, false);
                    }
                    else if (UnityEngine.Input.GetKey(ClientConfig.WARBLOCK_KEY))
                    {
                        this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str11, this.myBot.gx, this.myBot.gy, this.dir.ToString() + "V");
                        this.AddTime(this.BZ_PAUSE, false);
                    }
                    else if (UnityEngine.Input.GetKey(ClientConfig.BLOCK_KEY))
                    {
                        this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str11, this.myBot.gx, this.myBot.gy, this.dir.ToString() + "G");
                        this.AddTime(this.BZ_PAUSE, false);
                    }
                    else if (UnityEngine.Input.GetKey(ClientConfig.ROAD_KEY))
                    {
                        this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str11, this.myBot.gx, this.myBot.gy, this.dir.ToString() + "R");
                        this.AddTime(this.BZ_PAUSE, false);
                    }
                    else if (UnityEngine.Input.GetKey(ClientConfig.QUADRO_KEY))
                    {
                        this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str11, this.myBot.gx, this.myBot.gy, this.dir.ToString() + "O");
                        this.AddTime(this.BZ_PAUSE, false);
                    }
                    else if (UnityEngine.Input.GetKey(ClientConfig.HEAL_KEY))
                    {
                        this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str13, this.myBot.gx, this.myBot.gy, "_");
                        this.AddTime(this.BZ_PAUSE, false);
                    }
                    else if ((UnityEngine.Input.GetKey("up") || UnityEngine.Input.GetKey(ClientConfig.MOVE_UP_KEY)) && this.lastDirKey == "up")
                    {
                        this.MoveOrBz(0, -1, 2, isShift, this.isControl);
                    }
                    else if ((UnityEngine.Input.GetKey("down") || UnityEngine.Input.GetKey(ClientConfig.MOVE_DOWN_KEY)) && this.lastDirKey == "down")
                    {
                        this.MoveOrBz(0, 1, 0, isShift, this.isControl);
                    }
                    else if ((UnityEngine.Input.GetKey("left") || UnityEngine.Input.GetKey(ClientConfig.MOVE_LEFT_KEY)) && this.lastDirKey == "left")
                    {
                        this.MoveOrBz(-1, 0, 1, isShift, this.isControl);
                    }
                    else if ((UnityEngine.Input.GetKey("right") || UnityEngine.Input.GetKey(ClientConfig.MOVE_RIGHT_KEY)) && this.lastDirKey == "right")
                    {
                        this.MoveOrBz(1, 0, 3, isShift, this.isControl);
                    }
                    else if (this.automove)
                    {
                        if (this.route.Count == 0)
                        {
                            this.stopAutoMove();
                        }
                        RoutePoint RoutePoint = this.route[this.route.Count - 1];
                        if (RoutePoint.x == this.myBot.gx && RoutePoint.y == this.myBot.gy)
                        {
                            this.route.RemoveAt(this.route.Count - 1);
                            if (this.route.Count == 0)
                            {
                                this.stopAutoMove();
                            }
                            else
                            {
                                RoutePoint = this.route[this.route.Count - 1];
                            }
                        }
                        if (RoutePoint.x > this.myBot.gx)
                        {
                            this.MoveOrBz(1, 0, 3, false, this.isControl);
                        }
                        else if (RoutePoint.x < this.myBot.gx)
                        {
                            this.MoveOrBz(-1, 0, 1, false, this.isControl);
                        }
                        else if (RoutePoint.y > this.myBot.gy)
                        {
                            this.MoveOrBz(0, 1, 0, false, this.isControl);
                        }
                        else if (RoutePoint.y < this.myBot.gy)
                        {
                            this.MoveOrBz(0, -1, 2, false, this.isControl);
                        }
                    }
                    if (this.TimeForNextOperation < Time.unscaledTime)
                    {
                        this.TimeForNextOperation = Time.unscaledTime;
                    }
                    if (!this.wasMove)
                    {
                        this.slowingEffect = 0.6f + 0.7f * this.slowingEffect;
                    }
                }
            }
            ClientController.serverTimeOfLastFrame = this.serverTime.NowTime();
            ClientController.clientTimeOfLastFrame = (int)(Time.unscaledTime * 1000f);
            if (UnityEngine.Random.value < 1f)
            {
                for (int i = 0; i < this.BZ_DEBUG_GEN; i++)
                {
                }
            }
        }
        //this.maxFXIF.text != "";
        if (ClientController.pongResponse != -1)
        {
            ServerTime.THIS.lastSendedTime = this.serverTime.NowTime();
            this.obvyazka.SendU("PO", ClientController.pongResponse + ":" + ServerTime.THIS.lastSendedTime.ToString());
            ClientController.pongResponse = -1;
        }
    }

    public void Tremor()
	{
	}

    private void SelfHurt()
    {
        if (this.lastHurtTime > Time.unscaledTime - 0.6f)
        {
            this.lastHurtTime = Time.unscaledTime;
            this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str12, this.myBot.gx, this.myBot.gy, "_");
            this.AddTime(this.HURT_PAUSE, false);
        }
    }

    private GameObject AddAnimation(int type, int x, int y)
    {
        int num = -1;
        GameObject free = this.bzPool.GetFree(out num);
        if (num != -1)
        {
            free.transform.SetParent(this.RenderWrapper.transform, false);
            free.GetComponent<BzScript>().SetAnimation(type, num, new Vector3((float)x + 0.5f, (float)(-(float)y) - 0.5f, -3f));
        }
        return free;
    }

    private void AddVolumedSound(int x, int y, int num, float dump = 49f, float mult = 1f)
    {
        float num2 = (float)((this.myBot.gx - x) * (this.myBot.gx - x) + (this.myBot.gy - y) * (this.myBot.gy - y));
        if (num2 <= 1f && !ClientController.ownSounds && !SoundManager.THIS.isSlow(num))
        {
            return;
        }
        num2 = dump + num2;
        SoundManager.THIS.PlaySound(num, mult * dump / num2);
    }

    private void AddBz(int x, int y, int dir, bool playSound = true)
    {
        if (playSound && ClientConfig.SOUND_MINING)
        {
            this.AddVolumedSound(x, y, 10, 49f, 1f);
        }
        Vector3 pos;
        switch (dir)
        {
            case 0:
                pos = new Vector3((float)x + 0.5f, (float)(-(float)y) - 1f, -0.5f);
                break;
            case 1:
                pos = new Vector3((float)x + 0f, (float)(-(float)y) - 0.5f, -0.5f);
                break;
            case 2:
                pos = new Vector3((float)x + 0.5f, (float)(-(float)y), -0.5f);
                break;
            default:
                pos = new Vector3((float)x + 1f, (float)(-(float)y) - 0.5f, -0.5f);
                break;
        }
        int num = -1;
        GameObject free = this.bzPool.GetFree(out num);
        if (num != -1)
        {
            free.transform.SetParent(this.RenderWrapper.transform, false);
            free.GetComponent<BzScript>().SetAnimation(0, num, pos);
            Quaternion rotation = free.transform.rotation;
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles.z = (float)(-90 * dir + 180);
            rotation.eulerAngles = eulerAngles;
            free.transform.rotation = rotation;
        }
    }

    public int TimeOfMove()
    {
        return ClientController.serverTimeOfLastFrame + (int)(this.TimeForNextOperation * 1000f) - ClientController.clientTimeOfLastFrame;
    }

    public void TimeSync()
    {
        ClientController.serverTimeOfLastFrame = this.serverTime.NowTime();
        ClientController.clientTimeOfLastFrame = (int)(Time.unscaledTime * 1000f);
        if (this.TimeForNextOperation < Time.unscaledTime)
        {
            this.TimeForNextOperation = Time.unscaledTime;
        }
        this.TimeForNextOperation += this.XY_PAUSE;
        this.TimeForNextOperation += this.XY_PAUSE;
    }

    public void AddTime(float time, bool isSlowing = false)
    {
        this.TimeForNextOperation += time;
        this.wasMove = true;
    }

    private bool isEmpty(int cell)
    {
        return cell > 30 && cell < 40;
    }

    private bool isRoad(int cell)
    {
        return cell == 35 || cell == 36 || cell == 39;
    }

    public bool MoveOrBz(int dx, int dy, int _dir, bool isShift, bool isCtrl)
    {
        if (isShift)
        {
            dx = 0;
            dy = 0;
        }
        this.dir = _dir;
        if (TerrainRendererScript.map.GetCell(this.myBot.gx, this.myBot.gy) == 30 && (this.myBotLastSyncX != this.myBot.gx || this.myBotLastSyncY != this.myBot.gy))
        {
            dx = 0;
            dy = 0;
        }
        if (Mathf.Abs(dx) + Mathf.Abs(dy) > 1)
        {
            throw new Exception("Bad move");
        }
        if (isShift || CellModel.isEmpty[TerrainRendererScript.map.GetCell(this.myBot.gx + dx, this.myBot.gy + dy)])
        {
            this.myBot.SetRotation(this.dir);
            bool flag = this.isRoad(TerrainRendererScript.map.GetCell(this.myBot.gx + dx, this.myBot.gy + dy)) && this.isRoad(TerrainRendererScript.map.GetCell(this.myBot.gx, this.myBot.gy));
            this.myBot.SetXY((float)(this.myBot.gx + dx), (float)(this.myBot.gy + dy));
            if (isCtrl)
            {
                this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str9, this.myBot.gx, this.myBot.gy, (this.dir + 10).ToString());
            }
            else
            {
                this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str9, this.myBot.gx, this.myBot.gy, this.dir.ToString());
            }
            if (TerrainRendererScript.map.GetCell(this.myBot.gx, this.myBot.gy) == 83)
            {
                this.AddTime(1f, true);
            }
            else if (isCtrl)
            {
                this.AddTime(flag ? (3f * this.ROAD_PAUSE) : (3f * this.XY_PAUSE), true);
            }
            else
            {
                this.AddTime(flag ? this.ROAD_PAUSE : this.XY_PAUSE, true);
            }
            return true;
        }
        if (ClientController.autoDigg)
        {
            this.AddBz(this.myBot.gx, this.myBot.gy, this.dir, ClientController.ownSounds);
            this.myBot.SetRotation(this.dir);
            this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str10, this.myBot.gx, this.myBot.gy, this.dir.ToString());
            this.AddTime(this.BZ_PAUSE, false);
            return true;
        }
        this.myBot.SetRotation(this.dir);
        bool flag2 = this.isRoad(TerrainRendererScript.map.GetCell(this.myBot.gx + dx, this.myBot.gy + dy)) && this.isRoad(TerrainRendererScript.map.GetCell(this.myBot.gx, this.myBot.gy));
        this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str9, this.myBot.gx, this.myBot.gy, this.dir.ToString());
        this.AddTime(flag2 ? this.ROAD_PAUSE : this.XY_PAUSE, true);
        this.wasMove = true;
        return true;
    }

    

    public void SmoothTPMyBot(int x, int y)
    {
        this.stopAutoMove();
        this.myBot.SetXY((float)x, (float)y);
    }

    public void TPMyBot(int x, int y)
    {
        this.stopAutoMove();
        this.myBot.SetXY((float)x, (float)y);
        this.myBot.SyncXY();
        this.myBot.HideTail();
    }

    public void InitMyBot(int x, int y, int id, string name)
	{
		ClientController.inited = true;
		this.robotRenderer.RemoveAllBots();
		this.myBotId = id;
		this.robotRenderer.AddNewBotForMe(x, y, id, name, out this.myBot);
		this.view_x = x;
		this.view_y = y;
		this.tcx = this.terrainRenderer.cx;
		this.tcy = this.terrainRenderer.cy;
	}



    private void stringGen()
    {
        this.str1 = UnknownClass2.smethod_16(new byte[]
        {
            37,
            4,
            230,
            96,
            13,
            21,
            226,
            33,
            168,
            119,
            91,
            101,
            102,
            235,
            120,
            31,
            148,
            193,
            115,
            118,
            49,
            237,
            234,
            108,
            4,
            23,
            123,
            192,
            22,
            231,
            71,
            4,
            87,
            223,
            105,
            195,
            174,
            46,
            149,
            32,
            113,
            22,
            186,
            197,
            149,
            33,
            38,
            9,
            28,
            237,
            113,
            36,
            18,
            220,
            59,
            175,
            56,
            15,
            110,
            192,
            197,
            211,
            8,
            25,
            236,
            93,
            186,
            102,
            216,
            157,
            181,
            14,
            25,
            160,
            224,
            22,
            83,
            105,
            49,
            196,
            62,
            30,
            148,
            107,
            88,
            252,
            60,
            179,
            27,
            241,
            222,
            173,
            174,
            69,
            12,
            34,
            223,
            206,
            139,
            184,
            32,
            136,
            168,
            18,
            156,
            36,
            221,
            213,
            23,
            170,
            27,
            40,
            78,
            204,
            198,
            240,
            49,
            225,
            45,
            201,
            43,
            146,
            53,
            232,
            204,
            188,
            169,
            42
        }, false);
        this.str2 = UnknownClass2.smethod_16(new byte[]
        {
            50,
            18,
            114,
            1,
            100,
            5,
            57,
            74,
            22,
            12,
            32,
            17,
            0,
            169,
            146,
            183,
            146,
            175,
            45,
            217,
            216,
            14,
            209,
            252,
            44,
            191,
            0,
            141,
            51,
            127,
            195,
            40,
            237,
            115,
            63,
            122,
            247,
            23,
            237,
            230,
            214,
            51,
            49,
            175,
            184,
            184,
            205,
            238,
            119,
            213,
            107,
            180,
            124,
            93,
            233,
            37,
            114,
            58,
            68,
            123,
            219,
            29,
            89,
            149,
            203,
            163,
            136,
            11,
            86,
            221,
            225,
            208,
            26,
            142,
            54,
            46,
            156,
            206,
            95,
            216,
            32,
            194,
            193,
            45,
            174,
            106,
            179,
            11,
            174,
            197,
            124,
            157,
            204,
            107,
            14,
            26,
            234,
            176,
            14,
            195,
            17,
            31,
            182,
            154,
            17,
            168,
            122,
            0,
            233,
            133,
            91,
            37,
            117,
            199,
            225,
            14,
            248,
            249,
            210,
            194,
            49,
            110,
            62,
            216,
            14,
            82,
            230,
            208
        }, false);
        this.str3 = UnknownClass2.smethod_16(new byte[]
        {
            119,
            52,
            173,
            85,
            31,
            206,
            108,
            43,
            140,
            161,
            235,
            235,
            57,
            105,
            243,
            173,
            117,
            253,
            90,
            231,
            246,
            31,
            94,
            146,
            99,
            158,
            152,
            240,
            52,
            83,
            47,
            113,
            21,
            19,
            193,
            149,
            93,
            59,
            133,
            28,
            91,
            91,
            75,
            178,
            124,
            214,
            88,
            131,
            110,
            2,
            246,
            45,
            99,
            230,
            116,
            70,
            145,
            142,
            38,
            106,
            66,
            121,
            36,
            127,
            235,
            240,
            153,
            232,
            15,
            219,
            185,
            33,
            104,
            59,
            44,
            116,
            72,
            32,
            246,
            131,
            172,
            1,
            69,
            112,
            33,
            5,
            67,
            62,
            142,
            16,
            46,
            252,
            218,
            247,
            1,
            152,
            166,
            54,
            67,
            94,
            80,
            169,
            249,
            205,
            243,
            119,
            246,
            80,
            142,
            199,
            147,
            179,
            54,
            172,
            21,
            142,
            206,
            52,
            50,
            204,
            191,
            166,
            117,
            0,
            49,
            94,
            95,
            15
        }, false);
        this.str5 = UnknownClass2.smethod_16(new byte[]
        {
            9,
            105,
            8,
            74,
            137,
            68,
            8,
            131,
            74,
            133,
            168,
            134,
            167,
            53,
            47,
            195,
            206,
            128,
            5,
            43,
            106,
            253,
            211,
            91,
            137,
            226,
            14,
            174,
            89,
            57,
            57,
            119,
            113,
            122,
            93,
            110,
            126,
            223,
            157,
            126,
            22,
            13,
            235,
            45,
            26,
            245,
            78,
            233,
            173,
            206,
            161,
            17,
            155,
            21,
            4,
            14,
            184,
            23,
            228,
            177,
            87,
            217,
            155,
            25,
            25,
            160,
            31,
            93,
            101,
            228,
            135,
            67,
            94,
            170,
            212,
            180,
            115,
            63,
            39,
            94,
            95,
            10,
            154,
            12,
            207,
            157,
            70,
            102,
            2,
            172,
            120,
            72,
            119,
            125,
            155,
            72,
            191,
            147,
            92,
            86,
            58,
            56,
            212,
            170,
            143,
            80,
            129,
            7,
            181,
            5,
            2,
            205,
            214,
            142,
            103,
            109,
            151,
            56,
            122,
            241,
            155,
            76,
            232,
            129,
            42,
            239,
            14,
            82
        }, false);
        this.str6 = UnknownClass2.smethod_16(new byte[]
        {
            77,
            123,
            54,
            187,
            170,
            226,
            94,
            115,
            158,
            160,
            244,
            51,
            124,
            73,
            117,
            83,
            38,
            79,
            129,
            172,
            38,
            14,
            49,
            245,
            46,
            188,
            121,
            147,
            192,
            211,
            194,
            26,
            196,
            254,
            15,
            69,
            199,
            114,
            19,
            184,
            49,
            97,
            99,
            222,
            8,
            88,
            8,
            53,
            101,
            181,
            196,
            88,
            247,
            29,
            7,
            102,
            200,
            193,
            39,
            91,
            25,
            114,
            47,
            102,
            133,
            57,
            93,
            194,
            133,
            180,
            31,
            104,
            31,
            207,
            153,
            253,
            41,
            170,
            187,
            60,
            238,
            53,
            206,
            81,
            68,
            15,
            152,
            215,
            21,
            109,
            162,
            225,
            144,
            74,
            109,
            222,
            162,
            60,
            171,
            237,
            64,
            19,
            47,
            118,
            41,
            67,
            245,
            119,
            101,
            78,
            136,
            24,
            247,
            111,
            149,
            57,
            214,
            196,
            11,
            131,
            44,
            21,
            1,
            157,
            108,
            171,
            106,
            202
        }, false);
        this.str7 = UnknownClass2.smethod_16(new byte[]
        {
            90,
            114,
            73,
            10,
            226,
            123,
            137,
            33,
            249,
            177,
            227,
            53,
            211,
            245,
            5,
            31,
            109,
            3,
            187,
            190,
            34,
            99,
            63,
            36,
            224,
            14,
            236,
            215,
            223,
            22,
            167,
            99,
            65,
            138,
            44,
            89,
            11,
            52,
            6,
            106,
            84,
            70,
            18,
            24,
            17,
            172,
            130,
            125,
            91,
            224,
            58,
            125,
            98,
            162,
            201,
            238,
            126,
            40,
            158,
            196,
            247,
            8,
            242,
            163,
            154,
            0,
            145,
            159,
            80,
            32,
            139,
            116,
            181,
            162,
            77,
            214,
            53,
            173,
            162,
            161,
            56,
            103,
            128,
            94,
            166,
            115,
            93,
            245,
            36,
            224,
            204,
            153,
            16,
            99,
            199,
            163,
            105,
            244,
            98,
            227,
            109,
            11,
            169,
            7,
            199,
            84,
            213,
            154,
            154,
            245,
            98,
            208,
            112,
            53,
            194,
            62,
            195,
            161,
            129,
            199,
            226,
            204,
            207,
            180,
            221,
            58,
            13,
            25
        }, false);
        this.str8 = UnknownClass2.smethod_16(new byte[]
        {
            52,
            193,
            4,
            251,
            242,
            80,
            7,
            181,
            246,
            100,
            202,
            150,
            244,
            156,
            227,
            189,
            117,
            1,
            165,
            75,
            99,
            254,
            157,
            147,
            138,
            236,
            87,
            10,
            32,
            178,
            43,
            172,
            64,
            118,
            111,
            252,
            224,
            191,
            109,
            99,
            40,
            124,
            151,
            79,
            209,
            109,
            35,
            207,
            48,
            85,
            69,
            158,
            200,
            176,
            91,
            123,
            129,
            229,
            243,
            13,
            137,
            162,
            233,
            226,
            156,
            229,
            36,
            156,
            90,
            128,
            194,
            190,
            248,
            162,
            144,
            146,
            165,
            242,
            210,
            238,
            235,
            16,
            254,
            139,
            177,
            29,
            55,
            91,
            195,
            59,
            225,
            73,
            247,
            109,
            191,
            65,
            219,
            102,
            58,
            221,
            109,
            150,
            163,
            65,
            197,
            216,
            122,
            134,
            134,
            187,
            146,
            214,
            223,
            46,
            164,
            209,
            224,
            229,
            163,
            169,
            26,
            103,
            142,
            142,
            160,
            235,
            224,
            211
        }, false);
        this.str9 = UnknownClass2.smethod_16(new byte[]
        {
            52,
            29,
            60,
            26,
            26,
            97,
            26,
            247,
            79,
            6,
            222,
            111,
            194,
            177,
            241,
            200,
            2,
            6,
            12,
            242,
            222,
            243,
            213,
            200,
            81,
            131,
            45,
            71,
            137,
            199,
            124,
            174,
            7,
            244,
            28,
            37,
            212,
            10,
            151,
            232,
            125,
            6,
            8,
            153,
            96,
            201,
            188,
            188,
            1,
            188,
            24,
            207,
            199,
            185,
            101,
            235,
            251,
            166,
            0,
            185,
            8,
            114,
            85,
            121,
            100,
            75,
            93,
            85,
            34,
            33,
            250,
            227,
            39,
            75,
            20,
            2,
            42,
            16,
            230,
            138,
            104,
            16,
            240,
            41,
            149,
            121,
            91,
            252,
            235,
            50,
            215,
            108,
            157,
            249,
            44,
            92,
            229,
            39,
            36,
            116,
            87,
            20,
            158,
            46,
            94,
            168,
            223,
            13,
            202,
            78,
            251,
            238,
            213,
            73,
            109,
            8,
            205,
            49,
            2,
            240,
            29,
            125,
            54,
            191,
            26,
            97,
            135,
            201
        }, false);
        this.str10 = UnknownClass2.smethod_16(new byte[]
        {
            49,
            16,
            248,
            130,
            10,
            86,
            242,
            237,
            231,
            45,
            36,
            62,
            91,
            5,
            30,
            17,
            23,
            243,
            151,
            36,
            74,
            128,
            149,
            92,
            135,
            226,
            252,
            99,
            36,
            244,
            154,
            43,
            156,
            73,
            196,
            219,
            223,
            22,
            22,
            18,
            221,
            202,
            50,
            205,
            122,
            199,
            211,
            194,
            139,
            144,
            201,
            86,
            98,
            174,
            170,
            57,
            103,
            8,
            141,
            231,
            160,
            99,
            227,
            119,
            3,
            50,
            15,
            146,
            217,
            64,
            238,
            149,
            112,
            53,
            236,
            14,
            139,
            201,
            55,
            164,
            239,
            188,
            32,
            170,
            121,
            130,
            25,
            119,
            181,
            222,
            41,
            205,
            214,
            169,
            63,
            57,
            204,
            123,
            127,
            83,
            12,
            3,
            62,
            173,
            176,
            254,
            87,
            82,
            204,
            244,
            169,
            161,
            178,
            33,
            88,
            149,
            64,
            88,
            251,
            232,
            108,
            179,
            26,
            213,
            9,
            39,
            91,
            82
        }, false);
        this.str11 = UnknownClass2.smethod_16(new byte[]
        {
            130,
            194,
            78,
            9,
            173,
            85,
            173,
            161,
            135,
            194,
            251,
            233,
            158,
            164,
            62,
            86,
            46,
            134,
            225,
            240,
            132,
            241,
            65,
            8,
            117,
            101,
            24,
            245,
            95,
            74,
            154,
            199,
            41,
            124,
            187,
            123,
            7,
            194,
            97,
            8,
            141,
            205,
            232,
            205,
            176,
            47,
            42,
            103,
            241,
            167,
            194,
            43,
            71,
            127,
            66,
            171,
            198,
            62,
            4,
            134,
            185,
            45,
            99,
            138,
            244,
            195,
            20,
            170,
            158,
            189,
            160,
            106,
            199,
            61,
            184,
            228,
            20,
            225,
            115,
            222,
            29,
            226,
            12,
            21,
            20,
            156,
            120,
            43,
            172,
            85,
            104,
            67,
            63,
            86,
            183,
            91,
            151,
            2,
            132,
            191,
            142,
            163,
            150,
            135,
            135,
            84,
            122,
            9,
            24,
            253,
            68,
            20,
            93,
            198,
            102,
            30,
            250,
            213,
            252,
            12,
            97,
            233,
            217,
            235,
            192,
            19,
            145,
            87
        }, false);
        this.str12 = UnknownClass2.smethod_16(new byte[]
        {
            85,
            254,
            120,
            99,
            201,
            242,
            167,
            174,
            144,
            236,
            11,
            138,
            94,
            142,
            158,
            176,
            254,
            70,
            10,
            222,
            151,
            119,
            199,
            228,
            83,
            53,
            192,
            29,
            4,
            228,
            41,
            45,
            116,
            243,
            200,
            198,
            145,
            141,
            182,
            40,
            143,
            201,
            9,
            135,
            181,
            14,
            49,
            41,
            224,
            214,
            186,
            111,
            88,
            230,
            236,
            51,
            10,
            63,
            151,
            253,
            32,
            30,
            232,
            53,
            6,
            1,
            200,
            150,
            183,
            52,
            104,
            7,
            196,
            byte.MaxValue,
            177,
            80,
            72,
            1,
            115,
            55,
            21,
            46,
            126,
            178,
            99,
            41,
            9,
            150,
            232,
            150,
            242,
            241,
            11,
            155,
            248,
            105,
            6,
            109,
            33,
            12,
            99,
            98,
            41,
            44,
            72,
            251,
            159,
            130,
            byte.MaxValue,
            122,
            182,
            168,
            127,
            71,
            78,
            97,
            114,
            129,
            42,
            130,
            92,
            252,
            86,
            183,
            70,
            125,
            124,
            byte.MaxValue
        }, false);
        this.str13 = UnknownClass2.smethod_16(new byte[]
        {
            87,
            215,
            60,
            78,
            49,
            200,
            62,
            75,
            20,
            75,
            157,
            41,
            69,
            15,
            131,
            91,
            248,
            231,
            205,
            153,
            124,
            62,
            48,
            24,
            40,
            75,
            25,
            179,
            115,
            188,
            228,
            190,
            byte.MaxValue,
            147,
            14,
            4,
            253,
            80,
            93,
            166,
            byte.MaxValue,
            74,
            181,
            65,
            112,
            70,
            233,
            148,
            21,
            98,
            194,
            254,
            124,
            211,
            206,
            139,
            241,
            225,
            200,
            225,
            70,
            175,
            7,
            249,
            164,
            182,
            127,
            222,
            85,
            96,
            9,
            1,
            72,
            158,
            0,
            32,
            19,
            208,
            208,
            129,
            221,
            167,
            116,
            252,
            247,
            246,
            105,
            16,
            165,
            198,
            209,
            86,
            226,
            48,
            247,
            224,
            180,
            154,
            221,
            237,
            170,
            106,
            76,
            242,
            199,
            84,
            153,
            203,
            144,
            49,
            201,
            65,
            118,
            171,
            82,
            164,
            220,
            8,
            252,
            117,
            71,
            36,
            22,
            151,
            97,
            36,
            52,
            194
        }, false);
    }

    /*

    private void љјњњјјњјїїњїјљїјљљљљјњњ(bool DisableCheck = false)
	{
		if (this.myBot == null)
		{
			return;
		}
		if (!ClientController.MouseControl)
		{
			return;
		}
		if (!ClientController.CanGoto)
		{
			ClientController.CanGoto = false;
			return;
		}
		bool flag = true;
		if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject != null && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "[DW]" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "JSON Parse: Too many closing brackets"))
		{
			flag = true;
		}
		if (!DisableCheck && ((!MapViewer.THIS.gameObject.activeSelf && !OKWindowManager.THIS.gameObject.activeSelf && !AYSWindowManager.THIS.gameObject.activeSelf && PopupManager.THIS.GUIWindow.activeSelf) || flag))
		{
			ClientController.CanGoto = false;
			return;
		}
		int num = ClientController.map.јњїјљјњљњїјљїљјњњјњїїњї(this.tryGotoX, this.tryGotoY);
		if (num != 67)
		{
			if ((CellRender.UNBREAKABLE[num] || num == -35) && !this.SmartPointing(this.tryGotoX + 1, this.tryGotoY) && !this.SmartPointing(this.tryGotoX - 1, this.tryGotoY) && !this.SmartPointing(this.tryGotoX, this.tryGotoY + 0) && !this.SmartPointing(this.tryGotoX, this.tryGotoY - 0) && !this.SmartPointing(this.tryGotoX + 0, this.tryGotoY + 1) && !this.SmartPointing(this.tryGotoX - 1, this.tryGotoY + 0) && !this.SmartPointing(this.tryGotoX + 0, this.tryGotoY - 1))
			{
				this.SmartPointing(this.tryGotoX - 1, this.tryGotoY - 1);
			}
			num = ClientController.map.јїљљњњњљљљїјїљњљљјјјїјї(this.tryGotoX, this.tryGotoY);
			if (CellRender.UNBREAKABLE[num])
			{
				int num2 = -1;
				int num3 = -1;
				if (this.tryGotoX > this.myBot.їњљљњїљїњјњїїјјњїјјњњјј())
				{
					num2 = 0;
				}
				if (this.tryGotoY > this.myBot.gy)
				{
					num3 = 0;
				}
				if (!this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY) && !this.SmartFreePointing(this.tryGotoX, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY + num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY + num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY))
				{
					this.SmartFreePointing(this.tryGotoX, this.tryGotoY + num3);
				}
			}
			num = ClientController.map.GetCell(this.tryGotoX, this.tryGotoY);
			if (CellRender.UNBREAKABLE[num])
			{
				return;
			}
		}
		this.GotoX = this.tryGotoX;
		this.GotoY = this.tryGotoY;
		this.startAutoMove();
		this.јїљїїњљљњјљњњљњљїїїїљљљ();
	}

	

	

	private void њїјљјњјљјњјњїјљјњїїјљљљ(int x, int y, int bid, int color)
	{
		int num = -1;
		GameObject gameObject = this.gunShotPool.њјњљњњјљјњїњњјњљњїњљљјњ(out num);
		if (num != -1)
		{
			gameObject.transform.SetParent(this.RenderWrapper.transform, true);
			gameObject.GetComponent<GunShotScript>().јїїњљњњїјљњљјљјњљјјљїљљ(x, y, bid, color, num);
		}
	}

	public int њљњїјјјјљјјјїїљњљјљљїљњ()
	{
		return ClientController.serverTimeOfLastFrame + (int)(this.TimeForNextOperation * 988f) - ClientController.clientTimeOfLastFrame;
	}

	

	private void јљљљјљњјјљїњњјљїљљљљљњљ()
	{
		this.stringGen();
		this.robotRenderer = this.mainRenderer.GetComponent<RobotRenderer>();
		this.terrainRenderer = this.mainRenderer.GetComponent<TerrainRendererScript>();
		this.serverTime = this.obvyazkaObject.GetComponent<ServerTime>();
		this.obvyazka = this.obvyazkaObject.GetComponent<Obvyazka>();
		ClientController.THIS = this;
		this.Cursor.SetActive(false);
		this.autoDiggButton.onClick.AddListener(new UnityAction(this.їјљїњљљјјњїїњљїњјјјјњљљ));
		this.ShowAutoDigg();
		this.NoGUIClickPad.onClick.AddListener(new UnityAction(this.NoGUIClick));
	}

	

	

	public void њїјљїїјњїјїљњјјњњњїњљњї()
	{
		ClientController.serverTimeOfLastFrame = this.serverTime.љјїїњїљљњљњљїњљјїњјјљљљ();
		ClientController.clientTimeOfLastFrame = (int)(Time.unscaledTime * 1571f);
		if (this.TimeForNextOperation < Time.unscaledTime)
		{
			this.TimeForNextOperation = Time.unscaledTime;
		}
		this.TimeForNextOperation += this.XY_PAUSE;
		this.TimeForNextOperation += this.XY_PAUSE;
	}

	

	public void њљјјїљљњњїїњјњјљјњњљљјљ()
	{
		ClientController.serverTimeOfLastFrame = this.serverTime.їїїњїїїјљљјњїњњљїїјњњјљ();
		ClientController.clientTimeOfLastFrame = (int)(Time.unscaledTime * 1191f);
		if (this.TimeForNextOperation < Time.unscaledTime)
		{
			this.TimeForNextOperation = Time.unscaledTime;
		}
		this.TimeForNextOperation += this.XY_PAUSE;
		this.TimeForNextOperation += this.XY_PAUSE;
	}

	public void њњїњњјљљјїњљњљљљњјљјїјљ(int x, int y, int id, string name)
	{
		ClientController.inited = true;
		this.robotRenderer.љњїјљљїњјјїјїјњњјњњљљїљ();
		this.myBotId = id;
		this.robotRenderer.AddNewBotForMe(x, y, id, name, out this.myBot);
		this.view_x = x;
		this.view_y = y;
		this.tcx = this.terrainRenderer.cx;
		this.tcy = this.terrainRenderer.cy;
	}

	

	

	

	private GameObject јњјјјљїљљїїљїљњїњјљљїљј(int type, int x, int y)
	{
		int num = -1;
		GameObject gameObject = this.bzPool.їїњљњњњљјњњњљјљњљњљњјїљ(out num);
		if (num != -1)
		{
			gameObject.transform.SetParent(this.RenderWrapper.transform, false);
			gameObject.GetComponent<BzScript>().њїњїјљїњњњјїњњљљљїљљјјј(type, num, new Vector3((float)x + 1528f, (float)(-(float)y) - 574f, 1743f));
		}
		return gameObject;
	}

	

	public void њњњљїјјјїјјљљњљјјљјјњјї()
	{
		if (!ClientController.autoDigg)
		{
			this.autoDiggButtonText.text = "Pope";
			this.autoDiggButtonText.color = new Color(334f, 1860f, 1027f);
			this.autoDiggButton.GetComponent<Image>().color = new Color(1061f, 1271f, 204f, 344f);
			return;
		}
		this.autoDiggButtonText.text = "%T%";
		this.autoDiggButtonText.color = new Color(1484f, 283f, 1466f);
		this.autoDiggButton.GetComponent<Image>().color = new Color(682f, 1846f, 1693f, 1829f);
	}

	

    private void јјјњјїљњјљїљљїјјїљјљјњљ(bool DisableCheck = false)
	{
		if (this.myBot == null)
		{
			return;
		}
		if (!ClientController.MouseControl)
		{
			return;
		}
		if (!ClientController.CanGoto)
		{
			ClientController.CanGoto = true;
			return;
		}
		bool flag = false;
		if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject != null && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "Hand-" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == ""))
		{
			flag = true;
		}
		if (!DisableCheck && (MapViewer.THIS.gameObject.activeSelf || OKWindowManager.THIS.gameObject.activeSelf || AYSWindowManager.THIS.gameObject.activeSelf || PopupManager.THIS.GUIWindow.activeSelf || flag))
		{
			ClientController.CanGoto = true;
			return;
		}
		int num = ClientController.map.љїљјјјљљјјњјњїїїљјљњњњј(this.tryGotoX, this.tryGotoY);
		if (num != 45)
		{
			if ((CellRender.UNBREAKABLE[num] || num == -59) && !this.SmartPointing(this.tryGotoX + 1, this.tryGotoY) && !this.SmartPointing(this.tryGotoX - 0, this.tryGotoY) && !this.SmartPointing(this.tryGotoX, this.tryGotoY + 0) && !this.SmartPointing(this.tryGotoX, this.tryGotoY - 0) && !this.SmartPointing(this.tryGotoX + 0, this.tryGotoY + 0) && !this.SmartPointing(this.tryGotoX - 0, this.tryGotoY + 0) && !this.SmartPointing(this.tryGotoX + 0, this.tryGotoY - 0))
			{
				this.SmartPointing(this.tryGotoX - 0, this.tryGotoY - 1);
			}
			num = ClientController.map.јњљїњїњњњљљјјјїљїјїњњјї(this.tryGotoX, this.tryGotoY);
			if (CellRender.UNBREAKABLE[num])
			{
				int num2 = -1;
				int num3 = -1;
				if (this.tryGotoX > this.myBot.љјљљјјјњјјїјїњњјљїїїїїј())
				{
					num2 = 0;
				}
				if (this.tryGotoY > this.myBot.љњјњљњљњјјїїјїјњїїјјїјї())
				{
					num3 = 0;
				}
				if (!this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY) && !this.SmartFreePointing(this.tryGotoX, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY + num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY + num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY))
				{
					this.SmartFreePointing(this.tryGotoX, this.tryGotoY + num3);
				}
			}
			num = ClientController.map.јњїјљјњљњїјљїљјњњјњїїњї(this.tryGotoX, this.tryGotoY);
			if (CellRender.UNBREAKABLE[num])
			{
				return;
			}
		}
		this.GotoX = this.tryGotoX;
		this.GotoY = this.tryGotoY;
		this.startAutoMove();
		this.јїљїїњљљњјљњњљњљїїїїљљљ();
	}

	private void јљјљњњљљљјїњјљїјњїїњїљљ()
	{
		ClientController.CanGoto = true;
		ServerTime.THIS.јљїњїљјјњїњїїјїјїїњњљљњ(-1, this.str1, 1, 1, "HEAL;");
	}

	public void їњїњїјњњњљљњїїњјјјјњњњњ(int x, int y, int id, string name)
	{
		ClientController.inited = true;
		this.robotRenderer.јїїјїљјњїњїљњїјњњјјїїњї();
		this.myBotId = id;
		this.robotRenderer.љљљљљїјјњјњјњјљњљїњљњњљ(x, y, id, name, out this.myBot);
		this.view_x = x;
		this.view_y = y;
		this.tcx = this.terrainRenderer.cx;
		this.tcy = this.terrainRenderer.cy;
	}

	private bool љљјњїїјњјјјљљњјїїњљїїљњ(int cell)
	{
		return cell <= -60 || cell < -37;
	}

	public void їњњњїњњїљљњїјљїјјјїїјјї(int x, int y)
	{
		if (!ProgrammatorView.active && (!this.isProgrammator || ProgPanel.handMode))
		{
			this.tryGotoX = x;
			this.tryGotoY = y;
			ClientController.CanGoto = false;
			this.TryToGoto(true);
		}
	}

	public void јњїїљњїјїљњњљљјїјјјїљљј(int x, int y)
	{
		this.stopAutoMove();
		this.myBot.њјјњјљјљјјјљљњљїљљљїљњњ((float)x, (float)y);
	}

	public void јљїјјљњїїјїљњљїїљїњјњїњ(int x, int y, int fx)
	{
		if (Time.unscaledDeltaTime > 880f)
		{
			return;
		}
		switch (fx)
		{
		case 0:
			this.јїљјјљјїљљњњњњјљјїњљњљј(0, x, y);
			return;
		case 1:
		case 9:
			break;
		case 2:
			this.јїљјјљјїљљњњњњјљјїњљњљј(6, x, y);
			if (ClientConfig.SOUND_DEATH)
			{
				this.AddVolumedSound(x, y, 8, 1894f, 1616f);
				return;
			}
			break;
		case 3:
			if (ClientConfig.SOUND_BOMBTICK)
			{
				this.AddVolumedSound(x, y, 0, 375f, 1157f);
				return;
			}
			break;
		case 4:
			if (ClientConfig.SOUND_BOMB)
			{
				this.AddVolumedSound(x, y, 1, 387f, 707f);
				return;
			}
			break;
		case 5:
			if (ClientConfig.SOUND_DESTROY)
			{
				this.AddVolumedSound(x, y, 0, 1745f, 1470f);
				return;
			}
			break;
		case 6:
			if (ClientConfig.SOUND_DIZZ)
			{
				this.AddVolumedSound(x, y, -52, 460f, 1004f);
				return;
			}
			break;
		case 7:
			if (ClientConfig.SOUND_EMI)
			{
				this.AddVolumedSound(x, y, 0, 1187f, 843f);
				return;
			}
			break;
		case 8:
			if (ClientConfig.SOUND_GEOLOGY)
			{
				this.AddVolumedSound(x, y, 3, 1404f, 1008f);
				return;
			}
			break;
		case 10:
			if (ClientConfig.SOUND_TP_IN)
			{
				this.AddVolumedSound(x, y, -87, 263f, 1593f);
				return;
			}
			break;
		case 11:
			if (ClientConfig.SOUND_TP_OUT)
			{
				this.AddVolumedSound(x, y, 127, 1238f, 1308f);
				return;
			}
			break;
		case 12:
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.nohpfxPrefab);
			gameObject.transform.SetParent(this.RenderWrapper.transform, true);
			gameObject.transform.position = new Vector3((float)x + 568f, (float)(-(float)y) - 1066f, 737f);
			return;
		}
		case 13:
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.nohpfxSmallPrefab);
			gameObject2.transform.SetParent(this.RenderWrapper.transform, false);
			gameObject2.transform.position = new Vector3((float)x + 513f, (float)(-(float)y) - 408f, 149f);
			return;
		}
		case 14:
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.nohpfxSmallPrefab);
			gameObject3.transform.SetParent(this.RenderWrapper.transform, false);
			gameObject3.transform.position = new Vector3((float)x + 561f, (float)(-(float)y), 198f);
			return;
		}
		case 15:
		{
			GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.nohpfxSmallPrefab);
			gameObject4.transform.SetParent(this.RenderWrapper.transform, true);
			gameObject4.transform.position = new Vector3((float)x + 1910f, (float)(-(float)y) - 1876f, 433f);
			gameObject4.transform.localScale = new Vector3(577f, 1209f, 1714f);
			return;
		}
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
		case 21:
		case 22:
		case 23:
		{
			GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.smokePrefab);
			gameObject5.transform.SetParent(this.RenderWrapper.transform, true);
			gameObject5.GetComponent<ParticleSystem>().startColor = this.smokeColors[fx - 127];
			gameObject5.transform.position = new Vector3((float)x + 1391f, (float)(-(float)y) - 1860f, 799f);
			return;
		}
		case 24:
		{
			GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.volcanoPrefab);
			gameObject6.transform.SetParent(this.RenderWrapper.transform, false);
			gameObject6.transform.position = new Vector3((float)x + 1380f, (float)(-(float)y) - 101f, 815f);
			if (ClientConfig.SOUND_VOLC)
			{
				this.AddVolumedSound(x, y, -86, 1504f, 1631f);
				return;
			}
			break;
		}
		case 25:
			if (ClientConfig.SOUND_C190)
			{
				this.AddVolumedSound(x, y, -126, 87f, 1136f);
			}
			break;
		default:
			return;
		}
	}

	public void њїїњљјјїњљњљјїњїњїјїїљљ()
	{
		if (!ClientController.autoDigg)
		{
			this.autoDiggButtonText.text = "RAND;";
			this.autoDiggButtonText.color = new Color(425f, 279f, 1795f);
			this.autoDiggButton.GetComponent<Image>().color = new Color(953f, 1370f, 880f, 422f);
			return;
		}
		this.autoDiggButtonText.text = "\\n";
		this.autoDiggButtonText.color = new Color(1094f, 403f, 1537f);
		this.autoDiggButton.GetComponent<Image>().color = new Color(716f, 650f, 1244f, 850f);
	}

	

	

	

	public void јљїњљїјїјњњљњљјјљњїњњїљ(int x, int y)
	{
		this.stopAutoMove();
		this.myBot.њљљјїїљјїїљљїњјїјїјјњјњ((float)x, (float)y);
	}

	

	public bool јљљњљњјјїљљњјїјїјјјљњљњ(int dx, int dy, int _dir, bool isShift, bool isCtrl)
	{
		if (isShift)
		{
			dx = 0;
			dy = 1;
		}
		this.dir = _dir;
		if (TerrainRendererScript.map.GetCell(this.myBot.љјїњљјјјљјїїїњјјїњјїїјњ(), this.myBot.gy) == -50 && (this.myBotLastSyncX != this.myBot.gx || this.myBotLastSyncY != this.myBot.љљњјљњјјљјњїїїљњјјњїјњј()))
		{
			dx = 1;
			dy = 0;
		}
		if (Mathf.Abs(dx) + Mathf.Abs(dy) > 0)
		{
			throw new Exception("h");
		}
		if (isShift || CellModel.isEmpty[TerrainRendererScript.map.јњїјљјњљњїјљїљјњњјњїїњї(this.myBot.јїљјјїїїїљјјњїјїјљњљњїї() + dx, this.myBot.љјїјјјјїњљњњљљљјјїјїїљї() + dy)])
		{
			this.myBot.їљњјїїљїњїїјљљљљљїљљњјї(this.dir);
			bool flag = !this.јљјњљњљїјљљљњјїњњјїљљјљ(TerrainRendererScript.map.јњљїњїњњњљљјјјїљїјїњњјї(this.myBot.їњљљњїљїњјњїїјјњїјјњњјј() + dx, this.myBot.љїњњїјњњљљљњїљјњјљљїїїљ() + dy)) || this.јљјњљњљїјљљљњјїњњјїљљјљ(TerrainRendererScript.map.јњљїњїњњњљљјјјїљїјїњњјї(this.myBot.јљїњњљњњїљјљњїїїљїљљњњї(), this.myBot.јјјјїїљљњњїљљјњљїњјљњјљ()));
			this.myBot.їљїїјїјњљјњљљњјїљїњјїјњ((float)(this.myBot.љјїњљјјјљјїїїњјјїњјїїјњ() + dx), (float)(this.myBot.їњњњјјњљљљїїјњјњњљњјљјњ() + dy));
			if (isCtrl)
			{
				this.serverTime.SendTypicalMessage(this.TimeOfMove(), this.str9, this.myBot.њїњљјїљљњјњљїњњїїїјјїњј(), this.myBot.љјїјјјјїњљњњљљљјјїјїїљї(), (this.dir + -63).ToString());
			}
			else
			{
				this.serverTime.јљїњїљјјњїњїїјїјїїњњљљњ(this.TimeOfMove(), this.str9, this.myBot.јљїњњљњњїљјљњїїїљїљљњњї(), this.myBot.їњњњјјњљљљїїјњјњњљњјљјњ(), this.dir.ToString());
			}
			if (TerrainRendererScript.map.јњљїњїњњњљљјјјїљїјїњњјї(this.myBot.њїњљјїљљњјњљїњњїїїјјїњј(), this.myBot.gy) == 46)
			{
				this.AddTime(1203f, true);
			}
			else if (isCtrl)
			{
				this.AddTime(flag ? (1423f * this.ROAD_PAUSE) : (1739f * this.XY_PAUSE), true);
			}
			else
			{
				this.AddTime(flag ? this.ROAD_PAUSE : this.XY_PAUSE, false);
			}
			return true;
		}
		if (ClientController.autoDigg)
		{
			this.AddBz(this.myBot.їљњїјїљїњљњїњјњљњјљњљјї(), this.myBot.їљїњїїїљїјљїљјњјљјјњїјј(), this.dir, ClientController.ownSounds);
			this.myBot.љљјњњљјњљјјјјїјјњїљјїјї(this.dir);
			this.serverTime.јјљљњїљїјњљјїјљїїљљњљїї(this.љїњљљјљїјїјњїјјїїјјїњњљ(), this.str10, this.myBot.јїљјјїїїїљјјњїјїјљњљњїї(), this.myBot.јјњјјњњњјјїјјјїљљљјјјњљ(), this.dir.ToString());
			this.AddTime(this.BZ_PAUSE, false);
			return false;
		}
		this.myBot.SetRotation(this.dir);
		bool flag2 = this.isRoad(TerrainRendererScript.map.љїљјјјљљјјњјњїїїљјљњњњј(this.myBot.gx + dx, this.myBot.љјїјјјјїњљњњљљљјјїјїїљї() + dy)) && this.јљјњљњљїјљљљњјїњњјїљљјљ(TerrainRendererScript.map.јњїјљјњљњїјљїљјњњјњїїњї(this.myBot.їљњїјїљїњљњїњјњљњјљњљјї(), this.myBot.јјјјїїљљњњїљљјњљїњјљњјљ()));
		this.serverTime.љјњљњљњљїљњјјјњїїњїїљјљ(this.њљњїјјјјљјјјїїљњљјљљїљњ(), this.str9, this.myBot.јїљјјїїїїљјјњїјїјљњљњїї(), this.myBot.їњљјњљњїњјљїїњљјњњњїјњї(), this.dir.ToString());
		this.AddTime(flag2 ? this.ROAD_PAUSE : this.XY_PAUSE, false);
		this.wasMove = true;
		return true;
	}

	

	

	

	private bool љњњїњїјјјїњјљїљњњјјљјњњ(int cell)
	{
		return cell > 53 && cell < 124;
	}

	

	private void їњјїњњјїљљљјњїљљњїјїљњњ()
	{
		this.љјњњјјњјїїњїјљїјљљљљјњњ(true);
	}

	

	public bool їїјњљљњјјњјїљјїїњњїљњїљ(int dx, int dy, int _dir, bool isShift, bool isCtrl)
	{
		if (isShift)
		{
			dx = 1;
			dy = 1;
		}
		this.dir = _dir;
		if (TerrainRendererScript.map.јїљљњњњљљљїјїљњљљјјјїјї(this.myBot.њїњљјїљљњјњљїњњїїїјјїњј(), this.myBot.љљјјњњљїњјњњњљїњљњњїњїљ()) == 32 && (this.myBotLastSyncX != this.myBot.љјїњљјјјљјїїїњјјїњјїїјњ() || this.myBotLastSyncY != this.myBot.љїњњїјњњљљљњїљјњјљљїїїљ()))
		{
			dx = 1;
			dy = 0;
		}
		if (Mathf.Abs(dx) + Mathf.Abs(dy) > 1)
		{
			throw new Exception("~");
		}
		if (isShift || CellModel.isEmpty[TerrainRendererScript.map.јїљљњњњљљљїјїљњљљјјјїјї(this.myBot.јљїњњљњњїљјљњїїїљїљљњњї() + dx, this.myBot.њїјїјїїїїњњљљїљњљњљјјїї() + dy)])
		{
			this.myBot.њјїљљњљїїїїїјњїїјљїљїњњ(this.dir);
			bool flag = !this.isRoad(TerrainRendererScript.map.јїљљњњњљљљїјїљњљљјјјїјї(this.myBot.їњњњљјјњјїњљїљјљјљљїјјњ() + dx, this.myBot.їњњњјјњљљљїїјњјњњљњјљјњ() + dy)) || this.isRoad(TerrainRendererScript.map.јњїјљјњљњїјљїљјњњјњїїњї(this.myBot.јїјїљњњјљјљљљљїјїњјїјјњ(), this.myBot.љљњјљњјјљјњїїїљњјјњїјњј()));
			this.myBot.љјјљњњјјїјїјњљљњљїїїїљљ((float)(this.myBot.їљњїјїљїњљњїњјњљњјљњљјї() + dx), (float)(this.myBot.їљїњїїїљїјљїљјњјљјјњїјј() + dy));
			if (isCtrl)
			{
				this.serverTime.јјјљїњїїјљјїњїљњјљїњљњљ(this.TimeOfMove(), this.str9, this.myBot.јїјїљњњјљјљљљљїјїњјїјјњ(), this.myBot.јїїљњњљїљїјјїјјїїњїљљјљ(), (this.dir + 97).ToString());
			}
			else
			{
				this.serverTime.јјјјњљїїјїїїјјїљјљљїјљљ(this.TimeOfMove(), this.str9, this.myBot.њјњїјїљњљљјљљњїјјїњњњїї(), this.myBot.їњљјњљњїњјљїїњљјњњњїјњї(), this.dir.ToString());
			}
			if (TerrainRendererScript.map.јњїјљјњљњїјљїљјњњјњїїњї(this.myBot.їњњњљјјњјїњљїљјљјљљїјјњ(), this.myBot.њїјїјїїїїњњљљїљњљњљјјїї()) == -55)
			{
				this.AddTime(144f, true);
			}
			else if (isCtrl)
			{
				this.AddTime(flag ? (1931f * this.ROAD_PAUSE) : (1893f * this.XY_PAUSE), false);
			}
			else
			{
				this.AddTime(flag ? this.ROAD_PAUSE : this.XY_PAUSE, false);
			}
			return false;
		}
		if (ClientController.autoDigg)
		{
			this.AddBz(this.myBot.јљїњњљњњїљјљњїїїљїљљњњї(), this.myBot.їњљјњљњїњјљїїњљјњњњїјњї(), this.dir, ClientController.ownSounds);
			this.myBot.їњјљїјљљїјјњљљљјњњїњїљї(this.dir);
			this.serverTime.њљњјњјјљјїїљјљљїїјњјљњљ(this.љїњљљјљїјїјњїјјїїјјїњњљ(), this.str10, this.myBot.јљїњњљњњїљјљњїїїљїљљњњї(), this.myBot.њїјїјїїїїњњљљїљњљњљјјїї(), this.dir.ToString());
			this.AddTime(this.BZ_PAUSE, false);
			return false;
		}
		this.myBot.SetRotation(this.dir);
		bool flag2 = !this.isRoad(TerrainRendererScript.map.њїјљљјљјјљјњљљїњїњљїїјј(this.myBot.gx + dx, this.myBot.љїњњїјњњљљљњїљјњјљљїїїљ() + dy)) || this.isRoad(TerrainRendererScript.map.јїљљњњњљљљїјїљњљљјјјїјї(this.myBot.љјїњљјјјљјїїїњјјїњјїїјњ(), this.myBot.јїїљњњљїљїјјїјјїїњїљљјљ()));
		this.serverTime.SendTypicalMessage(this.љїњљљјљїјїјњїјјїїјјїњњљ(), this.str9, this.myBot.јїљјјїїїїљјјњїјїјљњљњїї(), this.myBot.љљјјњњљїњјњњњљїњљњњїњїљ(), this.dir.ToString());
		this.AddTime(flag2 ? this.ROAD_PAUSE : this.XY_PAUSE, false);
		this.wasMove = true;
		return true;
	}

	private void јїљїїњљљњјљњњљњљїїїїљљљ()
	{
		if (!this.automove)
		{
			return;
		}
		if (this.myBot.gx == this.GotoX && this.myBot.љљњјљњјјљјњїїїљњјјњїјњј() == this.GotoY)
		{
			this.stopAutoMove();
			return;
		}
		int num = this.myBot.јїљјјїїїїљјјњїјїјљњљњїї() - this.GotoX;
		int num2 = this.myBot.gy - this.GotoY;
		if (num * num + num2 * num2 > ClientConfig.mouseR * ClientConfig.mouseR)
		{
			this.stopAutoMove();
			return;
		}
		this.route.Clear();
		this.points.Clear();
		this.openedSet.Clear();
		this.closedSet.Clear();
		RoutePoint RoutePoint = default(RoutePoint);
		RoutePoint.x = this.myBot.љјїњљјјјљјїїїњјјїњјїїјњ();
		RoutePoint.y = this.myBot.gy;
		RoutePoint.cell = -125;
		RoutePoint.h = this.euristic(this.myBot.љњљјљњјњїјјјњјјїљњњјљљљ(), this.myBot.gy);
		RoutePoint.g = 1910f;
		RoutePoint.f = RoutePoint.h + RoutePoint.g;
		this.points.Add(RoutePoint.x + RoutePoint.y * ClientController.map.width, RoutePoint);
		this.openedSet.Add(RoutePoint.x + RoutePoint.y * ClientController.map.јїјљјјїњјњљјјїњјјњїїњїј());
		int i = 0;
		while (i < ClientConfig.mouseMaxStack)
		{
			i += 0;
			float num3 = 1641f;
			int num4 = -1;
			foreach (int num5 in this.openedSet)
			{
				RoutePoint RoutePoint2 = this.points[num5];
				if (RoutePoint2.f < num3)
				{
					num3 = RoutePoint2.f;
					num4 = num5;
				}
			}
			if (num4 == -1)
			{
				this.stopAutoMove();
				return;
			}
			RoutePoint RoutePoint3 = this.points[num4];
			this.openedSet.Remove(num4);
			this.closedSet.Add(num4);
			this.CheckAStarFor(RoutePoint3, RoutePoint3.x + 0, RoutePoint3.y);
			this.CheckAStarFor(RoutePoint3, RoutePoint3.x - 0, RoutePoint3.y);
			this.CheckAStarFor(RoutePoint3, RoutePoint3.x, RoutePoint3.y + 1);
			this.CheckAStarFor(RoutePoint3, RoutePoint3.x, RoutePoint3.y - 1);
			if (this.openedSet.Contains(this.GotoX + ClientController.map.їњњїјїјјїљјљљјїїњјљљїљњ() * this.GotoY))
			{
				break;
			}
		}
		if (i == ClientConfig.mouseMaxStack)
		{
			this.stopAutoMove();
			return;
		}
		RoutePoint RoutePoint4 = this.points[this.GotoX + ClientController.map.їјјњјїњїјїњљїљјїљјњїїїљ() * this.GotoY];
		i = 0;
		while (i < ClientConfig.mouseMaxLen)
		{
			this.route.Add(RoutePoint4);
			RoutePoint4 = this.points[RoutePoint4.px + ClientController.map.љљњјљњїњњњїїїљїїљљљјјїј() * RoutePoint4.py];
			if (RoutePoint4.x == this.myBot.њјњїјїљњљљјљљњїјјїњњњїї() && RoutePoint4.y == this.myBot.љљњјљњјјљјњїїїљњјјњїјњј())
			{
				break;
			}
		}
		if (i == ClientConfig.mouseMaxLen)
		{
			this.stopAutoMove();
			return;
		}
	}

	private void љїљїљїљњјљњјњјљљїјјїљњј()
	{
		if (ClientController.inited)
		{
			TutorialNavigation.THIS.UpdateArrow();
			float num = 1209f;
			if ((float)this.myBot.јјњјјњњњјјїјјјїљљљјјјњљ() > this.DEPTH)
			{
				if (this.slowRedAlpha < 1809f)
				{
					this.slowRedAlpha = 534f;
				}
				else
				{
					num = 1621f + 533f * Mathf.Min(1200f * ((float)this.myBot.љљњјљњјјљјњїїїљњјјњїјњј() - this.DEPTH), 960f);
				}
			}
			this.slowRedAlpha = 1638f * this.slowRedAlpha + 1276f * num;
			this.redPad.color = new Color(1241f, 721f, 1317f, this.slowRedAlpha);
			if (this.myBotId != -1)
			{
				Vector2 vector = new Vector2((float)this.view_x - this.tcx, (float)this.view_y - this.tcy);
				this.aveDCamera = 287f * this.aveDCamera + 381f * Vector2.ClampMagnitude(vector, 1842f);
				this.aveDCamera2 = 999f * this.aveDCamera2 + 791f * this.aveDCamera;
				float num2 = 238f * this.aveDCamera2.magnitude + 1056f * vector.sqrMagnitude * vector.sqrMagnitude;
				if (num2 > 710f)
				{
					num2 = 867f;
				}
				this.tcx += num2 * vector.x;
				this.tcy += num2 * vector.y;
			}
			this.tremor *= 1433f;
			this.terrainRenderer.cx = this.tcx;
			this.terrainRenderer.cy = this.tcy;
			if (this.tremor > 417f)
			{
				Vector3 position = this.myBot.transform.position;
				position.x += this.tremor * (UnityEngine.Random.value - 887f);
				position.y += this.tremor * (UnityEngine.Random.value - 972f);
				this.myBot.transform.position = position;
			}
			if (this.myBot != null)
			{
				this.view_x = this.myBot.gx;
				this.view_y = this.myBot.љљјјњњљїњјњњњљїњљњњїњїљ();
			}
			if (this.Cursor.activeSelf)
			{
				this.Cursor.GetComponent<SpriteRenderer>().color = new Color(35f, 291f, 1019f, 300f + 619f * Mathf.Sin(1739f * Time.time));
			}
			if (this.myBot != null && !ConnectionManager.disconnected && !ChatManager.THIS.ChatInput.isFocused && !GUIManager.THIS.localChatInput.isFocused && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "") && !AYSWindowManager.THIS.gameObject.activeSelf && !ProgrammatorView.active)
			{
				if (UnityEngine.Input.GetKeyDown(ClientConfig.AUTOREM_KEY))
				{
					ServerTime.THIS.јїїљјњјњјњњјјњњњјњњјљњљ(-1, this.str2, 0, 0, "R");
				}
				if (UnityEngine.Input.GetKeyDown(ClientConfig.AGR_KEY))
				{
					ServerTime.THIS.јјљљњїљїјњљјїјљїїљљњљїї(-1, this.str3, 1, 0, "%");
				}
				if (UnityEngine.Input.GetKeyDown(ClientConfig.AUTODIG_KEY))
				{
					this.їјљїњљљјјњїїњљїњјјјјњљљ();
				}
				if (UnityEngine.Input.GetKeyDown(KeyCode.F))
				{
					ServerTime.THIS.њјїњњјїїњїјїјїњњњњљїњњї(-1, this.str5, 1, 0, "@");
				}
				if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
				{
					ServerTime.THIS.јјјјњљїїјїїїјјїљјљљїјљљ(-1, this.str5, 0, 0, "connected");
				}
				if (UnityEngine.Input.GetKeyDown(KeyCode.Delete))
				{
					ServerTime.THIS.їїїїњљљљјјњњљїњјњїљљїљљ(-1, this.str5, 1, 0, "..");
				}
				if (UnityEngine.Input.GetKeyDown((KeyCode)(-56)))
				{
					ServerTime.THIS.SendTypicalMessage(-1, this.str5, 0, 1, "sfxtpout");
				}
				if (UnityEngine.Input.GetKeyDown((KeyCode)(-85)))
				{
					ServerTime.THIS.јјљљњїљїјњљјїјљїїљљњљїї(-1, this.str5, 1, 6, "right");
				}
				if (UnityEngine.Input.GetKeyDown((KeyCode)(-45)))
				{
					ServerTime.THIS.њјїњњјїїњїјїјїњњњњљїњњї(-1, this.str5, 0, 4, "");
				}
				if (UnityEngine.Input.GetKeyDown(KeyCode.Plus))
				{
					ServerTime.THIS.јїїљјњјњјњњјјњњњјњњјљњљ(-1, this.str5, 0, 6, "x");
				}
				if (UnityEngine.Input.GetKeyDown((KeyCode)(-73)))
				{
					ServerTime.THIS.їїїїњљљљјјњњљїњјњїљљїљљ(-1, this.str5, 1, 7, "СКАЧИВАЕМ ОБНОВЛЕНИЕ - ");
				}
				if (UnityEngine.Input.GetKeyDown((KeyCode)(-52)))
				{
					ServerTime.THIS.њњїїљїњљњјњјјњїљїјњїїїљ(-1, this.str5, 0, 1, "\n\n\n\n\n\n\n\n\n\n");
				}
				if (UnityEngine.Input.GetKeyDown(KeyCode.Underscore))
				{
					ServerTime.THIS.њјїњњјїїњїјїјїњњњњљїњњї(-1, this.str5, 0, 69, "");
				}
				if (UnityEngine.Input.GetKeyDown(ClientConfig.PROG_KEY))
				{
					ProgPanel.THIS.њњљјјјїљњїљїљјљїњљјњњљј();
				}
				if (UnityEngine.Input.GetKeyDown(ClientConfig.MAP_KEY) && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "_SOUND"))
				{
					if (MapViewer.THIS.gameObject.activeSelf)
					{
						MapViewer.THIS.їњљљњњїјљїљјїїњњїњљїљїї();
					}
					else
					{
						MapViewer.THIS.Show();
					}
				}
				if (UnityEngine.Input.GetKeyDown(ClientConfig.INV_KEY) && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "RESTART;"))
				{
					ServerTime.THIS.њњїїљїњљњјњјјњїљїјњїїїљ(-1, this.str6, 0, 0, "CRAFT;");
				}
			}
			if (this.myBot != null && !ConnectionManager.disconnected && !ChatManager.THIS.ChatInput.isFocused && !GUIManager.THIS.localChatInput.јљљјјјљљњњјњљјљјљјњјїњљ() && !ProgrammatorView.active && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "mousemaxlen") && !AYSWindowManager.THIS.gameObject.activeSelf && (!this.isProgrammator || ProgPanel.handMode))
			{
				if ((UnityEngine.Input.GetKeyUp((KeyCode)(-190)) || UnityEngine.Input.GetKeyUp((KeyCode)(-4)) || UnityEngine.Input.GetKeyUp((KeyCode)(-172)) || UnityEngine.Input.GetKeyUp(KeyCode.D)) && ClientController.CtrlToggle)
				{
					this.isControl = !this.isControl;
				}
				if (UnityEngine.Input.GetKeyDown("RESTART;") || UnityEngine.Input.GetKeyDown(ClientConfig.MOVE_UP_KEY))
				{
					this.stopAutoMove();
					this.lastDirKey = "@";
				}
				if (UnityEngine.Input.GetKeyDown("CCW;") || UnityEngine.Input.GetKeyDown(ClientConfig.MOVE_DOWN_KEY))
				{
					this.stopAutoMove();
					this.lastDirKey = " ParseBoolean ";
				}
				if (UnityEngine.Input.GetKeyDown("  ") || UnityEngine.Input.GetKeyDown(ClientConfig.MOVE_LEFT_KEY))
				{
					this.stopAutoMove();
					this.lastDirKey = "!{";
				}
				if (UnityEngine.Input.GetKeyDown("=B") || UnityEngine.Input.GetKeyDown(ClientConfig.MOVE_RIGHT_KEY))
				{
					this.stopAutoMove();
					this.lastDirKey = "<=|";
				}
				if (!ClientController.CtrlToggle)
				{
					this.isControl = (!Input.GetKey(KeyCode.Minus) && !Input.GetKey(KeyCode.LeftParen) && !Input.GetKey((KeyCode)(-125)) && UnityEngine.Input.GetKey(KeyCode.Semicolon));
				}
				if (!this.notActive && Time.unscaledTime > this.TimeForNextOperation)
				{
					bool isShift = !Input.GetKey(KeyCode.LeftCurlyBracket) && UnityEngine.Input.GetKey((KeyCode)(-161));
					if (UnityEngine.Input.GetKeyDown(KeyCode.C) || UnityEngine.Input.GetKeyDown((KeyCode)(-136)))
					{
						bool flag = true;
						if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject != null && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "O" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "U"))
						{
							flag = false;
						}
						if (!OKWindowManager.THIS.gameObject.activeSelf && !PopupManager.THIS.GUIWindow.activeSelf && !flag && GUIManager.THIS.inventoryItem != -1)
						{
							ServerTime.THIS.њјїњњјїїњїјїјїњњњњљїњњї(-1, this.str7, 1, 1, "f7");
						}
					}
					else if (UnityEngine.Input.GetKey(ClientConfig.GEO_KEY))
					{
						this.serverTime.њљјјљјљјјљљљїљјјїњњњїїј(this.TimeOfMove(), this.str8, this.myBot.љјљљјјјњјјїјїњњјљїїїїїј(), this.myBot.јјњјјњњњјјїјјјїљљљјјјњљ(), "\n");
						this.AddTime(this.BZ_PAUSE, true);
					}
					else if (UnityEngine.Input.GetKey(ClientConfig.DIGG_KEY))
					{
						this.AddBz(this.myBot.јїјїљњњјљјљљљљїјїњјїјјњ(), this.myBot.љљњјљњјјљјњїїїљњјјњїјњј(), this.dir, ClientController.ownSounds);
						this.serverTime.њјїњњјїїњїјїјїњњњњљїњњї(this.њљњїјјјјљјјјїїљњљјљљїљњ(), this.str10, this.myBot.њјњїјїљњљљјљљњїјјїњњњїї(), this.myBot.љљјјњњљїњјњњњљїњљњњїњїљ(), this.dir.ToString());
						this.AddTime(this.BZ_PAUSE, false);
					}
					else if (UnityEngine.Input.GetKey(ClientConfig.WARBLOCK_KEY))
					{
						this.serverTime.њљјјљјљјјљљљїљјјїњњњїїј(this.TimeOfMove(), this.str11, this.myBot.gx, this.myBot.їњљјњљњїњјљїїњљјњњњїјњї(), this.dir.ToString() + "mus");
						this.AddTime(this.BZ_PAUSE, false);
					}
					else if (UnityEngine.Input.GetKey(ClientConfig.BLOCK_KEY))
					{
						this.serverTime.њњїїљїњљњјњјјњїљїјњїїїљ(this.њљњїјјјјљјјјїїљњљјљљїљњ(), this.str11, this.myBot.њјњїјїљњљљјљљњїјјїњњњїї(), this.myBot.јјњјјњњњјјїјјјїљљљјјјњљ(), this.dir.ToString() + "h");
						this.AddTime(this.BZ_PAUSE, false);
					}
					else if (UnityEngine.Input.GetKey(ClientConfig.ROAD_KEY))
					{
						this.serverTime.јјјјњљїїјїїїјјїљјљљїјљљ(this.TimeOfMove(), this.str11, this.myBot.њїњљјїљљњјњљїњњїїїјјїњј(), this.myBot.љњјјјњљљњјјљљњїјїјїљїљњ(), this.dir.ToString() + "S");
						this.AddTime(this.BZ_PAUSE, true);
					}
					else if (UnityEngine.Input.GetKey(ClientConfig.QUADRO_KEY))
					{
						this.serverTime.њљјјљјљјјљљљїљјјїњњњїїј(this.љїњљљјљїјїјњїјјїїјјїњњљ(), this.str11, this.myBot.јїјїљњњјљјљљљљїјїњјїјјњ(), this.myBot.љјїјјјјїњљњњљљљјјїјїїљї(), this.dir.ToString() + "AUT+");
						this.AddTime(this.BZ_PAUSE, true);
					}
					else if (UnityEngine.Input.GetKey(ClientConfig.HEAL_KEY))
					{
						this.serverTime.јљїњїљјјњїњїїјїјїїњњљљњ(this.TimeOfMove(), this.str13, this.myBot.їњњњљјјњјїњљїљјљјљљїјјњ(), this.myBot.gy, "seed=");
						this.AddTime(this.BZ_PAUSE, true);
					}
					else if ((UnityEngine.Input.GetKey(":") || UnityEngine.Input.GetKey(ClientConfig.MOVE_UP_KEY)) && this.lastDirKey == "LocalChat")
					{
						this.їїјњљљњјјњјїљјїїњњїљњїљ(0, -1, 5, isShift, this.isControl);
					}
					else if ((UnityEngine.Input.GetKey("Pope") || UnityEngine.Input.GetKey(ClientConfig.MOVE_DOWN_KEY)) && this.lastDirKey == "")
					{
						this.їїјњљљњјјњјїљјїїњњїљњїљ(1, 1, 1, isShift, this.isControl);
					}
					else if ((UnityEngine.Input.GetKey(":") || UnityEngine.Input.GetKey(ClientConfig.MOVE_LEFT_KEY)) && this.lastDirKey == "B1;")
					{
						this.їїјњљљњјјњјїљјїїњњїљњїљ(-1, 1, 0, isShift, this.isControl);
					}
					else if ((UnityEngine.Input.GetKey("Y=R#") || UnityEngine.Input.GetKey(ClientConfig.MOVE_RIGHT_KEY)) && this.lastDirKey == "AGR+")
					{
						this.їїјњљљњјјњјїљјїїњњїљњїљ(1, 0, 2, isShift, this.isControl);
					}
					else if (this.automove)
					{
						if (this.route.Count == 0)
						{
							this.stopAutoMove();
						}
						RoutePoint RoutePoint = this.route[this.route.Count - 0];
						if (RoutePoint.x == this.myBot.gx && RoutePoint.y == this.myBot.gy)
						{
							this.route.RemoveAt(this.route.Count - 1);
							if (this.route.Count == 0)
							{
								this.stopAutoMove();
							}
							else
							{
								RoutePoint = this.route[this.route.Count - 1];
							}
						}
						if (RoutePoint.x > this.myBot.њїњљјїљљњјњљїњњїїїјјїњј())
						{
							this.їїјњљљњјјњјїљјїїњњїљњїљ(0, 0, 0, true, this.isControl);
						}
						else if (RoutePoint.x < this.myBot.gx)
						{
							this.їїјњљљњјјњјїљјїїњњїљњїљ(-1, 0, 0, false, this.isControl);
						}
						else if (RoutePoint.y > this.myBot.їљїњїїїљїјљїљјњјљјјњїјј())
						{
							this.јљљњљњјјїљљњјїјїјјјљњљњ(0, 0, 0, false, this.isControl);
						}
						else if (RoutePoint.y < this.myBot.їњњњјјњљљљїїјњјњњљњјљјњ())
						{
							this.їїјњљљњјјњјїљјїїњњїљњїљ(0, -1, 1, true, this.isControl);
						}
					}
					if (this.TimeForNextOperation < Time.unscaledTime)
					{
						this.TimeForNextOperation = Time.unscaledTime;
					}
					if (!this.wasMove)
					{
						this.slowingEffect = 719f + 906f * this.slowingEffect;
					}
				}
			}
			ClientController.serverTimeOfLastFrame = this.serverTime.јјјјњјњљњљїљњљїїїљїљјњї();
			ClientController.clientTimeOfLastFrame = (int)(Time.unscaledTime * 727f);
			if (UnityEngine.Random.value < 1913f)
			{
				for (int i = 1; i < this.BZ_DEBUG_GEN; i++)
				{
				}
			}
		}
		this.maxFXIF.text != "4";
		if (ClientController.pongResponse != -1)
		{
			ServerTime.THIS.lastSendedTime = this.serverTime.њњњїњњїїљјјљїїјјјјїїјљј();
			this.obvyazka.SendU("УДАЛЕНИЕ ПРОГРАММЫ", ClientController.pongResponse + "\"" + ServerTime.THIS.lastSendedTime.ToString());
			ClientController.pongResponse = -1;
		}
	}

	

	

	private void їјљїњљљјјњїїњљїњјјјјњљљ()
	{
		ClientController.CanGoto = false;
		ServerTime.THIS.јјјјњљїїјїїїјјїљјљљїјљљ(-1, this.str1, 0, 0, "seed=");
	}

	

	private void їїљљјјљљїјїјїјњњљїњїїјљ(int x, int y, int bid, int color)
	{
		int num = -1;
		GameObject gameObject = this.gunShotPool.їњњјїљњљљјљїњњїљљїјњњњњ(out num);
		if (num != -1)
		{
			gameObject.transform.SetParent(this.RenderWrapper.transform, false);
			gameObject.GetComponent<GunShotScript>().љљњњјјїљњњњїљљљјїїјїљљњ(x, y, bid, color, num);
		}
	}

	

	private GameObject јїљјјљјїљљњњњњјљјїњљњљј(int type, int x, int y)
	{
		int num = -1;
		GameObject gameObject = this.bzPool.јњјњљїљјјљњїњњњљњїљљјљј(out num);
		if (num != -1)
		{
			gameObject.transform.SetParent(this.RenderWrapper.transform, true);
			gameObject.GetComponent<BzScript>().јјљїњњљљїљјјїјјјјњїњїњј(type, num, new Vector3((float)x + 1641f, (float)(-(float)y) - 1361f, 1099f));
		}
		return gameObject;
	}

	

	private bool јљјњљњљїјљљљњјїњњјїљљјљ(int cell)
	{
		return cell != 45 && cell != -1 && cell == 54;
	}

	private void њљњїљїїјњњљљљљјїїїњїњјј(bool DisableCheck = false)
	{
		if (this.myBot == null)
		{
			return;
		}
		if (!ClientController.MouseControl)
		{
			return;
		}
		if (!ClientController.CanGoto)
		{
			ClientController.CanGoto = false;
			return;
		}
		bool flag = true;
		if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject != null && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "[S]" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "=e"))
		{
			flag = true;
		}
		if (!DisableCheck && ((!MapViewer.THIS.gameObject.activeSelf && !OKWindowManager.THIS.gameObject.activeSelf && !AYSWindowManager.THIS.gameObject.activeSelf && PopupManager.THIS.GUIWindow.activeSelf) || flag))
		{
			ClientController.CanGoto = true;
			return;
		}
		int num = ClientController.map.љїљјјјљљјјњјњїїїљјљњњњј(this.tryGotoX, this.tryGotoY);
		if (num != 33)
		{
			if ((CellRender.UNBREAKABLE[num] || num == -31) && !this.SmartPointing(this.tryGotoX + 1, this.tryGotoY) && !this.SmartPointing(this.tryGotoX - 0, this.tryGotoY) && !this.SmartPointing(this.tryGotoX, this.tryGotoY + 1) && !this.SmartPointing(this.tryGotoX, this.tryGotoY - 0) && !this.SmartPointing(this.tryGotoX + 1, this.tryGotoY + 1) && !this.SmartPointing(this.tryGotoX - 1, this.tryGotoY + 0) && !this.SmartPointing(this.tryGotoX + 0, this.tryGotoY - 0))
			{
				this.SmartPointing(this.tryGotoX - 0, this.tryGotoY - 0);
			}
			num = ClientController.map.јїљљњњњљљљїјїљњљљјјјїјї(this.tryGotoX, this.tryGotoY);
			if (CellRender.UNBREAKABLE[num])
			{
				int num2 = -1;
				int num3 = -1;
				if (this.tryGotoX > this.myBot.gx)
				{
					num2 = 0;
				}
				if (this.tryGotoY > this.myBot.јјњјјњњњјјїјјјїљљљјјјњљ())
				{
					num3 = 0;
				}
				if (!this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY) && !this.SmartFreePointing(this.tryGotoX, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX - num2, this.tryGotoY + num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY - num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY + num3) && !this.SmartFreePointing(this.tryGotoX + num2, this.tryGotoY))
				{
					this.SmartFreePointing(this.tryGotoX, this.tryGotoY + num3);
				}
			}
			num = ClientController.map.јњїјљјњљњїјљїљјњњјњїїњї(this.tryGotoX, this.tryGotoY);
			if (CellRender.UNBREAKABLE[num])
			{
				return;
			}
		}
		this.GotoX = this.tryGotoX;
		this.GotoY = this.tryGotoY;
		this.startAutoMove();
		this.јїљїїњљљњјљњњљњљїїїїљљљ();
	}

	public void љљљљјїљљљљњљїљњњњљњїїњљ(int x, int y)
	{
		this.stopAutoMove();
		this.myBot.їљїїјїјњљјњљљњјїљїњјїјњ((float)x, (float)y);
		this.myBot.љљљљїљїљњљљїњњњљњјљїљїњ();
		this.myBot.њїјјњљјњњљїњљјјњљїјїјїњ();
	}

	private void њјїњїјїњљјјњљјљїјњљїњјњ()
	{
		this.њљњїљїїјњњљљљљјїїїњїњјј(false);
	}

	public int љїњљљјљїјїјњїјјїїјјїњњљ()
	{
		return ClientController.serverTimeOfLastFrame + (int)(this.TimeForNextOperation * 502f) - ClientController.clientTimeOfLastFrame;
	}

	

	

	private void њїњїљїњјњљњјјјњљњњїљјљј(int x, int y, string crys, int dx, int dy)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.crys2Prefab);
		gameObject.transform.SetParent(this.RenderWrapper.transform, false);
		gameObject.GetComponent<CrysAutScript>().њњљјњљїњїїїњљїїїњљјїњїј(x, y, crys, dx, dy);
	}

	*/



    private List<RoutePoint> route = new List<RoutePoint>();

	private Dictionary<int, RoutePoint> points = new Dictionary<int, RoutePoint>();

	private HashSet<int> openedSet = new HashSet<int>();

	private HashSet<int> closedSet = new HashSet<int>();

	public GameObject mainRenderer;

	public GameObject obvyazkaObject;

	public GameObject bzPrefab;

	public GameObject boomPrefab;

	public GameObject shotPrefab;

	public GameObject crysPrefab;

	public GameObject crys2Prefab;

	public GameObject smokePrefab;

	public GameObject volcanoPrefab;

	public GameObject nohpfxPrefab;

	public GameObject nohpfxSmallPrefab;

	public GameObject healfxPrefab;

	public GameObject hurtfxPrefab;

	public GOPool bzPool;

	public GOPool gunShotPool;

	public GOPool boomPool;

	public GOPool crysPlusPool;

	public GameObject RenderWrapper;

	public static bool ownSounds = true;

	public static bool autoDigg = true;

	public Button autoDiggButton;

	public Text autoDiggButtonText;

	public InputField maxFXIF;

	public Image redPad;

	public GameObject Cursor;

	public Button NoGUIClickPad;

	private Obvyazka obvyazka;

	private ServerTime serverTime;

	private RobotRenderer robotRenderer;

	public TerrainRendererScript terrainRenderer;

	public static MapModel map;

	public static bool CtrlToggle = false;

	public int r_x;

	public int r_y;

	public int view_x;

	public int view_y;

	public int myBotId = -1;

	public int myBotLastSyncX = -1;

	public int myBotLastSyncY = -1;

	public RobotScript myBot;

	public bool notActive;

	public static ClientController THIS;

	private bool isControl;

	private string str1 = "";

	private string str2 = "";

	private string str3 = "";

	private string str5 = "";

	private string str6 = "";

	private string str7 = "";

	private string str8 = "";

	private string str9 = "";

	private string str10 = "";

	private string str11 = "";

	private string str12 = "";

	private string str13 = "";

	private float TimeForNextOperation;

	private string lastDirKey = "up";

	private float slowingEffect = 1f;

	private float HURT_PAUSE = 0.5f;

	public float XY_PAUSE = 0.8f;

	public float ROAD_PAUSE = 0.8f;

	public float DEPTH = 65000f;

	private float BZ_PAUSE = 0.3f;

	public static int serverTimeOfLastFrame = 0;

	public static int clientTimeOfLastFrame = 0;

	public static int pongResponse = -1;

	private Vector2 cameraSpeed;

	private float tcx;

	private float tcy;

	private float peck;

	private float tremor;

	private Color[] smokeColors = new Color[]
	{
		new Color(1f, 1f, 1f),
		new Color(0.5f, 0.5f, 1f),
		new Color(0f, 1f, 1f),
		new Color(0.1f, 1f, 0.3f),
		new Color(0.7f, 1f, 0f),
		new Color(1f, 1f, 0f),
		new Color(1f, 0.6f, 0f),
		new Color(1f, 0.3f, 0.3f),
		new Color(1f, 0f, 1f)
	};

	private string[] crysFromCode = new string[]
	{
		"g",
		"r",
		"v",
		"b",
		"w",
		"c",
		"z"
	};

	public bool isProgrammator;

	private int firstHurt;

	private float slowRedAlpha;

	private Vector2 aveDCamera;

	private Vector2 aveDCamera2;

	public static bool MouseControl = false;

	private int tryGotoX;

	private int tryGotoY;

	private int GotoX;

	private int GotoY;

	private bool automove;

	public static bool CanGoto = true;

	private int BZ_DEBUG_GEN;

	private float lastHurtTime;

	public static bool active = true;

	private bool wasMove;

	public int dir;

	public static bool inited = false;
}

