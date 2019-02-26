using System.Web.Mvc;
using TheBackEndLayer.Repositories;
using TheBackEndLayer.Services;
using Unity;
using Unity.Mvc5;

namespace BAISTGOLF.COM
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IScoreService, ScoreService>();
            container.RegisterType<IHoleRepository, HoleRepository>();
            container.RegisterType<IReservationService, ReservationService>();
            container.RegisterType<ITeeTimesService, TeeTimesService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IMemberService, MemberService>();
            container.RegisterType<IAppService, AppService>();
            container.RegisterType<IGolfCourseRepository, GolfCourseRepository>();
            container.RegisterType<IHandiCapRepository, HandiCapRepository>();
            //container.RegisterType<IEmailService, EmailService>();





            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}