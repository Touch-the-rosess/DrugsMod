using System;
using System.Collections.Generic;

namespace HealthCalculator
{
    // Token: 0x02000106 RID: 262
    public static class HealthCalculator
    {
        // Token: 0x06000678 RID: 1656 RVA: 0x0000711D File Offset: 0x0000531D
        public static void Add(int bid)
        {
            if (!HealthCalculator.states.ContainsKey(bid))
            {
                HealthCalculator.states.Add(bid, new HealthCalculator.HealthState());
            }
        }

        // Token: 0x06000679 RID: 1657 RVA: 0x0000713C File Offset: 0x0000533C
        public static void Remove(int bid)
        {
            if (HealthCalculator.states.ContainsKey(bid) && HealthCalculator.states[bid].HasPrediction())
            {
                HealthCalculator.log.Add(new KeyValuePair<int, HealthCalculator.HealthState>(bid, HealthCalculator.states[bid]));
            }
        }

        // Token: 0x0600067A RID: 1658 RVA: 0x0003D5D4 File Offset: 0x0003B7D4
        public static void Healed(int bid)
        {
            if (HealthCalculator.states.ContainsKey(bid))
            {
                if (HealthCalculator.states[bid].HasPrediction())
                {
                    HealthCalculator.log.Add(new KeyValuePair<int, HealthCalculator.HealthState>(bid, HealthCalculator.states[bid]));
                }
                HealthCalculator.states[bid] = new HealthCalculator.HealthState();
            }
        }

        // Token: 0x0600067B RID: 1659 RVA: 0x00007178 File Offset: 0x00005378
        public static void SetHP(int bid, int percent)
        {
            if (HealthCalculator.states.ContainsKey(bid))
            {
                HealthCalculator.states[bid].SetPercent(percent);
            }
        }

        // Token: 0x0600067C RID: 1660 RVA: 0x00007198 File Offset: 0x00005398
        public static void Damage(int bid)
        {
            if (HealthCalculator.states.ContainsKey(bid))
            {
                HealthCalculator.states[bid].AddDamage(HealthCalculator.damage);
            }
        }

        // Token: 0x0600067D RID: 1661 RVA: 0x000071BC File Offset: 0x000053BC
        public static void SetDamageScale(int dmg)
        {
            HealthCalculator.damage = dmg;
        }

        // Token: 0x0600067F RID: 1663 RVA: 0x000071E0 File Offset: 0x000053E0
        public static float GetMaxPrediction(int bid)
        {
            if (HealthCalculator.states.ContainsKey(bid))
            {
                return HealthCalculator.states[bid].maxHealthPrediction;
            }
            return -1f;
        }

        // Token: 0x06000680 RID: 1664 RVA: 0x00007205 File Offset: 0x00005405
        public static float GetCurrentPrediction(int bid)
        {
            if (HealthCalculator.states.ContainsKey(bid))
            {
                return HealthCalculator.states[bid].currentHealthPrediction;
            }
            return -1f;
        }

        // Token: 0x040007CA RID: 1994
        public static Dictionary<int, HealthCalculator.HealthState> states = new Dictionary<int, HealthCalculator.HealthState>();

        // Token: 0x040007CB RID: 1995
        public static int damage = 1;

        // Token: 0x040007CC RID: 1996
        public static List<KeyValuePair<int, HealthCalculator.HealthState>> log = new List<KeyValuePair<int, HealthCalculator.HealthState>>();

        // Token: 0x02000107 RID: 263
        public class HealthState
        {
            // Token: 0x06000681 RID: 1665 RVA: 0x0003D62C File Offset: 0x0003B82C
            public void AddDamage(int dmg)
            {
                this.damageDealt += dmg;
                this.tmpDmgPerPercent += dmg;
                float num = this.currentPercent - 1f / (float)this.dmgPerPercent * (float)this.tmpDmgPerPercent;
                float num2 = 100f * (float)this.damageDealt / (this.startPercent - num);
                if (this.maxHealthPrediction < num2)
                {
                    this.maxHealthPrediction = num2;
                }
                this.currentHealthPrediction = (float)((int)(num / 100f * this.maxHealthPrediction));
            }

            // Token: 0x06000682 RID: 1666 RVA: 0x0003D6B0 File Offset: 0x0003B8B0
            public void SetPercent(int percent)
            {
                if (this.startPercent == -1f)
                {
                    this.startPercent = (float)percent;
                }
                if (this.currentPercent != (float)percent)
                {
                    if (this.dmgPerPercent < this.tmpDmgPerPercent)
                    {
                        this.dmgPerPercent = this.tmpDmgPerPercent;
                    }
                    this.tmpDmgPerPercent = 1;
                }
                this.currentPercent = (float)percent;
            }

            // Token: 0x06000684 RID: 1668 RVA: 0x0000722A File Offset: 0x0000542A
            public bool HasPrediction()
            {
                return this.startPercent > this.currentPercent && this.maxHealthPrediction != -1f && this.currentHealthPrediction != -1f;
            }

            // Token: 0x040007CD RID: 1997
            public int damageDealt;

            // Token: 0x040007CE RID: 1998
            public float currentHealthPrediction = -1f;

            // Token: 0x040007CF RID: 1999
            public float maxHealthPrediction = -1f;

            // Token: 0x040007D0 RID: 2000
            public float startPercent = -1f;

            // Token: 0x040007D1 RID: 2001
            public float currentPercent = -1f;

            // Token: 0x040007D2 RID: 2002
            public int dmgPerPercent = 1;

            // Token: 0x040007D3 RID: 2003
            public int tmpDmgPerPercent = 1;
        }
    }
}
