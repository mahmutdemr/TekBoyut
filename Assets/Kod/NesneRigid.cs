using UnityEngine;

public class NesneRigid : MonoBehaviour
{

    float hiz;
    int agirlik = 1;

    //Nesnenin duvarla olan iletişimini kolaylaştırmak için oluçturulmuş yön belirten bool
    public bool yon
    {
        get
        {
            return hiz > 0 ? true : false;
        }
        set
        {
            if (value)
                hiz *= -1;
            else
                hiz = 0;
        }
    }
    
    //Nesnenin bilgileri unity sahnesinde oluşturulan referansına isim olarak atanır (amaç değerleri rahat gözlemyebilmek)
    public string Degerler
    {
        get
        {
            return "((hiz:" + hiz + " Agirlik:" + agirlik + " Momentum:" + (hiz * agirlik) + "))";
        }
    }

    public Vector2 BilgiAl
    {
        get
        {
            return new Vector2(hiz, agirlik);
        }
    }
    public float hizHesapla
    {
        set
        {
            hiz = value;
        }
    }
    public float FizikselHesap
    {
        get
        {
            //Nesnenin o anki hizini gonderir
            return hiz * Zaman.deltaZaman;

        }
        
    }
    public void DegerAl(float nesneHiz, int nesneAgirlik)
    {
        agirlik = nesneAgirlik;
        hiz = nesneHiz;
    }
}
