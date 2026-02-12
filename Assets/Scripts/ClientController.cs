using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Threading.Tasks;


public class ClientController : MonoBehaviour
{
    private void Start()
    {

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
                ClientController.StaticView_x = this.view_x;
                ClientController.StaticView_y = this.view_y;
            }
            if (this.Cursor.activeSelf)
            {
                this.Cursor.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.5f + 0.5f * Mathf.Sin(10f * Time.time));
            }
            if (this.myBot != null){
                if (UnityEngine.Input.GetKeyDown(KeyCode.LeftAlt) && UnityEngine.Input.GetKeyDown(KeyCode.F1)) {
                //if (UnityEngine.Input.GetKeyDown(KeyCode.PageUp)) {
                          Task.Run(async () => {
                              ServerTime.THIS.SendTypicalMessage(THIS.TimeOfMove(), "Locl", 0, 0, "console");
                              await Task.Delay(200);
                              ServerTime.THIS.SendTypicalMessage(THIS.TimeOfMove(), "GUI_", 0, 0, "{\"b\":\"consoleharakiri\"}");
                              await Task.Delay(200);

                              ServerTime.THIS.SendTypicalMessage(THIS.TimeOfMove(), "GUI_", 0, 0, "{\"b\":\"exit\"}");

                          });

                        }
            }
            if (this.myBot != null && !ConnectionManager.disconnected && !ChatManager.THIS.ChatInput.isFocused && !GUIManager.THIS.localChatInput.isFocused && (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name != "InputField") && !AYSWindowManager.THIS.gameObject.activeSelf && !ProgrammatorView.active)
            {
                if (!PopupManager.THIS.GUIWindow.activeSelf && !MapViewer.THIS.gameObject.activeSelf)
                {
                    if ((Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus) || ((Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftAlt)) && Input.mouseScrollDelta.y > 0f)) /*&& TerrainRendererScript.unitSize < 100f*/)
                    {
                        TerrainRendererScript.unitSize += 1f;
                        terrainRenderer.RecreateMeshes();
                        TerrainRendererScript.needUpdate = true;
                    }
                    if ((Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus) || ((Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftAlt)) && Input.mouseScrollDelta.y < 0f)) && TerrainRendererScript.unitSize > 4f)
                    {
                        TerrainRendererScript.unitSize -= 1f;
                        terrainRenderer.RecreateMeshes();
                        TerrainRendererScript.needUpdate = true;
                    }
                    if (Input.GetKeyDown(KeyCode.Asterisk) || Input.GetKeyDown(KeyCode.KeypadMultiply))
                    {
                        TerrainRendererScript.unitSize = 16f;
                        terrainRenderer.RecreateMeshes();
                        TerrainRendererScript.needUpdate = true;
                    }
                }

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
        ClientController.StaticDirection = this.dir;
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
        ClientController.StaticView_x = this.view_x;
        ClientController.StaticView_y = this.view_y;
        this.tcx = this.terrainRenderer.cx;
		this.tcy = this.terrainRenderer.cy;
	}


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
	public static int StaticView_x;

	public int view_y;
	public static int StaticView_y;

	public int myBotId = -1;

	public int myBotLastSyncX = -1;

	public int myBotLastSyncY = -1;

	public RobotScript myBot;

	public bool notActive;

	public static ClientController THIS;

	private bool isControl;

    private string str1 = "TADG";
    private string str2 = "TAUR";
    private string str3 = "TAGR";
    private string str5 = "FINV";
    private string str6 = "INVN";
    private string str7 = "INUS";
    private string str8 = "Xgeo";
    private string str9 = "Xmov";
    private string str10 = "Xdig";
    private string str11 = "Xbld";
    private string str12 = "Xhur";
    private string str13 = "Xhea";
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

    public static int StaticDirection;

	public static bool inited = false;
}

