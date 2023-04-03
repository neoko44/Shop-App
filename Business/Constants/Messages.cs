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
        public static string UserAlreadyExists = "Kullanıcı zaten sistemde kayıtlı";
        public static string SuccessfulLogin = "Kullanıcı başarıyla giriş yaptı";
        public static string PasswordError = "Şifre hatalı";

        
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        public static string AuthorizationDenied = "Yetkiniz yok";

    }
}
