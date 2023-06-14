using System;
using Confluent.Kafka;
using Newtonsoft.Json;
// BootstrapServers = "172.18.112.1:9092",
namespace KafkaConsumer
{
    class Program
    {
        private const string BootstrapServers = "localhost:9092";
        private const string Topic = "topic2";
        private const string GroupId = "test-group";

        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = BootstrapServers,
                GroupId = GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(Topic);

                Console.WriteLine($"Consuming messages from topic: {Topic}");

                while (true)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult != null)
                    {
                        var message = JsonConvert.DeserializeObject<string>(consumeResult.Message.Value);
                        Console.WriteLine($"Received message: {message}");
                    }
                   
                }
            }
        }
    }
}
