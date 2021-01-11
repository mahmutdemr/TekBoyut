using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class CarpismaAlgilayici
{
    static List<Nesne> tumNesneler = new List<Nesne>();
    static List<Duvar> tumDuvarlar = new List<Duvar>();
    static int nesneSayisi;
    static int duvarSayisi;
    public static int carpismaSayisi;
    public static void nesneEkle(Nesne eklenecek)
    {
        tumNesneler.Add(eklenecek);
        nesneSayisi = tumNesneler.Count;
    }
    
    public static void duvarEkle(Duvar eklenecek)
    {
        tumDuvarlar.Add(eklenecek);
        duvarSayisi = tumDuvarlar.Count;
    }
    //CarpismaHesapla voidi nesnelerin konumunu hesaplayarak çarpışan nesnelerin hızlarını Carpistir voidi ile yeniden hesaplatır
    public static void CarpismaHesapla()
    {
        for (int i = 0; i < nesneSayisi - 1; i++)
        {
            for (int j = i + 1; j < nesneSayisi; j++)
            {
                if (tumNesneler[i].konum - tumNesneler[j].konum  <= 1f && tumNesneler[i].konum - tumNesneler[j].konum  >= -1f)
                    if (!tumNesneler[i].temasEdenler.Contains(tumNesneler[j].isim))
                    {
                        tumNesneler[j].temasEdenler.Add(tumNesneler[i].isim);
                        tumNesneler[i].temasEdenler.Add(tumNesneler[j].isim);
                        Carpistir(tumNesneler[j], tumNesneler[i]);
                    } else { }
                else
                {
                    if (tumNesneler[j].temasEdenler.Contains(tumNesneler[i].isim))
                    {
                        tumNesneler[j].temasEdenler.Remove(tumNesneler[i].isim);
                        tumNesneler[i].temasEdenler.Remove(tumNesneler[j].isim);
                    }
                }
            }
        }
        //Herbir duvar için eğer yakınında ona doğru gelmekte olan nesne varsa hiz yonu tersine çevrilir
        tumDuvarlar.ForEach(Duvar =>
        {
            tumNesneler.ForEach(Nesne =>
            {
                if ((Duvar.konum - Nesne.konum  <= 1f && Nesne.rigid.yon && Duvar.konum - Nesne.konum >= 0) || (Duvar.konum - Nesne.konum >= -1f && !Nesne.rigid.yon && Duvar.konum - Nesne.konum <= 0) || Duvar.konum == Nesne.konum)
                    if (!Duvar.temasEdenler.Contains(Nesne.isim))
                    {
                        Nesne.rigid.yon = true;
                        Duvar.temasEdenler.Add(Nesne.isim);

                        carpismaSayisi++;
                    }
                    else { }
                else
                {
                    if(Duvar.temasEdenler.Contains(Nesne.isim))
                        Duvar.temasEdenler.Remove(Nesne.isim);
                }
            });
        });

    }
    static void Carpistir(Nesne sol, Nesne sag)
    {
        carpismaSayisi++;
        //belirtilen nesnelerin hiz ve agirlik bilgileri alınarak yeni hızları tekrar hesaplatılır
        float hiz1 = hizHesapla(sol.rigid.BilgiAl, sag.rigid.BilgiAl);
        float hiz2 = hizHesapla(sag.rigid.BilgiAl, sol.rigid.BilgiAl);
        //hesaplanan hizlar nesnelere atanır
        sol.rigid.hizHesapla = hiz1;
        sag.rigid.hizHesapla = hiz2;
    }
    static float hizHesapla(Vector2 bir, Vector2 iki)
    {
        float deger = (bir.y - iki.y) / (bir.y + iki.y) * bir.x + 2 * iki.y * iki.x / (bir.y + iki.y); // Esnek çarpışan iki cismin yeni hızını hesaplayan formül
        return deger;
    }
}
