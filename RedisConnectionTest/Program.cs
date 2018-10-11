using System;
using System.Configuration;

namespace RedisConnectionTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var program = new Program();

            //var start = DateTime.Now;
            //Console.WriteLine("Saving random data in cache");
            //program.SaveBigData();
            //var end = DateTime.Now;
            //Console.WriteLine("Time span: {0}", (end - start).TotalMilliseconds);

            //Console.WriteLine("Display all keys");
            //program.ViewKeys();

            //Console.WriteLine("Reading data from cache");
            //program.ReadData();

            //Console.WriteLine("Delete all keys");
            //program.DeleteKeys();

            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }

        public void ReadData()
        {
            var server = RedisConnectorHelper.Connection.GetServer(ConfigurationManager.AppSettings["redisserver"]);
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            foreach (var key in server.Keys(pattern: "*"))
            {
                Console.WriteLine(cache.StringGet(key));
            }
        }

        public void SaveBigData()
        {
            var devicesCount = 10;
            var rnd = new Random();
            var cache = RedisConnectorHelper.Connection.GetDatabase();

            for (int i = 0; i < devicesCount; i++)
            {
                var value = rnd.Next(0, 10000);
                cache.StringSet($"Device_Status:{i}", value);
            }
        }

        public void ViewKeys()
        {
            var server = RedisConnectorHelper.Connection.GetServer(ConfigurationManager.AppSettings["redisserver"]);
            foreach (var key in server.Keys(pattern: "*"))
            {
                Console.WriteLine(key);
            }
        }

        public void DeleteKeys()
        {
            var server = RedisConnectorHelper.Connection.GetServer(ConfigurationManager.AppSettings["redisserver"]);
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            foreach (var key in server.Keys(pattern: "*"))
            {
                cache.KeyDelete(key);
            }
        }
    }
}
