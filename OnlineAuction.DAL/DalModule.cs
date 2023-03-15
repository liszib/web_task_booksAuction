using Ninject.Modules;
using OnlineAuction.DAL.Interface;

namespace OnlineAuction.DAL
{
    public class DalModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILotDao>().To<LotDao>();
            Bind<IUserDao>().To<UserDao>();
        }
    }
}
