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
            int id = 20;
            int kisiId = 5;
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
                    if (kullaniciGuncelleParametre.Tel != null && kullaniciGuncelleParametre.Mail.Length == 11)
                    {
                        kisi.Tel = kullaniciGuncelleParametre.Tel;
                    }

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

            }
            return createdUserId;
        }
        string kisiTel;
        string kisiTc;
        int IdKisi;
        int IdKullanici;
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
        public int KisiCek()
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

    }
}