using UnityEngine;
public static class Zaman
{
    static float anlikZaman;
    static float oncekiZaman;

    public static float deltaZaman;
    public static void Guncelle()
    {
        anlikZaman = Time.time; //Time.time oyun basından beri gecen saniyeyi float olarak verir
        deltaZaman = anlikZaman - oncekiZaman; // bir onceki kareden geçen süre hesaplanır
        oncekiZaman = anlikZaman; 
    }
}
