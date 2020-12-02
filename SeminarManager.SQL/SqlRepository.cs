using System;
using Npgsql;
using SeminarManager.Model;

namespace SeminarManager.SQL
{
    public class SqlRepository : IRepository, IDisposable
    {
        private NpgsqlConnection connection;
        private SqlPersonRepository person_repository;
        private SqlSeminarRepository seminar_repository;

        public SqlRepository() 
        {
            var connection_string = string.Format("Host={0};Username={1};Password={2};Database={3}",
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_HOST", "localhost"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_USER", "postgres"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_PASSWORD", "secret"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_DATABASE", "postgres"));

            connection = new NpgsqlConnection(connection_string);
            connection.Open();

            person_repository = new SqlPersonRepository(connection);
            seminar_repository = new SqlSeminarRepository(connection);
        }

        public IPersonRepository Persons 
        {
            get { return person_repository; }
        }

        public ISeminarRepository Seminars 
        {
            get { return seminar_repository; }
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}