using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<ClaimManager>().As<IClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<CartManager>().As<ICartService>();
            builder.RegisterType<EfCartDal>().As<ICartDal>();

            builder.RegisterType<WalletManager>().As<IWalletService>();
            builder.RegisterType<EfWalletDal>().As<IWalletDal>();

            builder.RegisterType<UserCartManager>().As<IUserCartService>();
            builder.RegisterType<EfUserCartDal>().As<IUserCartDal>();

            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            

        }
    }
}
