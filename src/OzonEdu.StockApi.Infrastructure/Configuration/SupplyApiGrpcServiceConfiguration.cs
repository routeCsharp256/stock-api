namespace OzonEdu.StockApi.Infrastructure.Configuration
{
    /// <summary>
    /// Конфигурации подключения к сервису supply
    /// </summary>
    public class SupplyApiGrpcServiceConfiguration
    {
        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string ServerAddress { get; set; }
    }
}