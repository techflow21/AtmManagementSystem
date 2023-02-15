using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Domain
{
    public class AtmDbContext : IDisposable
    {
        private readonly string _connectionString;

        private bool _disposed;

        private SqlConnection _dbConnection = null;

        public AtmDbContext() : this("Data Source=.;Initial Catalog=AtmSystemDB; Encrypt = False; Integrated Security=True")
        {

        }

        public AtmDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SqlConnection> OpenConnection()
        {
            _dbConnection = new SqlConnection(_connectionString);
            await _dbConnection.OpenAsync();
            return _dbConnection;
        }

        public async Task CloseConnection()
        {
            if (_dbConnection?.State != ConnectionState.Closed)
            {
                await _dbConnection?.CloseAsync();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbConnection.Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

