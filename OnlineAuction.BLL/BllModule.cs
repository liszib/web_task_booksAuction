using Ninject.Modules;
using OnlineAuction.BLL.Interface;

namespace OnlineAuction.BLL
{
    public class BllModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserLogic>().To<UserLogic>();
            Bind<ILotLogic>().To<LotLogic>();
        }
    }
}
