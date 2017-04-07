[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Capstone.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Capstone.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Capstone.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Configuration;
    using DAL;
    
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Dependency Injection Configuration
            // The connection string is required for each of the DAL classes.
            // The connection string is located in our web.config file.
            kernel.Bind<IUserDAL>().To<UserSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["mealplanner"].ConnectionString);
            kernel.Bind<IRecipeDAL>().To<RecipeSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["mealplanner"].ConnectionString);
            kernel.Bind<IIngredientDAL>().To<IngredientSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["mealplanner"].ConnectionString);
            kernel.Bind<IRecipeIngredientDAL>().To<RecipeIngredientSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["mealplanner"].ConnectionString);
            kernel.Bind<IPreparationStepsDAL>().To<PreparationStepsSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["mealplanner"].ConnectionString);
            kernel.Bind<IMealDAL>().To<MealSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["mealplanner"].ConnectionString);

        }
    }
}
