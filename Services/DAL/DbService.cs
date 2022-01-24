using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheTowerAPI.Models;

namespace TheTowerAPI.Services.DAL
{
    public class DbService
    {
        private readonly ApiDbContext _context;

        // TODO REMAKE ALL DBService SO IT TAKES ONLY MODELS 
        public DbService(ApiDbContext context)
        {
            _context = context;
        }

        public User GetUser(string nickname)
        {
            return _context.Users.Find(nickname);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User CreateUser(User user)
        {
            User temp = _context.Users.Add(user).Entity;
            _context.SaveChanges();
            return temp;
        }

        public int VerifyUser(User user)
        {
            User u = _context.Users.Find(user.Nickname);
            if (u != null && u.Password.Equals(user.Password))
            {
                return u.Role;
            }

            if (u != null)
            {
                return -1;
            }

            return 0;
        }

        public int GetUserRole(User user)
        {
            return _context.Users.Find(user.Nickname).Role;
        }

        public void ChangeUserEmail(string nickname, string newEmail)
        {
            _context.Users.Find(nickname).Email = newEmail;
            _context.SaveChanges();
        }

        public void DeleteUser(string nickname)
        {
            User u = _context.Users.Find(nickname);
            if (u.Role <= 1)
            {
                _context.Users.Remove(u);
                _context.SaveChanges();
            }
        }

        public bool Exists(User user)
        {
            return _context.Users.Any(u => u.Nickname.Equals(user.Nickname));
        }

        public void AddLevel(string name)
        {
            Level l = new Level {LevelName = name};
            _context.Levels.Add(l);
            _context.SaveChanges();
        }

        public Record AddRecord(Record record)
        {
            var current = _context.Records.Find(record.Nickname, record.LevelName);
            
            if (current != null)
            {
                current.Time = current.Time <= record.Time ? current.Time : record.Time;
            }
            else
            {
                current = _context.Records.Add(record).Entity;
            }
                
            _context.SaveChanges();
            return current;
        }

        public ICollection<Record> GetRecords()
        {
            return _context.Records.OrderBy(r => r.Time).ToList();
        }
        
        public ICollection<Level> GetLevelsWithRecords()
        {
            return _context.Levels.Include(l => l.Records).ToList();
        }
    }
}