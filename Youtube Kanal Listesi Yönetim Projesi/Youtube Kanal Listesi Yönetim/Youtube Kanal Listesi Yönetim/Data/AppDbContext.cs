using System.Collections.Generic;
using Dapper;
using Npgsql;

namespace Youtube_Kanal_Listesi_Yönetim.Data
{
    public class AppDbContext
    {
        private readonly string _connectionString;

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Channel> GetAllChannels()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return connection.Query<Channel>("SELECT * FROM channels").ToList();
            }
        }

        public void AddChannel(Channel channel)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(
                    "INSERT INTO channels (channel_name, subscriber_count) VALUES (@ChannelName, @SubscriberCount)",
                    channel);
            }
        }
    }
    public class Channel
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public int SubscriberCount { get; set; }
    }
}