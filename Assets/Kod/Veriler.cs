using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct duvarOzellik
{
    public float konum;
    public duvarOzellik(float duvarKonum)
    {
        konum = duvarKonum;
    }
}
[Serializable]
public struct nesneOzellik
{
    public float hiz;
    public float konum;
    public int agirlik;

}
public enum HareketDurumu
{
    sabit,
    hareketli
}
public class Veriler : MonoBehaviour
{
    [SerializeField] int CarpismaSayisi = 0;

    public List<duvarOzellik> uretilecekDuvarlar;
    public List<nesneOzellik> uretilecekNesneler;
    private void Update()
    {
        CarpismaSayisi = CarpismaAlgilayici.carpismaSayisi;
    }
}
