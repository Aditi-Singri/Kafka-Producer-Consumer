using Confluent.Kafka;
using Newtonsoft.Json;



namespace KafkaProducer
{
    class Program
    {
        //private const string BootstrapServers = "localhost:9092";
        private const string Topic = "topic2";



        static async Task Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                while (true)
                {
                    Console.WriteLine("Enter a message (or 'exit' to quit):");
                    var message = Console.ReadLine();

                    if (message == "exit")
                        break;

                    var messageValue = JsonConvert.SerializeObject(message);
                    var result = await producer.ProduceAsync(Topic, new Message<Null, string> { Value = messageValue });


                    Console.WriteLine($"Message delivered to {result.TopicPartitionOffset}");
                }
            }
        }
    }
}