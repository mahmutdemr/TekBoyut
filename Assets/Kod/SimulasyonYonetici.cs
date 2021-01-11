using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulasyonYonetici : MonoBehaviour
{
    [SerializeField] int EvrenUzunluk = 100;
    [SerializeField] float fpsAralik = 0.1f; // iki kare arasında gecilecek zaman

    List<Nesne> nesneler = new List<Nesne>(); //Uretilecen olan nesnelerin tutulacağı liste
    List<Duvar> duvarlar = new List<Duvar>(); //DUVARLAR HAREKETSİZ NESNELERDİR, KONUMLARI DEĞİŞMEZ

    List<duvarOzellik> uretilecekDuvarlar; // Inspector panelinden Simulasyon başlamadan girilecek olan duvar
    List<nesneOzellik> uretilecekNesneler; // ve nesne özelliklerinin listesi

    void Start()
    {
        duvarOzellik ilkDuvar = new duvarOzellik(0);
        duvarOzellik sonDuvar = new duvarOzellik(EvrenUzunluk);

        Veriler veri; //Inspector panelinden girilecek olan verilerin çekilmesi için tanımlandı
        veri = GetComponent<Veriler>();

        uretilecekDuvarlar = veri.uretilecekDuvarlar;
        uretilecekNesneler = veri.uretilecekNesneler;

        uretilecekDuvarlar.Add(ilkDuvar); //0. konumda ve
        uretilecekDuvarlar.Add(sonDuvar); //Sonuncu konumda iki duvar uretilir (kaldırmak isterseniz silebilirsiniz)
        //Daha fazla duvar inspector panelinden simulasyon baslamadan eklenebilir

        birimKonumlariCiz();//Simulasyonun gercekleşeceği tek boyutlu zemini cizer
        otoNesneUret(); //SimYonetici objesine inspector panelinden girilmis olan nesneleri uretir
        otoDuvarUret(); //0. ve Sonuncu duvarlara artı olarak SimYonetici objesine inspector panelinden girilmis olan duvaları uretir

        StartCoroutine(SimGuncelle());//Simülasyon Dongusunu Baslatır
    }
    private void Update()
    {
         KameraKonumuMerkezle(); //Evrenin genisliği ve ekran yüksekliğine göre kamerayı merkezler
    }
    IEnumerator SimGuncelle()
    {
        while (true)
        {
            yield return new WaitForSeconds(fpsAralik); //Bekleme süresi

            Zaman.Guncelle();//İki kare arasındaki gecen net zamanı hesaplatır (nesnelerin hareketrini vs. zamana bağlı hesaplayabilmek için kullanılır) 
            CarpismaAlgilayici.CarpismaHesapla(); //Simülasyondaki tüm nesnelerin birbirine göre durumları ölçülüp çarpışmalar kontrol edilir
            nesneler.ForEach(Nesne => Nesne.Guncelle()); //Herbir nesne guncellenir
        }
    }

    void birimKonumlariCiz()
    {
        // oluşturulan tek boyutlu zemine ait bloklara Parent olarak atanacak bir bos obje olusturuluyor
        Transform parent = Instantiate(Resources.Load("bosObje", typeof(GameObject)) as GameObject, Vector3.zero , Quaternion.identity).transform; 
        GameObject zeminObje = Resources.Load("plane", typeof(GameObject)) as GameObject; //Birim zemin objesi

        Vector3 konum = Vector3.zero; 
        Quaternion yon = Quaternion.identity;
        
        for (int i = 0; i < EvrenUzunluk; i++)
        {
            Instantiate(zeminObje, konum, yon, parent).name = "birim" + (i + 1); //Evren uzunluğu kadar zemin uretiliyor
            konum.x += 1;
        }

        parent.name = "Toplam" + EvrenUzunluk + "BirimUzunluk";
    }


    void otoDuvarUret()
    {
        int i = 0; //Sırasına göre duvara isim vermek için
        uretilecekDuvarlar.ForEach(duvarOzellik => 
        {
            DuvarUret("nesneDuvar" + (i ++), duvarOzellik.konum);
        });
    }
    void otoNesneUret()
    {
        int i = 0; //Sırasına göre nesneye isim vermek için
        uretilecekNesneler.ForEach(nesneOzellik =>
        {
            NesneUret("nesne" + (i++), nesneOzellik.hiz , nesneOzellik.konum , nesneOzellik.agirlik);
        });
    }


    void DuvarUret(string isim, float konum)
    {
        duvarlar.Add(new Duvar(isim, konum));
    }
    void NesneUret(string isim, float hiz, float konum, int agirlik)
    {
        nesneler.Add(new Nesne(isim, hiz, konum, agirlik));
    }


    void KameraKonumuMerkezle()
    {
        Camera.main.transform.position = new Vector3(EvrenUzunluk / 2f, 10, 0);
        Camera.main.GetComponent<Camera>().orthographicSize = Screen.height / 20 * EvrenUzunluk / 100f;
    }
}

