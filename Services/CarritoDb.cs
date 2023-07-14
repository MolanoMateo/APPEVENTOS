using System;
using SQLite;
//using APPEVENTOS.Utils;
using APPEVENTOS.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace APPEVENTOS.Services
{
	public class CarritoDb
    {
        string _dbPath;
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection _connection;
        private async Task Init()
        {
            if(_connection is not null)
            {
                return;
            }
            else
            {
                _connection = new (_dbPath);
                await _connection.CreateTableAsync<Carrito>();
            }
        }
        public CarritoDb(string dbPath)
		{
            _dbPath=dbPath;
		}
        public async Task saveCarrito(Carrito carrito)
        {
            await Init();
            int result = await _connection.InsertAsync(carrito);
        }
        public async Task<List<Carrito>> obtenerCarrito()
        {
            await Init ();
            return await _connection.Table<Carrito>().ToListAsync();
        }
        public async Task eliminarCarrito(Carrito c)
        {
            await Init();
            await _connection.DeleteAsync(c);
        }

    }
}