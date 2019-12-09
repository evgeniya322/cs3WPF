using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public static class Test
    {
        public static List<Server> Servers { get; } = new List<Server>
        {
            new Server { Id = 1, Name = "@yandex.ru", Address = "smtp.yandex.ru", UserName = "UserName", Password = "Pass"},
            new Server { Id = 2, Name = "@mail.ru", Address = "smtp.mail.ru", UserName = "UserName", Password = "Pass"},
            new Server { Id = 3, Name = "@gmail.com", Address = "smtp.gmail.com", Port = 465, UserName = "UserName", Password = "Pass"},
        };
    }
}
