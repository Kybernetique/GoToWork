using System.Windows;
using Unity;
using Unity.Lifetime;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.StoragesContracts;
using GoToWorkDatabaseImplement.Implements;
using GoToWorkEngineer;
using GoToWorkBusinessLogic.OfficePackage;
using GoToWorkBusinessLogic.HelperModels;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<IEngineerStorage, EngineerStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<EngineerLogic>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<IPartStorage, PartStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<PartLogic>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<IProductStorage, ProductStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ProductLogic>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<ICertificateStorage, CertificateStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<CertificateLogic>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<IWorkerStorage, WorkerStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<WorkerLogic>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<IShiftStorage, ShiftStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ShiftLogic>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<IMachineStorage, MachineStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<MachineLogic>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new
            HierarchicalLifetimeManager());

            return currentContainer;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = BuildUnityContainer();
            var welcomeWindow = container.Resolve<WindowAuthorization>();
            welcomeWindow.Show();
        }
    }
}
