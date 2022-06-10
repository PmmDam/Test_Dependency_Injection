using Microsoft.Extensions.Configuration;
using Test_Dependency_Injection.Models.ConfigModels;
using Microsoft.Data.SqlClient;
using Test_Dependency_Injection.Services;

namespace Test_Dependency_Injection
{
    internal class Program
    {
        // Ruta al fichero de configuración general
        private static readonly string AppConfigFilePath = Path.Combine(Environment.CurrentDirectory, "Data", "AppConfig.json");
       
        // Ruta a los ficheros de configuración de los distintos Environments 
        private static readonly string AppConfigProductionEnviroFilePath = Path.Combine(Environment.CurrentDirectory, "Data", "EnvironmentConfig", "AppConfig_Production_Environment.json");
        private static readonly string AppConfigDevelopmentEnviroFilePath = Path.Combine(Environment.CurrentDirectory, "Data","EnvironmentConfig", "AppConfig_Development_Environment.json");
        private static readonly string AppConfigStagingEnviroFilePath = Path.Combine(Environment.CurrentDirectory, "Data", "EnvironmentConfig", "AppConfig_Staging_Environment.json");

        static async Task Main(string[] args)
        {
            #region SerializarJsons
            //ConfigModel config = new ConfigModel();
            //config.GeneralSettings.ActualEnvironment = "Development";
            //config.GeneralSettings.User = "Pablo";
            //config.GeneralSettings.Password = "123456789/a";
            //config.GeneralSettings.LogEnabled = true;



            //config.DevelopmentEnvironment.EnvironmentName = "Development";
            //config.DevelopmentEnvironment.ConnectionString = CreateConnString("testdb");

            //config.StagingEnvironment.EnvironmentName = "Staging";
            //config.StagingEnvironment.ConnectionString = CreateConnString("josedb");

            //config.ProductionEnvironment.EnvironmentName = "Production";
            //config.ProductionEnvironment.ConnectionString = CreateConnString("UD_09");

            //SerializerServices.SerializeIntoJson<GeneralSettingsModel>(AppConfigFilePath, config.GeneralSettings);

            //SerializerServices.SerializeIntoJson<EnvironmentModel>(AppConfigDevelopmentEnviroFilePath, config.DevelopmentEnvironment);
            //SerializerServices.SerializeIntoJson<EnvironmentModel>(AppConfigProductionEnviroFilePath, config.ProductionEnvironment);
            //SerializerServices.SerializeIntoJson<EnvironmentModel>(AppConfigStagingEnviroFilePath, config.StagingEnvironment);

            #endregion SerializarJsons

            // En un primer pase construimos la configuración general
            ConfigurationBuilder configBuilder = BuildConfig(AppConfigFilePath, AppConfigDevelopmentEnviroFilePath, AppConfigStagingEnviroFilePath, AppConfigProductionEnviroFilePath, args);

            IConfiguration configTree = configBuilder.Build();

            ConfigModel generalConfig = configTree.Get<ConfigModel>();

            

        }

        private static string CreateConnString(string initialCatalog)
        {
            SqlConnectionStringBuilder ConnStringBuilder = new SqlConnectionStringBuilder();
            ConnStringBuilder.DataSource = "127.0.0.1";
            ConnStringBuilder.Password = "123456789/a";
            ConnStringBuilder.InitialCatalog = initialCatalog;
            ConnStringBuilder.UserID = "LoginTest";
            ConnStringBuilder.TrustServerCertificate = true;

            return ConnStringBuilder.ToString();
        }
        
        /// <summary>
        /// Carga unicamente la configuración general del programa
        /// </summary>
        /// <param name="generalConfigFilePath"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static ConfigurationBuilder BuildConfig(string generalConfigFilePath,string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(generalConfigFilePath, false, true);
            configBuilder.AddCommandLine(args);
            return configBuilder;
        }

        /// <summary>
        /// Carga toda la configguración general y de los distintos Environments
        /// </summary>
        /// <param name="generalConfigFilePath"></param>
        /// <param name="devEnviro"></param>
        /// <param name="stagEnviro"></param>
        /// <param name="prodEnviro"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static ConfigurationBuilder BuildConfig
            (
            string generalConfigFilePath, 
            string devEnviro, 
            string stagEnviro, 
            string prodEnviro, 
            string[] args
            )
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(generalConfigFilePath, false, true);
            configBuilder.AddJsonFile(devEnviro, false, true);
            configBuilder.AddJsonFile(stagEnviro, false, true);
            configBuilder.AddJsonFile(prodEnviro, false, true);
            configBuilder.AddCommandLine(args);
            return configBuilder;
        }


    }
}