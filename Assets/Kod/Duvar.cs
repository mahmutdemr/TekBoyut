using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duvar : MonoBehaviour
{
    /*
        Temel olarak Nesne sınıfı ile aynı özelliktedir sadecce konum bilgisine sahip olup konumu bir daha atanmaz ve değişmez
    */

    public float konum;
    public List<string> temasEdenler = new List<string>();
    public Duvar(string nesneIsim, float nesneKonum)
    {
        CarpismaAlgilayici.duvarEkle(this); //CarpismaAlgilayiciya kendini tanıtır
        GameObject goruntu1 = Instantiate(Resources.Load("duvar", typeof(GameObject))) as GameObject;
        Transform goruntu = goruntu1.transform;

        goruntu.parent = GameObject.Find("SimYonetici").transform;
        goruntu.name = nesneIsim;

        konum = nesneKonum;
        goruntu.position = new Vector3(konum, 0,0);//Sahneye yansıtır

    }
}
