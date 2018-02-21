using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using ShoeProject.Domain.Contracts.Commands;
using ShoeProject.Domain.Contracts.Queries;
using ShoeProject.Domain.DTO;
using ShoeProject.Domain.Handlers.CommandHandlers;
using ShoeProject.Domain.Handlers.QueryHandlers;
using ShoeProject.Domain.Repository;
using ShoeTracker;
using ShoeTracker.Core;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace ShoeTracker
{


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
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            kernel.Bind<IQueryHandler<GetNamesQuery, List<Names>>>()
                .To<ShoeTrackerQueryHandlers>()
                .InSingletonScope();
            kernel.Bind<IQueryHandler<GetAllShoeTypesQuery, List<ShoeTypes>>>()
                .To<ShoeTrackerQueryHandlers>()
                .InSingletonScope();
            kernel.Bind<IQueryHandler<GetAllShoesQuery, List<ShoeTrackerDto>>>()
                .To<ShoeTrackerQueryHandlers>()
                .InSingletonScope();
            kernel.Bind<IQueryHandler<GetShoeByChildNameQuery, List<ShoeTrackerDto>>>()
                .To<ShoeTrackerQueryHandlers>()
                .InSingletonScope();
            kernel.Bind<IQueryHandler<GetShoeByShoeIdQuery, ShoeTrackerDto>>()
                .To<ShoeTrackerQueryHandlers>()
                .InSingletonScope();
            kernel.Bind<ICommandHandler<DeleteShoeCommand>>()
                .To<ShoeTrackerCommandHandler>()
                .InSingletonScope();
            kernel.Bind<ICommandHandler<AddShoeCommand>>()
                .To<ShoeTrackerCommandHandler>()
                .InSingletonScope();
            kernel.Bind<IShoeTrackerRepository>()
                .To<ShoeTrackerRepository>().InSingletonScope();
            kernel.Bind<ICommandHandler<AddReceiptPathCommand>>()
                .To<ShoeTrackerCommandHandler>()
                .InSingletonScope();
            kernel.Bind<IQueryHandler<GetReceiptPathQuery, List<string>>>()
                .To<ShoeTrackerQueryHandlers>()
                .InSingletonScope();
            kernel.Bind<ICommandHandler<EditShoeCommand>>()
                .To<ShoeTrackerCommandHandler>()
                .InSingletonScope();
            kernel.Bind<IQueryHandler<GetMonthlyBudgetQuery, MonthlyBudget>>()
                  .To<ShoeTrackerQueryHandlers>()
                  .InSingletonScope();
            kernel.Bind<ICommandHandler<AddMonthlyBudgetCommand>>()
                .To<ShoeTrackerCommandHandler>()
                .InSingletonScope();
            kernel.Bind<ShoeTrackerDatabaseRepo>()
                .ToSelf().InSingletonScope()
                .WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IQueryDispatcher>()
                .To<QueryDispatcher>()
                .InSingletonScope();
            kernel.Bind<ICommandDispatcher>()
                .To<CommandDispatcher>()
                .InSingletonScope();
        }
    }
}

