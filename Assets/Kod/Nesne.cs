using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nesne : MonoBehaviour
{
    public List<string> temasEdenler = new List<string>(); //Nesneye anlık temas etmekte olan nesnelerin listesini tutar
    public NesneRigid rigid;//Nesnenin fiziksel hareketlerini hesaplayacak sınıf

    public string isim;
    public float konum; //nesnenin anlık konumu

    Transform goruntu;
    Vector3 konumVektor;
    
    public Nesne(string nesneIsim , float nesneHiz ,float baslaKonum , int nesneAgirlik)
    {
        CarpismaAlgilayici.nesneEkle(this);//Uretilen her nesne kendini carpisma algilayıcıya tanıtır
        GameObject goruntu1 = Instantiate(Resources.Load("nesne", typeof(GameObject))) as GameObject; //nesnenin sahnede görüntülenmesini sağlayacak obje
        
        //Nesne ismine göre varsa onceden belirlenmis materyal atılır
        if((Material)Resources.Load("Materyal/Materyal_" + nesneIsim, typeof(Material)) != null)
            goruntu1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materyal/Materyal_" + nesneIsim, typeof(Material));
        else
            goruntu1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materyal/default", typeof(Material));

        goruntu = goruntu1.transform;
        goruntu.parent = GameObject.Find("SimYonetici").transform;

        rigid = new NesneRigid();
        rigid.DegerAl(nesneHiz, nesneAgirlik); //Nesneye atanan hiz ve agirlik bilgileri nesnenin Fizik sınıfına aktarılır

        isim = nesneIsim;
        goruntu.name = isim;

        konum = baslaKonum;
        konumVektor = Vector3.zero;

        Cizdir();//Nesne sahnedeki konumunu alır
    }
  

    public void Guncelle()
    {
        KonumHesapla(); //Nesnenin konumu hesaplatılır
        Cizdir();//Nesne sahnedeki konumunu alır
    }

    void KonumHesapla()
    {
        konum += rigid.FizikselHesap; //Fizik sınıfından alınan anlık hesaplanmış değere göre nesnenin yeni konumu tanımlanır
    }

    void Cizdir()
    {
        konumVektor.x = konum;
        goruntu.position = konumVektor;
        goruntu.name = isim + rigid.Degerler;
    }
}
