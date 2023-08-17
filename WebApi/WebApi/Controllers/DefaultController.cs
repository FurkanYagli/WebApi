using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.App;
using WebApi.App.FromApi;
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
                    kayitParametre.Aktif = false;
                    Kisi _kisi = new Kisi()
                    {
                        Ad = kayitParametre.Ad,
                        Soyad = kayitParametre.Soyad,
                        Tc = kayitParametre.Tc,
                        Tel = kayitParametre.Tel,
                        Mail = kayitParametre.Mail,
                        Aktif = kayitParametre.Aktif,
                        KayitTarihi = DateTime.Now
                    };
                    if (mukerrerKisi(_kisi.Tc, _kisi.Tel) == true)
                    {
                        if (_kisi.Tc.Length == 11 && _kisi.Tel.Length == 11)
                        {
                            context.Kisiler.Add(_kisi);
                            context.SaveChanges();
                            _return.data = _kisi;
                            _return.success = true;
                            _return.message = "Başarılı";
                            smsKod = smsGonder(_kisi.Id);
                            return _return;
                        }
                        else
                        {
                            _return.data = _kisi;
                            _return.success = false;
                            _return.message = "Bilgilerinizi tekarar kontrol ediniz";
                            return _return;
                        }


                    }
                    else if (mukerrerKisi(_kisi.Tc, _kisi.Tel) == false && _kisi.Aktif == false && banKontrol(tcKisiCek(_kisi.Tc, _kisi.Tel)) == true)
                    {

                        if (_kisi.Tc.Length == 11 && _kisi.Tel.Length == 11)
                        {
                            context.SaveChanges();
                            _return.data = _kisi;
                            _return.success = true;
                            _return.message = "Başarılı";
                            smsKod = smsGonder(tcKisiCek(_kisi.Tc, _kisi.Tel));
                            return _return;
                        }
                        else
                        {
                            _return.data = _kisi;
                            _return.success = false;
                            _return.message = "Bilgilerinizi tekarar kontrol ediniz";
                            return _return;
                        }

                    }
                    else
                    {
                        _return.data = _kisi;
                        _return.success = false;
                        _return.message = "Zaten Kayıtlı (Kaydedilmedi)";
                        return _return;
                    }



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
        public Return girisYap([FromBody]GirisParametre girisParametre)
        {
            Return _return = new Return();
            using (WebApiContext context = new WebApiContext())
            {
                kisiTc = girisParametre.Tc;
                kisiTel = girisParametre.Tel;
                int id = kulCek(kisiTc, kisiTel);
                try
                {
                    Kullanici kullanici = context.Kullanicilar.Find(id);
                    if (kullanici.Aktif == true && banKontrol(kullanici.KisiId) == true)
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
                        int kisiId = tcKisiCek(kisiTc, kisiTel);
                        context.Hareketler.Add(_hareket);
                        context.SaveChanges();
                        smsGonder(kisiId);

                        _return.data = _hareket;
                        _return.message = "Başarılı";
                        _return.success = true;
                        return _return;
                    }
                    else
                    {
                        _return.data = null;
                        _return.success = false;
                        _return.message = "Hesabınız silinmiş";
                        return _return;
                    }

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
            int kisiId = tcKisiCek(kisiTc, kisiTel);
            int id = kulCek(kisiTc, kisiTel);
            Return _return = new Return();
            using (WebApiContext context = new WebApiContext())
            {
                try
                {
                    Kisi kisi = context.Kisiler.Find(kisiId);
                    Kullanici kullanici = context.Kullanicilar.Find(id);
                    kullanici.Aktif = false;
                    kisi.Aktif = false;
                    context.SaveChanges();
                    smsGonder(kisiId);
                    _return.data = kullanici;
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
                    smsGonder(kisi.Id);
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
                    bildiri.Aktif = false;
                    bildiri.KullaniciId = bildiriEkle.KullaniciId;
                    context.Bildiriler.Add(bildiri);
                    context.SaveChanges();

                    if (gikomId(bildiri.Id) != null)
                    {
                        Bildiri bildiriGuncel = context.Bildiriler.Find(bildiri.Id);
                        bildiriGuncel.Aktif = true;
                        context.SaveChanges();
                        _return.data = bildiri;
                        _return.success = true;
                        _return.message = "Başarılı";
                    }
                    else
                    {
                        _return.data = bildiri;
                        _return.success = true;
                        _return.message = "Bildiri Oluştu Ancak Gikom Tarafından Kabul Edilmedi";
                        return _return;
                    }

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

        [HttpPost]
        public int gikomId(int id)
        {
            int createGikomId = 0;
            using (WebApiContext context = new WebApiContext())
            {
                Gikom gikom = new Gikom();
                gikom.KayitTarihi = DateTime.Now;
                gikom.BildiriId = id;
                context.GikomBilgiler.Add(gikom);
                createGikomId = context.SaveChanges();
            }
            return createGikomId;
        }

        [HttpPost]
        public Return smsKontrol(SmsKontrolParametre smsKontrolParametre)
        {
            Return _return = new Return();
            KayitParametre kayit = new KayitParametre();
            IdKisi = smsKontrolParametre.KisiId;

            using (WebApiContext context = new WebApiContext())
            {
                Sms mesaj = context.Smsler.Where(x => x.KisiId == IdKisi).OrderByDescending(x => x.Id).FirstOrDefault() as Sms;
                if (mesaj.GecerlilikTarih >= DateTime.Now)
                {
                    Kisi kisi = context.Kisiler.Find(IdKisi);
                    kisiTc = kisi.Tc;
                    kisiTel = kisi.Tel;
                    kayit.Ad = kisi.Ad;
                    kayit.Mail = kisi.Mail;
                    kayit.Soyad = kisi.Soyad;
                    kayit.Tc = kisi.Tc;
                    kayit.Tel = kisi.Tel;


                    if (smsKontrolParametre.SmsKod == mesaj.Kod)
                    {
                        kisi.Aktif = true;
                        kayit.Aktif = true;
                        context.SaveChanges();
                        createUser(kayit, kisi.Id);
                        _return.data = kisi;
                        _return.success = true;
                        _return.message = "Başarılı";
                        return _return;
                    }
                    else
                    {
                        kisi.Aktif = false;
                        kayit.Aktif = false;
                        context.SaveChanges();
                        _return.data = kisi;
                        _return.success = false;
                        _return.message = "Hatalı Bir İşlem Yaptınız";
                        return _return;
                    }
                }
                else
                {
                    _return.data = null;
                    _return.success = false;
                    _return.message = "Hata";
                    return _return;
                }
            }

        }

        [HttpPost]
        public Return kisiBan(BanParametre banParam)
        {
            Return _return = new Return();
            using (WebApiContext context = new WebApiContext())
            {
                Kisi kisi = context.Kisiler.Find(banParam.KisiId);
                Ban ban = new Ban();
                ban.Aktif = true;
                ban.KisiId = banParam.KisiId;
                ban.BitisTarihi = banParam.BitisTarihi;
                ban.BanSebebi = banParam.BanSebebi;
                kisi.Aktif = false;
                context.BanlananKullanicilar.Add(ban);
                context.SaveChanges();
                _return.data = ban;
                _return.success = true;
                _return.message = "Kisi Basariyla Banlandı";
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
        string smsKod;

        //metodlar

        public bool banKontrol(int id)
        {
            using (WebApiContext context = new WebApiContext())
            {
                Ban ban = context.BanlananKullanicilar.Where(x => x.KisiId == id).OrderByDescending(x => x.Id).FirstOrDefault() as Ban;
                Kisi kisi = context.Kisiler.Find(id);
                Kullanici kul = context.Kullanicilar.Where(x => x.KisiId == id).FirstOrDefault() as Kullanici;
                if (ban != null && ban.BitisTarihi > DateTime.Now)
                {
                    ban.Aktif = true;
                    kisi.Aktif = false;
                    kul.Aktif = false;
                    context.SaveChanges();
                    return false;
                }
                else if (ban == null)
                {
                    return true;
                }
                else if (ban.BitisTarihi <= DateTime.Now)
                {
                    ban.Aktif = false;
                    kisi.Aktif = true;
                    kul.Aktif = true;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int createUser(KayitParametre kayitParametre, int id)
        {
            int createdUserId = 0;
            if (kayitParametre != null)
            {

                using (WebApiContext context = new WebApiContext())
                {
                    if (true)
                    {
                        Kullanici kullanici = new Kullanici()
                        {

                            Aktif = true,
                            KayitTarihi = DateTime.Now,
                            KisiId = id
                        };
                        if (mukerrerKullanici(id) == true)
                        {
                            context.Kullanicilar.Add(kullanici);
                            createdUserId = context.SaveChanges();
                            kayitParametre.Aktif = true;
                        }
                    }

                }


            }
            return createdUserId;
        }
        public string smsGonder(int id)
        {
            Random rnd = new Random();
            string kod = rnd.Next(100000, 999999).ToString();
            using (WebApiContext context = new WebApiContext())
            {
                try
                {
                    Kisi kisi = context.Kisiler.Find(id);
                    Ad = kisi.Ad;
                    Soyad = kisi.Soyad;
                    kisiTel = kisi.Tel;
                    Sms sms = new Sms();
                    sms.KayitTarihi = DateTime.Now;
                    sms.KisiId = id;
                    sms.GecerlilikTarih = DateTime.Now.AddMinutes(5);
                    sms.Text = $"Merhaba {Ad} {Soyad} ALO 153 GBB MOBIL UYGULAMASI ICIN DOGRULAMA KODUNUZ:{kod}. IYI GUNLER DILERIZ. KODUN SON GECERLİLİK TARIHI {sms.GecerlilikTarih}";
                    sms.Kod = kod;
                    sms.GidenTel = kisiTel;
                    context.Smsler.Add(sms);
                    context.SaveChanges();
                    return kod;
                }
                catch (Exception)
                {

                    return "Hatalı Bilgi.";
                }
            }
        }

        public int kulCek(string Tc, string Tel)
        {

            using (WebApiContext context = new WebApiContext())
            {
                Kisi kisi = context.Kisiler.Where(x => x.Tel == Tel && x.Tc == Tc).FirstOrDefault() as Kisi;
                IdKisi = kisi.Id;
                Kullanici kullanic = context.Kullanicilar.Where(x => x.KisiId == IdKisi).SingleOrDefault() as Kullanici;
                IdKullanici = kullanic.Id;
            }
            return IdKullanici;
        }
        public int kisiCek(int kisiId)
        {

            using (WebApiContext context = new WebApiContext())
            {

                Kullanici kullanic = context.Kullanicilar.Where(x => x.KisiId == kisiId).SingleOrDefault() as Kullanici;
                IdKisi = kullanic.KisiId;
                Kisi kisi = context.Kisiler.Where(x => x.Id == IdKisi).FirstOrDefault() as Kisi;
                IdKisi = kisi.Id;
            };

            return IdKisi;
        }
        public int tcKisiCek(string tc, string tel)
        {
            using (WebApiContext context = new WebApiContext())
            {
                Kisi kisi = context.Kisiler.Where(x => x.Tel == tel && x.Tc == tc).FirstOrDefault() as Kisi;
                if (kisi.Id != 0 || kisi.Id != null)
                {
                    return kisi.Id;
                }
                else
                {
                    return -1;
                }
            };

        }

        public bool mukerrerKisi(string Tc, string Tel)
        {

            using (WebApiContext context = new WebApiContext())
            {
                Kisi kisi = context.Kisiler.Where(x => x.Tel == Tel || x.Tc == Tc).FirstOrDefault() as Kisi;
                if (kisi != null)
                {
                    if (banKontrol(kisi.Id) == true)
                    {
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            };
        }
        public bool mukerrerKullanici(int id)
        {
            using (WebApiContext context = new WebApiContext())
            {
                Kullanici kullanici = context.Kullanicilar.Where(x => x.KisiId == id).FirstOrDefault() as Kullanici;

                if (kullanici != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            };
        }

    }
}