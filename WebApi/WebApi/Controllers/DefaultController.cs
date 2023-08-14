using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.App;
using WebApi.Context;
using WebApi.Dto;
using WebApi.Models;
using WebApi.ToApi;

namespace WebApi.Controllers
{
    public class DefaultController : ApiController
    {

        [HttpPost]
        public Return kayitOlustur([FromBody]KayitParametre kayitParametre)
        {
            Return _return = new Return();
            using (WebApiContext context = new WebApiContext())
            {
                try
                {

                    Kisi _kisi = new Kisi()
                    {
                        Ad = kayitParametre.Ad,
                        Soyad = kayitParametre.Soyad,
                        Tel = kayitParametre.Tel,
                        Tc = kayitParametre.Tc,
                        Mail = kayitParametre.Mail,
                        KayitTarihi = DateTime.Now,
                    };
                    context.Kisiler.Add(_kisi);
                    context.SaveChanges();

                    createUser(kayitParametre, _kisi.Id);
                    _return.data = _kisi;
                    _return.message = "Başarılı";
                    _return.success = true;
                    return _return;
                }
                catch (Exception ex)
                {
                    _return.data = null;
                    _return.success = false;
                    _return.message = ex.Message;
                    return _return;

                }

            }


        }
        int kulId;
        [HttpPost]
        public Return girisYap([FromBody]GirisParametre girisParametre)
        {
            Return _return = new Return();
            using (WebApiContext context = new WebApiContext())
            {
                kisiTc = girisParametre.Tc;
                kisiTel = girisParametre.Tel;
                int id = kulCek();
                try
                {
                    //metod ile verileri çek
                    Hareket _hareket = new Hareket()
                    {
                        //enum
                        //sms
                        KullaniciId = id,
                        SayfaId = null,
                        IslemId = null,
                        GuncellemeTarihi = DateTime.Now

                    };
                    context.Hareketler.Add(_hareket);
                    context.SaveChanges();
                    smsGonder(kisiTc, kisiTel);

                    _return.data = _hareket;
                    _return.message = "Başarılı";
                    _return.success = true;
                    return _return;
                }
                catch (Exception ex)
                {
                    _return.data = null;
                    _return.success = false;
                    _return.message = ex.Message;
                    return _return;

                }
            }
        }
        [HttpPost]
        public Return kullaniciSil([FromBody]GirisParametre kullaniciSilParametre)
        {
            kisiTc = kullaniciSilParametre.Tc;
            kisiTel = kullaniciSilParametre.Tel;
            int id = kulCek();
            Return _return = new Return();
            using (WebApiContext context = new WebApiContext())
            {
                try
                {
                    Kullanici kullanici = context.Kullanicilar.Find(id);
                    kullanici.Aktif = false;
                    context.SaveChanges();
                    smsGonder(kisiTc, kisiTel);
                    _return.data = kullanici + " " + smsGonder(kisiTc, kisiTel);
                    _return.success = true;
                    _return.message = "Kişi Silindi";
                    return _return;

                }
                catch
                {
                    _return.data = null;
                    _return.success = false;
                    _return.message = "Kişi Silinemedi";
                    return _return;

                }
            }
        }
        [HttpPost]
        public Return kullaniciGuncelle([FromBody]KayitParametre kullaniciGuncelleParametre)
        {
            kisiTc = kullaniciGuncelleParametre.Tc;
            kisiTel = kullaniciGuncelleParametre.Tel;
            int id = 5;
            int kisiId = 2;
            // int kisiId = KisiCek();
            Return _return = new Return();
            using (WebApiContext context = new WebApiContext())
            {
                try
                {
                    Kisi kisi = context.Kisiler.Find(kisiId);
                    if (kullaniciGuncelleParametre.Mail != null)
                    {
                        kisi.Mail = kullaniciGuncelleParametre.Mail;
                    }
                    if (kullaniciGuncelleParametre.Tel != null && kullaniciGuncelleParametre.Tel.Length == 11)
                    {
                        kisi.Tel = kullaniciGuncelleParametre.Tel;
                    }
                    kisi.GuncellemeTarihi = DateTime.Now;
                    smsGonder(kisiTc, kisiTel);
                    context.SaveChanges();

                    _return.data = kisi;
                    _return.success = true;
                    _return.message = "Kişi Güncellendi";
                    return _return;

                }
                catch
                {
                    _return.data = null;
                    _return.success = false;
                    _return.message = "Başarısız";
                    return _return;

                }
            }
        }

        [HttpPost]
        public string smsGonder(string tc, string tel)
        {
            Random rnd = new Random();
            string kod = rnd.Next(100000, 999999).ToString();
            using (WebApiContext context = new WebApiContext())
            {
                try
                {
                    kisiTc = tc;
                    kisiTel = tel;
                    kulCek();
                    kisiCek();
                    Kisi kisi = context.Kisiler.Find(IdKisi);
                    Ad = kisi.Ad;
                    Soyad = kisi.Soyad;
                    kisiTel = kisi.Tel;
                    Sms sms = new Sms();
                    sms.KayitTarihi = DateTime.Now;
                    sms.KullaniciId = IdKullanici;
                    sms.Text = $"Merhaba {Ad} {Soyad} ALO 153 GBB MOBIL UYGULAMASI ICIN DOGRULAMA KODUNUZ:{kod}. IYI GUNLER DILERIZ.";
                    sms.Kod = kod;
                    sms.GidenTel = kisiTel;
                    context.Smsler.Add(sms);
                    context.SaveChanges();
                    return sms.Text;
                }
                catch (Exception)
                {

                    return "Hatalı Bilgi.";
                }
            }
        }

