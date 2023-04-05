using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün başarıyla eklendi";
        public static string ProductDeleted = "Ürün başarıyla silindi";
        public static string ProductUpdated = "Ürün başarıyla güncellendi";
        public static string ProductNameAlreadyExists = "Bu ürün zaten mevcut";

        public static string CategoryAdded = "Kategori başarıyla eklendi";
        public static string CategoryDeleted = "Kategori başarıyla silindi";
        public static string CategoryUpdated = "Kategori başarıyla güncellendi";

        public static string UserRegistered = "Kullanıcı başarıyla kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        //public static string UserAlreadyExists = "Kullanıcı zaten sistemde kayıtlı";
        public static string MailAlreadyExists = "Bu mail adresi sistemde kayıtlı";
        public static string SuccessfulLogin = "Kullanıcı başarıyla giriş yaptı";
        public static string PasswordError = "Şifre hatalı";
        public static string NewPassMustDifferent = "Yeni şifre eskisiyle aynı olamaz";
        public static string PassChangeSuccess = "Şifre değiştirme başarılı";
        public static string PasswordNotMatch = "Şifreler uyuşmuyor";

        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string RoleAdded = "Rol başarıyla eklendi";

        public static string ProductAddedToCart = "Ürün sepete başarıyla eklendi";
        public static string ProductDeletedFromCart = "Ürün sepetten silindi";
        public static string CartUpdated = "Sepet başarıyla güncellendi";

        public static string WalletAdded = "Cüzdan Başarıyla Eklendi";

        public static string InsufficientStock = "Yetersiz Stok";
        public static string UserCartCreated = "Kullanıcı için kart oluşturuldu";

        public static string CartEmpty = "Sepetiniz boş";

        public static string OrderCreated = "Siparişiniz başarıyla oluşturuldu";
    }
}
