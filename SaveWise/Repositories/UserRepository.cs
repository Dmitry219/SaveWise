using Dapper;
using Npgsql;
using SaveWise.Model;
using System.Data;

namespace SaveWise.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IConfiguration config)
        {
            _connection = new NpgsqlConnection(config.GetConnectionString("Postgres"));
        }

        public async Task AddUsers(User user)
        {
            var sql = @"insert into Users (name, age) 
                        values (@name, @age)
                        returning id, dteCreate";

            var result = await _connection.QuerySingleAsync<User>(sql, user);
            user.id = result.id;
            user.dteCreate = result.dteCreate;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _connection.QueryAsync<User>("select * from Users");
        }

        public async Task<User> GetByIdUser(int idUser)
        {
            return await _connection.QueryFirstOrDefaultAsync<User>("select * from Users where id = @idUser",
                new { id = idUser }
            );
        }

        public async Task DeleteByIdUser(int idUser)
        {
            await _connection.ExecuteAsync("Delete Users where id = @idUser",
                new { id = idUser }
            );
        }
    }
}