        [HttpPost]
        public Return bildiriOlustur([FromBody] BildiriParametre bildiriEkle)
        {

            Return _return = new Return();
            try
            {
                using (WebApiContext context = new WebApiContext())
                {
                    Bildiri bildiri = new Bildiri();
                    bildiri.Aciklama = bildiriEkle.Aciklama;
                    //bildiri.AltKategoriId = bildiriEkle.AltKategoriId;
                    //bildiri.FotografId = bildiriEkle.FotografId;
                    bildiri.KayitTarihi = DateTime.Now;
                    //bildiri.KonumId = bildiriEkle.KonumId;
                    bildiri.KullaniciId = bildiriEkle.KullaniciId;
                    context.Bildiriler.Add(bildiri);
                    context.SaveChanges();
                    _return.data = bildiri;
                    _return.success = true;
                    _return.message = "Başarılı";
                }
                return _return;
            }
            catch (Exception ex)
            {

                _return.data = null;
                _return.success = false;
                _return.message = ex.Message;
                return _return;
            }

        }

        [HttpPost]
        public Return bildiriGuncelle([FromBody] BildiriParametre bildiriDuzenle)
        {
            Return _return = new Return();
            try
            {
                using (WebApiContext context = new WebApiContext())
                {
                    int BildiriId = 8;
                    Bildiri bildiri = context.Bildiriler.Find(BildiriId);

                    bildiri.KullaniciId = bildiriDuzenle.KullaniciId;
                    bildiri.GuncellemeTarihi = DateTime.Now;

                    if (bildiriDuzenle.Aciklama.Length > 1)
                    {
                        bildiri.Aciklama = bildiriDuzenle.Aciklama;
                    }
                    if (bildiriDuzenle.AltKategoriId >= 1)
                    {
                        bildiri.AltKategoriId = bildiriDuzenle.AltKategoriId;
                    }
                    if (bildiriDuzenle.FotografId >= 1)
                    {
                        bildiri.FotografId = bildiriDuzenle.FotografId;
                    }
                    if (bildiriDuzenle.KonumId >= 1)
                    {
                        bildiri.KonumId = bildiriDuzenle.KonumId;
                    }
                    context.SaveChanges();
                    _return.data = bildiri;
                    _return.success = true;
                    _return.message = "Başarılı";
                }
                return _return;
            }
            catch (Exception ex)
            {

                _return.data = null;
                _return.success = false;
                _return.message = ex.Message;
                return _return;
            }

        }



        //Sabitler
        string Ad;
        string Soyad;
        string kisiTel;
        string kisiTc;
        int IdKisi;
        int IdKullanici;

        //metodlar
        public int createUser(KayitParametre kayitParametre, int id)
        {
            int createdUserId = 0;
            if (kayitParametre != null)
            {
                using (WebApiContext context = new WebApiContext())
                {
                    Kullanici kullanici = new Kullanici()
                    {
                        Aktif = true,
                        KayitTarihi = DateTime.Now,
                        KisiId = id
                    };
                    context.Kullanicilar.Add(kullanici);
                    createdUserId = context.SaveChanges();
                }
                smsGonder(kayitParametre.Tc, kayitParametre.Tel);

            }
            return createdUserId;
        }

        public int kulCek()
        {

            using (WebApiContext context = new WebApiContext())
            {
                Kisi kisi = context.Kisiler.Where(x => x.Tel == kisiTel && x.Tc == kisiTc).FirstOrDefault() as Kisi;
                IdKisi = kisi.Id;
                Kullanici kullanic = context.Kullanicilar.Where(x => x.KisiId == IdKisi).SingleOrDefault() as Kullanici;
                IdKullanici = kullanic.Id;
            }
            return IdKullanici;
        }
        public int kisiCek()
        {

            using (WebApiContext context = new WebApiContext())
            {

                Kullanici kullanic = context.Kullanicilar.Where(x => x.KisiId == IdKisi).SingleOrDefault() as Kullanici;
                IdKisi = kullanic.KisiId;
                Kisi kisi = context.Kisiler.Where(x => x.Id == IdKisi).FirstOrDefault() as Kisi;
                IdKisi = kisi.Id;
            };

            return IdKisi;
        }
        public int tcKisiCek()
        {
            using (WebApiContext context = new WebApiContext())
            {
                Kisi kisi = context.Kisiler.Where(x => x.Tel == kisiTel && x.Tc == kisiTc).FirstOrDefault() as Kisi;
                IdKisi = kisi.Id;
            };
            return IdKisi;
        }

    }
}