using Confluent.Kafka;

namespace UltimateCheatsheetApp.Services
{
    public class DocUploadProcuder(ILogger<DocUploadProcuder> _logger)
    {
        public async Task FileUploadProducer(string topic, string fileKey)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                AllowAutoCreateTopics = true,
                Acks = Acks.All
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            try
            {
                var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string>
                {
                    Value = fileKey,
                });

                _logger.LogInformation($"Delivered {deliveryResult.Key}, Offset: {deliveryResult.Offset}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed because: {ex.Message}");
            }
        }
    }
}
